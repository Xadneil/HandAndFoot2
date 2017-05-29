using HandAndFoot.Messages.ToClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HandAndFoot.Client
{
    public partial class HandAndFoot : Form
    {
        PlayerState currentState;
        ServerConnectControl serverConnectControl;
        NameControl nameControl;
        NetworkStream stream;
        string playerName;

        public HandAndFoot()
        {
            InitializeComponent();
            currentState = PlayerState.CONNECTING;
            serverConnectControl = new ServerConnectControl();
            serverConnectControl.Completed += ServerConnect_Completed;
            Controls.Add(serverConnectControl);
            Center(serverConnectControl);
        }

        private void ServerConnect_Completed(Socket socket)
        {
            Controls.Remove(serverConnectControl);
            serverConnectControl.Dispose();
            serverConnectControl = null;

            stream = new NetworkStream(socket, true);

            currentState = PlayerState.ENTER_NAME;
            nameControl = new NameControl(stream);
            Controls.Add(nameControl);
            Center(nameControl);
        }

        private void OnReceiveGameDetails(LobbyGameDetails details)
        {
            if (currentState == PlayerState.ENTER_NAME && nameControl != null)
            {
                var name = nameControl.NameAccepted();
                Controls.Remove(nameControl);
                nameControl.Dispose();
                nameControl = null;
                playerName = name;

                currentState = PlayerState.CHOOSE_TEAM;
                // create and display team choosing control
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (currentState == PlayerState.CONNECTING && serverConnectControl != null)
            {
                Center(serverConnectControl);
            }
            else if (currentState == PlayerState.ENTER_NAME && nameControl != null)
            {
                Center(nameControl);
            }
        }

        private void Center(Control c)
        {
            c.Location = new Point((Width - c.Width) / 2, (Height - c.Height) / 2);
        }

        private void ReceiveMessages()
        {
            var worker = new BackgroundWorker();
            worker.DoWork += (sender, e) =>
            {
                if (Thread.CurrentThread.Name == null)
                    Thread.CurrentThread.Name = "ClientSocketReceiver";
                var formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                e.Result = formatter.Deserialize(stream);
            };
            worker.RunWorkerCompleted += (sender, e) =>
            {
                if (e.Error != null)
                {
                    MessageBox.Show(this, e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(0);
                }
                HandleMessage(e.Result);
                worker.RunWorkerAsync();
            };

            worker.RunWorkerAsync();
        }

        private void HandleMessage(object message)
        {
            if (message is NotAllowed)
            {
                var notAllowed = message as NotAllowed;
                switch (currentState)
                {
                    case PlayerState.ENTER_NAME:
                        nameControl?.Error(new Exception(notAllowed.Reason));
                        break;
                }
            }
            else if (message is LobbyGameDetails)
            {
                var gameDetails = message as LobbyGameDetails;
                OnReceiveGameDetails(gameDetails);
            }
            else if (message is LobbyAnnouncePlayer)
            {
                var announcePlayer = message as LobbyAnnouncePlayer;
            }
            else if (message is LobbyPlayerChoseTeam)
            {
                var playerChoseTeam = message as LobbyPlayerChoseTeam;
            }
            else if (message is CurrentTurn)
            {
                var currentTurn = message as CurrentTurn;
            }
            else if (message is DealCards)
            {
                var dealCards = message as DealCards;
            }
            else if (message is DrawSixCards)
            {
                var drawSixCards = message as DrawSixCards;
            }
            else if (message is DrawTwoCards)
            {
                var drawTwoCards = message as DrawTwoCards;
            }
            else if (message is NewDiscard)
            {
                var newDiscard = message as NewDiscard;
            }
            else if (message is UpdateTeam)
            {
                var updateTeam = message as UpdateTeam;
            }
        }
    }
}

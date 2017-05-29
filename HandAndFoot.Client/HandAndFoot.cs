using HandAndFoot.Messages.ToClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
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
            nameControl.Completed += Name_Completed;
            Controls.Add(nameControl);
            Center(nameControl);
        }

        private void Name_Completed(string name)
        {
            Controls.Remove(nameControl);
            nameControl.Dispose();
            nameControl = null;
            playerName = name;
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

        public void ReceiveMessage(object message)
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
            //else if (message is LobbyGameDetails)
            //{
            //    var gameDetails = message as LobbyGameDetails;

            //}
        }
    }
}

using HandAndFoot.Messages.ToClient;
using HandAndFoot.Messages.ToServer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Net.Sockets;
using System.Windows.Forms;

namespace HandAndFoot.Client
{
    public partial class ChooseTeamControl : UserControl
    {
        List<Tuple<string, int>> players;
        NetworkStream stream;

        public ChooseTeamControl(LobbyGameDetails details, NetworkStream stream)
        {
            InitializeComponent();

            this.stream = stream;
            players = new List<Tuple<string, int>>(details.PlayersPerTeam * details.Teams);

            for (int team = 0; team < details.Teams; team++)
            {
                foreach (var player in details.PlayerTeamAllocation[team])
                {
                    players.Add(new Tuple<string, int>(player, team));
                }
            }


            foreach (var team in Enumerable.Range(0, details.Teams).Select(i => "Team " + (i + 1)))
            {
                lstChooseTeam.Items.Add(team);
            }

            DisplayPlayers();
        }

        private void DisplayPlayers()
        {
            grdOtherPlayers.DataSource = players.OrderBy(t => t.Item2).Select(p => new { Name1 = p.Item1, Team = p.Item2 == -1 ? "Undecided" : ("Team " + (p.Item2 + 1)) });
        }

        public void AddPlayer(string player, int team)
        {
            int index = players.FindIndex(t => t.Item1 == player);
            if (index >= 0)
            {
                players[index] = new Tuple<string, int>(player, team);
            }
            else
            {
                players.Add(new Tuple<string, int>(player, team));
            }
            DisplayPlayers();
        }

        private void btnChoose_Click(object sender, EventArgs e)
        {
            btnChoose.Enabled = false;
            var worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler((sender1, e1) =>
            {
                new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter().Serialize(stream, new SelectTeam(lstChooseTeam.SelectedIndex));
            });
            worker.RunWorkerCompleted += Team_Completed;
            worker.RunWorkerAsync();
        }

        private void Team_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                Error(e.Error);
            }
        }

        public void Error(Exception e)
        {
            MessageBox.Show(this, e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            btnChoose.Enabled = true;
        }
    }
}

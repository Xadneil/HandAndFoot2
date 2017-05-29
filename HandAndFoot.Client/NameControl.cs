using HandAndFoot.Client.Properties;
using HandAndFoot.Messages.ToServer;
using System;
using System.ComponentModel;
using System.Net.Sockets;
using System.Windows.Forms;

namespace HandAndFoot.Client
{
    public partial class NameControl : UserControl
    {
        public event Action<string> Completed;
        NetworkStream stream;

        public NameControl(NetworkStream stream)
        {
            InitializeComponent();
            this.stream = stream;
            var previousName = Settings.Default.PreviousName;
            if (!string.IsNullOrWhiteSpace(previousName))
            {
                txtName.Text = previousName;
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show(this, "You must enter a name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            btnSubmit.Text = "Submitting...";
            btnSubmit.Enabled = txtName.Enabled = false;

            var worker = new BackgroundWorker();

            worker.DoWork += new DoWorkEventHandler((sender1, e1) =>
            {
                new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter().Serialize(stream, new PlayerName(txtName.Text.Trim()));
            });
            worker.RunWorkerCompleted += Connect_Completed;
            worker.RunWorkerAsync();
        }

        private void Connect_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                Error(e.Error);
                return;
            }

            Settings.Default.PreviousName = txtName.Text.Trim();
            Settings.Default.Save();

            btnSubmit.Text = "Submit";
            btnSubmit.Enabled = txtName.Enabled = true;

            Completed?.Invoke(txtName.Text.Trim());
        }

        public void Error(Exception e)
        {
            MessageBox.Show(this, e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            btnSubmit.Text = "Submit";
            btnSubmit.Enabled = txtName.Enabled = true;
            txtName.Focus();
        }
    }
}

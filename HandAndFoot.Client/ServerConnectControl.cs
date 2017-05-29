using HandAndFoot.Client.Properties;
using System;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace HandAndFoot.Client
{
    public partial class ServerConnectControl : UserControl
    {
        public Socket Socket;
        public event Action<Socket> Completed;

        public ServerConnectControl()
        {
            InitializeComponent();
            txtHost.SetPlaceholder("Host");
            txtPort.SetPlaceholder("Port");
            var previousHost = Settings.Default.PreviousHost;
            var previousPort = Settings.Default.PreviousPort;

            if (!string.IsNullOrEmpty(previousHost))
                txtHost.Text = previousHost;
            if (previousPort != 0)
                txtPort.Text = previousPort.ToString();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtHost.Text) || string.IsNullOrWhiteSpace(txtPort.Text))
            {
                MessageBox.Show(this, "A host and a port must be entered.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            int port;
            if (!int.TryParse(txtPort.Text, out port))
            {
                MessageBox.Show(this, "The port must be a number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (port == 0)
            {
                MessageBox.Show(this, "The port cannot be 0.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            btnConnect.Text = "Connecting...";
            btnConnect.Enabled = txtHost.Enabled = txtPort.Enabled = false;

            try
            {
                Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                var args = new SocketAsyncEventArgs();
                args.RemoteEndPoint = new DnsEndPoint(txtHost.Text, port);
                args.Completed += Connect_Completed;

                if (!Socket.ConnectAsync(args))
                {
                    // action completed synchronously, handle it now.
                    Connect_Completed(null, args);
                }
            }
            catch (Exception ex)
            {
                Error(ex);
            }
        }

        private void Connect_Completed(object sender, SocketAsyncEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke((EventHandler<SocketAsyncEventArgs>)Connect_Completed, sender, e);
                return;
            }

            if (e.ConnectByNameError != null || e.SocketError != SocketError.Success)
            {
                Error(e.ConnectByNameError ?? new Exception($"Socket error code {(int)e.SocketError}. ({e.SocketError.ToString()})"));
                return;
            }

            Settings.Default.PreviousHost = txtHost.Text.Trim();
            Settings.Default.PreviousPort = Convert.ToInt32(txtPort.Text);
            Settings.Default.Save();

            btnConnect.Text = "Connect";
            btnConnect.Enabled = txtHost.Enabled = txtPort.Enabled = true;

            Completed?.Invoke(e.ConnectSocket);
        }

        private void Error(Exception e)
        {
            MessageBox.Show(this, e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            btnConnect.Text = "Connect";
            btnConnect.Enabled = txtHost.Enabled = txtPort.Enabled = true;
            txtHost.Focus();
        }
    }
}

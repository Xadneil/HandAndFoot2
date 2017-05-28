using HandAndFoot.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandAndFoot.Messages.ToClient;
using System.Net.Sockets;
using System.Net;

namespace HandAndFoot.Server
{
    public class SocketServer : Server
    {
        static SocketServer _instance;
        static SocketServer Instance { get { if (_instance == null) throw new InvalidOperationException("The server has not been initialized."); return _instance; } }

        public static void Initialize(Game game, PlayerPreference[] preferences)
        {
            _instance = new SocketServer(game, preferences.Select(p => p.Stream).ToArray());
        }

        public static void Reset()
        {
            _instance = null;
        }

        Dictionary<Player, NetworkStream> sockets;

        private SocketServer(Game game, NetworkStream[] sockets) : base(game)
        {
            this.sockets = new Dictionary<Player, NetworkStream>(sockets.Length);
            foreach (var socket in sockets.Select((x, i) => new { Socket = x, Index = i }))
            {
                this.sockets[game.Teams.SelectMany(t => t.Players).First(p => p.PlayerId == socket.Index)] = socket.Socket;
            }
        }

        public override void SendMessageToAll(Func<Team, Player, IClientMessage> message)
        {
            foreach (var pair in sockets)
            {
                new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter().Serialize(pair.Value, message(pair.Key.Team, pair.Key));
            }
        }

        public override void SendMessageToAll(IClientMessage message)
        {
            foreach (var socket in sockets.Values)
            {
                new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter().Serialize(socket, message);
            }
        }

        public override void SendMessage(Player player, IClientMessage message)
        {
            new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter().Serialize(sockets[player], message);
        }
    }
}

using HandAndFoot.Messages.ToClient;
using HandAndFoot.Messages.ToServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HandAndFoot.Server
{
    public class Lobby
    {
        public static PlayerPreference[] ConnectPlayers(int teams, int players, int port)
        {
            var playerCount = teams * players;
            PlayerPreference[] playerPreferences = new PlayerPreference[playerCount];

            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(new IPEndPoint(IPAddress.Any, port));
            socket.Listen(playerCount);

            var countdown = new CountdownEvent(playerCount);

            for (int i = 0; i < playerCount; i++)
            {
                var client = socket.Accept();
                var stream = new NetworkStream(client, true);
                var playerId = i;
                new Thread(() =>
                {
                    ConnectPlayer(stream, playerId, teams, players, playerPreferences);
                    countdown.Signal();
                }).Start();
            }

            countdown.Wait();
            socket.Close();

            return playerPreferences.OrderBy(p => p.Team).ThenBy(p => p.PlayerId).ToArray();
        }

        private static void ConnectPlayer(NetworkStream stream, int playerId, int teams, int playersPerTeam, PlayerPreference[] others)
        {
            var formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

            var ret = new PlayerPreference();
            others[playerId] = ret;
            ret.Stream = stream;
            ret.PlayerId = playerId;
            var name = formatter.Deserialize(stream) as PlayerName;
            ret.Name = name.Name;

            string[][] playerAllocation = new string[teams + 1][];
            for (int i = 0; i < teams; i++)
            {
                playerAllocation[i] = others.Where(p => p != null && p.Name != null && p.Team.HasValue && p.Team == i).Select(p => p.Name).ToArray();
            }
            var unteamedPlayers = others.Where(p => p != null && p.Name != null && !p.Team.HasValue).Select(p => p.Name).ToArray();
            playerAllocation[teams] = unteamedPlayers;

            formatter.Serialize(stream, new LobbyGameDetails(teams, playersPerTeam, playerAllocation));
            var selectedTeam = formatter.Deserialize(stream) as SelectTeam;
            ret.Team = selectedTeam.Team;

            foreach (var pref in others.Where(p => p != null && p.Stream != null))
            {
                if (pref.PlayerId != playerId)
                    formatter.Serialize(pref.Stream, new LobbyPlayerChoseTeam(name.Name, selectedTeam.Team));
            }
        }
    }
}

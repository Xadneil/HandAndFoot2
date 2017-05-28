using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading;
using System.Net.Sockets;
using HandAndFoot.Messages.ToServer;
using HandAndFoot.Messages.ToClient;

namespace HandAndFoot.Server.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Server1Player()
        {
            int numTeams = 1, playersPerTeam = 1, port = 9792;

            var thread = new Thread(() =>
            {
                var prefs = Lobby.ConnectPlayers(numTeams, playersPerTeam, port);
                Assert.AreEqual(1, prefs.Length);
                Assert.AreEqual("Test Name", prefs.First().Name);
                Assert.AreEqual(0, prefs.First().Team);
            });
            thread.Start();
            Thread.Sleep(1); // speed up test time by allowing server to start up and begin accepting connections

            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect("localhost", port);
            var stream = new NetworkStream(socket, true);
            var formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

            formatter.Serialize(stream, new PlayerName("Test Name"));
            var gameConfig = formatter.Deserialize(stream) as LobbyGameDetails;
            formatter.Serialize(stream, new SelectTeam(0));

            socket.Close();
            thread.Join();
        }

        //[TestMethod]
        //public void Server2Player()
        //{
        //    int numTeams = 2, playersPerTeam = 1, port = 9792;

        //    var thread = new Thread(() =>
        //    {
        //        var prefs = Lobby.ConnectPlayers(numTeams, playersPerTeam, port);
        //        Assert.AreEqual(2, prefs.Length);
        //        Assert.AreEqual("Test Name", prefs[0].Name);
        //        Assert.AreEqual(0, prefs[0].Team);
        //        Assert.AreEqual("Test Name 2", prefs[1].Name);
        //        Assert.AreEqual(1, prefs[1].Team);
        //    });
        //    thread.Start();
        //    Thread.Sleep(1); // speed up test time by allowing server to start up and begin accepting connections

        //    var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //    socket.Connect("localhost", port);
        //    var stream = new NetworkStream(socket, true);
        //    var formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

        //    formatter.Serialize(stream, new PlayerName("Test Name"));
        //    var gameConfig = formatter.Deserialize(stream) as LobbyGameDetails;
        //    formatter.Serialize(stream, new SelectTeam(0));

        //    var socket2 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //    socket2.Connect("localhost", port);
        //    stream = new NetworkStream(socket2, true);
        //    formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

        //    formatter.Serialize(stream, new PlayerName("Test Name 2"));
        //    gameConfig = formatter.Deserialize(stream) as LobbyGameDetails;
        //    Assert.AreEqual("Test Name", gameConfig.PlayerTeamAllocation[0][0]);
        //    formatter.Serialize(stream, new SelectTeam(1));

        //    socket.Close();
        //    socket2.Close();
        //    thread.Join();
        //}
    }
}

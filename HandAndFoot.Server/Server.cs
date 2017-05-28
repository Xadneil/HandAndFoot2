using HandAndFoot.Core;
using HandAndFoot.Messages.ToClient;
using HandAndFoot.Messages.ToServer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HandAndFoot.Server
{
    public abstract class Server
    {
        Dictionary<Team, TeamState> teamState;
        Dictionary<Player, PlayerState> playerState;
        int currentPlayer;
        protected Game game;

        protected Server(Game game)
        {
            this.game = game;
            currentPlayer = 0;
            teamState = new Dictionary<Team, TeamState>();
            playerState = new Dictionary<Player, PlayerState>();
            foreach (var team in game.Teams)
            {
                teamState[team] = new TeamState() { IsDown = false };
                foreach (var player in team.Players)
                {
                    playerState[player] = new PlayerState() { HasDrawn = false };
                }
            }
        }

        public void Discard(Player player, Discard client)
        {
            if (!MyTurn(player))
                return;
            if (!HasDrawn(player))
                return;
            if (player.Team.Any() && !TeamIsDown(player))
                return;


            SendMessageToAll(new NewDiscard(game.DiscardPile[game.DiscardPile.Count - 1]));
            playerState[player].HasDrawn = false;
        }

        public void DrawSevenCards(Player player, DrawSevenCards client)
        {
            if (!MyTurn(player))
                return;
            if (!TeamIsDown(player))
                return;

            var cards = game.DrawSevenCardsFromDiscardAndPlayNewBook(player.Team, player, client.Card1, client.Card2);
            playerState[player].HasDrawn = true;
            SendMessageToAll((t, p) => new UpdateTeam(player.Team == t, player.Team.ToArray()));
            SendMessageToAll(new NewDiscard(game.DiscardPile[game.DiscardPile.Count - 1]));
            SendMessage(player, new DrawSixCards(cards[0], cards[1], cards[2], cards[3], cards[4], cards[5]));
        }

        public void DrawTwoCards(Player player, Messages.ToServer.DrawTwoCards client)
        {
            if (!MyTurn(player))
                return;
            if (!HasDrawn(player))
                return;

            var cards = game.DrawTwoCards(player);
            playerState[player].HasDrawn = true;
            SendMessage(player, new Messages.ToClient.DrawTwoCards(cards[0], cards[1]));
        }

        public void PlayBook(Player player, PlayBook client)
        {
            if (!MyTurn(player))
                return;
            if (!HasDrawn(player))
                return;

            game.PlayNewBook(player.Team, player, client.Book);

            SendMessageToAll((t, p) => new UpdateTeam(player.Team == t, player.Team.ToArray()));
        }

        public void PlayCard(Player player, PlayCard client)
        {
            if (!MyTurn(player))
                return;
            if (!HasDrawn(player))
                return;

            game.PlayOnBook(player, client.Card, player.Team[client.BookId]);

            SendMessageToAll((t, p) => new UpdateTeam(player.Team == t, player.Team.ToArray()));
        }

        public abstract void SendMessageToAll(Func<Team, Player, IClientMessage> message);

        public abstract void SendMessageToAll(IClientMessage message);

        public abstract void SendMessage(Player player, IClientMessage message);

        private bool MyTurn(Player player)
        {
            if (player.PlayerId != currentPlayer)
            {
                SendMessage(player, new NotAllowed("It is not your turn."));
                return false;
            }
            return true;
        }

        private bool TeamIsDown(Player player)
        {
            if (!teamState[player.Team].IsDown)
            {
                if (CalculateTeamIsDown(player.Team))
                {
                    teamState[player.Team].IsDown = true;
                }
                else
                {
                    SendMessage(player, new NotAllowed("Your team has not melded yet."));
                    return false;
                }
            }
            return true;
        }

        private bool HasDrawn(Player player)
        {
            if (!playerState[player].HasDrawn)
            {
                SendMessage(player, new NotAllowed("You must draw first."));
                return false;
            }
            return true;
        }

        private bool CalculateTeamIsDown(Team team)
        {
            return team.Sum(b => b.Score()) >= game.Round.PointsToMeld();
        }
    }

    class TeamState
    {
        public bool IsDown;
    }

    class PlayerState
    {
        public bool HasDrawn;
    }
}

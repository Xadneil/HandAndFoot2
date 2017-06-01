using System;

namespace HandAndFoot.Messages.ToClient
{
    [Serializable]
    public class LobbyTeamOK : IClientMessage
    {
        public bool ReadyToStart;

        public LobbyTeamOK(bool readyToStart = false)
        {
            ReadyToStart = readyToStart;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandAndFoot.Client
{
    enum PlayerState
    {
        CONNECTING,
        ENTER_NAME,
        CHOOSE_TEAM,
        MY_TURN,
        NOT_MY_TURN,
        GAME_OVER
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    interface IBot
    {
        void BotMove(GameModel model);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    interface IControler
    {
        void MakeMove(int i, int j, GameModel.State side);
    }
}

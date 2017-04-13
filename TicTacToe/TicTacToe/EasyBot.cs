using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class EasyBot : IBot
    {
        // Координаты ячеек
        public struct Coordinate
        {
            public int i;
            public int j;
        }

        // Хранение координат свободных ячеек
        List<Coordinate> freeСells;

        public EasyBot()
        {
            freeСells = new List<Coordinate>();
        }
        
        public void BotMove(GameModel model)
        {
            Random rnd = new Random();
            for (int i = 0; i < model.Field.GetLength(0); ++i)
            {
                for (int j = 0; j < model.Field.GetLength(1); ++j)
                {
                    if (model.Field[i, j] == GameModel.State.none)
                    {
                        Coordinate temp = new Coordinate();
                        temp.i = i;
                        temp.j = j;
                        freeСells.Add(temp);
                    }
                }
            }

            do
            {
                try
                {
                    Coordinate SelectedMove = freeСells[rnd.Next(0, freeСells.Count)];
                    model.MakeMove(SelectedMove.i, SelectedMove.j, model.CurrentMove);
                    break;
                }
                catch (Exception)
                {
                    continue;
                }
            } while (true);
        }
    }
}

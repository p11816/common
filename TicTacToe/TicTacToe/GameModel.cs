using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class GameModel
    {
        public enum State
        {
            x,
            o,
            none
        }

        // Массив со значениями
        public State[,] Field { get; private set; }

        // Сетчик ходов
        public int CountStep { get; private set; }

        // Значение победителя
        public State Winner { get; private set; }

        // Флаг конца игры
        public bool GameOver { get; private set; }

        // Значение текущего хода
        public State CurrentMove { get; private set; }


        public GameModel()
        {
            Field = new State[3, 3];
            for (int i = 0; i < Field.GetLength(0); i++)
            {
                for (int j = 0; j < Field.GetLength(1); j++)
                {
                    Field[i, j] = State.none;
                }
            }
            CountStep = 0;
            Winner = State.none;
            GameOver = false;
            CurrentMove = State.x;
        }


        public delegate void UpdateViewDelegate(GameModel model);

        public event UpdateViewDelegate UpdateView;

        public void MakeMove(int i, int j, State side)
        { 
            if(GameOver)
            {
                throw new Exception("Game is already over!");
            }

            if(i < 0 || i > 2 || j < 0 || j > 2)
            {
                throw new IndexOutOfRangeException("Index out of field");
            }

            if(side != CurrentMove)
            {
                throw new Exception("it's not your turn");
            }

            if(Field[i, j] != State.none)
            {
                throw new Exception("Not empty field");
            }

            CountStep++;
            Field[i, j] = side;

            CheckForGameOver();
            CurrentMove = side == State.o ? State.x : State.o;
            UpdateView(this);
        }

        private void CheckForGameOver()
        {
            if (CountStep < 5) return;
            
            if (CountStep == 9)
            {
                return;
            }

            for(int i =0 ; i<3; i++) {
                if (Field[0, i] == Field[1, i] && Field[0, i] == Field[2, i] 
                    || Field[i,0] == Field[ i,1] && Field[ i,0] == Field[i,2]
                    || Field[0,0] == Field[1, 1] && Field[1, 1] == Field[2, 2]
                    || Field[2, 0] == Field[1, 1] && Field[1, 1] == Field[0, 2])
                {
                    Winner = CurrentMove;
                    GameOver = true;
                }
            }
        }
    }

    
}

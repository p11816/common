using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

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

        
        public State[,] Field { get; private set; }

        public int CountStep { get; set; }
        public State Winner { get; set; }
        public bool GameOver { get; set; }
        public State CurrentMove { get; set; }

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
                GameOver = true;
                Winner = State.none;
            }
            for(int i =0 ; i<3; i++) {
                if(
                    ((Field[0, i] == Field[1, i]) && (Field[0, i] == Field[2, i]))
                    ||
                    ((Field[i,0] == Field[ i,1]) && (Field[ i,0] == Field[i,2]))
                    )
                {
                    Winner = CurrentMove;
                    GameOver = true;
                }

            }
        }

        
        
    }

    
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class GameField : Form, IControler, IView
    {
        private GameModel model = new GameModel();
        private Button[,] field;
        Dictionary<GameModel.State, string> symbols = new Dictionary<GameModel.State, string>();
         public struct OptionsSizeButton        // структура хранит нужные параметры для подсчета новых координат кнопак
        {
            public int hButt;            // высота кнопки
            public int lButt;            // длина кнопки
            public int dH1;              // расстояние между кнопками по вертикали
            public int dL1;              // расстояние между кнопками по горизонтали
            public int dH2;                // расстояние от крайней кнопки до границы формы по вертикали
            public int dL2;                // расстояние от крайней кнопки до границы формы по вертикали
        }

        public GameField()
        {
            InitializeComponent();
            OptionsSizeButton sizeButt;
            getOptionsSizeButton(out sizeButt);
            symbols[GameModel.State.x] = "x";
            symbols[GameModel.State.o] = "o";
            symbols[GameModel.State.none] = "";
            field = new Button[3, 3];
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    Button b = new Button();
                    // логические координаты кнопки
                    b.Left = sizeButt.dL2 + i * (sizeButt.dL1 + sizeButt.lButt);
                    b.Top = sizeButt.dH2 + j * (sizeButt.dH1 + sizeButt.hButt); ;
                    b.Size = new Size(sizeButt.lButt, sizeButt.hButt);
                    b.Tag = new Point(i, j);
                    b.Click += GameField_Click;
                    // добавили на форму
                    this.Controls.Add(b);
                    // добавили в массив
                    field[i, j] = b;
                }
            }


            model.UpdateView += UpdateView;
        }

        void GameField_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            Point p = (Point)b.Tag;
            try
            {
                this.MakeMove(p.X, p.Y, model.CurrentMove);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public void UpdateView(GameModel model)
        {
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    field[i, j].Text = symbols[model.Field[i, j]];
                }
            }
            if (model.GameOver)
            {
                MessageBox.Show("Game Over, winner is " + symbols[model.Winner]);
            }
        }

        public void MakeMove(int i, int j, GameModel.State side)
        {
            model.MakeMove(i, j, side);
        }

        private void GameField_ResizeEnd(object sender, EventArgs e)
        {
            regdrawField();
        }

        private void regdrawField()
        {
            OptionsSizeButton sizeButt;
            getOptionsSizeButton(out sizeButt);
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    // логические координаты кнопки
                    field[i, j].Left = sizeButt.dL2 + i * (sizeButt.dL1 + sizeButt.lButt);
                    field[i, j].Top = sizeButt.dH2 + j * (sizeButt.dH1 + sizeButt.hButt); ;
                    field[i, j].Size = new Size(sizeButt.lButt, sizeButt.hButt);
                }
            }
        }

        private void getOptionsSizeButton(out OptionsSizeButton newSise)
        {   
            int hForm = this.ClientSize.Height;
            int lForm = this.ClientSize.Width;
            newSise.hButt = (int)(hForm * 0.20);            // высота кнопки
            newSise.lButt = (int)(lForm * 0.20);            // длина кнопки
            newSise.dH1 = (int)(hForm * 0.03);              // расстояние между кнопками по вертикали
            newSise.dL1 = (int)(lForm * 0.03);              // расстояние между кнопками по горизонтали
            newSise.dH2 = (hForm - newSise.hButt * 3 - 2 * newSise.dH1) / 2;    // расстояние от крайней кнопки до границы формы по вертикали
            newSise.dL2 = (lForm - newSise.lButt * 3 - 2 * newSise.dL1) / 2;    // расстояние от крайней кнопки до границы формы по вертикали
        }

    }
}

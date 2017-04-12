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

        public GameField()
        {
            InitializeComponent();
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
                    b.Left = i * 50 + 10;
                    b.Top = j * 50 + 10;
                    b.Size = new Size(40, 40);
                    b.Tag = new Point(i, j);
                    b.Click += GameField_Click;
                    // добавили на форму
                    this.Controls.Add(b);
                    // добавили в массив
                    field[i, j] = b;
                }
            }
            this.Width = 200;
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

        private void GameField_Resize(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            if (control.ClientSize.Height != control.ClientSize.Width)
            {
                control.ClientSize = new Size(control.ClientSize.Width, control.ClientSize.Width);
            }
            int width = control.ClientSize.Width;
            int heiht = control.ClientSize.Height;
            int intend = 8 + width / 40;

            for (int i = 0; i < field.GetLength(0); i++) 
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    // логические координаты кнопки
                    field[i, j].Left = i * (width - intend * 4) / 3 + (i + 1) * intend;
                    field[i, j].Top = j * (heiht - intend * 4) / 3 + (j + 1) * intend;
                    field[i, j].Size = new Size((width - intend * 4) / 3, (heiht - intend * 4) / 3);
                    field[i, j].Tag = new Point(i, j);
                }
            }
        }
    }
}

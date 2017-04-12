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
        // Рабочая модель
        private GameModel model = new GameModel();

        // Массив кнопок
        private Button[,] field;

        // Коллекция для хранения знаений
        Dictionary<GameModel.State, string> symbols = new Dictionary<GameModel.State, string>();

        public GameField()
        {
            InitializeComponent();
            symbols[GameModel.State.x] = "X";
            symbols[GameModel.State.o] = "O";
            symbols[GameModel.State.none] = "";

            field = new Button[3, 3];

            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    Button b = new Button();
                    // Логические координаты кнопки
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

        private void GameField_Load(object sender, EventArgs e)
        {

        }
    }
}

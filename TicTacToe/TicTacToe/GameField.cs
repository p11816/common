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
        List<Bitmap> images;
        
        public GameField()
        {
            InitializeComponent();
            field = new Button[3, 3];
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    Button b = new Button();
                    b.BackgroundImageLayout = ImageLayout.Stretch;
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
            images = new List<Bitmap>();
            images.Add(new Bitmap("..\\..\\X.gif"));
            images.Add(new Bitmap("..\\..\\O.gif"));

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
                    if(model.Field[i, j] == GameModel.State.x) field[i, j].BackgroundImage = images[0];
                    else if (model.Field[i, j] == GameModel.State.o) field[i, j].BackgroundImage = images[1];

                }
            }
            if (model.GameOver)
            {
                string strWiner;
                // проверка победителя возможно 3 варианта
                if (model.Winner == GameModel.State.none)
                {
                    strWiner = "no winner, dead heat";
                }
                else
                {
                    strWiner = (model.Winner == GameModel.State.x ? " winner is player X" : "  winner is player O");
                }

                MessageBox.Show("Game Over, " + strWiner);
            }
        }

        public void MakeMove(int i, int j, GameModel.State side)
        {
            model.MakeMove(i, j, side);
        }
    }
}

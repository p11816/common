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
        private GameModel newmodel = null;
        private GameModel model = new GameModel();
        private Button[,] field;
        List<Bitmap> images;
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
                    if(model.Field[i, j] == GameModel.State.x) field[i, j].BackgroundImage = images[0];
                    else if (model.Field[i, j] == GameModel.State.o) field[i, j].BackgroundImage = images[1];

                }
            }
            if (model.GameOver)
            {
                MessageBox.Show("Game Over, winner is " + symbols[model.Winner] + "\n" +
                    "Count of X's winners: " + GameModel.countXWin + "\n" +
                    "Count of O's winners: " + GameModel.countOWin + "\n" +
                    "Count of draws: " + GameModel.draw);

                DialogResult result = MessageBox.Show("Would you like to play again?", "Message", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                if(result == DialogResult.Yes)
                {
                    newmodel = new GameModel();
                    newmodel.UpdateView += UpdateView;
            }
        }
        }
        public void MakeMove(int i, int j, GameModel.State side)
        {
            model.MakeMove(i, j, side);
            if (newmodel != null)
            {
                model = newmodel;
                UpdateView(model);
                newmodel = null;
            }
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

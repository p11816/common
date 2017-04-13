using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace TicTacToe
{
    public partial class GameField : Form, IControler, IView
    {
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
            SaveFileDialog.Filter = "xml files (*.xml)|*.xml";
            OpenFileDialog.Filter = "All Files (*.*)|*.*| xml files (*.xml)|*.xml";
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

            GameField_Resize(this, null);

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
                    if (model.Field[i, j] == GameModel.State.none) field[i, j].BackgroundImage = null;
                    else if (model.Field[i, j] == GameModel.State.x) field[i, j].BackgroundImage = images[0];
                    else if (model.Field[i, j] == GameModel.State.o) field[i, j].BackgroundImage = images[1];
                }
            }
        }
        public void MakeMove(int i, int j, GameModel.State side)
        {
            model.MakeMove(i, j, side);

            if (model.GameOver)
            {
                MessageBox.Show("Game Over, winner is " + symbols[model.Winner] + "\n" +
                    "Count of X's winners: " + GameModel.countXWin + "\n" +
                    "Count of O's winners: " + GameModel.countOWin + "\n" +
                    "Count of draws: " + GameModel.draw);

                DialogResult result = MessageBox.Show("Would you like to play again?", "Message", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                if (result == DialogResult.Yes)
                {
                    model = new GameModel();
                    model.UpdateView += UpdateView;
                    UpdateView(model);
                }

                else this.Close();
            }
        }

        private void GameField_Resize(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            if (control.ClientSize.Height - 20 != control.ClientSize.Width)
            {
               //MessageBox.Show("control.ClientSize.Height = " + control.ClientSize.Height + "\ncontrol.ClientSize.Width = " + control.ClientSize.Width);
                control.ClientSize = new Size(control.ClientSize.Width - 20, control.ClientSize.Width);
            }

            int width = control.ClientSize.Width;
            int heiht = control.ClientSize.Height - 20;
            int intend = 8 + width / 40;

            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    // логические координаты кнопки
                    field[i, j].Left = i * (width - intend * 4) / 3 + (i + 1) * intend;
                    field[i, j].Top = 20 + j * (heiht - intend * 4) / 3 + (j + 1) * intend;
                    field[i, j].Size = new Size((width - intend * 4) / 3, (heiht - intend * 4) / 3);
                    field[i, j].Tag = new Point(i, j);
                }
            }
        }

        private void LoadToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenFileDialog.FileName = "";

            try
            {
                if (OpenFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string fileName = OpenFileDialog.FileName;

                    XmlDocument doc = new XmlDocument();
                    doc.Load(fileName);

                    var nodes = doc.SelectNodes("//Field");
                    string[] fieldStr = nodes.Item(0).InnerText.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    model = new GameModel();

                    for (int i = 0, k = 0; i < 3; ++i)
                    {
                        for (int j = 0; j < 3; ++j, ++k)
                        {
                            model.Field[i, j] = (GameModel.State)Enum.Parse(typeof(GameModel.State), fieldStr[k]);
                        }
                    }

                    nodes = doc.SelectNodes("//CountStep");
                    model.CountStep = Convert.ToInt32(nodes.Item(0).InnerText);
                    nodes = doc.SelectNodes("//Winner");
                    model.Winner = (GameModel.State)Enum.Parse(typeof(GameModel.State), nodes.Item(0).InnerText);
                    nodes = doc.SelectNodes("//GameOver");
                    model.GameOver = Convert.ToBoolean(nodes.Item(0).InnerText);
                    nodes = doc.SelectNodes("//CurrentMove");
                    model.CurrentMove = (GameModel.State)Enum.Parse(typeof(GameModel.State), nodes.Item(0).InnerText);
                    nodes = doc.SelectNodes("//CountXWin");
                    GameModel.countXWin = Convert.ToInt32(nodes.Item(0).InnerText);
                    nodes = doc.SelectNodes("//CountOWin");
                    GameModel.countOWin = Convert.ToInt32(nodes.Item(0).InnerText);
                    nodes = doc.SelectNodes("//Draw");
                    GameModel.draw = Convert.ToInt32(nodes.Item(0).InnerText);
                    model.UpdateView += UpdateView;
                    UpdateView(model);
                }
            }

            catch (Exception obj) { MessageBox.Show(obj.Message, ""); }
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog.FileName = "";

            try
            {
                if (SaveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string fileName = SaveFileDialog.FileName;
                    XmlTextWriter xml = new XmlTextWriter(fileName, Encoding.UTF8);

                    xml.WriteStartDocument();
                    xml.WriteStartElement("GameModel");
                    xml.WriteEndElement();
                    xml.Close();
                    XmlDocument doc = new XmlDocument();
                    doc.Load(fileName);
                    XmlNode node = doc.CreateElement("Field");
                    string arr = "";

                    foreach (var elem in model.Field)
                    {
                        arr += (elem != GameModel.State.none ? symbols[elem] : "none") + " ";
                    }

                    doc.DocumentElement.AppendChild(node);
                    node.InnerText = arr;
                    node = doc.CreateElement("CountStep");
                    node.InnerText = model.CountStep.ToString();
                    doc.DocumentElement.AppendChild(node);
                    node = doc.CreateElement("Winner");
                    node.InnerText = (model.Winner != GameModel.State.none ? symbols[model.Winner] : "none");
                    doc.DocumentElement.AppendChild(node);
                    node = doc.CreateElement("GameOver");
                    node.InnerText = model.GameOver ? "true" : "false";
                    doc.DocumentElement.AppendChild(node);
                    node = doc.CreateElement("CurrentMove");
                    node.InnerText = symbols[model.CurrentMove];
                    doc.DocumentElement.AppendChild(node);
                    node = doc.CreateElement("CountXWin");
                    node.InnerText = GameModel.countXWin.ToString();
                    doc.DocumentElement.AppendChild(node);
                    node = doc.CreateElement("CountOWin");
                    node.InnerText = GameModel.countOWin.ToString();
                    doc.DocumentElement.AppendChild(node);
                    node = doc.CreateElement("Draw");
                    node.InnerText = GameModel.draw.ToString();
                    doc.DocumentElement.AppendChild(node);
                    doc.Save(fileName);
                }
            }

            catch (Exception obj) { MessageBox.Show(obj.Message, ""); }
        }

        // Выыбор компьютера в качестве противника
        private void ComputerToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}

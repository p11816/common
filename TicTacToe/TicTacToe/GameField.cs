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
                    string[] fieldStr = nodes.Item(0).InnerText.Split(new char[]{' '}, StringSplitOptions.RemoveEmptyEntries);
                    model = new GameModel();

                    for(int i = 0, k = 0; i < 3; ++i)
                    {
                        for(int j = 0; j < 3; ++j, ++k)
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
                    doc.Save(fileName);
                }
            }

            catch (Exception obj) { MessageBox.Show(obj.Message, "Error"); }
        }
    }
}

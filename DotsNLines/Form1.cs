using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DotsNLines
{
    public enum PlayerType
    {
        None,
        Human,
        Computer
    }

    public partial class FormDotsNLines : Form
    {
        // Spacing
        private int rows = 4;
        private int columns = 4;
        private int dotSize = 20;
        private int paddingX = 45;
        private int paddingY = 65;
        private int spacingX = 120;
        private int spacingY = 120;

        // Game settings
        private Board _board;
        private PlayerType _playerType;

        public FormDotsNLines()
        {
            InitializeComponent();

            _playerType = PlayerType.Human;
            _board = new Board(rows, columns);
        }

        private void pictureBoxGame_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // Brushes
            SolidBrush redBrush = new SolidBrush(Color.FromArgb(100, 255, 100, 100));
            SolidBrush blueBrush = new SolidBrush(Color.FromArgb(100, 100, 200, 255));
            SolidBrush blackBrush = new SolidBrush(Color.Black);
            SolidBrush whiteBrush = new SolidBrush(Color.White);
            SolidBrush brush;

            // Pens
            Pen humanPen = new Pen(Color.FromArgb(100, 100, 200, 255), 10);
            Pen computerPen = new Pen(Color.FromArgb(100, 255, 100, 100), 10);
            Pen emptyPen = new Pen(Color.DarkGray, 10);

            // desenare background patrate
            foreach (Box box in _board.boxes)
            {
                if (box.OwnedBy == PlayerType.Human)
                {
                    Rectangle rect = GetBoxRect(box.X1, box.Y1);
                    g.FillRectangle(blueBrush, rect.X, rect.Y, spacingX, spacingY);
                }

                if (box.OwnedBy == PlayerType.Computer)
                {
                    Rectangle rect = GetBoxRect(box.X1, box.Y1);
                    g.FillRectangle(redBrush, rect.X, rect.Y, spacingX, spacingY);
                }

                if (box.OwnedBy == PlayerType.None)
                {
                    Rectangle rect = GetBoxRect(box.X1, box.Y1);
                    g.FillRectangle(whiteBrush, rect.X, rect.Y, spacingX, spacingY);
                }
            }

            foreach (Line line in _board.Lines)
            {
                Point P1 = GetPointCoordinates(line.X1, line.Y1);
                Point P2 = GetPointCoordinates(line.X2, line.Y2);
                Pen pen = line.OwnedBy == PlayerType.None ? emptyPen : (line.OwnedBy == PlayerType.Human ? humanPen : computerPen);

                g.DrawLine(pen, P1, P2);
            }

            brush = blackBrush;
            for (int i = 0; i < rows; ++i)
            {
                for (int j = 0; j < columns; ++j)
                {
                    Point p = GetPointCoordinates(i, j);
                    g.FillEllipse(brush, p.X - dotSize / 2, p.Y - dotSize / 2, dotSize, dotSize);
                }
            }

            g.DrawString($"You: {_board.humanScore}\tAI: {_board.computerScore}", new Font("Comic Sans MS", 12, FontStyle.Bold), new SolidBrush(Color.Black), 10, 10);
        }

        private void pictureBoxBackground_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            SolidBrush redBrush = new SolidBrush(Color.FromArgb(100, 255, 100, 100));
            SolidBrush blueBrush = new SolidBrush(Color.FromArgb(100, 100, 200, 255));
            SolidBrush blackBrush = new SolidBrush(Color.Black);
            SolidBrush brush;

            if (_playerType == PlayerType.Computer)
            {
                brush = redBrush;
            }
            else
            {
                brush = blueBrush;
            }
            g.FillRectangle(brush, 0, 0, 800, 800);

            g.DrawString("Create New Game", new Font("Comic Sans MS", 24), new SolidBrush(Color.Black), 150, 300);
        }

        private Point GetPointCoordinates(int indexX, int indexY)
        {
            int x = paddingX + indexX * spacingX;
            int y = paddingY + indexY * spacingY;

            return new Point(x, y);
        }

        private Rectangle GetBoxRect(int indexX, int indexY)
        {
            Point topLeft = GetPointCoordinates(indexX, indexY);
            Point bottomRight = GetPointCoordinates(indexX + 1, indexY + 1);

            return new Rectangle(topLeft.X, topLeft.Y, bottomRight.X - topLeft.X, bottomRight.Y - topLeft.Y);
        }

        private void pictureBoxGame_MouseUp(object sender, MouseEventArgs e)
        {
            Point P = new Point(e.X, e.Y);

            for(int i = 0; i < _board.Lines.Count(); ++i)
            {
                Line line = _board.Lines[i];
                Point P1 = GetPointCoordinates(line.X1, line.Y1);
                Point P2 = GetPointCoordinates(line.X2, line.Y2);
                if(P.X >= P1.X - 20 && P.X <= P2.X + 20 && P.Y >= P1.Y - 20 && P.Y <= P2.Y + 20)
                {
                    _board.Lines[i].OwnedBy = PlayerType.Human;
                    for(int k = 0; k < _board.boxes.Count; ++k)
                    {
                        for(int l = 0; l < _board.boxes[k].Lines.Count; ++l)
                        {
                            if (_board.boxes[k].Lines[l].Id == _board.Lines[i].Id)
                            {
                                _board.boxes[k].Lines[l].OwnedBy = PlayerType.Human;
                            }
                        }
                    }

                    for(int k = 0; k < _board.boxes.Count; ++k)
                    {
                        if (_board.boxes[k].Lines.Where(l => l.OwnedBy != PlayerType.None).Count() == 4)
                        {
                            _board.boxes[k].OwnedBy = PlayerType.Human;
                        }
                    }
                    //
                }
            }
            _board.humanScore = 0;
            foreach(Box box in _board.boxes)
            {
                if(box.OwnedBy == PlayerType.Human)
                {
                    _board.humanScore++;
                }
            }
            Refresh();

            CheckFinish();
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBoxGame.Visible = true;
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void CheckFinish()
        {
            if(_board.boxes.Where(b => b.OwnedBy == PlayerType.None).Count() == 0)
            {
                if(_board.humanScore > _board.computerScore)
                {
                    if(MessageBox.Show("Ai castigat.", "Game Info", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                    {
                        Application.Exit();
                    }
                }
                else
                {
                    if (MessageBox.Show("Ai pierdut.", "Game Info", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                    {
                        Application.Exit();
                    }
                }
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Informatii despre aplicatie");
        }

        private void pictureBoxBackground_Click(object sender, EventArgs e)
        {
            pictureBoxGame.Visible = true;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace DotsNLines
{
    public partial class Board
    {
        public List<Line> Lines;
        public List<Box> boxes;
        public int humanScore;
        public int computerScore;
        public int rowsDots, columnsDots;
        public int difficulty;

        public Board(int rowsDots, int columnsDots)
        {
            int id = 0;
            this.rowsDots = rowsDots;
            this.columnsDots = columnsDots;

            Lines = new List<Line>();
            boxes = new List<Box>();
            for(int i = 0; i < this.rowsDots - 1; ++i)
            {
                for(int j = 0; j < this.columnsDots - 1; ++j)
                {
                    boxes.Add(new Box(id++, j, i, j + 1, i + 1, new List<Line>()));
                }
            }

            humanScore = 0;
            computerScore = 0;

            id = 0;

            // orizontale
            for (int i = 0; i < this.rowsDots; ++i)
            {
                for(int j = 0; j < this.columnsDots - 1; ++j)
                {
                    Line line = new Line() { Id = id++, X1 = j, Y1 = i, X2 = j + 1, Y2 = i };
                    Lines.Add(line);
                    for(int k = 0; k < boxes.Count; ++k)
                    {
                        if(boxes[k].IsAdjacent(line))
                        {
                            boxes[k].Lines.Add(line);
                        }
                    }
                }
            }

            // verticale
            for (int i = 0; i < this.rowsDots - 1; ++i)
            {
                for (int j = 0; j < this.columnsDots; ++j)
                {
                    Line line = new Line() { Id = id++, X1 = j, Y1 = i, X2 = j, Y2 = i + 1 };
                    Lines.Add(line);
                    for (int k = 0; k < boxes.Count; ++k)
                    {
                        if (boxes[k].IsAdjacent(line))
                        {
                            boxes[k].Lines.Add(line);
                        }
                    }
                }
            }
        }
    }
}

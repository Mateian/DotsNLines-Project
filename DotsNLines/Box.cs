using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace DotsNLines
{
    public class Box
    {
        private int _id;
        private int _x1, _y1, _x2, _y2;
        private List<Line> _adjacentLines;
        private PlayerType _ownedBy = PlayerType.None;

        public int Id => _id;
        public int X1 => _x1;
        public int Y1 => _y1;
        public int X2 => _x2;
        public int Y2 => _y2;
        public List<Line> Lines => _adjacentLines;
        public PlayerType OwnedBy { get => _ownedBy; set => _ownedBy = value; }

        public Box(int id, int x1, int y1, int x2, int y2, List<Line> lines)
        {
            _id = id;
            _x1 = x1;
            _y1 = y1;
            _x2 = x2;
            _y2 = y2;
            _adjacentLines = lines;
        }

        public bool IsAdjacent(Line line) =>
            (line.X1 == X1 && line.Y1 == Y1 && line.X2 == X2 && line.Y2 == Y1)
            ||
            (line.X1 == X1 && line.Y1 == Y2 && line.X2 == X2 && line.Y2 == Y2)
            ||
            (line.X1 == X1 && line.Y1 == Y1 && line.X2 == X1 && line.Y2 == Y2) 
            ||
            (line.X1 == X2 && line.Y1 == Y1 && line.X2 == X2 && line.Y2 == Y2);
    }
}

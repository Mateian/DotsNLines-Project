using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotsNLines
{
    public class Line
    {
        public int Id;
        public int X1, Y1, X2, Y2;
        public PlayerType OwnedBy = PlayerType.None;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Point
    {
        public Point()
        {

        }
        public Point(int Row,int Col)
        {
            this.Row = Row;
            this.Col = Col;
        }
        public int Row { get; set; }
        public int Col { get; set; }
    }
}

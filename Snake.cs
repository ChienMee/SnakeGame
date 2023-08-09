using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleApp1
{
    internal class Snake
    {
        public Snake()
        {
            this.Head = new Point(1,1);
            this.Body = new List<Point>();
            this.Body.Add(new Point(-1, -1));
            this.Body.Add(new Point(-1, -1));
        }
        public Point Head { get; set; }
        public List<Point> Body { get;set; }
        private void moveBody(int aRow,int aCol)
        {
            for (int i = 0; i < this.Body.Count; i++)
            {
                int tempRow = this.Body[i].Row;
                int tempCol = this.Body[i].Col;

                this.Body[i].Row = aRow;
                this.Body[i].Col = aCol;

                aRow = tempRow;
                aCol = tempCol;
            }
        }
        public bool IsHeadInBody()
        {
            for(int i = 0;i < this.Body.Count; i++)
            {
                Point element = this.Body[i];
                if(element.Row == this.Head.Row && element.Col == this.Head.Col)
                {
                    return true;
                }
            }
            return false;
        }
        public void move(string direction,int n, int m)
        {
            if (direction == Direction.DIRECTION_RIGHT)
            {
                // column ++ --> thay đổi vị trí của đầu rắn
                int currentColumn = this.Head.Col;
                this.Head.Col = currentColumn + 1;
                if (this.Head.Col >= m - 1)
                {
                    this.Head.Col = 1;
                }
                this.moveBody(this.Head.Row,currentColumn);
            }
            else if (direction == Direction.DIRECTION_LEFT)
            {
                // column --
                int currentColumn = this.Head.Col;
                this.Head.Col = currentColumn - 1;
                if (this.Head.Col <= 0)
                {
                    this.Head.Col = m - 2;
                }
                this.moveBody(this.Head.Row, currentColumn);
            }
            else if (direction == Direction.DIRECTION_UP)
            {
                // row --
                int currentRow = this.Head.Row;
                this.Head.Row = currentRow - 1;
                if (this.Head.Row <= 0)
                {
                    this.Head.Row = n - 2;
                }
                this.moveBody(currentRow, this.Head.Col);
            }
            else if (direction == Direction.DIRECTION_DOWN)
            {
                // row ++
                int currentRow = this.Head.Row;
                this.Head.Row = currentRow + 1;
                if (this.Head.Row >= n - 1)
                {
                    this.Head.Row = 1;
                }
                this.moveBody(currentRow, this.Head.Col);
            }
        }
    }
}

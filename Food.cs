using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Food
    {
        public Food()
        {
            this.Point = new Point();
        }
        public int Size;
        public Point Point { get; set; }

        public void RandomPoint(Snake snake,int n,int m)
        {
            // không được nằm trong tường,thân rắn và đầu rắn
            Random random = new Random();
            do
            {
                this.Point.Row = random.Next(n);
                this.Point.Col = random.Next(m);
            } while (this.Point.Row >= n - 1 || this.Point.Col >= m - 1 || this.Point.Row <= 0 || this.Point.Col <= 0 || IsFoodInHeadOrBodyOfSnake(snake));
        }
        
        private bool IsFoodInHeadOrBodyOfSnake(Snake snake)
        {
            List<Point> points = new List<Point>();
            points.Add(snake.Head);
            points.AddRange(snake.Body);

            Console.WriteLine(points.Count);

            for(int i = 0;i < points.Count; i++)
            {
                Point element = points[i];
                if(element.Row == this.Point.Row && element.Col == this.Point.Col)
                {
                    return true;
                }
            }

            return false;
        }
    }
}

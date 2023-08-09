using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static bool isGameOver = false;

        static int N = 15;
        static int M = 30;
        static int Speed = 500;
        static int Score = 0;

        static string BRICK = "#";
        static string SPACE = " ";
        static string SKIN = "*";
        static string APPLE = "@";

        static string direction = Direction.DIRECTION_RIGHT;

        static string[,] board = new string[N, M];
        static Snake snake = new Snake();
        static Food food = new Food();

        private static void CalcWall()
        {
            for(int i = 0; i < N; i++)
            {
                for(int j = 0; j < M; j++)
                {
                    if(i == 0 || i == N-1 || j == 0 || j == M-1)
                    {
                        board[i, j] = BRICK;
                    }
                }
            }
        }

        private static void CalcSnake()
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    // đầu rắn
                    int row = snake.Head.Row;
                    int col = snake.Head.Col;
                    if (i == row && j == col)
                    {
                        board[i, j] = SKIN;
                    }
                    // thân rắn
                    List<Point> body = snake.Body;
                    for (int k = 0;k < body.Count; k++)
                    {
                        Point element = body[k]; 
                        if(i == element.Row && j == element.Col)
                        {
                            board[i, j] = SKIN;
                        }
                    }
                }
            }
        }

        private static void CalcFood()
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    if (i == food.Point.Row && j == food.Point.Col)
                    {
                        board[i, j] = APPLE;
                    }
                }
            }
        }

        private static void ResetBoard()
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    board[i, j] = SPACE;
                }
            }
        }
        private static void PrintBoard()
        {
            Console.WriteLine($"Score:{Score}");
            Console.WriteLine($"Speed:{Speed}");
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    string value = board[i, j];
                    // nếu wall là # thì sẽ in ra màu đỏ
                    if (value.Equals(BRICK))
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.Write(value);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write(value);
                    }
                }
                Console.WriteLine();
            }

        }

        static void ListenKey()
        {
            
            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    if(direction != Direction.DIRECTION_DOWN)
                    {
                        direction = Direction.DIRECTION_UP;
                    }
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    if(direction != Direction.DIRECTION_UP)
                    {
                        direction = Direction.DIRECTION_DOWN;
                    }
                }
                else if (keyInfo.Key == ConsoleKey.LeftArrow)
                {
                    if(direction != Direction.DIRECTION_RIGHT)
                    {
                        direction = Direction.DIRECTION_LEFT;
                    }
                }
                else if (keyInfo.Key == ConsoleKey.RightArrow)
                {
                    if (direction != Direction.DIRECTION_LEFT)
                    {
                        direction = Direction.DIRECTION_RIGHT;
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            // bắt sự kiển điều hướng bằng bàn phím
            Thread thread = new Thread(Program.ListenKey);
            thread.Start();

            food.RandomPoint(snake, N, M);

            while (!isGameOver)
            {
                Console.Clear();

                // khởi tạo giá trị ban đầu cho board là khoảng trống 
                ResetBoard();

                // tính toán vị trí của tường
                CalcWall();

                //tính toán vị trí của thức ăn sẽ xuất hiện
                CalcFood();

                // tính toán vị trí của đầu sâu :))
                CalcSnake();

                // in ra dữ liệu đã tính toán được
                PrintBoard();

                // va chạm với thức ăn thì dài ra, điểm và tốc độ tăng lên
                if (snake.Head.Row == food.Point.Row && snake.Head.Col == food.Point.Col)
                {
                    // rắn dài ra
                    snake.Body.Add(new Point(-1, -1));

                    // điểm tốc độ tăng lên
                    Score++;
                    Speed -= 50;
                    Speed = Speed < 100 ? 100 : Speed;

                    // thức ăn đổi vị trí
                    food.RandomPoint(snake, N, M);
                }

                // đầu chạm vào thân sẽ chết
                if (snake.IsHeadInBody())
                {
                    Console.WriteLine("Game Over");
                    isGameOver = true;
                    break;
                }

                // di chuyển sâu
                snake.move(direction, N, M);
                Task.Delay(Speed).Wait();


            }
            //thread.Suspend();
        }
    }
}

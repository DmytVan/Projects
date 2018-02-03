using System;
using System.Collections.Generic;
using System.Threading;

namespace Lab1
{


    class Program
    {

        static Maze maze = new Maze(21,21);

        static void Main(string[] args)
        {

            Console.WindowHeight = Console.LargestWindowHeight;
            Console.WindowWidth = Console.LargestWindowWidth;
            
            ConsoleKeyInfo keypress;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Старт\n2. Задать размеры поля\n3. Выход");
                keypress = Console.ReadKey(true);
                if (keypress.Key == ConsoleKey.D1 || keypress.Key == ConsoleKey.NumPad1)
                    Start();
                if (keypress.Key == ConsoleKey.D2 || keypress.Key == ConsoleKey.NumPad2)
                {
                    maze.SizeField();
                    Start();
                }
                if (keypress.Key == ConsoleKey.D3 || keypress.Key == ConsoleKey.NumPad3)
                    break;
            }
        }



        static public void Start()
        {
            Console.Clear();
            maze.Build();
            maze.Game();
        }

    }




    class Point
    {
        public int x;
        public int y;
      
        public Point(int x, int y) {
            this.x = x;
            this.y = y;
        }
    }

    class Maze
    {

        int height, width;
        byte[,] maze;
        List<Point> stack = new List<Point>();
        Random rnd = new Random();
        List<Point> exit = new List<Point>(); 

        public Maze()
        { }

        public Maze(int heightField, int widthField)
        {
            height = heightField;
            width = widthField;
        }



        public void SizeField()
        {
            bool result = false;
            do
            {
                Console.WriteLine("Высота поля > 4: ");
                result = Int32.TryParse(Console.ReadLine(), out height);
                if (height < 5 || height > 3000)
                    result = false;
            }
            while (!result);

            do
            {
                Console.WriteLine("Ширина поля 4>&<" + (Console.LargestWindowWidth - 10) + "): ");
                result = Int32.TryParse(Console.ReadLine(), out width);
                if (width > Console.LargestWindowWidth-10 || width < 5)
                    result = false;
            }
            while (!result);
            if (height % 2 == 0) height += 1;
            if (width % 2 == 0) width += 1;

        }

        void GetCellsNeighborsStates(Point cur, out int l, out int t, out int r, out int  b)
        {
            if (cur.x > 2)          l = maze[cur.x - 2, cur.y]; else l = 1;
            if (cur.y > 2)          t = maze[cur.x, cur.y - 2]; else t = 1;
            if (cur.x < height - 2)  r = maze[cur.x + 2, cur.y]; else r = 1;
            if (cur.y < width - 2) b = maze[cur.x, cur.y + 2]; else b = 1;
        }

        bool GetRundomNextPoint(Point cur, out Point next)
        {
            int n = 0, l = 0, t = 0, r = 0, b = 0;
            Point[] a = new Point[3];

            GetCellsNeighborsStates(cur, out l, out t, out r, out b);
            bool result = ((l == 0) || (t == 0) || (r == 0) || (b == 0));
            if (result)
            {
                n = -1;
                if (l == 0)
                {
                    n++;
                    a[n] = new Point(cur.x - 2, cur.y);
                }
                if (t == 0)
                {
                    n++;
                    a[n] = new Point(cur.x, cur.y - 2);
                }
                if (r == 0)
                {
                    n++;
                    a[n] = new Point(cur.x + 2, cur.y);
                }
                if (b == 0)
                {
                    n++;
                    a[n] = new Point(cur.x, cur.y + 2);
                }
                n = rnd.Next(n+1);
                next = a[n];
            } else {
                next = null;// new Point(0, 0);
            }
            return result;
        }
     
        public void Build()
        {
            maze = new Byte[height, width];
            Clear();        
            Point cur = new Point(1,1);
            Point next = null;

            while (true)
            {
                next = null;
                if (GetRundomNextPoint(cur, out next))
                {
                    maze[cur.x, cur.y] = 2;
                    stack.Add(next);

                    if (next.x == height - 2 && next.y == width - 2)
                    {
                        exit.AddRange(stack);
                    }
                    if (cur.x == next.x) 
                    {
                        if (next.y > cur.y)
                            maze[cur.x, cur.y + 1] = 2;
                        else
                        if (next.y < cur.y)
                            maze[cur.x, cur.y - 1] = 2;
                    } else                        
                    if (cur.y == next.y)
                    {
                    if (next.x > cur.x)
                        maze[cur.x + 1, cur.y] = 2;
                    else
                    if (next.x < cur.x)
                        maze[cur.x - 1, cur.y] = 2;
                                };
                    cur = next;
                } else {
                    maze[cur.x, cur.y] = 2;
                    if (stack.Count > 0) {
                        stack.RemoveAt(stack.Count - 1);
                        if (stack.Count > 0)
                            cur = stack[stack.Count - 1];
                    }
                    else
                        break;
                }
            }
                    
        }


        public void Clear()
        {
            for (int x = 0; x < height; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    if ((x % 2 != 0) && (y % 2 != 0))
                        maze[x, y] = 0;
                    else maze[x, y] = 1;
                }
            }
        }

        public void Game()
        {
            Console.Clear();
            ConsoleKeyInfo keypress;
            Point cur = new Point(1, 1);
            maze[cur.x, cur.y] = 4;
            Print();


            while (true)
            {
                try
                {
                    Console.SetCursorPosition(width, cur.x);
                    Console.CursorVisible = false;
                }
                catch (ArgumentOutOfRangeException e)
                {
                    Console.Clear();
                    Console.WriteLine("Введите ширину экрана меньше текущей");
                    SizeField();
                    Console.Clear();
                    Build();
                    Game();
                }
                try
                {
                    Console.SetCursorPosition(width, cur.x - 10);
                    Console.SetCursorPosition(width, cur.x + 10);
                }
                catch (ArgumentOutOfRangeException e)
                { }
                    if (maze[cur.x, cur.y] == maze[height - 2, width - 2])
                {
                    Console.WriteLine("You win!");
                   // Console.ReadKey();
                    Thread.Sleep(2000);
                    Console.Clear();
                    break;
                }
                keypress = Console.ReadKey(true);

                if (keypress.Key == ConsoleKey.Escape)
                {
                    break;
                }
                if (keypress.Key == ConsoleKey.D || keypress.Key == ConsoleKey.RightArrow)
                    
                {
                    if (maze[cur.x, cur.y + 1] != 1)
                    {
                        WriteAt(" ", cur.x, cur.y);
                        maze[cur.x, cur.y] = 2;
                        cur.y = cur.y + 1;
                        WriteAt("■", cur.x, cur.y);
                        maze[cur.x, cur.y] = 4;
                        //Console.Clear();
                        //Print();
                    }
                }
                if (keypress.Key == ConsoleKey.A || keypress.Key == ConsoleKey.LeftArrow)
                {
                    if (maze[cur.x, cur.y - 1] != 1)
                    {
                        WriteAt(" ", cur.x, cur.y);
                        maze[cur.x, cur.y] = 2;
                        cur.y = cur.y - 1;
                        WriteAt("■", cur.x, cur.y);
                        maze[cur.x, cur.y] = 4;
                        //Console.Clear();
                        //Print();
                    }
                }
                if (keypress.Key == ConsoleKey.W || keypress.Key == ConsoleKey.UpArrow)
                {
                    if (maze[cur.x-1, cur.y] != 1)
                    {
                        WriteAt(" ", cur.x, cur.y);
                        maze[cur.x, cur.y] = 2;
                        cur.x = cur.x - 1;
                        WriteAt("■", cur.x, cur.y);
                        maze[cur.x, cur.y] = 4;
                       // Console.Clear();
                       // Print();
                    }
                }
                if (keypress.Key == ConsoleKey.S || keypress.Key == ConsoleKey.DownArrow)
                {
                    if (maze[cur.x + 1, cur.y] != 1)
                    {
                        WriteAt(" ", cur.x, cur.y);
                        maze[cur.x, cur.y] = 2;
                        cur.x = cur.x + 1;
                        WriteAt("■", cur.x, cur.y);
                        maze[cur.x, cur.y] = 4;
                        //Console.Clear();
                       // Print();
                    }
                }

                if (keypress.Key == ConsoleKey.C)
                {
                    Cheat();
                    Console.Clear();
                    Print();
                }

            }
        }



        public void Print()
        {
            // for (int x = 0; x < width; x++)
            // {
            //     for (int y = 0; y < width; y++)
            //     {
            //         Console.Write((maze[x, y]==1)? "■" : "X");
            //     }
            //     Console.WriteLine();
            // }


            for (int x = 0; x < height; x++) 
            {
                for (int y = 0; y < width; y++)
                {
                    if (x == 0 && y == 0)
                    {
                        Console.Write("╔");
                        continue;
                    }
                    else
                    {
                        if (x == 0 && y == width - 1)
                        {
                            Console.Write("╗");
                            continue;
                        }
                        else
                        {
                            if (x == height - 1 && y == 0)
                            {
                                Console.Write("╚");
                                continue;
                            }
                            else
                            {
                                if (x == height - 1 && y == width - 1)
                                {
                                    Console.Write("╝");
                                    continue;
                                }
                                else
                                {
                                    if (x == height - 2 && y == width - 2)
                                    {
                                        Console.Write("X");
                                        continue;
                                    }
                                    else
                                    {

                                        if (x == 0 || x == height - 1)
                                        {
                                            Console.Write("═");
                                            continue;
                                        }
                                        else
                                        {
                                            if (y == 0 || y == width - 1)
                                            {
                                                Console.Write("║");
                                                continue;
                                            }
                                            else
                                            {
                                                if (maze[x, y] == 1 && maze[x + 1, y] == 1 && maze[x - 1, y] == 1 && maze[x, y + 1] == 1 && maze[x, y - 1] == 1)
                                                {
                                                    Console.Write("╬");
                                                    continue;
                                                }
                                                else
                                                {
                                                    if (maze[x, y] == 1 && maze[x + 1, y] == 1 && maze[x - 1, y] == 1 && maze[x, y + 1] == 1)
                                                    {
                                                        Console.Write("╠");
                                                        continue;
                                                    }
                                                    else
                                                    {
                                                        if (maze[x, y] == 1 && maze[x + 1, y] == 1 && maze[x - 1, y] == 1 && maze[x, y - 1] == 1)
                                                        {
                                                            Console.Write("╣");
                                                            continue;
                                                        }
                                                        else
                                                        {
                                                            if (maze[x, y] == 1 && maze[x - 1, y] == 1 && maze[x, y + 1] == 1 && maze[x, y - 1] == 1)
                                                            {
                                                                Console.Write("╩");
                                                                continue;
                                                            }
                                                            else
                                                            {
                                                                if (maze[x, y] == 1 && maze[x + 1, y] == 1 && maze[x, y + 1] == 1 && maze[x, y - 1] == 1)
                                                                {
                                                                    Console.Write("╦");
                                                                    continue;
                                                                }
                                                                else
                                                                {
                                                                    if (maze[x, y] == 1 && maze[x + 1, y] == 1 && maze[x, y + 1] == 1)
                                                                    {
                                                                        Console.Write("╔");
                                                                        continue;
                                                                    }
                                                                    else
                                                                    {
                                                                        if (maze[x, y] == 1 && maze[x + 1, y] == 1 && maze[x, y - 1] == 1)
                                                                        {
                                                                            Console.Write("╗");
                                                                            continue;
                                                                        }
                                                                        else
                                                                        {
                                                                            if (maze[x, y] == 1 && maze[x - 1, y] == 1 && maze[x, y - 1] == 1)
                                                                            {
                                                                                Console.Write("╝");
                                                                                continue;
                                                                            }
                                                                            else
                                                                            {
                                                                                if (maze[x, y] == 1 && maze[x - 1, y] == 1 && maze[x, y + 1] == 1)
                                                                                {
                                                                                    Console.Write("╚");
                                                                                    continue;
                                                                                }
                                                                                else
                                                                                {
                                                                                    if (maze[x, y] == 1 && maze[x - 1, y] == 1 || maze[x, y] == 1 && maze[x + 1, y] == 1)
                                                                                    {
                                                                                        Console.Write("║");
                                                                                        continue;
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        if (maze[x, y] == 1 && maze[x, y - 1] == 1 || maze[x, y] == 1 && maze[x, y + 1] == 1)
                                                                                        {
                                                                                            Console.Write("═");
                                                                                            continue;
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            if (maze[x, y] == 4)
                                                                                            {
                                                                                                Console.Write("■");
                                                                                                continue;
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                if (maze[x, y] == 5)
                                                                                                {
                                                                                                    Console.Write(".");
                                                                                                    continue;
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    Console.Write(" ");
                                                                                                }
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                        }

                                                                    }

                                                                }

                                                            }

                                                        }

                                                    }

                                                }
                                            }
                                        }

                                    }
                                }
                               
                            }
                        }
                    }
                
                }
                Console.WriteLine();
            }



            //  Console.ReadKey();
        }

        public void WriteAt(string s, int x, int y)
        {
            Console.SetCursorPosition(y, x+1);
            Console.Write("\b");
            Console.SetCursorPosition(y, x);
            Console.Write(s);
        }


        public void Cheat()
        {
            for (int i = 0; i < exit.Count; i++)
            {
                if (maze[exit[i].x, exit[i].y] !=4)
                maze[exit[i].x, exit[i].y] = 5;
            }
        }




    }
}

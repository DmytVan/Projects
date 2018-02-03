using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace Lab2
{
    class Program
    {
        static Tetris tetris;

        static public int speed = 300;
        static public Thread myThread;

        static void Main(string[] args)
        {

            Console.WindowHeight = 40;
            Console.WindowWidth = 40;


            ConsoleKeyInfo keypress;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Старт\n2. Выход");
                keypress = Console.ReadKey(true);
                if (keypress.Key == ConsoleKey.D1 || keypress.Key == ConsoleKey.NumPad1)
                {
                    Levels();
                }
                if (keypress.Key == ConsoleKey.D2 || keypress.Key == ConsoleKey.NumPad2 || keypress.Key == ConsoleKey.Escape)
                    break;
            }

            void Levels()
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("1. Лёгкий уровень\n2. Средний уровень\n3. Сложный уровень\n4. Меню");                   
                    keypress = Console.ReadKey(true);
                    if (keypress.Key == ConsoleKey.D1 || keypress.Key == ConsoleKey.NumPad1)
                    {
                        speed = 300;
                        tetris = new Tetris(22, 12);
                        Start();
                    }
                    if (keypress.Key == ConsoleKey.D2 || keypress.Key == ConsoleKey.NumPad2)
                    {
                        speed = 250;
                        tetris = new Tetris(27, 22);
                        Start();
                    }
                    if (keypress.Key == ConsoleKey.D3 || keypress.Key == ConsoleKey.NumPad3)
                    {
                        speed = 200;
                        tetris = new Tetris(32, 32);
                        Start();
                    }
                    if (keypress.Key == ConsoleKey.D4 || keypress.Key == ConsoleKey.NumPad4 || keypress.Key == ConsoleKey.Escape)
                    {
                        break;
                    }
                }
            }

            void Start()
            {
                myThread = new Thread(FigureDown);
                tetris.Initialization();
                tetris.RandomFigure();
                tetris.Print();
                Console.CursorVisible = false;
                myThread.Start();
                while (true)
                {
                    if (myThread.IsAlive)
                        tetris.Control();
                    else
                    {
                        break;
                    }
                }
                Console.ResetColor();
                Console.Clear();
                Console.WriteLine("You Loose\nScore:" + tetris.Score);
                Thread.Sleep(1000);
            }

            


        }
        static void FigureDown()
        {
            while (true)
            {
                tetris.DownMove();
                Thread.Sleep(speed);
            }
        }

        public static void ThreadStop()
        {
            myThread.Abort();

        }
    }




    class Point
    {
        public int x;
        public int y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    class Tetris
    {
        const int figureElement = 4;
        static int height, width ;
        int centerX = 0, centerY = 0, temp = 0, randJ, score = 0;
        byte[,] field;
        Point[] curFigure = new Point[figureElement] { new Point(1, 6), new Point(0, 6), new Point(-1, 6), new Point(-2, 6) };
        Point[] tempFigure = new Point[figureElement] { new Point(0, 0), new Point(0, 0), new Point(0, 0), new Point(0, 0) };
        Random rand = new Random();
        static object locker = new object();
        Point[,] figure;

        public Tetris() { }


        public Tetris(int heightField, int widthField)
        {
            height = heightField;
            width = widthField;
        }

        ConsoleKeyInfo keypress;

        public int Score
        {
            get { return score; } 
        }

        public void Initialization()
        {
            field = new byte[height, width];
            figure = new Point[7, figureElement]
           {
         { new Point(0, width/2), new Point(1, width/2), new Point(-1, width/2), new Point(-2, width/2)}, // | [i, 0] - bot, [i, 1] - left, [i, 2] - right, [i, 3] - top
         { new Point(0, width/2), new Point(1, width/2), new Point(1, width/2-1), new Point(1, width/2+1)}, // ┴
         { new Point(0, width/2), new Point(-1, width/2), new Point(1, width/2), new Point(1, width/2-1)}, // ┘
         { new Point(0, width/2), new Point(-1, width/2), new Point(1, width/2), new Point(1, width/2+1)}, // └
         { new Point(0, width/2), new Point(1, width/2), new Point(0, width/2-1), new Point(1, width/2+1)}, // z
         { new Point(0, width/2), new Point(1, width/2), new Point(1, width/2-1), new Point(0, width/2+1)}, // z
         { new Point(0, width/2), new Point(1, width/2), new Point(0, width/2-1), new Point(1, width/2-1)}  // █
           };
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                    if (i == 0 || i == height - 1 || j == 0 || j == width - 1)
                        field[i, j] = 2;
                    else
                        field[i, j] = 0;
        }

        void SetFigure()
        {
            for (int i = 0; i < 4; i++)
                if (curFigure[i].x > 0)
                    field[curFigure[i].x, curFigure[i].y] = 1;
        }
        void ChangeFigure()
        {
            for (int i = 0; i < 4; i++)
            {
                if (curFigure[i].x > 0)
                {
                    WriteAt(" ", curFigure[i].x, curFigure[i].y);
                    field[curFigure[i].x, curFigure[i].y] = 0;

                }
            }
        }


        public void Print()
        {
            Console.SetCursorPosition(0, 0);
            Console.Clear();
            // SetFigure();
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (field[i, j] == 2 || field[i, j] == 1 || field[i, j] == 3)
                    {
                        if(field[i, j] == 2)
                        Console.ForegroundColor = ConsoleColor.White;
                        if (field[i, j] == 3)
                            Console.ForegroundColor = ConsoleColor.Red;
                        if (field[i, j] == 3)
                            Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("█");
                    }
                    else
                        Console.Write(" ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("Score: " + score);
        }

        public void RandomFigure()
        {
            randJ = rand.Next(7);
            for (int i = 0; i < 4; i++)
            {
                curFigure[i].x = figure[randJ, i].x;
                curFigure[i].y = figure[randJ, i].y;
                if (curFigure[i].x > 0)
                    WriteAt("█", curFigure[i].x, curFigure[i].y);
            }
        }

        public void DownMove()
        {
            lock (locker)
            {
                if (CanMove(1, 0))
                {
                    ChangeFigure();
                    for (int i = 0; i < 4; i++)
                    {

                        curFigure[i].x = curFigure[i].x + 1;
                        if (curFigure[i].x > 0)
                            WriteAt("█", curFigure[i].x, curFigure[i].y);
                    }

                }
                else
                {
                    for (int i = 0; i < 4; i++)
                        if (curFigure[i].x < 1 || curFigure[i].y < 1)
                        {
                            Program.ThreadStop();
                        }
                    try
                    {
                        for (int i = 0; i < 4; i++)
                            field[curFigure[i].x, curFigure[i].y] = 3;
                    }
                    catch (Exception ) { }
                    Bingo();
                    RandomFigure();
                }
            }

            // Print();
        }

        bool CanMove(int x, int y)
        {
            for (int i = 0; i < 4; i++)
            {
                if (curFigure[i].x  > 0)
                    if (field[curFigure[i].x + x, curFigure[i].y + y] == 2 || field[curFigure[i].x + x, curFigure[i].y + y] == 3)
                        return false;
            }
            return true;
        }

        bool CanRotate()
        {
            for (int i = 0; i < 4; i++)
            {
                if (field[tempFigure[i].x, tempFigure[i].y] == 2 || field[tempFigure[i].x, tempFigure[i].y] == 3)
                    return false;
            }
            return true;
        }

        public void Control()
        {
 
            keypress = Console.ReadKey(true);
            if (Program.myThread.IsAlive)
            {
                if (keypress.Key == ConsoleKey.RightArrow)
                {
                    if (CanMove(0, 1))
                    {
                        ChangeFigure();
                        for (int i = 0; i < 4; i++)
                        {
                            curFigure[i].y = curFigure[i].y + 1;
                            if (curFigure[i].x > 0)
                                WriteAt("█", curFigure[i].x, curFigure[i].y);
                        }
                    }
                }
                if (keypress.Key == ConsoleKey.LeftArrow)
                {
                    if (CanMove(0, -1))
                    {
                        ChangeFigure();
                        for (int i = 0; i < 4; i++)
                        {
                            curFigure[i].y = curFigure[i].y - 1;
                            if (curFigure[i].x > 0)
                                WriteAt("█", curFigure[i].x, curFigure[i].y);
                        }
                    }
                }
                if (keypress.Key == ConsoleKey.UpArrow && randJ != 6)
                    Rotate();
                // Print();
                if (keypress.Key == ConsoleKey.DownArrow)
                {
                    DownMove();
                }
                if (keypress.Key == ConsoleKey.Escape)
                {
                    Program.ThreadStop();  
                }
            }
        }

        void Rotate()
        {
            centerX = curFigure[0].x;
            centerY = curFigure[0].y;
            for (int i = 0; i < figureElement; i++)
            {
                tempFigure[i].x = (curFigure[i].x - centerX);
                tempFigure[i].y = curFigure[i].y - centerY;
                temp = tempFigure[i].x;
                tempFigure[i].x = tempFigure[i].y + centerX;
                tempFigure[i].y = -temp + centerY;
            }
            if (CanRotate())
            {
                ChangeFigure();
                for (int i = 0; i < figureElement; i++)
                {
                    curFigure[i].x = tempFigure[i].x;
                    curFigure[i].y = tempFigure[i].y;
                    WriteAt("█", curFigure[i].x, curFigure[i].y);
                }
            }
        }

        void Bingo()
        {
            int count = 0;
            for (int i = 1; i < height - 1; i++)
            {
                for (int j = 1; j < width - 1; j++)
                {
                    count = count + field[i, j];
                }
                if (count == (width - 2) * 3)
                {
                    score++;
                    Console.Beep();
                    temp = score / 5;
                    if (score > 5 || Program.speed > 150)
                        Program.speed = 300 - 10 * temp;
                    for (int n = i; n > 1; n--)
                        for (int m = 1; m < width - 1; m++)
                        {
                            field[n, m] = field[n - 1, m];
                        }
                    for (int m = 1; m < width - 1; m++)
                        field[1, m] = 0;
                }
                count = 0;
            }
            Print();
        }

        void WriteAt(string s, int x, int y)
        {
            lock (locker)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(y, x + 1);
                Console.Write("\b");
                Console.SetCursorPosition(y, x);
                Console.Write(s);
            }
        }
    }
}









       //     {
       //         Thread t = Thread.CurrentThread;
       //         Console.WriteLine("Имя потока: {0}", t.Name);
       //         t.Name = "Метод Main";
       //         Console.WriteLine("Имя потока: {0}", t.Name);
       //         Console.WriteLine("Запущен ли поток: {0}", t.IsAlive);
       //         Console.WriteLine("Приоритет потока: {0}", t.Priority);
       //         Console.WriteLine("Статус потока: {0}", t.ThreadState);
       //         Console.WriteLine("Домен приложения: {0}", Thread.GetDomain().FriendlyName);
       //         Thread myThread = new Thread(func);
       //         myThread.Start();
       //         while(myThread.IsAlive)
       //         {
       //             Console.WriteLine("Поток 1 выводит " + 0.ToString());
       //             Thread.Sleep(0);
       //         }
       //         for (int i = 0; i < 9; i++)
       //         {
       //             Console.WriteLine(a[i]);
       //         }
       //         Console.ReadKey();
       //     }
       //
       //     static void func()
       //     {
       //         for (int i = 0; i < 9; i++)
       //         {
       //             a[i] = 1;
       //         }
       //     }
       // 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Snake
{
    class Program
    {
        public static ConsoleKeyInfo keyPressed;
        public static bool runGame = true;

        static void Main()
        {            
            string[] game =
            {
                "+++++++++++++++++++++++",
                "+                     +",
                "+                     +",
                "+                     +",
                "+                     +",
                "+                     +",
                "+                     +",
                "+                     +",
                "+                     +",
                "+                     +",
                "+                     +",
                "+                     +",
                "+++++++++++++++++++++++"
            };
            char[] lineToEdit = game[3].ToCharArray();
            int x = 11;
            int y = 6;
            string direction = "";
            Thread getKeyThread = new Thread(GetKey);
            getKeyThread.Start();
            while (runGame == true)
            {
                foreach (string line in game)
                {
                    Console.WriteLine(line);
                }

                Thread.Sleep(75);

                lineToEdit[x] = ' ';
                game[y] = new string(lineToEdit);

                if (keyPressed.Key == ConsoleKey.UpArrow || keyPressed.Key == ConsoleKey.W)
                {
                    direction = "Up";
                }
                else if (keyPressed.Key == ConsoleKey.DownArrow || keyPressed.Key == ConsoleKey.S)
                {
                    direction = "Down";
                }
                else if (keyPressed.Key == ConsoleKey.LeftArrow || keyPressed.Key == ConsoleKey.A)
                {
                    direction = "Left";
                }
                else if (keyPressed.Key == ConsoleKey.RightArrow || keyPressed.Key == ConsoleKey.D)
                {
                    direction = "Right";
                }

                switch (direction)
                {
                    case "Up":
                        y--;
                        break;
                    case "Down":
                        y++;
                        break;
                    case "Left":
                        x -= 2;
                        break;
                    case "Right":
                        x += 2;
                        break;
                }

                x = Limit(x, 1, game[0].Length - 2);
                y = Limit(y, 1, game.Length - 2);

                lineToEdit = game[y].ToCharArray();
                lineToEdit[x] = 's';
                game[y] = new string(lineToEdit);

                Console.Clear();
            }
        }

        static void GetKey()
        {
            while (runGame == true)
            {
                keyPressed = Console.ReadKey(true);
                if (keyPressed.Key == ConsoleKey.Escape)
                {
                    runGame = false;
                }
            }
        }

        static int Limit (int number, int min, int max)
        {
            if (number <= min)
            {
                number = min;
            }
            if (number >= max)
            {
                number = max;
            }
            return number;
        }

    }
}

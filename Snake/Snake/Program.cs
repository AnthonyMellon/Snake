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
        public static int time = 0;               

        static void Main()
        {
            string[] game =
            {
                "++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++",
                "+                                                                                    +",
                "+                                                                                    +",
                "+                                                                                    +",
                "+                                                                                    +",
                "+                                                                                    +",
                "+                                                                                    +",
                "+                                                                                    +",
                "+                                                                                    +",
                "+                                                                                    +",
                "+                                                                                    +",
                "+                                                                                    +",
                "+                                                                                    +",
                "+                                                                                    +",
                "+                                                                                    +",
                "+                                                                                    +",
                "+                                                                                    +",
                "+                                                                                    +",
                "+                                                                                    +",
                "+                                                                                    +",
                "+                                                                                    +",
                "+                                                                                    +",
                "+                                                                                    +",
                "+                                                                                    +",
                "+                                                                                    +",
                "+                                                                                    +",
                "++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++"
            };
            char[] snakeLine = game[3].ToCharArray();
            char[] fruitLine = game[3].ToCharArray();
            int x = 11, y = 6;
            int fruitX = 2, fruitY = 5;
            int speedBase = 100;
            int score = 0;
            string direction = "";            

            Random rand = new Random();

            Thread getKeyThread = new Thread(GetKey);
            getKeyThread.Start();
            Thread timer = new Thread(Timer);
            timer.Start();

            fruitLine = game[fruitY].ToCharArray();
            fruitLine[fruitX] = 'F';
            game[fruitY] = new string(fruitLine);

            while (runGame == true)
            {       
                //TRANSLATING THE KEY INPUT                
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

                //ACTING ON THE KEY INPUT
                switch (direction)
                {
                    case "Up":
                        y--;
                        break;
                    case "Down":
                        y++;
                        break;
                    case "Left":
                        x --;
                        break;
                    case "Right":
                        x ++;
                        break;
                }

                //MAKING SURE SNAKE ISN'T OUT OF BOUNDS
                x = Limit(x, 1, game[0].Length - 2);
                y = Limit(y, 1, game.Length - 2);

                //PUTTING AN 'S' IN SNAKES SPOT
                snakeLine = game[y].ToCharArray();
                snakeLine[x] = 'S';
                game[y] = new string(snakeLine);

                //ACTING WHEN SNAKE IS ON FRUIT
                if (x == fruitX && y == fruitY)
                {
                    score++;

                    fruitX = rand.Next(1, game[0].Length - 1);
                    fruitY = rand.Next(1, game.Length - 1);
                    
                    fruitLine = game[fruitY].ToCharArray();
                    fruitLine[fruitX] = 'F';
                    game[fruitY] = new string(fruitLine);
                }

                //WRITING THE GAME BOARD  
                Thread.Sleep(speedBase - score);
                Console.Clear();
                Console.WriteLine($"Score: {score}".PadRight(game[0].Length - 9) + $"Time: " + Convert.ToString(time).PadLeft(3, '0'));
                foreach (string line in game)
                {
                    Console.WriteLine(line);                    
                }

                //BLANKING THE SNAKES SPOT                
                snakeLine[x] = ' ';
                game[y] = new string(snakeLine);
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

        static void Timer()
        {
            while (runGame == true)
            {
                Thread.Sleep(1000);
                time++;
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

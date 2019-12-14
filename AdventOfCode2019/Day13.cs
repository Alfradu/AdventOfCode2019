using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2019
{
    class Day13
    {
        int score = 0;
        public void Puzzle()
        {
            string input = System.IO.File.ReadAllText(@"K:\Android projects\AdventOfCode2019\input13.txt");
            Comp c = new Comp(input);
            c.Puzzle(0);
            int counter = 0;
            for (int i = 0; i < c.outputArr.Length/3; i++)
            {
                if (c.outputArr[i*3+2] == 2)
                {
                    counter++;
                }
            }
            Console.WriteLine("number of tiles: " + counter);
            c.readData();
            c.changeValue(0, 2);
            c.clearOutPutArr();
            c.outputCode = 0;
            int joystick = 0;
            c.Puzzle(new int[] { }, 0);
            render(c.outputArr);
            c.clearOutPutArr();
            joystick = readJoystick();
            while (c.outputCode != 99)
            {
                c.Puzzle(new int[] { joystick }, 0);
                render(c.outputArr);
                c.clearOutPutArr();
                joystick = readJoystick();
            }
        }

        public int readJoystick()
        {
            if (ballX == paddleX)
            {
                return 0;
            }
            else if (ballX > paddleX)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }

        int[,] screen;
        bool doOnce = true;
        int ballX=0;
        int paddleX=0;
        public void render(long[] pixels)
        {
            List<long[]> p = new List<long[]>();
            for (int i = 0; i < pixels.Length / 3; i++) //TODO : update correctly
            {
                p.Add(new long[] { pixels[i*3], pixels[i*3 + 1], pixels[i*3 + 2] });
            }
            if (doOnce)
            {
                screen = new int[p.Max(v => v[0])+1, p.Max(v => v[1])+1];
                doOnce = false;
            }
            foreach(long[] pixel in p)
            {
                if (pixel[2] == 4)
                {
                    ballX = (int)pixel[0];
                }
                if (pixel[2] == 3)
                {
                    paddleX = (int)pixel[0];
                }
                if (pixel[0] == -1 && pixel[1] == 0)
                {
                    score = (int)pixel[2];
                }
                else
                {
                    screen[pixel[0], pixel[1]] = (int)pixel[2];
                }
            }
            Console.Clear();
            Console.WriteLine("Score: " + score);
            for (int j = 0; j < screen.GetLength(1); j++)
            {
                for (int i = 0; i < screen.GetLength(0); i++)
                {
                    switch(screen[i,j])
                    {
                        case 0:
                            Console.Write(" ");
                            break;
                        case 1:
                            Console.Write("H");
                            break;
                        case 2:
                            Console.Write("#");
                            break;
                        case 3:
                            Console.Write("-");
                            break;
                        case 4:
                            Console.Write("O");
                            break;
                    }
                }
                Console.WriteLine();
            }
        }
    }
}

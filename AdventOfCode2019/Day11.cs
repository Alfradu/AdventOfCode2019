using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2019
{
    class Day11
    {
        public void Puzzle()
        {
            string input = System.IO.File.ReadAllText(@"K:\Android projects\AdventOfCode2019\input11.txt");
            Comp c = new Comp(input);
            List<int[]> moves = new List<int[]>();
            int[] tile = { 0, 0, 1};
            int direction = 1;
            int pointer = 0;
            while (c.outputCode != 99)
            {
                c.Puzzle(new int[] { tile[2] }, 0);
                tile[2] = (int)c.outputArr[0 + pointer];
                if (!tileExists(tile, moves))
                {
                    moves.Add(new int[] { tile[0], tile[1], tile[2] });
                }
                direction = (int)c.outputArr[1 + pointer] == 0 ? direction-1 : direction+1;
                if (direction < 0)
                {
                    direction = 3;
                }
                else if (direction > 3)
                {
                    direction = 0;
                }
                pointer += 2;

                var newCoord = updateLocation(tile[0], tile[1], direction);
                tile[0] = newCoord[0];
                tile[1] = newCoord[1];
                var newTile = fetchTile(tile, moves);
                if (newTile[2] != 3)
                {
                    tile[2] = newTile[2];
                }
                else
                {
                    tile[2] = 0;
                }

            }
            Console.WriteLine(moves.Count);
            int[] xbounds = new int[2];
            int[] ybounds = new int[2];
            foreach (int[] move in moves)
            {
                xbounds[0] = xbounds[0] < move[0] ? xbounds[0] : move[0];
                xbounds[1] = xbounds[1] > move[0] ? xbounds[1] : move[0];
                ybounds[0] = ybounds[0] < move[1] ? ybounds[0] : move[1];
                ybounds[1] = ybounds[1] > move[1] ? ybounds[1] : move[1];
            }
            int[,] hull = new int[xbounds[1] - xbounds[0]+1, ybounds[1] - ybounds[0]+1];
            foreach ( int[] move in moves)
            {
                hull[move[0], move[1]] = move[2];
            }
            for (int i = 0; i < hull.GetLength(1); i++)
            {
                for (int j = 0; j < hull.GetLength(0); j++)
                {
                    switch (hull[j, i])
                    {
                        case 0:

                            Console.Write(".");
                            break;
                        case 1:
                            Console.Write("#");
                            break;
                    }
                }
                Console.Write("\n");
            }


        }

        public bool tileExists(int[] tile, List<int[]> list)
        {
            foreach (int[] t in list)
            {
                if (tile[0] == t[0] & tile[1] == t[1])
                {
                    t[2] = tile[2];
                    return true;
                }
            }
            return false;
        }

        public int[] fetchTile(int[] tile, List<int[]> list)
        {
            foreach (int[] t in list)
            {
                if (tile[0] == t[0] & tile[1] == t[1])
                {
                    return t;
                }
            }
            return new int[] { 0, 0, 3 };
        }

        public int[] updateLocation(int x, int y, int dir)
        {
            var x1 = x;
            var y1 = y;
            switch (dir)
            {
                case 0:
                    x1--;
                    break;
                case 1:
                    y1--;
                    break;
                case 2:
                    x1++;
                    break;
                case 3:
                    y1++;
                    break;
            }
            return new int[] { x1, y1 };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2019
{
    class Day3
    {
        string[] wire1;
        string[] wire2;
        int[] wire1Coord = { 0, 0 };
        int[] wire2Coord = { 0, 0 };
        int distance = int.MaxValue;
        int wire1steps = 0;
        int wire2steps = 0;
        public void Puzzle()
        {
            string[] input = System.IO.File.ReadAllText(@"K:\Android projects\AdventOfCode2019\input3.txt").Split('\n');
            //string[] input = "R75,D30,R83,U83,L12,D49,R71,U7,L72\nU62,R66,U55,R34,D71,R55,D58,R83".Split('\n');
            wire1 = input[0].Split(',');
            wire2 = input[1].Split(',');
            foreach (string s in wire1)
            {
                switch (s[0])
                {
                    case 'U':
                        for (int i = 0; i < int.Parse(s.Substring(1)); i++)
                        {
                            wire1Coord[1] -= 1;
                            if (checkCoordOverlap(wire2))
                            {
                                getSteps(wire1Coord);
                                distance = Math.Abs(wire1Coord[0]) + Math.Abs(wire1Coord[1]) < distance ? Math.Abs(wire1Coord[0]) + Math.Abs(wire1Coord[1]) : distance;
                            }
                        }
                        break;
                    case 'D':
                        for (int i = 0; i < int.Parse(s.Substring(1)); i++)
                        {
                            wire1Coord[1] += 1;
                            if (checkCoordOverlap(wire2))
                            {
                                getSteps(wire1Coord);
                                distance = Math.Abs(wire1Coord[0]) + Math.Abs(wire1Coord[1]) < distance ? Math.Abs(wire1Coord[0]) + Math.Abs(wire1Coord[1]) : distance;
                            }
                        }
                        break;
                    case 'L':
                        for (int i = 0; i < int.Parse(s.Substring(1)); i++)
                        {
                            wire1Coord[0] -= 1;
                            if (checkCoordOverlap(wire2))
                            {
                                getSteps(wire1Coord);
                                distance = Math.Abs(wire1Coord[0]) + Math.Abs(wire1Coord[1]) < distance ? Math.Abs(wire1Coord[0]) + Math.Abs(wire1Coord[1]) : distance;
                            }
                        }
                        break;
                    case 'R':
                        for (int i = 0; i < int.Parse(s.Substring(1)); i++)
                        {
                            wire1Coord[0] += 1;
                            if (checkCoordOverlap(wire2))
                            {
                                getSteps(wire1Coord);
                                distance = Math.Abs(wire1Coord[0]) + Math.Abs(wire1Coord[1]) < distance ? Math.Abs(wire1Coord[0]) + Math.Abs(wire1Coord[1]) : distance;
                            }
                        }
                        break;
                }
            }
            Console.WriteLine("closest intersection: " + distance);
        }

        public bool checkCoordOverlap( string[] wire)
        {
            wire2Coord[0] = 0;
            wire2Coord[1] = 0;
            foreach (string s in wire)
            {
                switch (s[0])
                {
                    case 'U':
                        for (int i = 0; i < int.Parse(s.Substring(1)); i++)
                        {
                            wire2Coord[1] -= 1;
                            if (wire2Coord[0] == wire1Coord[0] && wire2Coord[1] == wire1Coord[1])
                            {
                                return true;
                            }
                        }
                        break;
                    case 'D':
                        for (int i = 0; i < int.Parse(s.Substring(1)); i++)
                        {
                            wire2Coord[1] += 1;
                            if (wire2Coord[0] == wire1Coord[0] && wire2Coord[1] == wire1Coord[1])
                            {
                                return true;
                            }
                        }
                        break;
                    case 'L':
                        for (int i = 0; i < int.Parse(s.Substring(1)); i++)
                        {
                            wire2Coord[0] -= 1;
                            if (wire2Coord[0] == wire1Coord[0] && wire2Coord[1] == wire1Coord[1])
                            {
                                return true;
                            }
                        }
                        break;
                    case 'R':
                        for (int i = 0; i < int.Parse(s.Substring(1)); i++)
                        {
                            wire2Coord[0] += 1;
                            if (wire2Coord[0] == wire1Coord[0] && wire2Coord[1] == wire1Coord[1])
                            {
                                return true;
                            }
                        }
                        break;
                }
            }
            return false;
        }

        public void getSteps(int[] intersectPoint)
        {
            wire1steps = 0;
            wire2steps = 0;
            int x = 0, y = 0;
            foreach (string s in wire1)
            {
                switch (s[0])
                {
                    case 'U':
                        for (int i = 0; i < int.Parse(s.Substring(1)); i++)
                        {
                            if (x == intersectPoint[0] && y == intersectPoint[1])
                            {
                                break;
                            }
                            y -= 1;
                            wire1steps++;
                        }
                        break;
                    case 'D':
                        for (int i = 0; i < int.Parse(s.Substring(1)); i++)
                        {
                            if (x == intersectPoint[0] && y == intersectPoint[1])
                            {
                                break;
                            }
                            y += 1;
                            wire1steps++;
                        }
                        break;
                    case 'L':
                        for (int i = 0; i < int.Parse(s.Substring(1)); i++)
                        {
                            if (x == intersectPoint[0] && y== intersectPoint[1])
                            {
                                break;
                            }
                            x -= 1;
                            wire1steps++;
                        }
                        break;
                    case 'R':
                        for (int i = 0; i < int.Parse(s.Substring(1)); i++)
                        {
                            if (x == intersectPoint[0] && y == intersectPoint[1])
                            {
                                break;
                            }
                            x += 1;
                            wire1steps++;
                        }
                        break;
                }
            }
            x = 0;
            y = 0;
            foreach (string s in wire2)
            {
                switch (s[0])
                {
                    case 'U':
                        for (int i = 0; i < int.Parse(s.Substring(1)); i++)
                        {
                            if (x == intersectPoint[0] && y == intersectPoint[1])
                            {
                                break;
                            }
                            y -= 1;
                            wire2steps++;
                        }
                        break;
                    case 'D':
                        for (int i = 0; i < int.Parse(s.Substring(1)); i++)
                        {
                            if (x == intersectPoint[0] && y == intersectPoint[1])
                            {
                                break;
                            }
                            y += 1;
                            wire2steps++;
                        }
                        break;
                    case 'L':
                        for (int i = 0; i < int.Parse(s.Substring(1)); i++)
                        {
                            if (x == intersectPoint[0] && y == intersectPoint[1])
                            {
                                break;
                            }
                            x -= 1;
                            wire2steps++;
                        }
                        break;
                    case 'R':
                        for (int i = 0; i < int.Parse(s.Substring(1)); i++)
                        {
                            if (x == intersectPoint[0] && y == intersectPoint[1])
                            {
                                break;
                            }
                            x += 1;
                            wire2steps++;
                        }
                        break;
                }
            }
            Console.WriteLine("Intersection after steps: " + (wire1steps + wire2steps));
        }
    }
}

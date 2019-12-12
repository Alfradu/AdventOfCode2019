using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2019
{
    class Day12
    {
        public void Puzzle()
        {
            string[] input = System.IO.File.ReadAllText(@"K:\Android projects\AdventOfCode2019\input12.txt").Split('\n');
            //string[] input = "<x=-8, y=-10, z=0>\n< x = 5, y = 5, z = 10 >\n< x = 2, y = -7, z = 3 >\n< x = 9, y = -8, z = -3 > ".Split('\n');
            List<int[]> moons = new List<int[]>();
            readData(input, moons);
            int energy = 0;
            Console.Write("Steps: ");
            int steps = int.Parse(Console.ReadLine());
            Console.Write("Printsteps: ");
            int pstep = int.Parse(Console.ReadLine());
            for (int i = 0; i <= steps; i++)
            {
                if (i % pstep == 0)
                {
                    printMoons(i,moons);
                }
                if (i < steps)
                {
                    moons = calculateNextStep(moons);
                }
            }
            foreach (int[] moon in moons)
            {
                energy += (Math.Abs(moon[0]) + Math.Abs(moon[1]) + Math.Abs(moon[2])) * (Math.Abs(moon[3]) + Math.Abs(moon[4]) + Math.Abs(moon[5]));
            }
            Console.WriteLine("\nTotal energy in system after " + steps + " steps: " + energy);
            readData(input, moons);
            long counter = 0;
            long[] counterXYZ = { 0, 0, 0 };
            int[] axis = getAxis(0, moons);
            for (int i = 0; i < 3; i++)
            {
                axis = getAxis(i, moons);
                while (!axis.SequenceEqual(getAxis(i, calculateNextStep(moons))))
                {
                    moons = calculateNextStep(moons);
                    counter++;
                }
                counterXYZ[i] = counter+1;
                counter = 0;
                readData(input, moons);
            }
            Console.WriteLine("Steps to reach matching state: " + LCM(counterXYZ)*2);
        }

        public long GCD(long a, long b)
        {
            return b == 0 ? a : GCD(b, a % b);
        }

        public long lcm(long a, long b)
        {
            return Math.Abs(a * b) / GCD(a, b);
        }

        public long LCM(long[] counter)
        {
            return counter.Aggregate(lcm);
        }

        public int[] getAxis(int i, List<int[]> moons)
        {
            int c = 0;
            int[] a = new int[4];
            foreach (int[] moon in moons)
            {
                a[c] = moon[i];
                c++;
            }
            return a;
        }

        public void readData(string[] input, List<int[]> moons)
        {
            moons.Clear();
            foreach (string s in input)
            {
                var x = s.Split('=')[1].Split(',')[0];
                var y = s.Split('=')[2].Split(',')[0];
                var z = s.Split('=')[3].Split('>')[0];
                moons.Add(new int[] { int.Parse(x), int.Parse(y), int.Parse(z), 0, 0, 0 });
            }
        }
        public void printMoons(int i, List<int[]> moons)
        {
            Console.WriteLine("\nAfter " + i + " steps:");
            foreach (int[] moon in moons)
            {
                Console.WriteLine(String.Format("pos=<x={0,3}, y={1,3}, z={2,3}>, vel=< x ={3,3}, y={4,3}, z={5,3}>", moon[0], moon[1], moon[2], moon[3], moon[4], moon[5]));
            }
        }

        public int changeVel(int a, int b)
        {
            if (a == b)
            {
                return 0;
            }
            else if (a < b)
            {
                return -1;
            }
            else if (a > b)
            {
                return 1;
            }
            return 0;
        }

        public List<int[]> calculateNextStep(List<int[]> moons)
        {
            foreach (int[] moon in moons)
            {
                foreach (int[] m in moons)
                {
                    if (!moon.Equals(m))
                    {
                        moon[3] += changeVel(m[0], moon[0]);
                        moon[4] += changeVel(m[1], moon[1]);
                        moon[5] += changeVel(m[2], moon[2]);
                    }
                }
            }
            foreach (int[] moon in moons)
            {
                moon[0] += moon[3];
                moon[1] += moon[4];
                moon[2] += moon[5];
            }
            return moons;
        }

        public List<int[]> calculatePreStep(List<int[]> moons)
        {
            foreach (int[] moon in moons)
            {
                moon[0] -= moon[3];
                moon[1] -= moon[4];
                moon[2] -= moon[5];
            }
            foreach (int[] moon in moons)
            {
                foreach (int[] m in moons)
                {
                    if (!moon.Equals(m))
                    {
                        moon[3] -= changeVel(m[0], moon[0]);
                        moon[4] -= changeVel(m[1], moon[1]);
                        moon[5] -= changeVel(m[2], moon[2]);
                    }
                }
            }
            return moons;
        }
    }
}

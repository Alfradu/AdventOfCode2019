using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2019
{
    class Day1
    {
        public void Puzzle()
        {
            string[] modules = System.IO.File.ReadAllText(@"K:\Android projects\AdventOfCode2019\input.txt").Split('\n');
            int fuel = 0;
            int totalFuel = 0;
            foreach (string module in modules)
            {
                float f = float.Parse(module);
                int value = ReturnFuel(f);
                fuel += value;
                while (value > 0)
                {
                    totalFuel += value;
                    value = ReturnFuel(value);
                }
            }
            Console.WriteLine("fuel: " + fuel);
            Console.WriteLine("totalFuel: " + totalFuel);
        }

        public static int ReturnFuel(float mass)
        {
            return (int)(Math.Floor(mass / 3f) - 2f);
        }
    }
}

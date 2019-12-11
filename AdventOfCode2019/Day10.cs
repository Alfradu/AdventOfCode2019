using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2019
{
    class Day10
    {
        string[,] asteroidField;
        int[,] asteroidVisibility;
        public void Puzzle()
        {
            string[] input = System.IO.File.ReadAllText(@"K:\Android projects\AdventOfCode2019\input10.txt").Split('\n');
            List<double[]> angles = new List<double[]>();
            asteroidField = new string[input[0].Length, input.Length];
            asteroidVisibility = new int[input[0].Length, input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    asteroidField[i, j] = input[i][j].ToString();
                }
            }
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    if (asteroidField[i, j].Equals("#"))
                    {
                        scan(i, j);
                    }
                }
            }
            int counter = 0;
            int[] startCoord = new int[2];
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    if (counter < asteroidVisibility[ i, j])
                    {
                        startCoord = new int[]{ i, j};
                        counter = asteroidVisibility[ i, j];
                    }
                }
            }
            Console.WriteLine(counter);
            angles = scan(startCoord[0], startCoord[1]).OrderBy(o => o[1]).ToList();
            double startAngle = -90;
            var c = countAsteroids(asteroidField, startCoord);
            while (c > 0)
            {
                var asteroid = angles.FindAll(a => a[1] == startAngle).OrderBy(a => Math.Abs(a[0] - (startCoord[0] * 100 + startCoord[1]))).First();
                if (startAngle == 180)
                {
                    startAngle = -startAngle;
                }
                try
                {
                    startAngle = angles.FindAll(a => a[1] > startAngle).OrderBy(a => a[1]).First()[1];
                }
                catch (InvalidOperationException)
                {
                    startAngle = angles.FindAll(a => a[1] > -180).OrderBy(a => a[1]).First()[1];
                }
                angles.Remove(asteroid);
                zap(asteroid);
                c = countAsteroids(asteroidField, startCoord);
            }
            Console.WriteLine("Finished zapping.");
        }

        int aCount = 0;
        public void zap(double[] asteroid)
        {
            aCount++;
            var x = (int)Math.Floor(asteroid[0] / 100);
            var y = (int)asteroid[0] % 100;
            asteroidField[x, y] = aCount == 200 ? "c" : ".";
            Console.WriteLine("destroyed asteroid #" + aCount + " at location " + (y*100+x));
            
        }

        public int countAsteroids(string[,] field, int[] coords)
        {
            int count = 0;
            for (int i = 0; i < field.GetLength(1); i++)
            {
                for (int j = 0; j < field.GetLength(0); j++)
                {
                    if (field[i, j].Equals("#") && i != coords[0] | j != coords[1])
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        public List<double[]> scan(int x, int y)
        {
            List<double[]> lineOfSight = new List<double[]>();
            for (int i = 0; i < asteroidField.GetLength(1); i++)
            {
                for (int j = 0; j < asteroidField.GetLength(0); j++)
                {
                    if (asteroidField[i, j].Equals("#") && j != y | i != x)
                    {
                        var angle = Math.Atan2(i-x, j-y);
                        lineOfSight.Add(new double[] {i*100+j, angle * (180 / Math.PI) });
                    }
                }
            }
            asteroidVisibility[x, y] = lineOfSight.Select(a => a[1]).Distinct().Count();
            return lineOfSight;
        }
    }
}

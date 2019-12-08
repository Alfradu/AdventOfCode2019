using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2019
{
    class Day7
    {
        List<int[]> permutations;
        public void Puzzle()
        {
            permutations = new List<int[]>();
            int[] ampInput = { 0, 1, 2, 3, 4 };
            permutate(ampInput, ampInput.Length);
            string input = System.IO.File.ReadAllText(@"K:\Android projects\AdventOfCode2019\input7.txt");
            string input2 = "3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5";
            string input3 = "3,52,1001,52,-5,52,3,53,1,52,56,54,1007,54,5,55,1005,55,26,1001,54,-5,54,1105,1,12,1,53,54,53,1008,54,0,55,1001,55,1,55,2,53,55,53,4,53,1001,56,-1,56,1005,56,6,99,0,0,0,0,10";
            Day5[] computers = new Day5[] { new Day5(input), new Day5(input), new Day5(input), new Day5(input), new Day5(input) };
            int counter = 0;
            string perm = "";
            int output = 0;
            foreach (int[] p in permutations)
            {
                int i = 0;
                output = 0;
                foreach (Day5 comp in computers)
                {
                    comp.Puzzle(new int[] { p[i], output }, 0);
                    output = comp.ou[comp.ou.Length - 1];
                    i++;
                }
                perm = output > counter ? p[0].ToString() + p[1].ToString() + p[2].ToString() + p[3].ToString() + p[4].ToString() : perm;
                counter = output > counter ? output : counter;
            }
            Console.WriteLine("Biggest power thrust: " + counter + "\nFrom permutation    : " + perm);
            computers = new Day5[] { new Day5(input), new Day5(input), new Day5(input), new Day5(input), new Day5(input) };
            int[] ampInput2 = { 5, 6, 7, 8, 9 };
            permutations = new List<int[]>();
            permutate(ampInput2, ampInput2.Length);

            counter = 0;
            foreach (int[] p in permutations)
            {
                output = 0;
                bool firstRound = true;
                bool done = false;
                while (!done)
                {
                    if(firstRound)
                    {
                        computers[0].Puzzle(new int[] { p[0], output },0); //A
                        output = computers[0].ou[computers[0].ou.Length - 1];
                        computers[1].Puzzle(new int[] { p[1], output }, 1); //B
                        output = computers[1].ou[computers[1].ou.Length - 1];
                        computers[2].Puzzle(new int[] { p[2], output }, 2); //C
                        output = computers[2].ou[computers[2].ou.Length - 1];
                        computers[3].Puzzle(new int[] { p[3], output }, 3); //D
                        output = computers[3].ou[computers[3].ou.Length - 1];
                        computers[4].Puzzle(new int[] { p[4], output }, 4); //E
                        output = computers[4].ou[computers[4].ou.Length - 1];
                        firstRound = false;
                    }
                    else
                    {
                        computers[0].Puzzle(new int[] { output }, 0); //A
                        output = computers[0].ou[computers[0].ou.Length - 1];
                        computers[1].Puzzle(new int[] { output },1); //B
                        output = computers[1].ou[computers[1].ou.Length - 1];
                        computers[2].Puzzle(new int[] { output },2); //C
                        output = computers[2].ou[computers[2].ou.Length - 1];
                        computers[3].Puzzle(new int[] { output },3); //D
                        output = computers[3].ou[computers[3].ou.Length - 1];
                        computers[4].Puzzle(new int[] { output },4); //E
                        output = computers[4].ou[computers[4].ou.Length - 1];
                    }
                    if (computersDone(computers))
                    {
                        done = true;
                        output = computers[4].ou[computers[4].ou.Length - 1];
                        resetComputers(computers);

                    }
                }

                perm = output > counter ? p[0].ToString() + p[1].ToString() + p[2].ToString() + p[3].ToString() + p[4].ToString() : perm;
                counter = output > counter ? output : counter;
            }
            Console.WriteLine("Biggerest power thrust: " + counter + "\nFrom permutation      : " + perm);
        }

        public void permutate(int[] a, int n)
        {
            int[] b = new int[a.Length];
            for (int i = 0; i < a.Length; i++)
            {
                b[i] = a[i];
            }
            if (n == 1)
            {
                permutations.Add(b);
                return;
            }
            for (int i = 0; i < n; i++)
            {
                b[i] = a[n - 1];
                b[n - 1] = a[i];
                permutate(b, n - 1);
                b[i] = a[i];
                b[n - 1] = a[n - 1];
            }
        }

        public bool computersDone(Day5[] computers)
        {
            foreach (Day5 comp in computers)
            {
                if (comp.outputCode != 99)
                {
                    return false;
                }
            }
            return true;
        }

        public void resetComputers(Day5[] computers)
        {
            foreach(Day5 comp in computers)
            {
                comp.readData();
                comp.pointer = 0;
            }
        }
    }
}

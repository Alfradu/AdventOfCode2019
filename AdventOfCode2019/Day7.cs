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
            Comp[] computers = new Comp[] { new Comp(input), new Comp(input), new Comp(input), new Comp(input), new Comp(input) };
            long counter = 0;
            string perm = "";
            long output = 0;
            foreach (int[] p in permutations)
            {
                int i = 0;
                output = 0;
                foreach (Comp comp in computers)
                {
                    comp.Puzzle(new int[] { p[i], (int)output }, 0);
                    output = comp.outputArr[comp.outputArr.Length - 1];
                    i++;
                }
                perm = output > counter ? p[0].ToString() + p[1].ToString() + p[2].ToString() + p[3].ToString() + p[4].ToString() : perm;
                counter = output > counter ? output : counter;
            }
            Console.WriteLine("Biggest power thrust: " + counter + "\nFrom permutation    : " + perm);
            computers = new Comp[] { new Comp(input), new Comp(input), new Comp(input), new Comp(input), new Comp(input) };
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
                        computers[0].Puzzle(new int[] { p[0], (int)output },0); //A
                        output = computers[0].outputArr[computers[0].outputArr.Length - 1];
                        computers[1].Puzzle(new int[] { p[1], (int)output }, 1); //B
                        output = computers[1].outputArr[computers[1].outputArr.Length - 1];
                        computers[2].Puzzle(new int[] { p[2], (int)output }, 2); //C
                        output = computers[2].outputArr[computers[2].outputArr.Length - 1];
                        computers[3].Puzzle(new int[] { p[3], (int)output }, 3); //D
                        output = computers[3].outputArr[computers[3].outputArr.Length - 1];
                        computers[4].Puzzle(new int[] { p[4], (int)output }, 4); //E
                        output = computers[4].outputArr[computers[4].outputArr.Length - 1];
                        firstRound = false;
                    }
                    else
                    {
                        computers[0].Puzzle(new int[] { (int)output }, 0); //A
                        output = computers[0].outputArr[computers[0].outputArr.Length - 1];
                        computers[1].Puzzle(new int[] { (int)output },1); //B
                        output = computers[1].outputArr[computers[1].outputArr.Length - 1];
                        computers[2].Puzzle(new int[] { (int)output },2); //C
                        output = computers[2].outputArr[computers[2].outputArr.Length - 1];
                        computers[3].Puzzle(new int[] { (int)output },3); //D
                        output = computers[3].outputArr[computers[3].outputArr.Length - 1];
                        computers[4].Puzzle(new int[] { (int)output },4); //E
                        output = computers[4].outputArr[computers[4].outputArr.Length - 1];
                    }
                    if (computersDone(computers))
                    {
                        done = true;
                        output = computers[4].outputArr[computers[4].outputArr.Length - 1];
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

        public bool computersDone(Comp[] computers)
        {
            foreach (Comp comp in computers)
            {
                if (comp.outputCode != 99)
                {
                    return false;
                }
            }
            return true;
        }

        public void resetComputers(Comp[] computers)
        {
            foreach(Comp comp in computers)
            {
                comp.readData();
                comp.pointer = 0;
            }
        }
    }
}

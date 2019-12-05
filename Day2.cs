using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2019
{
    class Day2
    {
        string[] input;
        int[] IntCode;
        bool halted = false;
        int pointer;
        public void Puzzle()
        {
            input = System.IO.File.ReadAllText(@"K:\Android projects\AdventOfCode2019\input2.txt").Split(',');
            int outputCode = 19690720;
            readData();
            changeValue(1, 12);
            changeValue(2, 2);
            run();
            Console.WriteLine("Position 0 code: " + IntCode[0]);
            readData();
            int code = findCode(outputCode);
            Console.WriteLine("program code resulting in position 0 value " + outputCode + " is: " + code);
        }

        public void readData()
        {
            IntCode = new int[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                IntCode[i] = Int32.Parse(input[i]);
            }
        }

        public void run()
        {
            pointer = 0;
            halted = false;
            while (!halted)
            {
                readCode(pointer);
            }
        }

        public int findCode(int outputCode)
        {
            for (int noun = 0; noun < 100; noun++)
            {
                for (int verb = 0; verb < 100; verb++)
                {
                    changeValue(1, noun);
                    changeValue(2, verb);
                    run();
                    if (outputCode == IntCode[0])
                    {
                        return 100 * noun + verb;
                    }
                    readData();
                }
            }
            return -1;
        }

        public void readCode(int position)
        {
            switch (IntCode[position])
            {
                case 1:
                    add(IntCode[position + 1], IntCode[position + 2], IntCode[position + 3]);
                    pointer += 4;
                    break;
                case 2:
                    multiply(IntCode[position + 1], IntCode[position + 2], IntCode[position + 3]);
                    pointer += 4;
                    break;
                case 99:
                    halt();
                    break;
            }
        }

        public void changeValue(int pos, int val)
        {
            IntCode[pos] = val;
        }

        public void add(int posA, int posB, int pos)
        {
            IntCode[pos] = IntCode[posA] + IntCode[posB];
        }

        public void multiply(int posA, int posB, int pos)
        {
            IntCode[pos] = IntCode[posA] * IntCode[posB];
        }
        
        public void halt()
        {
            halted = true;
        }
    }
}

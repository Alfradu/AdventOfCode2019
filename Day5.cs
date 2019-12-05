using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2019
{
    class Day5
    {
        string[] input;
        int[] IntCode;
        bool halted = false;
        int pointer;
        public void Puzzle()
        {
            input = System.IO.File.ReadAllText(@"K:\Android projects\AdventOfCode2019\input5.txt").Split(',');
            readData();
            run();
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
                readCode();
            }
        }

        public int[] createCode(int input)
        {
            char[] _ch = input.ToString().ToCharArray();
            int[] newCode = new int[5];
            for (int i = 4; i >= 0; i--)
            {
                if (_ch.Length - 1 - (4 - i) < 0)
                {
                    break;
                }
                newCode[i] = (int)char.GetNumericValue(_ch[_ch.Length - 1 - (4 - i)]);
            }
            return newCode;
        }

        public int getOpCode(int[] arr)
        {
            return arr[arr.Length - 2] * 10 + arr[arr.Length - 1];
        }

        public void readCode()
        {
            int[] opcode = createCode(IntCode[pointer]);
            switch (getOpCode(opcode))
            {
                case 1:
                    add(opcode);
                    pointer += 4;
                    break;
                case 2:
                    multiply(opcode);
                    pointer += 4;
                    break;
                case 3:
                    Console.Write("Opcode 3 input: ");
                    add(int.Parse(Console.ReadLine()), IntCode[pointer + 1]);
                    pointer += 2;
                    break;
                case 4:
                    Console.Write("Opcode 4 output: ");
                    output(opcode);
                    pointer += 2;
                    break;
                case 5:
                    jumpIfTrue(opcode);
                    pointer += 3;
                    break;
                case 6:
                    jumpIfFalse(opcode);
                    pointer += 3;
                    break;
                case 7:
                    lessThan(opcode);
                    pointer += 4;
                    break;
                case 8:
                    equals(opcode);
                    pointer += 4;
                    break;
                case 99:
                    Console.Write("Opcode 99 Halting program.");
                    halt();
                    break;
            }
        }

        public void changeValue(int pos, int val)
        {
            IntCode[pos] = val;
        }

        public int getValue(int pos)
        {
            return IntCode[pos];
        }


        public void output(int[] code)
        {
            int outp = code[2] == 0 ? IntCode[IntCode[pointer + 1]] : IntCode[pointer + 1];
            Console.WriteLine(outp);
        }

        public void add(int value, int pos)
        {
            IntCode[pos] = value;
        }

        public void add(int[] code)
        {
            int outp, val1, val2;
            outp = code[0] == 0 ? IntCode[pointer + 3] : pointer + 3;
            val1 = code[1] == 0 ? IntCode[pointer + 2] : pointer + 2;
            val2 = code[2] == 0 ? IntCode[pointer + 1] : pointer + 1;
            IntCode[outp] = IntCode[val1] + IntCode[val2];
        }

        public void multiply(int[] code)
        {
            int outp, val1, val2;
            outp = code[0] == 0 ? IntCode[pointer + 3] : pointer + 3;
            val1 = code[1] == 0 ? IntCode[pointer + 2] : pointer + 2;
            val2 = code[2] == 0 ? IntCode[pointer + 1] : pointer + 1;
            IntCode[outp] = IntCode[val1] * IntCode[val2];
        }
        
        public void jumpIfTrue(int[] code)
        {
            int param1, param2;
            param1 = code[2] == 0 ? IntCode[pointer + 1] : pointer + 1;
            param2 = code[1] == 0 ? IntCode[pointer + 2] : pointer + 2;
            if (IntCode[param1] != 0)
            {
                pointer = IntCode[param2];
                pointer -= 3;
            }
        }

        public void jumpIfFalse(int[] code)
        {
            int param1, param2, param3;
            param1 = code[2] == 0 ? IntCode[pointer + 1] : pointer + 1;
            param2 = code[1] == 0 ? IntCode[pointer + 2] : pointer + 2;
            param3 = code[0] == 0 ? IntCode[pointer + 3] : pointer + 3;
            if (IntCode[param1] == 0)
            {
                pointer = IntCode[param2];
                pointer -= 3;
            }
        }

        public void lessThan(int[] code)
        {
            int param1, param2, param3;
            param1 = code[2] == 0 ? IntCode[pointer + 1] : pointer + 1;
            param2 = code[1] == 0 ? IntCode[pointer + 2] : pointer + 2;
            param3 = code[0] == 0 ? IntCode[pointer + 3] : pointer + 3;

            IntCode[param3] = IntCode[param1] < IntCode[param2] ? 1 : 0;
        }

        public void equals(int[] code)
        {
            int param1, param2, param3;
            param1 = code[2] == 0 ? IntCode[pointer + 1] : pointer + 1;
            param2 = code[1] == 0 ? IntCode[pointer + 2] : pointer + 2;
            param3 = code[0] == 0 ? IntCode[pointer + 3] : pointer + 3;

            IntCode[param3] = IntCode[param1] == IntCode[param2] ? 1 : 0;
        }

        public void halt()
        {
            halted = true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2019
{
    class Day5
    {
        public int name;
        int arg;
        string[] input;
        int[] IntCode;
        bool halted = false;
        bool paused = false;
        public int pointer;
        public int outputCode;
        public int[] internalArgs;
        public int[] ou = new int[0];

        public Day5(string input)
        {
            this.input = input.Split(',');
            readData();
        }

        public void Puzzle(string input)
        {
            readData();
            run();
        }

        public void Puzzle(int[] args, int name)
        {
            this.name = name;
            internalArgs = new int[args.Length];
            if (args != null)
            {
                for (int i = 0; i < args.Length; i++)
                {
                    internalArgs[i] = args[i];
                }
            }
            arg = internalArgs[internalArgs.Length - 1];
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
            if (!paused) { pointer = 0; }
            outputCode = 0;
            paused = false;
            halted = false;
            while (!halted)
            {
                readCode();
            }
        }

        public void readCode()
        {
            int[] opcode = createCode(IntCode[pointer]);
            Console.WriteLine("System[{0}]: [ADRESS]:{1} [PROGRAM]:{2}", name, pointer, getOpCode(opcode));
            switch (getOpCode(opcode))
            {
                case 1:
                    outputCode = 1;
                    add(opcode);
                    pointer += 4;
                    break;
                case 2:
                    outputCode = 2;
                    multiply(opcode);
                    pointer += 4;
                    break;
                case 3:
                    inp();
                    if (outputCode == 3) { return; }
                    pointer += 2;
                    break;
                case 4:
                    outputCode = 4;
                    output(opcode);
                    pointer += 2;
                    break;
                case 5:
                    outputCode = 5;
                    jumpIfTrue(opcode);
                    pointer += 3;
                    break;
                case 6:
                    outputCode = 6;
                    jumpIfFalse(opcode);
                    pointer += 3;
                    break;
                case 7:
                    outputCode = 7;
                    lessThan(opcode);
                    pointer += 4;
                    break;
                case 8:
                    outputCode = 8;
                    equals(opcode);
                    pointer += 4;
                    break;
                case 99:
                    outputCode = 99;
                    halted = true;
                    break;
                default:
                    Console.WriteLine("System["+name+"]: Unknown opcode, exiting");
                    halted = true;
                    break;
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

        public void changeValue(int pos, int val)
        {
            IntCode[pos] = val;
        }

        public int getValue(int pos)
        {
            return IntCode[pos];
        }

        public void inp()
        {
            if (internalArgs.Length > 0)
            {
                var inputCounter = 0;
                inputCounter = internalArgs[0];
                internalArgs = internalArgs.Skip(1).ToArray();
                IntCode[IntCode[pointer + 1]] = inputCounter;
            }
            else
            {
                outputCode = 3;
                paused = true;
                halted = true;
                //IntCode[IntCode[pointer + 1]] = int.Parse(Console.ReadLine());
            }
        }

        public void output(int[] code)
        {
            int outp = code[2] == 0 ? IntCode[IntCode[pointer + 1]] : IntCode[pointer + 1];
            Console.WriteLine("System["+name+"]: output: "+ outp + " input: "+arg);
            addToOutPutArr(outp);
        }

        public void addToOutPutArr(int nr)
        {
            int[] _ = new int[ou.Length];
            for (int i = 0; i < _.Length; i++)
            {
                _[i] = ou[i];
            }
            ou = new int[_.Length + 1];
            for (int i = 0; i < _.Length; i++)
            {
                ou[i] = _[i];
            }
            ou[ou.Length - 1] = nr;
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
    }
}

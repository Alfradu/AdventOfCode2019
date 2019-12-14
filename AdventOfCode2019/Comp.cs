using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2019
{
    class Comp
    {
        public int name;
        int memory = 2000000;
        string[] data;
        long[] IntCode;
        long relativeBase;
        bool halted = false;
        bool paused = false;
        bool showPrints = false;
        bool manualMode = false;
        public long pointer;
        public int outputCode;
        public int[] internalArgs;
        public long[] outputArr = new long[0];

        public Comp(string input, bool showPrints = false)
        {
            this.showPrints = showPrints;
            data = input.Split(',');
            readData();
        }

        public void Puzzle(int name)
        {
            manualMode = true;
            run();
        }

        public void Puzzle(int[] args, int name)
        {
            manualMode = false;
            this.name = name;
            internalArgs = new int[args.Length];
            if (args != null)
            {
                for (int i = 0; i < args.Length; i++)
                {
                    internalArgs[i] = args[i];
                }
            }
            run();
        }

        public void readData()
        {
            IntCode = new long[memory];
            for (int i = 0; i < data.Length; i++)
            {
                IntCode[i] = long.Parse(data[i]);
            }
        }

        private void run()
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

        private void readCode()
        {
            long[] opcode = createCode(IntCode[pointer]);
            printMsg("System["+name+"]: [ADRESS]:"+pointer+" [PROGRAM]:"+getOpCode(opcode));
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
                    input(opcode);
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
                case 9:
                    outputCode = 9;
                    adjustOffset(opcode);
                    pointer += 2;
                    break;
                case 99:
                    outputCode = 99;
                    halted = true;
                    break;
                default:
                    printMsg("System[" + name + "]: Unknown opcode, exiting.");
                    halted = true;
                    break;
            }
        }

        private void input(long[] code)
        {
            if (manualMode)
            {
                long i = long.Parse(printRequest("System[" + name + "] Input: ", 2));
                IntCode[checkParam(code[2], pointer + 1)] = i;
            }
            else
            {
                if (internalArgs.Length > 0)
                {
                    var inputCounter = 0;
                    inputCounter = internalArgs[0];
                    internalArgs = internalArgs.Skip(1).ToArray();
                    IntCode[checkParam(code[2], pointer + 1)] = inputCounter;
                    printMsg("System[" + name + "]: Input: " + inputCounter);
                }
                else
                {
                    outputCode = 3;
                    paused = true;
                    halted = true;
                }
            }
        }

        private void output(long[] code)
        {
            long outp = IntCode[checkParam(code[2], pointer + 1)];
            printMsg("System[" + name + "]: output: " + outp);
            addToOutPutArr(outp);
        }

        private long[] createCode(long input)
        {
            char[] _ch = input.ToString().ToCharArray();
            long[] newCode = new long[5];
            for (int i = 4; i >= 0; i--)
            {
                if (_ch.Length - 1 - (4 - i) < 0)
                {
                    break;
                }
                newCode[i] = (long)char.GetNumericValue(_ch[_ch.Length - 1 - (4 - i)]);
            }
            return newCode;
        }

        private long getOpCode(long[] arr)
        {
            return arr[arr.Length - 2] * 10 + arr[arr.Length - 1];
        }

        public void changeValue(long pos, int val)
        {
            IntCode[pos] = val;
        }

        private void addToOutPutArr(long nr)
        {
            long[] _ = new long[outputArr.Length];
            for (long i = 0; i < _.Length; i++)
            {
                _[i] = outputArr[i];
            }
            outputArr = new long[_.Length + 1];
            for (long i = 0; i < _.Length; i++)
            {
                outputArr[i] = _[i];
            }
            outputArr[outputArr.Length - 1] = nr;
        }

        public void clearOutPutArr()
        {
            outputArr = new long[0];
        }
        private void add(long[] code)
        {
            IntCode[checkParam(code[0], pointer + 3)] = IntCode[checkParam(code[1], pointer + 2)] + IntCode[checkParam(code[2], pointer + 1)];
        }

        private void multiply(long[] code)
        {
            IntCode[checkParam(code[0], pointer + 3)] = IntCode[checkParam(code[1], pointer + 2)] * IntCode[checkParam(code[2], pointer + 1)];
        }

        private void jumpIfTrue(long[] code)
        {
            if (IntCode[checkParam(code[2], pointer + 1)] != 0)
            {
                pointer = IntCode[checkParam(code[1], pointer + 2)];
                pointer -= 3;
            }
        }

        private void jumpIfFalse(long[] code)
        {
            if (IntCode[checkParam(code[2], pointer + 1)] == 0)
            {
                pointer = IntCode[checkParam(code[1], pointer + 2)];
                pointer -= 3;
            }
        }

        private void lessThan(long[] code)
        {
            IntCode[checkParam(code[0], pointer + 3)] = IntCode[checkParam(code[2], pointer + 1)] < IntCode[checkParam(code[1], pointer + 2)] ? 1 : 0;
        }

        private void equals(long[] code)
        {
            IntCode[checkParam(code[0],pointer + 3)] = IntCode[checkParam(code[2], pointer + 1)] == IntCode[checkParam(code[1], pointer + 2)] ? 1 : 0;
        }

        private void adjustOffset(long[] code)
        {
            relativeBase += IntCode[checkParam(code[2], pointer + 1)];
        }

        private long checkParam(long param, long instruction)
        {
            switch (param)
            {
                default:
                    return IntCode[instruction];
                case 1:
                    return instruction;
                case 2:
                    return relativeBase + IntCode[instruction];
            }
        }

        int len = 0;
        private void printMsg(string msg)
        {
            len = msg.Length > len ? msg.Length : len;
            if (showPrints)
            {
                Console.WriteLine(msg);
            }
        }

        private string printRequest(string msg, int lines)
        {
            len = msg.Length > len ? msg.Length : len;
            for (int i = 0; i < len; i++) { Console.Write("-"); }
            for (int i = 0; i < lines; i++){Console.WriteLine("");}
            Console.Write(msg);
            string s = Console.ReadLine();
            for (int i = 0; i < lines-1; i++){Console.WriteLine("");}
            for (int i = 0; i < len; i++) { Console.Write("-"); }
            Console.WriteLine("");
            return s;
        }
    }
}

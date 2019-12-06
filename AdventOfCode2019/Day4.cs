using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2019
{
    class Day4
    {
        public void Puzzle()
        {
            string input = "136818-685979";
            int bottom = int.Parse(input.Split('-')[0]);
            int top = int.Parse(input.Split('-')[1]);
            int acceptedPasswords = 0;

            for (int i = bottom; i <= top; i++)
            {
                if (i < top && HasEqualAdjacent(i) && HasIncreasingDigits(i))
                {
                    acceptedPasswords++;
                }
            }
            Console.WriteLine("Number of accepted passwords: " + acceptedPasswords);
        }

        public bool HasEqualAdjacent(int number)
        {
            bool equal = false;
            char[] tempNumber = number.ToString().ToCharArray();
            int[] numbers = new int[6];
            int[] pairs = new int[6];
            for (int i = 0; i < tempNumber.Length; i++)
            {
                numbers[i] = (int)char.GetNumericValue(tempNumber[i]);
            }

            /*for (int i = 0; i < numbers.Length-1; i++)
            {
                if (numbers[i] == numbers[i+1])
                {
                    equal = true;
                }
            } old solution
            */

            for (int i = 0; i < numbers.Length; i++)
            {
                for (int j = i + 1; j < numbers.Length; j++)
                {
                    if (numbers[i] == numbers[j] && numbers[j-1] == numbers[j])
                    {
                        pairs[i]++;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            string num = "";
            foreach(int i in pairs)
            {
                num = num + i.ToString();
            }
            if(num.IndexOf("01") != -1 || num.IndexOf("1") == 0)
            {
                equal = true;
            }

            return equal;
        }

        public bool HasIncreasingDigits(int number)
        {
            bool onlyIncreasing = true;
            char[] tempNumber = number.ToString().ToCharArray();
            for (int i = 0; i < tempNumber.Length-1; i++)
            {
                if (int.Parse(tempNumber[i].ToString()) > int.Parse(tempNumber[i + 1].ToString()))
                {
                    onlyIncreasing = false;
                }
            }
            return onlyIncreasing;
        }
    }
}

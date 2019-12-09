using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2019
{
    class Day8
    {
        public void Puzzle()
        {
            string input = System.IO.File.ReadAllText(@"K:\Android projects\AdventOfCode2019\input8.txt");
            int width = 25;
            int height = 6;
            string[] layers = new string[input.Length/(width*height)];

            for(int i = 0; i < layers.Length; i++)
            {
                layers[i] = input.Substring(i * (width * height), (width * height));
            }
            int selectedLayer = layers.Length-1;
            for (int i = 0; i < layers.Length; i++)
            {
                if (Regex.Matches(layers[i], "0").Count < Regex.Matches(layers[selectedLayer], "0").Count)
                {
                    selectedLayer = i;
                }
            }
            Console.WriteLine("Number of 1 and 2 in layer " 
                + selectedLayer + ": " 
                + Regex.Matches(layers[selectedLayer], "1").Count * Regex.Matches(layers[selectedLayer], "2").Count);

            StringBuilder finishedLayer = new StringBuilder("", input.Length/(width * height));
            for (int i = 0; i < width * height; i++)
            {
                for (int j = 0; j < layers.Length; j++)
                {
                    switch (char.ToString(layers[j][i]))
                    {
                        case "0":
                            finishedLayer.Insert(i, "'");
                            goto Next;
                        case "1":
                            finishedLayer.Insert(i, "#");
                            goto Next;
                        case "2":
                            break;
                    }
                }
                Next:;
            }

            for (int i = 0; i < height; i++)
            {
                Console.WriteLine(finishedLayer.ToString(i*width,width));
            }
        }
    }
}

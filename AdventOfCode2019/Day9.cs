using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2019
{
    class Day9
    {
        public void Puzzle()
        {
            string input = System.IO.File.ReadAllText(@"K:\Android projects\AdventOfCode2019\input9.txt");
            Comp c = new Comp(input, true);
            c.Puzzle(0);
        }
    }
}

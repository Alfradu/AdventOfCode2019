﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2019
{
    class Day5
    {
        public void Puzzle()
        {
            Comp c = new Comp(System.IO.File.ReadAllText(@"K:\Android projects\AdventOfCode2019\input5.txt"), true);
            c.Puzzle(0);
        }
    }
}

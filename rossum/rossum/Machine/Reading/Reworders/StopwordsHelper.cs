﻿using System;
using System.Linq;

namespace rossum.Machine.Reading.Reworders
{
    public static class ReworderHelper
    {
        public static string Map(string input, IReworder reworder)
        {
            return String.Join(" ", input.Split(' ').Select(c => reworder.Map(c.ToLower())));
        }
    }
}
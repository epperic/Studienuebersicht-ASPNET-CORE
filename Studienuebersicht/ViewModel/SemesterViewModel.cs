﻿using Studienuebersicht.Model;
using System.Collections.Generic;

namespace Studienuebersicht
{
    public class SemesterViewModel
    {
        public List<Module> Modules { get; set; }
        public int AllECTS { get; set; }
        public double AverageGrade { get; set; }
    }
}

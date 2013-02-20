﻿using System.Collections.Generic;

namespace OptimizingParallelCompiler
{
    public class ThreeOPAnalysis
    {
        public string Name { get; set; }
        public bool Constant { get; set; }
        public string NameValue { get; set; }

        public int OperandCount { get; set; }
        public int Index { get; set; }

        public bool ArrayAccess { get; set; }
        public List<string> ArrayName { get; set; }
        public List<string> ArrayVariableName { get; set; }

        public int LetCount { get; set; }

        public ThreeOPAnalysis(string name, string nameValue, bool constant, int operandCount, int index, bool arrayAccess,
            List<string> arrayName, List<string> arrayVariableName, int letCount = 1)
        {
            Name = name;
            Constant = constant;
            OperandCount = operandCount;
            Index = index;
            ArrayAccess = arrayAccess;
            NameValue = nameValue;
            ArrayName = arrayName;
            ArrayVariableName = arrayVariableName;
            LetCount = letCount;
        }
    }
}

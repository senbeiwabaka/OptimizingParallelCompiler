using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OptimizingParallelCompiler
{
    public class ThreeOPAnalysis
    {
        public string VariableName { get; set; }
        public bool Constant { get; set; }
        public int OperandCount { get; set; }

        public ThreeOPAnalysis(string vname, bool constant, int operandcount)
        {
            VariableName = vname;
            Constant = constant;
            OperandCount = operandcount;
        }


    }
}

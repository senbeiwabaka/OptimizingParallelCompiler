namespace OptimizingParallelCompiler
{
    public class ThreeOPAnalysis
    {
        public string VariableName { get; set; }
        public bool Constant { get; set; }
        public int OperandCount { get; set; }
        public int Index { get; set; }

        public ThreeOPAnalysis(string vname, bool constant, int operandcount, int index)
        {
            VariableName = vname;
            Constant = constant;
            OperandCount = operandcount;
            Index = index;
        }
    }
}

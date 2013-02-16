namespace OptimizingParallelCompiler
{
    public class ThreeOPAnalysis
    {
        public string VariableName { get; set; }
        public bool Constant { get; set; }
        public int OperandCount { get; set; }
        public int Index { get; set; }
        public bool ArrayAccess { get; set; }

        public ThreeOPAnalysis(string vName, bool constant, int operandCount, int index, bool arrayAccess)
        {
            VariableName = vName;
            Constant = constant;
            OperandCount = operandCount;
            Index = index;
            ArrayAccess = arrayAccess;
        }
    }
}

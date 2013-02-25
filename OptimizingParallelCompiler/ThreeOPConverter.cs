using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace OptimizingParallelCompiler
{
    class ThreeOPConverter
    {

        public static void Transform(List<string> code)
        {
            var threeOP = new List<ThreeOPAnalysis>();

            var counter = 0;

            string intStatements = string.Empty;
            string letStatement = string.Empty;
            var lets = new Dictionary<int, string>();

            int index;
            code.ForEach(x =>
                {
                    var nonModifiedStatement = x;

                    x = x.Trim(' ', '\t');

                    var statement = x.Substring(x.IndexOf("=", StringComparison.Ordinal) + 1,
                                                    x.Length - (x.IndexOf("=", StringComparison.Ordinal) + 1));

                    var codeIndex = code.IndexOf(nonModifiedStatement);
                    

                    if (x.IndexOf("let", StringComparison.Ordinal) == 0)
                    {
                        if (Regex.Matches(x, "[[]").Count > 0)
                        {
                            var indexBracketFront = x.IndexOf("[", 0, StringComparison.Ordinal);
                            var indexBracketEnd = x.IndexOf("]", 0, StringComparison.Ordinal);

                            var countBracket = Regex.Matches(statement, "[[]").Count;

                            intStatements = "int t_" + counter + "\n";
                            letStatement = "let " + "t_" + counter + " = " + 
                                        x.Substring(indexBracketFront + 1, (indexBracketEnd - 1) - indexBracketFront) + "\n";

                            var replace = "let" + x.Substring(x.IndexOf(" "), indexBracketFront - x.IndexOf(" ")) + "[t_" + counter++ + 
                                x.Substring(indexBracketEnd, (x.Length) - indexBracketEnd);

                            

                            while (countBracket > 0)
                            {
                                indexBracketEnd = 0;
                                indexBracketFront = 0;
                                indexBracketFront = statement.IndexOf("[", indexBracketFront + 1,
                                                                      StringComparison.Ordinal);
                                indexBracketEnd = statement.IndexOf("]", indexBracketEnd + 1, StringComparison.Ordinal);
                                var arrayIndex = statement.Substring(indexBracketFront + 1,
                                                                     (indexBracketEnd - 1) - indexBracketFront);

                                if (Regex.Matches(arrayIndex, "[-+*/]").Count > 0)
                                {
                                    intStatements += "int t_" + counter + "\n";
                                    var variable = "t_" + counter;
                                    letStatement += "let " + "t_" + counter++ + " = " +
                                                    arrayIndex +"\n";
                                    intStatements += "int t_" + counter;
                                    letStatement += "let t_" + counter + " = " + statement.Substring(statement.IndexOf(" ") + 1,
                                                                        indexBracketFront -
                                                                        (statement.IndexOf(" ") + 1)) + "[" + variable + "]\n";
                                }

                                if (statement.Length < indexBracketEnd + 2)
                                {
                                    statement = statement.Substring(indexBracketEnd + 2);
                                }

                                --countBracket; 
                            }

                            index = code.IndexOf(nonModifiedStatement);
                            lets.Add(index, letStatement);
                            code[index] = replace;
                        }
                        else if (Regex.Matches(statement, "[-+/*]").Count > 2)
                        {
                            var operandAmount = Regex.Matches(statement, "[-+/*]").Count;

                            index = code.IndexOf(nonModifiedStatement);

                            while (operandAmount > 1)
                            {
                                var letAdd = "let";

                                --operandAmount;
                            }
                        }
                        
                    }
                });


            foreach (var @let in lets)
            {
                code.Insert(let.Key, let.Value);
            }

            var varIndex = code.IndexOf("\tvar") != -1 ? code.IndexOf("\tvar") + 1 : code.IndexOf(" var") + 1;

            code.Insert(varIndex, intStatements);
        }
    }
}
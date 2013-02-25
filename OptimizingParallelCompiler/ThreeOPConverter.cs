using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace OptimizingParallelCompiler
{
    internal class ThreeOPConverter
    {

        public static void Transform(List<string> code)
        {
            var counter = 0;

            var intStatements = string.Empty;
            var lets = new Dictionary<int, string>();

            int index;

            code.ForEach(x =>
                {
                    var nonModifiedStatement = x;

                    x = x.Trim(' ', '\t');

                    var statement = x.Substring(x.IndexOf("=", StringComparison.Ordinal) + 1,
                                                x.Length - (x.IndexOf("=", StringComparison.Ordinal) + 1));

                    if (x.IndexOf("let", StringComparison.Ordinal) == 0)
                    {
                        if (Regex.Matches(x, "[[]").Count > 0)
                        {
                            var indexBracketFront = x.IndexOf("[", 0, StringComparison.Ordinal);
                            var indexBracketEnd = x.IndexOf("]", 0, StringComparison.Ordinal);

                            var countBracket = Regex.Matches(statement, "[[]").Count;

                            var result = 0;
                            var replace = string.Empty;
                            var letStatement = string.Empty;

                            if (
                                int.TryParse(
                                    x.Substring(indexBracketFront + 1, (indexBracketEnd - 1) - indexBracketFront),
                                    out result) == false)
                            {

                                intStatements += "int t_" + counter + "\n";
                                letStatement = "let " + "t_" + counter + " = " +
                                                   x.Substring(indexBracketFront + 1,
                                                               (indexBracketEnd - 1) - indexBracketFront) + "\n";

                                replace = "let" + x.Substring(x.IndexOf(" "), indexBracketFront - x.IndexOf(" ")) +
                                              "[t_" + counter++ +
                                              x.Substring(indexBracketEnd, (x.Length) - indexBracketEnd);
                            }

                            while (countBracket > 0)
                            {
                                indexBracketEnd = 0;
                                indexBracketFront = 0;
                                indexBracketFront = statement.IndexOf("[", indexBracketFront + 1,
                                                                      StringComparison.Ordinal);
                                indexBracketEnd = statement.IndexOf("]", indexBracketEnd + 1, StringComparison.Ordinal);
                                var arrayIndex = statement.Substring(indexBracketFront + 1,
                                                                     (indexBracketEnd - 1) - indexBracketFront);

                                if (int.TryParse(arrayIndex, out result))
                                {
                                    intStatements += "int t_" + counter;
                                    var arrayName = statement.Substring(statement.IndexOf(" "),
                                                                        indexBracketFront - statement.IndexOf(" "));
                                    letStatement += "let t_" + counter + " = " + arrayName + "[" + arrayIndex + "]";
                                    replace = replace.Replace(arrayName + "[" + arrayIndex + "]", "t_" + counter++);
                                }
                                else
                                {
                                    intStatements += "int t_" + counter + "\n";
                                    var variable = "t_" + counter;
                                    letStatement += "let " + "t_" + counter++ + " = " +
                                                    arrayIndex + "\n";
                                    intStatements += "int t_" + counter + "\n";
                                    letStatement += "let t_" + counter + " = " +
                                                    statement.Substring(statement.IndexOf(" ") + 1,
                                                                        indexBracketFront -
                                                                        (statement.IndexOf(" ") + 1)) + "[" + variable +
                                                    "]\n";
                                    replace = replace.Replace(statement.Substring(statement.IndexOf(" ") + 1,
                                                                                  indexBracketFront -
                                                                                  (statement.IndexOf(" ") + 1)) + "[" +
                                                              arrayIndex + "]",
                                                              "t_" + counter++);
                                }

                                if (statement.Length > indexBracketEnd + 2)
                                {
                                    statement = statement.Substring(indexBracketEnd + 2);
                                }

                                --countBracket;
                            }

                            if (!string.IsNullOrEmpty(letStatement))
                            {
                                index = code.IndexOf(nonModifiedStatement);
                                lets.Add(index, letStatement);
                                code[index] = replace;
                            }
                        }
                    }
                });

            index = code.IndexOf("\tvar") != -1 ? code.IndexOf("\tvar") + 1 : code.IndexOf(" var") + 1;

            code.Insert(index, intStatements);

            foreach (var @let in lets)
            {
                code.Insert(let.Key + 1, let.Value);
            }
        }
    }
}
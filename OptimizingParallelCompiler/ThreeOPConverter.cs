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

                    if (x.StartsWith("let", StringComparison.Ordinal))
                    {
                        LetCreation(ref x, ref counter, ref intStatements, ref lets, ref statement,
                                    ref code, ref nonModifiedStatement);
                    }
                    else if (x.StartsWith("if"))
                    {
                        if (Regex.Matches(x, "[[]").Count == 1)
                        {
                            var indexBracketFront = x.IndexOf("[", 0, StringComparison.Ordinal);
                            var indexBracketEnd = x.IndexOf("]", 0, StringComparison.Ordinal);
                            var arrayName = x.Substring(x.IndexOf("(", StringComparison.Ordinal) + 1,
                                                            indexBracketFront - x.IndexOf("(", StringComparison.Ordinal) - 1);
                            var arrayIndex = x.Substring(indexBracketFront + 1, (indexBracketEnd - 1) - indexBracketFront);

                            intStatements += "int t_" + counter + "\n";
                            index = code.IndexOf(nonModifiedStatement);
                            
                            var c = counter;
                            lets.Add(index,
                                     "let " + "t_" + counter++ + " = " + arrayIndex + "\n"
                                     + "let " + "t_" + counter + " = " + arrayName + "[t_" + c + "]");

                            intStatements += "int t_" + counter;

                            code[index] = "if (t_" + counter++ + x.Substring(indexBracketEnd + 1, x.Length - (indexBracketEnd + 1));
                        }
                    }
                    else if (x.StartsWith("print", StringComparison.Ordinal))
                    {
                        if (Regex.Matches(x, "[[]").Count == 1)
                        {
                            var indexBracketFront = x.IndexOf("[", 0, StringComparison.Ordinal);
                            var indexBracketEnd = x.IndexOf("]", 0, StringComparison.Ordinal);
                            var lastSpace = x.LastIndexOf(" ", StringComparison.Ordinal);
                            var arrayName = x.Substring(lastSpace + 1,
                                                            indexBracketFront - lastSpace - 1);
                            var arrayIndex = x.Substring(indexBracketFront + 1, (indexBracketEnd - 1) - indexBracketFront);

                            intStatements += "int t_" + counter + "\n";
                            index = code.IndexOf(nonModifiedStatement);

                            var c = counter;
                            lets.Add(index,
                                     "let " + "t_" + counter++ + " = " + arrayIndex + "\n"
                                     + "let " + "t_" + counter + " = " + arrayName + "[t_" + c + "]");
                            intStatements += "int t_" + counter;

                            code[index] = code[index].Replace(arrayName + "[" + arrayIndex + "]", "t_" + counter++);
                            Console.WriteLine(lastSpace);
                        }
                    }
                });

            index = code.IndexOf("\tvar") != -1 ? code.IndexOf("\tvar") + 1 : code.IndexOf(" var") + 1;

            code.Insert(index, intStatements);

            var i = 1;
            foreach (var @let in lets)
            {
                code.Insert(let.Key + i++, let.Value);
            }
        }

        private static void LetCreation(ref string line, ref int counter, ref string intStatements, ref Dictionary<int, string> lets, ref string statement,
            ref List<string> code, ref string nonModifiedStatement)
        {
            var letStatement = string.Empty;
            if (Regex.Matches(line, "[[]").Count > 0)
            {
                var indexBracketFront = line.IndexOf("[", 0, StringComparison.Ordinal);
                var indexBracketEnd = line.IndexOf("]", 0, StringComparison.Ordinal);

                var countBracket = Regex.Matches(statement, "[[]").Count;

                var result = 0;
                var replace = string.Empty;

                if (
                    int.TryParse(
                        line.Substring(indexBracketFront + 1, (indexBracketEnd - 1) - indexBracketFront),
                        out result) == false)
                {

                    intStatements += "int t_" + counter + "\n";
                    letStatement = "let " + "t_" + counter + " = " +
                                   line.Substring(indexBracketFront + 1,
                                                  (indexBracketEnd - 1) - indexBracketFront) + "\n";

                    replace = "let" +
                              line.Substring(line.IndexOf(" ", StringComparison.Ordinal),
                                             indexBracketFront - line.IndexOf(" ", StringComparison.Ordinal)) +
                              "[t_" + counter++ +
                              line.Substring(indexBracketEnd, (line.Length) - indexBracketEnd);
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
                        var arrayName = statement.Substring(statement.IndexOf(" ", StringComparison.Ordinal),
                                                            indexBracketFront - statement.IndexOf(" ", StringComparison.Ordinal));
                        letStatement += "let t_" + counter + " = " + arrayName + "[" + arrayIndex + "]\n";
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
                                        statement.Substring(statement.IndexOf(" ", StringComparison.Ordinal) + 1,
                                                            indexBracketFront -
                                                            (statement.IndexOf(" ", StringComparison.Ordinal) + 1)) + "[" + variable +
                                        "]\n";
                        replace = replace.Replace(statement.Substring(statement.IndexOf(" ", StringComparison.Ordinal) + 1,
                                                                      indexBracketFront -
                                                                      (statement.IndexOf(" ", StringComparison.Ordinal) + 1)) + "[" +
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
                    var index = code.IndexOf(nonModifiedStatement);
                    lets.Add(index, letStatement);
                    code[index] = replace;
                }
            }
        }
    }
}
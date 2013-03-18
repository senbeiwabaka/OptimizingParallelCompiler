using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace OptimizingParallelCompiler
{
    internal class ThreeOPConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
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

                    //
                    if (x.StartsWith("let", StringComparison.Ordinal))
                    {
                        LetArrayCreation(ref x, ref counter, ref intStatements, ref lets, ref statement, ref code, ref nonModifiedStatement);

                        LetParenthesis(ref x, ref counter, ref intStatements, ref lets, ref statement, ref code, ref nonModifiedStatement);
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
                        else
                        {
                            var parenthesis = x.Substring(x.IndexOf("(") + 1, (x.IndexOf(")") - 1) - x.IndexOf("("));
                            Console.WriteLine(parenthesis);
                            var amount = Regex.Matches(parenthesis, "[-+/*]").Count;
                            if (amount == 1)
                            {
                                intStatements += "int t_" + counter;
                                var elements = string.Empty;
                                var spaceCount = 3;
                                while (spaceCount >= 1)
                                {
                                    elements += parenthesis.Substring(0, parenthesis.IndexOf(" ", StringComparison.Ordinal) + 1 - 0);
                                    parenthesis = parenthesis.Substring(parenthesis.IndexOf(" ", StringComparison.Ordinal) + 1, parenthesis.Length - parenthesis.IndexOf(" ", StringComparison.Ordinal) - 1);
                                    spaceCount--;
                                }

                                lets.Add(code.IndexOf(nonModifiedStatement), nonModifiedStatement.Substring(0, nonModifiedStatement.IndexOf("if", StringComparison.Ordinal)) + "let t_" + counter + " = " + elements);

                                elements = nonModifiedStatement.Substring(0, nonModifiedStatement.IndexOf("if", StringComparison.Ordinal)) + "if (t_" + counter++ + " " + parenthesis + nonModifiedStatement.Substring(nonModifiedStatement.IndexOf(")", StringComparison.Ordinal), nonModifiedStatement.Length - nonModifiedStatement.IndexOf(")", StringComparison.Ordinal));

                                code[code.IndexOf(nonModifiedStatement)] = elements;

                                Console.WriteLine(x.IndexOf(elements));
                            }
                        }
                    }
                    else if (x.StartsWith("print", StringComparison.Ordinal))
                    {
                        Print(code, ref counter, lets, ref x, nonModifiedStatement, ref intStatements);
                    }
                });

            index = code.IndexOf("\tvar") != -1 ? code.IndexOf("\tvar") + 1 : code.IndexOf(" var") + 1;

            code.Insert(index, intStatements);

            var i = 1;
            foreach (var @let in lets)
            {
                code.Insert(let.Key + i++, let.Value);
            }

            code.ForEach(x =>
                {
                });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="counter"></param>
        /// <param name="lets"></param>
        /// <param name="x"></param>
        /// <param name="nonModifiedStatement"></param>
        /// <param name="intStatements"></param>
        private static void Print(List<string> code, ref int counter, Dictionary<int, string> lets, ref string x, string nonModifiedStatement, ref string intStatements)
        {
            if (Regex.Matches(x, "[[]").Count == 1)
            {
                var indexBracketFront = x.IndexOf("[", 0, StringComparison.Ordinal);
                var indexBracketEnd = x.IndexOf("]", 0, StringComparison.Ordinal);
                var arrayIndex = x.Substring(indexBracketFront + 1, (indexBracketEnd - 1) - indexBracketFront);

                intStatements += "int t_" + counter + "\n";

                if (Regex.Matches(arrayIndex, "[-+*/]").Count > 0)
                {
                    code[code.IndexOf(nonModifiedStatement)] = code[code.IndexOf(nonModifiedStatement)].Replace(arrayIndex, "t_" + counter);
                    x = x.Replace(arrayIndex, "t_" + counter);

                    var lastSpace = x.LastIndexOf(" ", StringComparison.Ordinal);
                    var arrayName = x.Substring(lastSpace + 1, indexBracketFront - lastSpace - 1);

                    var c = counter;
                    lets.Add(code.IndexOf(nonModifiedStatement),
                             nonModifiedStatement.Substring(0, nonModifiedStatement.IndexOf("print", StringComparison.Ordinal)) + "let " + "t_" + counter++ + " = " + arrayIndex + "\n" + nonModifiedStatement.Substring(0, nonModifiedStatement.IndexOf("print", StringComparison.Ordinal)) + "let " + "t_" + counter + " = " + arrayName + "[t_" + c + "]");
                    intStatements += "int t_" + counter;

                    code[code.IndexOf(nonModifiedStatement)] = code[code.IndexOf(nonModifiedStatement)].Replace(arrayName + "[t_" + c + "]", "t_" + counter++);
                }
                else
                {
                    var lastSpace = x.LastIndexOf(" ", StringComparison.Ordinal);
                    var arrayName = x.Substring(lastSpace + 1, indexBracketFront - lastSpace - 1);

                    var c = counter;
                    lets.Add(code.IndexOf(nonModifiedStatement),
                             nonModifiedStatement.Substring(0, nonModifiedStatement.IndexOf("print", StringComparison.Ordinal)) + "let " + "t_" + counter++ + " = " + arrayIndex + "\n" + nonModifiedStatement.Substring(0, nonModifiedStatement.IndexOf("print", StringComparison.Ordinal)) + "let " + "t_" + counter + " = " + arrayName + "[t_" + c + "]");
                    intStatements += "int t_" + counter;

                    code[code.IndexOf(nonModifiedStatement)] = code[code.IndexOf(nonModifiedStatement)].Replace(arrayName + "[" + arrayIndex + "]", "t_" + counter++);
                    Console.WriteLine(lastSpace);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="line"></param>
        /// <param name="counter"></param>
        /// <param name="intStatements"></param>
        /// <param name="lets"></param>
        /// <param name="statement"></param>
        /// <param name="code"></param>
        /// <param name="nonModifiedStatement"></param>
        private static void LetArrayCreation(ref string line, ref int counter, ref string intStatements, ref Dictionary<int, string> lets, ref string statement, ref List<string> code, ref string nonModifiedStatement)
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

        private static void LetParenthesis(ref string line, ref int counter, ref string intStatements, ref Dictionary<int, string> lets, ref string statement, ref List<string> code, ref string nonModifiedStatement)
        {
            if (Regex.Matches(line, "[(]").Count > 0)
            {
                Console.WriteLine("first index = " + statement.IndexOf("(").ToString());
                Console.WriteLine("last index = " + statement.LastIndexOf("(").ToString());
                var isNested = statement.LastIndexOf("(") - statement.IndexOf("(") == 1 ? true : false;
                var elements = new List<string>();
                if (isNested)
                {
                    Console.WriteLine(statement.IndexOf(")"));
                    var list = Parenthesis(elements, statement.Substring(statement.LastIndexOf("(") + 1, statement.IndexOf(")") - statement.LastIndexOf("(") - 1), Regex.Matches(line, "[(]").Count);
                    while (Regex.Matches(list.ToString(), "[/*]").Count > 0)
                    {

                    }
                }
                else
                {
                    for (int i = 0; i < Regex.Matches(statement, "[(]").Count; i++)
                    {
                        var index = statement.IndexOf(")") - statement.LastIndexOf(")") == 1 ? statement.LastIndexOf(")") : statement.IndexOf(")");
                        Console.WriteLine(index);
                        var substring = statement.Substring(statement.IndexOf("(", StringComparison.Ordinal) + 1, index - statement.IndexOf("(", StringComparison.Ordinal) - 1);
                        Console.WriteLine(substring);
                    }
                }
            }
        }

        /// <summary>
        /// parenthesis 
        /// </summary>
        private static List<string> Parenthesis(List<string> words, string statment, int count)
        {
            while (statment.Length > 0)
            {
                words.Add(statment.Substring(0, statment.IndexOf(" ")));
                statment = statment.Substring(statment.IndexOf(" ") + 1);
            }
            return words;
        }
    }
}
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
        public static List<ThreeOPCreation> Transform(List<string> code)
        {
            var counter = 0;
            var intStatements = string.Empty;
            var lets = new List<ThreeOPCreation>();

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

                            intStatements += "int t_" + counter + Environment.NewLine;
                            index = code.IndexOf(nonModifiedStatement);

                            var c = counter;
                            lets.Add(new ThreeOPCreation { Index = index, Statements = "let " + "t_" + counter++ + " = " + arrayIndex });
                            lets.Add(new ThreeOPCreation { Index = index, Statements = "let " + "t_" + counter + " = " + arrayName + "[t_" + c + "]" });

                            intStatements += "int t_" + counter + Environment.NewLine;

                            code[index] = "if (t_" + counter++ + x.Substring(indexBracketEnd + 1, x.Length - (indexBracketEnd + 1));
                        }
                        else
                        {
                            var parenthesis = x.Substring(x.IndexOf("(") + 1, (x.IndexOf(")") - 1) - x.IndexOf("("));
                            Console.WriteLine(parenthesis);
                            var amount = Regex.Matches(parenthesis, "[-+/*]").Count;
                            if (amount == 1)
                            {
                                intStatements += "int t_" + counter + Environment.NewLine;
                                var elements = string.Empty;
                                var spaceCount = 3;
                                while (spaceCount >= 1)
                                {
                                    elements += parenthesis.Substring(0, parenthesis.IndexOf(" ", StringComparison.Ordinal) + 1 - 0);
                                    parenthesis = parenthesis.Substring(parenthesis.IndexOf(" ", StringComparison.Ordinal) + 1, parenthesis.Length - parenthesis.IndexOf(" ", StringComparison.Ordinal) - 1);
                                    spaceCount--;
                                }

                                lets.Add(new ThreeOPCreation { Index = code.IndexOf(nonModifiedStatement), Statements = nonModifiedStatement.Substring(0, nonModifiedStatement.IndexOf("if", StringComparison.Ordinal)) + "let t_" + counter + " = " + elements });

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

            //index = code.IndexOf("\tvar") > 0 ? code.IndexOf("\tvar") + 1 : code.IndexOf("var") + 1;

            //code.Insert(index, intStatements);

            return lets;
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
        private static void Print(List<string> code, ref int counter, List<ThreeOPCreation> lets, ref string x, string nonModifiedStatement, ref string intStatements)
        {
            if (Regex.Matches(x, "[[]").Count == 1)
            {
                var indexBracketFront = x.IndexOf("[", 0, StringComparison.Ordinal);
                var indexBracketEnd = x.IndexOf("]", 0, StringComparison.Ordinal);
                var arrayIndex = x.Substring(indexBracketFront + 1, (indexBracketEnd - 1) - indexBracketFront);

                intStatements += "int t_" + counter + Environment.NewLine;

                if (Regex.Matches(arrayIndex, "[-+*/]").Count > 0)
                {
                    code[code.IndexOf(nonModifiedStatement)] = code[code.IndexOf(nonModifiedStatement)].Replace(arrayIndex, "t_" + counter);
                    x = x.Replace(arrayIndex, "t_" + counter);

                    var lastSpace = x.LastIndexOf(" ", StringComparison.Ordinal);
                    var arrayName = x.Substring(lastSpace + 1, indexBracketFront - lastSpace - 1);

                    var c = counter;
                    lets.Add(new ThreeOPCreation { Index = code.IndexOf(nonModifiedStatement), Statements = nonModifiedStatement.Substring(0, nonModifiedStatement.IndexOf("print", StringComparison.Ordinal)) + "let " + "t_" + counter++ + " = " + arrayIndex });
                    lets.Add(new ThreeOPCreation { Index = code.IndexOf(nonModifiedStatement), Statements = nonModifiedStatement.Substring(0, nonModifiedStatement.IndexOf("print", StringComparison.Ordinal)) + "let " + "t_" + counter + " = " + arrayName + "[t_" + c + "]" });
                    //lets.Add(code.IndexOf(nonModifiedStatement), nonModifiedStatement.Substring(0, nonModifiedStatement.IndexOf("print", StringComparison.Ordinal)) + "let " + "t_" + counter++ + " = " + arrayIndex + Environment.NewLine + nonModifiedStatement.Substring(0, nonModifiedStatement.IndexOf("print", StringComparison.Ordinal)) + "let " + "t_" + counter + " = " + arrayName + "[t_" + c + "]");
                    intStatements += "int t_" + counter + Environment.NewLine;

                    code[code.IndexOf(nonModifiedStatement)] = code[code.IndexOf(nonModifiedStatement)].Replace(arrayName + "[t_" + c + "]", "t_" + counter++);
                }
                else
                {
                    var lastSpace = x.LastIndexOf(" ", StringComparison.Ordinal);
                    var arrayName = x.Substring(lastSpace + 1, indexBracketFront - lastSpace - 1);

                    var c = counter;
                    lets.Add(new ThreeOPCreation { Index = code.IndexOf(nonModifiedStatement), Statements = nonModifiedStatement.Substring(0, nonModifiedStatement.IndexOf("print", StringComparison.Ordinal)) + "let " + "t_" + counter++ + " = " + arrayIndex + Environment.NewLine });
                    lets.Add(new ThreeOPCreation { Index = code.IndexOf(nonModifiedStatement), Statements = nonModifiedStatement.Substring(0, nonModifiedStatement.IndexOf("print", StringComparison.Ordinal)) + "let " + "t_" + counter + " = " + arrayName + "[t_" + c + "]" });
                    //lets.Add(code.IndexOf(nonModifiedStatement), nonModifiedStatement.Substring(0, nonModifiedStatement.IndexOf("print", StringComparison.Ordinal)) + "let " + "t_" + counter++ + " = " + arrayIndex + Environment.NewLine + nonModifiedStatement.Substring(0, nonModifiedStatement.IndexOf("print", StringComparison.Ordinal)) + "let " + "t_" + counter + " = " + arrayName + "[t_" + c + "]");
                    intStatements += "int t_" + counter + Environment.NewLine;

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
        private static void LetArrayCreation(ref string line, ref int counter, ref string intStatements, ref List<ThreeOPCreation> lets, ref string statement, ref List<string> code, ref string nonModifiedStatement)
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

                    intStatements += "int t_" + counter + Environment.NewLine;
                    lets.Add(new ThreeOPCreation
                    {
                        Index = code.IndexOf(nonModifiedStatement),
                        Statements = nonModifiedStatement.Substring(0, nonModifiedStatement.IndexOf("let")) +  "let " + "t_" + counter + " = " +
                            line.Substring(indexBracketFront + 1,
                            (indexBracketEnd - 1) - indexBracketFront)
                    });
                    //letStatement = "let " + "t_" + counter + " = " + line.Substring(indexBracketFront + 1,                                   (indexBracketEnd - 1) - indexBracketFront) + Environment.NewLine;

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
                        intStatements += "int t_" + counter + Environment.NewLine;
                        var arrayName = statement.Substring(statement.IndexOf(" ", StringComparison.Ordinal),
                                                            indexBracketFront - statement.IndexOf(" ", StringComparison.Ordinal));
                        letStatement = nonModifiedStatement.Substring(0, nonModifiedStatement.IndexOf("let")) + "let t_" + counter + " = " + arrayName + "[" + arrayIndex + "]";
                        replace = replace.Replace(arrayName + "[" + arrayIndex + "]", "t_" + counter++);
                    }
                    else
                    {
                        intStatements += "int t_" + counter + Environment.NewLine;
                        var variable = "t_" + counter;
                        //letStatement += "let " + "t_" + counter++ + " = " + arrayIndex + Environment.NewLine;
                        lets.Add(new ThreeOPCreation
                        {
                            Index = code.IndexOf(nonModifiedStatement),
                            Statements = nonModifiedStatement.Substring(0, nonModifiedStatement.IndexOf("let")) + "let " + "t_" + counter++ + " = " +
                                arrayIndex
                        });
                        intStatements += "int t_" + counter + Environment.NewLine;
                        letStatement = nonModifiedStatement.Substring(0, nonModifiedStatement.IndexOf("let")) + "let t_" + counter + " = " + statement.Substring(statement.IndexOf(" ", StringComparison.Ordinal) + 1,
                             indexBracketFront - (statement.IndexOf(" ", StringComparison.Ordinal) + 1)) + "[" + variable + "]";

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

                    lets.Add(new ThreeOPCreation { Index = code.IndexOf(nonModifiedStatement), Statements = letStatement });

                    --countBracket;
                }

                if (!string.IsNullOrEmpty(replace))
                {
                    code[code.IndexOf(nonModifiedStatement)] = replace;
                }
            }
        }

        private static void LetParenthesis(ref string line, ref int counter, ref string intStatements, ref List<ThreeOPCreation> lets, ref string statement, ref List<string> code, ref string nonModifiedStatement)
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
                    Console.WriteLine(list.ToString());
                    var previousInt = "t_" + counter;
                    var i = 0;
                    var addition = string.Empty;
                    while ( i < list.Count)
                    {
                        if (list.Count > 1)
                        {
                            if (list[i] == "*")
                            {
                                intStatements += "int t_" + counter + Environment.NewLine;
                                previousInt = "t_" + counter;
                                var before = nonModifiedStatement.Substring(0, nonModifiedStatement.IndexOf("l"));
                                addition += before + "let t_" + counter++ + " = " + ((i - 1) < 0 ? previousInt : list[i - 1]) + list[i] + list[i + 1] + Environment.NewLine;
                                list.RemoveRange(((i - 1) < 0 ? 0 : i - 1), ((i - 1) < 0 ? 2 : 3));
                                Console.WriteLine(addition);
                                i = 0;
                            }
                            else
                            {
                                i++;
                            }
                        }
                    }
                    var index = code.IndexOf(nonModifiedStatement);
                    lets.Add(new ThreeOPCreation { Index = index, Statements = addition });
                    //lets.Add(index, addition);
                    nonModifiedStatement = nonModifiedStatement.Replace(nonModifiedStatement.Substring(nonModifiedStatement.LastIndexOf("("), nonModifiedStatement.IndexOf(")") - nonModifiedStatement.LastIndexOf("(")), previousInt);
                    code[index] = nonModifiedStatement;
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
                if (statment.IndexOf(" ", StringComparison.Ordinal) < 0)
                {
                    words.Add(statment);
                    statment = "";
                }
                else
                {
                    words.Add(statment.Substring(0, statment.IndexOf(" ")));
                    statment = statment.Substring(statment.IndexOf(" ") + 1);
                }
            }
            return words;
        }
    }

    public struct ThreeOPCreation
    {
        public int Index { get; set; }
        public string Statements { get; set; }
    }
}
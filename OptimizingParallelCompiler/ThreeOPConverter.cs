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
        public static List<ThreeOPCreation> Transform(List<string> code, List<ThreeOPCreation> intStatements)
        {
            var counter = 0;
            //var intStatments = new List<ThreeOPCreation>();
            var letStatementCreation = new List<ThreeOPCreation>();

            code.ForEach(x =>
                {
                    var nonModifiedStatement = x;

                    x = x.Trim(' ', '\t');

                    var afterEqual = string.Empty;
                    var beforeEqual = string.Empty;

                    if (x.StartsWith("let"))
                    {
                        afterEqual = x.Substring(x.IndexOf("=") + 1, x.Length - (x.IndexOf("=") + 1));
                        beforeEqual = x.Substring(0, x.IndexOf("="));
                        Console.WriteLine(afterEqual);
                        Console.WriteLine(beforeEqual);
                    }
                    else
                    {
                        afterEqual = x;
                        Console.WriteLine(afterEqual);
                    }

                    var index = code.IndexOf(nonModifiedStatement);

                    if (x.StartsWith("let", StringComparison.Ordinal))
                    {
                        LetBeforeEqualTransformation(code, nonModifiedStatement, ref beforeEqual, ref counter, intStatements, letStatementCreation);
                        nonModifiedStatement = code[index];

                        LetAfterEqualTransformation(code, nonModifiedStatement, afterEqual, ref counter, intStatements, letStatementCreation);

                        nonModifiedStatement = code[index];

                        LetParenthesisTransformation(code, nonModifiedStatement, afterEqual, ref counter, intStatements, letStatementCreation);
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

                            intStatements.Add(new ThreeOPCreation { Index = 2, Statements = "int t_" + counter });
                            index = code.IndexOf(nonModifiedStatement);

                            var c = counter;
                            letStatementCreation.Add(new ThreeOPCreation { Index = index, Statements = "let " + "t_" + counter++ + " = " + arrayIndex });
                            letStatementCreation.Add(new ThreeOPCreation { Index = index, Statements = "let " + "t_" + counter + " = " + arrayName + "[t_" + c + "]" });
                            intStatements.Add(new ThreeOPCreation { Index = 2, Statements = "int t_" + counter });

                            code[index] = "if (t_" + counter++ + x.Substring(indexBracketEnd + 1, x.Length - (indexBracketEnd + 1));
                        }
                        else
                        {
                            var parenthesis = x.Substring(x.IndexOf("(") + 1, (x.IndexOf(")") - 1) - x.IndexOf("("));
                            Console.WriteLine(parenthesis);
                            var amount = Regex.Matches(parenthesis, "[-+/*]").Count;

                            var returned = EquatorTypeAmount(parenthesis);

                            if (returned.SpacesBefore > 1)
                            {
                                intStatements.Add(new ThreeOPCreation { Index = 2, Statements = "int t_" + counter });
                                var elements = new List<string>();

                                ValueExtration(elements, parenthesis);

                                letStatementCreation.Add(new ThreeOPCreation { Index = code.IndexOf(nonModifiedStatement), Statements = nonModifiedStatement.Substring(0, nonModifiedStatement.IndexOf("if", StringComparison.Ordinal)) + "let t_" + counter + " = " + elements });

                                parenthesis = nonModifiedStatement.Substring(0, nonModifiedStatement.IndexOf("if", StringComparison.Ordinal)) + "if (t_" + counter++ + " " + parenthesis + nonModifiedStatement.Substring(nonModifiedStatement.IndexOf(")", StringComparison.Ordinal), nonModifiedStatement.Length - nonModifiedStatement.IndexOf(")", StringComparison.Ordinal));

                                code[code.IndexOf(nonModifiedStatement)] = parenthesis;

                                Console.WriteLine(x.IndexOf(parenthesis));
                            }
                        }
                    }
                    else if (x.StartsWith("print", StringComparison.Ordinal))
                    {
                        Print(code, nonModifiedStatement, ref x, ref counter, letStatementCreation, intStatements);
                    }
                });

            return letStatementCreation;
        }

        private static void Print(List<string> code, string nonModifiedStatement, ref string x, ref int counter, List<ThreeOPCreation> lets, List<ThreeOPCreation> ints)
        {
            if (Regex.Matches(x, "[[]").Count == 1)
            {
                var indexBracketFront = x.IndexOf("[", 0, StringComparison.Ordinal);
                var indexBracketEnd = x.IndexOf("]", 0, StringComparison.Ordinal);
                var arrayIndex = x.Substring(indexBracketFront + 1, (indexBracketEnd - 1) - indexBracketFront);

                ints.Add(new ThreeOPCreation { Index = 2, Statements = "int t_" + counter });
                //intStatements += "int t_" + counter + Environment.NewLine;

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
                    ints.Add(new ThreeOPCreation { Index = 2, Statements = "int t_" + counter });
                    //intStatements += "int t_" + counter + Environment.NewLine;

                    code[code.IndexOf(nonModifiedStatement)] = code[code.IndexOf(nonModifiedStatement)].Replace(arrayName + "[t_" + c + "]", "t_" + counter++);
                }
                else
                {
                    var lastSpace = x.LastIndexOf(" ", StringComparison.Ordinal);
                    var arrayName = x.Substring(lastSpace + 1, indexBracketFront - lastSpace - 1);

                    var c = counter;
                    lets.Add(new ThreeOPCreation { Index = code.IndexOf(nonModifiedStatement), Statements = nonModifiedStatement.Substring(0, nonModifiedStatement.IndexOf("print", StringComparison.Ordinal)) + "let " + "t_" + counter++ + " = " + arrayIndex });
                    lets.Add(new ThreeOPCreation { Index = code.IndexOf(nonModifiedStatement), Statements = nonModifiedStatement.Substring(0, nonModifiedStatement.IndexOf("print", StringComparison.Ordinal)) + "let " + "t_" + counter + " = " + arrayName + "[t_" + c + "]" });
                    //lets.Add(code.IndexOf(nonModifiedStatement), nonModifiedStatement.Substring(0, nonModifiedStatement.IndexOf("print", StringComparison.Ordinal)) + "let " + "t_" + counter++ + " = " + arrayIndex + Environment.NewLine + nonModifiedStatement.Substring(0, nonModifiedStatement.IndexOf("print", StringComparison.Ordinal)) + "let " + "t_" + counter + " = " + arrayName + "[t_" + c + "]");
                    ints.Add(new ThreeOPCreation { Index = 2, Statements = "int t_" + counter });
                    //intStatements += "int t_" + counter + Environment.NewLine;

                    code[code.IndexOf(nonModifiedStatement)] = code[code.IndexOf(nonModifiedStatement)].Replace(arrayName + "[" + arrayIndex + "]", "t_" + counter++);
                    Console.WriteLine(lastSpace);
                }
            }
        }

        private static void LetBeforeEqualTransformation(List<string> code, string nonModifiedStatement, ref string statement, ref int counter, List<ThreeOPCreation> ints, List<ThreeOPCreation> lets)
        {
            if (Regex.Matches(statement, "[[]").Count > 0)
            {
                var indexBracketFront = statement.IndexOf("[", 0, StringComparison.Ordinal);
                var indexBracketEnd = statement.IndexOf("]", 0, StringComparison.Ordinal);

                var result = 0;
                var replace = string.Empty;

                if (
                    int.TryParse(
                        statement.Substring(indexBracketFront + 1, (indexBracketEnd - 1) - indexBracketFront),
                        out result) == false)
                {
                    ints.Add(new ThreeOPCreation { Index = 2, Statements = "int t_" + counter });
                    //intStatements += "int t_" + counter + Environment.NewLine;
                    lets.Add(new ThreeOPCreation
                    {
                        Index = code.IndexOf(nonModifiedStatement),
                        Statements = nonModifiedStatement.Substring(0, nonModifiedStatement.IndexOf("let")) + "let " + "t_" + counter + " = " +
                            statement.Substring(indexBracketFront + 1,
                            (indexBracketEnd - 1) - indexBracketFront)
                    });
                    //letStatement = "let " + "t_" + counter + " = " + line.Substring(indexBracketFront + 1,                                   (indexBracketEnd - 1) - indexBracketFront) + Environment.NewLine;

                    replace = "let" +
                              statement.Substring(statement.IndexOf(" ", StringComparison.Ordinal),
                                             indexBracketFront - statement.IndexOf(" ", StringComparison.Ordinal)) +
                              "[t_" + counter++ +
                              statement.Substring(indexBracketEnd, (statement.Length) - indexBracketEnd);
                }

                //lets.Add(new ThreeOPCreation { Index = code.IndexOf(nonModifiedStatement), Statements = letStatement });

                if (!string.IsNullOrEmpty(replace))
                {
                    code[code.IndexOf(nonModifiedStatement)] = code[code.IndexOf(nonModifiedStatement)].Replace(statement, replace);
                }
            }
        }

        private static void LetAfterEqualTransformation(List<string> code, string nonModifiedStatement, string statement, ref int counter, List<ThreeOPCreation> ints, List<ThreeOPCreation> lets)
        {
            var letStatement = string.Empty;
            if (Regex.Matches(statement, "[[]").Count > 0)
            {
                var countBracket = Regex.Matches(statement, "[[]").Count;

                var result = 0;
                var replace = string.Empty;
                var index = code.IndexOf(nonModifiedStatement);

                while (countBracket > 0)
                {
                    if (Regex.Matches(statement.Substring(0, statement.IndexOf("[")), " ").Count > 1)
                    {
                        var arrayname = statement.Substring(0, statement.IndexOf("["));
                        var spaceindex = arrayname.LastIndexOf(" ");
                        statement = statement.Substring(spaceindex);
                        Console.WriteLine(statement);
                    }

                    var indexBracketFront = statement.IndexOf("[", 0, StringComparison.Ordinal);
                    var indexBracketEnd = statement.IndexOf("]", 0, StringComparison.Ordinal);

                    var arrayIndex = statement.Substring(indexBracketFront + 1,
                                                         (indexBracketEnd - 1) - indexBracketFront);

                    if (int.TryParse(arrayIndex, out result))
                    {
                        ints.Add(new ThreeOPCreation { Index = 2, Statements = "int t_" + counter });
                        var arrayName = statement.Substring(statement.IndexOf(" ", StringComparison.Ordinal),
                                                            indexBracketFront - statement.IndexOf(" ", StringComparison.Ordinal));
                        letStatement = nonModifiedStatement.Substring(0, nonModifiedStatement.IndexOf("let")) + "let t_" + counter + " = " + arrayName + "[" + arrayIndex + "]";
                        code[index] = code[index].Replace(arrayName + "[" + arrayIndex + "]", " t_" + counter++);
                    }
                    else
                    {
                        ints.Add(new ThreeOPCreation { Index = 2, Statements = "int t_" + counter });
                        //intStatements += "int t_" + counter + Environment.NewLine;
                        var previousInt = "t_" + counter;
                        //letStatement += "let " + "t_" + counter++ + " = " + arrayIndex + Environment.NewLine;
                        lets.Add(new ThreeOPCreation
                        {
                            Index = index,
                            Statements = nonModifiedStatement.Substring(0, nonModifiedStatement.IndexOf("let")) + "let " + "t_" + counter++ + " = " +
                                arrayIndex
                        });
                        ints.Add(new ThreeOPCreation { Index = 2, Statements = "int t_" + counter });
                        //intStatements += "int t_" + counter + Environment.NewLine;
                        letStatement = nonModifiedStatement.Substring(0, nonModifiedStatement.IndexOf("let")) + "let t_" + counter + " = " + statement.Substring(statement.IndexOf(" ", StringComparison.Ordinal) + 1,
                             indexBracketFront - (statement.IndexOf(" ", StringComparison.Ordinal) + 1)) + "[" + previousInt + "]";
                        var arrayname = statement.Substring(0, indexBracketFront);
                        if (Regex.IsMatch(arrayname, " "))
                        {
                            arrayname = arrayname.Substring(arrayname.IndexOf(" "));
                        }

                        code[index] = code[index].Replace(arrayname + "[" + arrayIndex + "]", " t_" + counter++);
                    }

                    if (statement.Length > indexBracketEnd + 2)
                    {
                        statement = statement.Substring(indexBracketEnd + 2);
                    }

                    lets.Add(new ThreeOPCreation { Index = index, Statements = letStatement });

                    --countBracket;
                }
            }
        }

        private static void LetParenthesisTransformation(List<string> code, string nonModifiedStatement, string statement, ref int counter, List<ThreeOPCreation> ints, List<ThreeOPCreation> lets)
        {
            var index = code.IndexOf(nonModifiedStatement);
            if (Regex.Matches(statement, "[(]").Count > 0)
            {
                Console.WriteLine("first index = " + statement.IndexOf("(").ToString());
                Console.WriteLine("last index = " + statement.LastIndexOf("(").ToString());
                var isNested = statement.LastIndexOf("(") - statement.IndexOf("(") == 1 ? true : false;
                var elements = new List<string>();
                if (isNested)
                {

                    ValueExtration(elements, statement.Substring(statement.LastIndexOf("(") + 1, statement.IndexOf(")") - statement.LastIndexOf("(") - 1));
                    OderOfOperations(ref counter, ints, lets, code, ref nonModifiedStatement, elements, statement.Substring(statement.LastIndexOf("("), statement.IndexOf(")") - statement.LastIndexOf("(") + 1));
                    //var index = code.IndexOf(nonModifiedStatement);
                    //lets.Add(new ThreeOPCreation { Index = index, Statements = addition });
                    //lets.Add(index, addition);
                    
                    //code[index] = nonModifiedStatement;
                    Console.WriteLine(nonModifiedStatement);
                    index = code.IndexOf(nonModifiedStatement);
                    Console.WriteLine(code[index]);
                }
                else
                {
                    for (int i = 0; i <= Regex.Matches(statement, "[(]").Count; i++)
                    {
                        ValueExtration(elements, statement.Substring(statement.IndexOf("(") + 1, statement.IndexOf(")") - statement.IndexOf("(") - 1));
                        OderOfOperations(ref counter, ints, lets, code, ref nonModifiedStatement, elements, statement.Substring(statement.IndexOf("("), statement.IndexOf(")") - statement.IndexOf("(") + 1));
                        statement = statement.Substring(0, statement.IndexOf("(")) + statement.Substring(statement.IndexOf(")") + 1, statement.Length - statement.IndexOf(")") - 1);
                        Console.WriteLine(nonModifiedStatement);
                        index = code.IndexOf(nonModifiedStatement);
                        Console.WriteLine(code[index]);
                    }
                }
            }
        }

        private static void OderOfOperations(ref int counter, List<ThreeOPCreation> ints, List<ThreeOPCreation> lets, List<string> code, ref string nonModifiedStatement, List<string> elements, string statement)
        {
            var previousInt = "t_" + counter;
            var i = 0;
            var addition = string.Empty;
            while (elements.Count > 1)
            {
                if (elements.Count > 1)
                {
                    if (elements[i] == "*" || elements[i] == "/" || elements[i] == "%")
                    {
                        ints.Add(new ThreeOPCreation { Index = 2, Statements = "int t_" + counter });
                        //intStatements += "int t_" + counter + Environment.NewLine;
                        
                        var before = nonModifiedStatement.Substring(0, nonModifiedStatement.IndexOf("l"));
                        addition += before + "let t_" + counter + " = " + ((i - 1) < 0 ? previousInt : elements[i - 1]) + elements[i] + elements[i + 1] + Environment.NewLine;
                        lets.Add(new ThreeOPCreation { Index = code.IndexOf(nonModifiedStatement), Statements = before + "let t_" + counter + " = " + ((i - 1) < 0 ? previousInt : elements[i - 1]) + elements[i] + elements[i + 1] });
                        elements.RemoveRange(((i - 1) < 0 ? 0 : i - 1), ((i - 1) < 0 ? 2 : 3));
                        Console.WriteLine(addition);
                        
                        previousInt = "t_" + counter;
                        elements.Insert(i - 1, previousInt);
                        counter++;

                        i = 0;
                    }
                    else if (elements[i] == "+" && i > (elements.IndexOf("*") > 0 ? elements.IndexOf("*") : elements.IndexOf("/")))
                    {
                        ints.Add(new ThreeOPCreation { Index = 2, Statements = "int t_" + counter });
                        //intStatements += "int t_" + counter + Environment.NewLine;

                        var before = nonModifiedStatement.Substring(0, nonModifiedStatement.IndexOf("l"));
                        addition += before + "let t_" + counter + " = " + ((i - 1) < 0 ? previousInt : elements[i - 1]) + elements[i] + elements[i + 1] + Environment.NewLine;
                        lets.Add(new ThreeOPCreation { Index = code.IndexOf(nonModifiedStatement), Statements = before + "let t_" + counter + " = " + ((i - 1) < 0 ? previousInt : elements[i - 1]) + elements[i] + elements[i + 1] });
                        elements.RemoveRange(((i - 1) < 0 ? 0 : i - 1), ((i - 1) < 0 ? 2 : 3));
                        Console.WriteLine(addition);
                        previousInt = "t_" + counter;
                        elements.Insert(i - 1, previousInt);
                        counter++;
                        i = 0;
                    }
                    else
                    {
                        i++;
                    }
                }
            }

            elements.Clear();

            code[code.IndexOf(nonModifiedStatement)] = code[code.IndexOf(nonModifiedStatement)].Replace(statement, previousInt);
            nonModifiedStatement = nonModifiedStatement.Replace(statement, previousInt);
        }

        private static void ValueExtration(List<string> words, string statment)
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
        }
         
        private static Equator EquatorTypeAmount(string statement)
        {
            var type = "";
            var amount = 0;
            var s = statement;
            var spacesBefore = 0;
            var spacesAfter = 0;

            if (statement.IndexOf("!=") > 0)
            {
                Console.WriteLine("!=");
                type = "!=";
                amount = 2;
                spacesBefore = Regex.Matches(s.Substring(0, s.IndexOf(type)), " ").Count;
                Console.WriteLine(spacesBefore);
                spacesAfter = Regex.Matches(s.Substring(s.IndexOf(type)+ 1), " ").Count;
                Console.WriteLine(spacesBefore);
            }
            else if (statement.IndexOf(">") > 0)
            {
                statement = statement.Substring(statement.IndexOf(">"), 2);
                if (statement == ">=")
                {
                    Console.WriteLine(">=");
                    type = ">=";
                    amount = 2;
                    spacesBefore = Regex.Matches(s.Substring(0, s.IndexOf(type)), " ").Count;
                    Console.WriteLine(spacesBefore);
                    spacesAfter = Regex.Matches(s.Substring(s.IndexOf(type) + 1), " ").Count;
                    Console.WriteLine(spacesBefore);
                }
                else
                {
                    Console.WriteLine(">");
                    type = ">";
                    amount = 1;
                    spacesBefore = Regex.Matches(s.Substring(0, s.IndexOf(type)), " ").Count;
                    Console.WriteLine(spacesBefore);
                    spacesAfter = Regex.Matches(s.Substring(s.IndexOf(type) + 1), " ").Count;
                    Console.WriteLine(spacesBefore);
                }
            }
            else if (statement.IndexOf("<") > 0)
            {
                statement = statement.Substring(statement.IndexOf("<"), 2);
                if (statement == "<=")
                {
                    Console.WriteLine("<=");
                    type = "<=";
                    amount = 2;
                    spacesBefore = Regex.Matches(s.Substring(0, s.IndexOf(type)), " ").Count;
                    Console.WriteLine(spacesBefore);
                    spacesAfter = Regex.Matches(s.Substring(s.IndexOf(type) + 1), " ").Count;
                    Console.WriteLine(spacesBefore);
                }
                else
                {
                    Console.WriteLine("<");
                    type = "<";
                    amount = 1;
                    spacesBefore = Regex.Matches(s.Substring(0, s.IndexOf(type)), " ").Count;
                    Console.WriteLine(spacesBefore);
                    spacesAfter = Regex.Matches(s.Substring(s.IndexOf(type) + 1), " ").Count;
                    Console.WriteLine(spacesBefore);
                }
            }
            else if (statement.IndexOf("=") > 0)
            {
                statement = statement.Substring(statement.IndexOf("="), 2);
                if (statement == "==")
                {
                    Console.WriteLine("==");
                    type = "==";
                    amount = 2;
                    spacesBefore = Regex.Matches(s.Substring(0, s.IndexOf(type)), " ").Count;
                    Console.WriteLine(spacesBefore);
                    spacesAfter = Regex.Matches(s.Substring(s.IndexOf(type) + 1), " ").Count;
                    Console.WriteLine(spacesBefore);
                }
                else
                {
                    Console.WriteLine("=");
                    type = "=";
                    amount = 1;
                    spacesBefore = Regex.Matches(s.Substring(0, s.IndexOf(type)), " ").Count;
                    Console.WriteLine(spacesBefore);
                    spacesAfter = Regex.Matches(s.Substring(s.IndexOf(type) + 1), " ").Count;
                    Console.WriteLine(spacesBefore);
                }
            }

            return new Equator { Type = type, Amount = amount, SpacesBefore = spacesBefore, SpacesAfter = spacesAfter };
        }

        private struct Equator
        {
            public string Type { get; set; }
            public int Amount { get; set; }
            public int SpacesBefore { get; set; }
            public int SpacesAfter { get; set; }
        }
    }

    public class ThreeOPCreation
    {
        public int Index { get; set; }
        public string Statements { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace OptimizingParallelCompiler
{
    internal class ThreeOPConverter
    {
        public static void Transform(List<string> code, List<ThreeOPCreation> generate, List<ThreeOPCreation> letStatementCreation)
        {
            var counter = 0;

            code.ForEach(x =>
                {
                    var original = x;

                    x = x.Trim(' ', '\t');

                    var afterEqual = x;

                    var index = code.IndexOf(original);

                    if (x.StartsWith("let", StringComparison.Ordinal))
                    {
                        afterEqual = x.Substring(x.IndexOf("=") + 1, x.Length - (x.IndexOf("=") + 1));
                        var beforeEqual = x.Substring(0, x.IndexOf("="));

                        //InformationOutput.InformationPrint("original statement : " + original);
                        //InformationOutput.InformationPrint("before equal : " + beforeEqual);

                        OneArrayTransformation(ref original, beforeEqual, ref counter, generate, letStatementCreation, index, "let");

                        code[index] = original;

                        //InformationOutput.InformationPrint("original statement : " + original);
                        //InformationOutput.InformationPrint("code : " + code[index]);

                        LetAfterEqualTransformation(ref original, ref afterEqual, ref counter, generate, letStatementCreation, index);

                        code[index] = original;

                        LetParenthesisTransformation(ref original, ref afterEqual, ref counter, generate, letStatementCreation, index);

                        code[index] = original;

                        var elements = new List<string>();
                        afterEqual = afterEqual.Trim(' ');

                        ValueExtration(elements, afterEqual);

                        if (Regex.Matches(afterEqual, "[-+*/%]").Count < 2)
                        {
                            elements.Clear();
                        }

                        OderOfOperations(ref original, afterEqual, elements, ref counter, generate, letStatementCreation, index);

                        code[index] = original;
                    }
                    else if (x.StartsWith("print", StringComparison.Ordinal) && x.Contains("["))
                    {
                        //InformationOutput.InformationPrint("original statement : " + original);
                        //InformationOutput.InformationPrint("trimmed statement : " + x);

                        var array = x.Substring(0, x.IndexOf("["));
                        array = array.Substring(array.LastIndexOf(" ") + 1);
                        array += x.Substring(x.IndexOf("["), x.IndexOf("]") - x.IndexOf("[")) + "]";

                        OneArrayTransformation(ref original, array, ref counter, generate, letStatementCreation, index, "print");

                        code[index] = original;

                        //InformationOutput.InformationPrint("original statement : " + original);
                        //InformationOutput.InformationPrint("code statement : " + code[index]);
                    }
                    else if (x.StartsWith("if", StringComparison.Ordinal))
                    {
                        //InformationOutput.InformationPrint("original statement : " + original);
                        //InformationOutput.InformationPrint("trimmed statement : " + x);

                        //var begin = afterEqual.Substring(0, afterEqual.IndexOf(")"));

                        if (x.Contains("let") || x.Contains("goto") || x.Contains("print") || x.Contains("prompt"))
                        {
                            code.Insert(index + 1, x.Substring(x.IndexOf("then") + "then".Length));

                            original = original.Replace(x.Substring(x.IndexOf("then") + "then".Length), "");
                            x = x.Substring(0, x.IndexOf("then") + "then".Length);

                            InformationOutput.InformationPrint("original statement : " + original);
                            InformationOutput.InformationPrint("code statement : " + code[index]);

                            code[index] = original;

                            //var equator = EquatorTypeAmount(begin.Substring(begin.IndexOf("(") + 1, begin.IndexOf(")") - begin.IndexOf("(") - 1));
                            //begin = afterEqual.Substring(0, afterEqual.IndexOf("="));
                            //afterEqual = afterEqual.Substring(afterEqual.IndexOf("=") + 1);
                            //OneArrayTransformation(ref original, begin, ref counter, generate, letStatementCreation, index, " let");
                            //InformationOutput.InformationPrint("original statement : " + original);
                            //InformationOutput.InformationPrint("code statement : " + code[index]);
                            //OneArrayTransformation(ref original, original.Substring(original.IndexOf("(") + 1, original.IndexOf(equator.Type) - original.IndexOf("(") - 1), ref counter, generate, letStatementCreation, index, "if");
                            //InformationOutput.InformationPrint("original statement : " + original);
                            //InformationOutput.InformationPrint("code statement : " + code[index]);

                            //LetAfterEqualTransformation(ref original, ref afterEqual, ref counter, generate, letStatementCreation, index);
                            //InformationOutput.InformationPrint("original statement : " + original);
                            //InformationOutput.InformationPrint("code statement : " + code[index]);

                            //code[index] = original;
                        }
                        //var between = afterEqual.Substring(0, afterEqual.IndexOf(")") + 1);
                        var equator = EquatorTypeAmount(afterEqual.Substring(afterEqual.IndexOf("(") + 1, afterEqual.IndexOf(")") - afterEqual.IndexOf("(") - 1));

                        OneArrayTransformation(ref original, original.Substring(original.IndexOf("(") + 1, original.IndexOf(equator.Type) - original.IndexOf("(") - 1), ref counter, generate, letStatementCreation, index, "if");
                        InformationOutput.InformationPrint(original.Substring(original.IndexOf(equator.Type) + equator.Type.Length, original.IndexOf(")") - original.IndexOf(equator.Type) - equator.Type.Length));
                        OneArrayTransformation(ref original, original.Substring(original.IndexOf(equator.Type) + equator.Type.Length, original.IndexOf(")") - original.IndexOf(equator.Type) - equator.Type.Length), ref counter, generate, letStatementCreation, index, "if");
                        InformationOutput.InformationPrint(original);
                        equator = EquatorTypeAmount(original.Substring(original.IndexOf("(") + 1, original.IndexOf(")") - original.IndexOf("(") - 1));
                        if (equator.SpacesBefore > 1 || equator.SpacesAfter > 1)
                        {
                            var between = equator.SpacesBefore > 1 ? original.Substring(original.IndexOf("(") + 1, original.IndexOf(equator.Type) - original.IndexOf("(") - 1) : original.Substring(original.IndexOf(equator.Type) + equator.Type.Length, original.IndexOf(")") - original.IndexOf(equator.Type) - equator.Type.Length);
                            //LetParenthesisTransformation(ref original, ref between, ref counter, generate, letStatementCreation, index);
                            var elements = new List<string>();
                            between = between.Trim(' ');
                            ValueExtration(elements, between);
                            OderOfOperations(ref original, between, elements, ref counter, generate, letStatementCreation, index, "if");
                            InformationOutput.InformationPrint(original);
                        }

                        code[index] = original;
                    }
                });
        }

        private static void OneArrayTransformation(ref string originalStatement, string beforeEqual, ref int counter, List<ThreeOPCreation> generate, List<ThreeOPCreation> lets, int index, string type)
        {
            if (Regex.Matches(beforeEqual, "[[]").Count > 0)
            {
                var indexBracketFront = beforeEqual.IndexOf("[", 0, StringComparison.Ordinal);
                var indexBracketEnd = beforeEqual.IndexOf("]", 0, StringComparison.Ordinal);

                var result = 0;
                var replace = string.Empty;

                if (
                    int.TryParse(
                        beforeEqual.Substring(indexBracketFront + 1, (indexBracketEnd - 1) - indexBracketFront),
                        out result) == false)
                {
                    generate.Add(new ThreeOPCreation { Index = 2, Statements = "int t_" + counter });

                    lets.Add(new ThreeOPCreation
                    {
                        Index = index,
                        Statements = originalStatement.Substring(0, originalStatement.IndexOf(type)) + "let " + "t_" + counter + " = " +
                            beforeEqual.Substring(indexBracketFront + 1,
                            (indexBracketEnd - 1) - indexBracketFront)
                    });

                    var previous = "t_" + counter;

                    //replace = (!type.Contains("if") ? type +
                    //          beforeEqual.Substring(beforeEqual.IndexOf(" ", StringComparison.Ordinal),
                    //                         indexBracketFront - beforeEqual.IndexOf(" ", StringComparison.Ordinal)) +
                    //          "[t_" + counter++ +
                    //          beforeEqual.Substring(indexBracketEnd, (beforeEqual.Length) - indexBracketEnd) :
                    //          beforeEqual.Substring(0, indexBracketFront - 1) + "[t_" + counter++ + "]");

                    replace = beforeEqual.Substring(0, indexBracketFront) + "[t_" + counter++ + "]";

                    if (type.Contains("print") || type.Contains("if"))
                    {
                        originalStatement = originalStatement.Replace(beforeEqual, replace);
                        beforeEqual = beforeEqual.Replace(beforeEqual, replace);

                        indexBracketEnd = beforeEqual.IndexOf("]", 0, StringComparison.Ordinal);

                        generate.Add(new ThreeOPCreation { Index = 2, Statements = "int t_" + counter });

                        //var arrayName = !type.Contains("if") ? beforeEqual.Substring(beforeEqual.IndexOf(" ", StringComparison.Ordinal),
                        //                                    indexBracketFront - beforeEqual.IndexOf(" ", StringComparison.Ordinal))
                        //                                    : beforeEqual.Substring(0, indexBracketFront - 1);
                        var arrayName = beforeEqual.Substring(0, indexBracketFront);
                        lets.Add(new ThreeOPCreation
                        {
                            Index = index,
                            Statements = originalStatement.Substring(0, originalStatement.IndexOf(type)) + "let " + "t_" + counter + " = " +
                                arrayName + "[" + previous + "]"
                        });

                        //replace = (type == "print" ? type + " t_" + counter++ : " t_" + counter++ + " ");
                        replace = " t_" + counter++ + " ";
                    }

                    originalStatement = originalStatement.Replace(beforeEqual, replace);

                    InformationOutput.InformationPrint(originalStatement);
                }
            }
        }

        private static void LetAfterEqualTransformation(ref string originalStatement, ref string afterEqual, ref int counter, List<ThreeOPCreation> ints, List<ThreeOPCreation> lets, int index)
        {
            var letStatement = string.Empty;
            if (Regex.Matches(afterEqual, "[[]").Count > 0)
            {
                var countBracket = Regex.Matches(afterEqual, "[[]").Count;

                var result = 0;
                var replace = string.Empty;

                while (countBracket > 0)
                {
                    if (Regex.Matches(afterEqual.Substring(0, afterEqual.IndexOf("[")), " ").Count > 1)
                    {
                        var arrayname = afterEqual.Substring(0, afterEqual.IndexOf("["));
                        var spaceindex = arrayname.LastIndexOf(" ");
                        afterEqual = afterEqual.Substring(spaceindex);
                    }

                    var indexBracketFront = afterEqual.IndexOf("[", 0, StringComparison.Ordinal);
                    var indexBracketEnd = afterEqual.IndexOf("]", 0, StringComparison.Ordinal);

                    var arrayIndex = afterEqual.Substring(indexBracketFront + 1,
                                                         (indexBracketEnd - 1) - indexBracketFront);

                    if (int.TryParse(arrayIndex, out result))
                    {
                        ints.Add(new ThreeOPCreation { Index = 2, Statements = "int t_" + counter });
                        var arrayName = afterEqual.Substring(afterEqual.IndexOf(" ", StringComparison.Ordinal),
                                                            indexBracketFront - afterEqual.IndexOf(" ", StringComparison.Ordinal));
                        letStatement = originalStatement.Substring(0, originalStatement.IndexOf("let")) + "let t_" + counter + " = " + arrayName + "[" + arrayIndex + "]";
                        originalStatement = originalStatement.Replace(arrayName + "[" + arrayIndex + "]", " t_" + counter++);
                    }
                    else
                    {
                        ints.Add(new ThreeOPCreation { Index = 2, Statements = "int t_" + counter });
                        var previousInt = "t_" + counter;
                        lets.Add(new ThreeOPCreation
                        {
                            Index = index,
                            Statements = originalStatement.Substring(0, originalStatement.IndexOf("let")) + "let " + "t_" + counter++ + " = " +
                                arrayIndex
                        });
                        ints.Add(new ThreeOPCreation { Index = 2, Statements = "int t_" + counter });
                        letStatement = originalStatement.Substring(0, originalStatement.IndexOf("let")) + "let t_" + counter + " = " + afterEqual.Substring(afterEqual.IndexOf(" ", StringComparison.Ordinal) + 1,
                             indexBracketFront - (afterEqual.IndexOf(" ", StringComparison.Ordinal) + 1)) + "[" + previousInt + "]";
                        var arrayname = afterEqual.Substring(0, indexBracketFront);
                        if (Regex.IsMatch(arrayname, " "))
                        {
                            arrayname = arrayname.Substring(arrayname.IndexOf(" "));
                        }

                        originalStatement = originalStatement.Replace(arrayname + "[" + arrayIndex + "]", " t_" + counter++);
                    }

                    if (afterEqual.Length > indexBracketEnd + 2)
                    {
                        afterEqual = afterEqual.Substring(indexBracketEnd + 2);
                    }

                    lets.Add(new ThreeOPCreation { Index = index, Statements = letStatement });

                    --countBracket;
                }
            }

            afterEqual = originalStatement.Substring(originalStatement.IndexOf("=") + 1);
        }

        private static void LetParenthesisTransformation(ref string originalStatement, ref string afterEqual, ref int counter, List<ThreeOPCreation> ints, List<ThreeOPCreation> lets, int index)
        {
            if (Regex.Matches(afterEqual, "[(]").Count > 0)
            {
                var isNested = afterEqual.LastIndexOf("(") - afterEqual.IndexOf("(") == 1 ? true : false;
                var elements = new List<string>();
                if (isNested)
                {
                    while (isNested)
                    {
                        ValueExtration(elements, afterEqual.Substring(afterEqual.LastIndexOf("(") + 1, afterEqual.IndexOf(")") - afterEqual.LastIndexOf("(") - 1));
                        OderOfOperations(ref originalStatement, afterEqual.Substring(afterEqual.LastIndexOf("("), afterEqual.IndexOf(")") - afterEqual.LastIndexOf("(") + 1), elements, ref counter, ints, lets, index);
                        afterEqual = originalStatement.Substring(originalStatement.IndexOf("=") + 1);
                        isNested = afterEqual.LastIndexOf("(") - afterEqual.IndexOf("(") == 1 ? true : false;
                    }

                    ValueExtration(elements, afterEqual.Substring(afterEqual.IndexOf("(") + 1, afterEqual.IndexOf(")") - afterEqual.IndexOf("(") - 1));
                    OderOfOperations(ref originalStatement, afterEqual.Substring(afterEqual.IndexOf("("), afterEqual.IndexOf(")") - afterEqual.IndexOf("(") + 1), elements, ref counter, ints, lets, index);

                    afterEqual = originalStatement.Substring(originalStatement.IndexOf("=") + 1);
                }
                else
                {
                    for (int i = 0; i <= Regex.Matches(afterEqual, "[(]").Count; i++)
                    {
                        ValueExtration(elements, afterEqual.Substring(afterEqual.IndexOf("(") + 1, afterEqual.IndexOf(")") - afterEqual.IndexOf("(") - 1));
                        OderOfOperations(ref originalStatement, afterEqual.Substring(afterEqual.IndexOf("("), afterEqual.IndexOf(")") - afterEqual.IndexOf("(") + 1), elements, ref counter, ints, lets, index);
                        afterEqual = originalStatement.Substring(originalStatement.IndexOf("=") + 1);
                    }
                }
            }
        }

        private static void OderOfOperations(ref string originalStatement, string statement, List<string> elements, ref int counter, List<ThreeOPCreation> ints, List<ThreeOPCreation> lets, int index, string type = "")
        {
            var changedString = statement;

            if (elements.Count <= 2 || elements == null)
            {
                elements.Clear();
                return;
            }

            var previousInt = "t_" + counter;
            var i = 0;
            while (elements.Count > 1 && elements.Count > i)
            {
                originalStatement = originalStatement.Replace(statement, changedString);
                statement = statement.Replace(statement, changedString);

                if (Regex.Matches(originalStatement.Substring(originalStatement.IndexOf("=") + 1), "[-+/*%]").Count <= 1 && type != "if")
                {
                    changedString = changedString.Replace("(", "");
                    changedString = changedString.Replace(")", "");
                    originalStatement = originalStatement.Replace(statement, changedString);
                    elements.Clear();
                    return;
                }

                if (elements[i] == "*" || elements[i] == "/" || elements[i] == "%")
                {
                    ints.Add(new ThreeOPCreation { Index = 2, Statements = "int t_" + counter });
                    lets.Add(new ThreeOPCreation { Index = index, Statements = originalStatement.Substring(0, originalStatement.IndexOf(type)) + "let t_" + counter + " = " + ((i - 1) < 0 ? previousInt : elements[i - 1]) + elements[i] + elements[i + 1] });
                    changedString = changedString.Replace(((i - 1) < 0 ? previousInt : elements[i - 1]) + " " + elements[i] + " " + elements[i + 1], "t_" + counter);

                    elements.RemoveRange(((i - 1) < 0 ? 0 : i - 1), ((i - 1) < 0 ? 2 : 3));
                    previousInt = "t_" + counter;
                    elements.Insert(i - 1, previousInt);
                    counter++;

                    i = 0;
                }
                else
                {
                    ++i;
                }
            }

            originalStatement = originalStatement.Replace(statement, changedString);
            statement = statement.Replace(statement, changedString);

            if (Regex.Matches(originalStatement.Substring(originalStatement.IndexOf("=") + 1), "[-+/*%]").Count <= 1 && type != "if")
            {
                changedString = changedString.Replace("(", "");
                changedString = changedString.Replace(")", "");
                originalStatement = originalStatement.Replace(statement, changedString);
                elements.Clear();
                return;
            }

            i = 0;
            while (elements.Count > 1 && elements.Count > i)
            {
                originalStatement = originalStatement.Replace(statement, changedString);
                statement = statement.Replace(statement, changedString);

                if (Regex.Matches(originalStatement.Substring(originalStatement.IndexOf("=") + 1), "[-+/*%]").Count <= 1 && type != "if")
                {
                    changedString = changedString.Replace("(", "");
                    changedString = changedString.Replace(")", "");
                    originalStatement = originalStatement.Replace(statement, changedString);
                    elements.Clear();
                    return;
                }

                if (elements[i] == "+" || elements[i] == "-")
                {
                    ints.Add(new ThreeOPCreation { Index = 2, Statements = "int t_" + counter });
                    lets.Add(new ThreeOPCreation { Index = index, Statements = originalStatement.Substring(0, originalStatement.IndexOf(type)) + "let t_" + counter + " = " + ((i - 1) < 0 ? previousInt : elements[i - 1]) + elements[i] + elements[i + 1] });
                    changedString = changedString.Replace(((i - 1) < 0 ? previousInt : elements[i - 1]) + " " + elements[i] + " " + elements[i + 1], "t_" + counter);
                    elements.RemoveRange(((i - 1) < 0 ? 0 : i - 1), ((i - 1) < 0 ? 2 : 3));
                    previousInt = "t_" + counter;
                    elements.Insert(i - 1, previousInt);
                    counter++;
                    i = 0;
                }
                else
                {
                    ++i;
                }
            }

            elements.Clear();

            originalStatement = originalStatement.Replace(statement, previousInt);
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
                type = "!=";
                amount = 2;
                spacesBefore = Regex.Matches(s.Substring(0, s.IndexOf(type)), " ").Count;
                spacesAfter = Regex.Matches(s.Substring(s.IndexOf(type)+ 1), " ").Count;
            }
            else if (statement.IndexOf(">") > 0)
            {
                statement = statement.Substring(statement.IndexOf(">"), 2);
                if (statement == ">=")
                {
                    type = ">=";
                    amount = 2;
                    spacesBefore = Regex.Matches(s.Substring(0, s.IndexOf(type)), " ").Count;
                    spacesAfter = Regex.Matches(s.Substring(s.IndexOf(type) + 1), " ").Count;
                }
                else
                {
                    type = ">";
                    amount = 1;
                    spacesBefore = Regex.Matches(s.Substring(0, s.IndexOf(type)), " ").Count;
                    spacesAfter = Regex.Matches(s.Substring(s.IndexOf(type) + 1), " ").Count;
                }
            }
            else if (statement.IndexOf("<") > 0)
            {
                statement = statement.Substring(statement.IndexOf("<"), 2);
                if (statement == "<=")
                {
                    type = "<=";
                    amount = 2;
                    spacesBefore = Regex.Matches(s.Substring(0, s.IndexOf(type)), " ").Count;
                    spacesAfter = Regex.Matches(s.Substring(s.IndexOf(type) + 1), " ").Count;
                }
                else
                {
                    type = "<";
                    amount = 1;
                    spacesBefore = Regex.Matches(s.Substring(0, s.IndexOf(type)), " ").Count;
                    spacesAfter = Regex.Matches(s.Substring(s.IndexOf(type) + 1), " ").Count;
                }
            }
            else if (statement.IndexOf("=") > 0)
            {
                statement = statement.Substring(statement.IndexOf("="), 2);
                if (statement == "==")
                {
                    type = "==";
                    amount = 2;
                    spacesBefore = Regex.Matches(s.Substring(0, s.IndexOf(type)), " ").Count;
                    spacesAfter = Regex.Matches(s.Substring(s.IndexOf(type) + 1), " ").Count;
                }
                else
                {
                    type = "=";
                    amount = 1;
                    spacesBefore = Regex.Matches(s.Substring(0, s.IndexOf(type)), " ").Count;
                    spacesAfter = Regex.Matches(s.Substring(s.IndexOf(type) + 1), " ").Count;
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
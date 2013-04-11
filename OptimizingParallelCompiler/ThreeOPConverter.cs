using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace OptimizingParallelCompiler
{
    internal class ThreeOPConverter
    {
        /// <summary>
        /// Transform's O'Neil code into 3OP O'Neil code
        /// </summary>
        /// <param name="code">the untransformed O'Neil Code</param>
        /// <param name="intStatements">an empty list of ThreeOPCreation type for int's that may or may not be created</param>
        /// <param name="letStatements">an empty list of ThreeOPCreation type for let statements that may or may not be created</param>
        /// <returns>the code that has been 3OP transformed</returns>
        public static string[] Transform(List<string> code, List<ThreeOPCode> intStatements, List<ThreeOPCode> letStatements)
        {
            var counter = 0;

            //goes through each line of code
            code.ForEach(x =>
                {
                    //unmodified statement
                    var original = x;

                    x = x.Trim(' ', '\t');

                    var afterEqual = x;

                    //index in the code list
                    var index = code.IndexOf(original);

                    if (x.StartsWith("let", StringComparison.Ordinal))
                    {
                        //checks to see if there is a multidimensional array
                        if (Regex.Matches(x, ",").Count <= 0)
                        {
                            afterEqual = x.Substring(x.IndexOf("=") + 1, x.Length - (x.IndexOf("=") + 1));
                            var beforeEqual = x.Substring(0, x.IndexOf("="));

                            OneArrayTransformation(ref original, beforeEqual, ref counter, intStatements, letStatements, index, "let");

                            code[index] = original;

                            AfterEqualTransformation(ref original, ref afterEqual, ref counter, intStatements, letStatements, index);

                            code[index] = original;

                            ParenthesisTransformation(ref original, ref afterEqual, ref counter, intStatements, letStatements, index);

                            code[index] = original;

                            var elements = new List<string>();
                            afterEqual = afterEqual.Trim(' ');

                            ValueExtration(elements, afterEqual);

                            if (Regex.Matches(afterEqual, "[-+*/%]").Count <= 2)
                            {
                                elements.Clear();
                            }

                            OderOfOperations(ref original, afterEqual, elements, ref counter, intStatements, letStatements, index);

                            code[index] = original;
                        }
                    }
                    else if (x.StartsWith("print", StringComparison.Ordinal) && x.Contains("["))
                    {
                        var array = x.Substring(0, x.IndexOf("["));
                        array = array.Substring(array.LastIndexOf(" ") + 1);
                        array += x.Substring(x.IndexOf("["), x.IndexOf("]") - x.IndexOf("[")) + "]";

                        OneArrayTransformation(ref original, array, ref counter, intStatements, letStatements, index, "print");

                        code[index] = original;
                    }
                    else if (x.StartsWith("if", StringComparison.Ordinal))
                    {
                        if (x.Contains("let") || x.Contains("goto") || x.Contains("print") || x.Contains("prompt"))
                        {
                            code.Insert(index + 1, original.Substring(0, original.IndexOf("if")) + " " + x.Substring(x.IndexOf("then") + "then".Length));

                            original = original.Replace(x.Substring(x.IndexOf("then") + "then".Length), "");
                            x = x.Substring(0, x.IndexOf("then") + "then".Length);

                            code[index] = original;
                        }
                        var equator = EquatorTypeAmount(afterEqual.Substring(afterEqual.IndexOf("(") + 1, afterEqual.IndexOf(")") - afterEqual.IndexOf("(") - 1));

                        OneArrayTransformation(ref original, original.Substring(original.IndexOf("(") + 1, original.IndexOf(equator.Type) - original.IndexOf("(") - 1), ref counter, intStatements, letStatements, index, "if");
                        
                        OneArrayTransformation(ref original, original.Substring(original.IndexOf(equator.Type) + equator.Type.Length, original.IndexOf(")") - original.IndexOf(equator.Type) - equator.Type.Length), ref counter, intStatements, letStatements, index, "if");
                        
                        equator = EquatorTypeAmount(original.Substring(original.IndexOf("(") + 1, original.IndexOf(")") - original.IndexOf("(") - 1));
                        if (equator.SpacesBefore > 1 || equator.SpacesAfter > 1)
                        {
                            var between = equator.SpacesBefore > 1 ? original.Substring(original.IndexOf("(") + 1, original.IndexOf(equator.Type) - original.IndexOf("(") - 1) : original.Substring(original.IndexOf(equator.Type) + equator.Type.Length, original.IndexOf(")") - original.IndexOf(equator.Type) - equator.Type.Length);
                            
                            var elements = new List<string>();
                            between = between.Trim(' ');
                            ValueExtration(elements, between);
                            OderOfOperations(ref original, between, elements, ref counter, intStatements, letStatements, index, "if");
                        }

                        code[index] = original;
                    }
                });

            return code.ToArray();
        }

        /// <summary>
        /// transforms ifs, lets, and prints/prompts arrays into threeOP code
        /// </summary>
        /// <param name="original">the original statement from the code</param>
        /// <param name="statement">the statement to be checked/changed</param>
        /// <param name="counter">counter for temp variable creation</param>
        /// <param name="intStatements">list of temp variables creation</param>
        /// <param name="letStatements">list of temp variables value set</param>
        /// <param name="index">where the specific line of code resides in the program</param>
        /// <param name="type">the type of statement being sent in</param>
        private static void OneArrayTransformation(ref string original, string statement, ref int counter, List<ThreeOPCode> intStatements, List<ThreeOPCode> letStatements, int index, string type)
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
                    intStatements.Add(new ThreeOPCode { Index = 2, Statement = "int t_" + counter });

                    letStatements.Add(new ThreeOPCode
                    {
                        Index = index,
                        Statement = original.Substring(0, original.IndexOf(type)) + "let " + "t_" + counter + " = " +
                            statement.Substring(indexBracketFront + 1,
                            (indexBracketEnd - 1) - indexBracketFront)
                    });

                    var previous = "t_" + counter;

                    replace = statement.Substring(0, indexBracketFront) + "[t_" + counter++ + "]";

                    if (type.Contains("print") || type.Contains("if"))
                    {
                        original = original.Replace(statement, replace);
                        statement = statement.Replace(statement, replace);

                        indexBracketEnd = statement.IndexOf("]", 0, StringComparison.Ordinal);

                        intStatements.Add(new ThreeOPCode { Index = 2, Statement = "int t_" + counter });

                        var arrayName = statement.Substring(0, indexBracketFront);
                        letStatements.Add(new ThreeOPCode
                        {
                            Index = index,
                            Statement = original.Substring(0, original.IndexOf(type)) + "let " + "t_" + counter + " = " +
                                arrayName + "[" + previous + "]"
                        });

                        replace = " t_" + counter++ + " ";
                    }

                    original = original.Replace(statement, replace);
                }
            }
        }

        /// <summary>
        /// transform's statements that have multiple arrays
        /// </summary>
        /// <param name="original">the original statement from the code</param>
        /// <param name="statement">the statement to be checked/changed</param>
        /// <param name="counter">counter for temp variable creation</param>
        /// <param name="intStatements">list of temp variables creation</param>
        /// <param name="letStatements">list of temp variables value set</param>
        /// <param name="index">where the specific line of code resides in the program</param>
        private static void AfterEqualTransformation(ref string original, ref string statement, ref int counter, List<ThreeOPCode> ints, List<ThreeOPCode> letStatements, int index)
        {
            var letStatement = string.Empty;

            //checks to see if there is an array    if not, it returns without doing anything
            if (Regex.Matches(statement, "[[]").Count > 0)
            {
                //counts the number of arrays in the statement
                var countBracket = Regex.Matches(statement, "[[]").Count;

                var result = 0;
                var replace = string.Empty;

                // turns the array accesses into 3OP
                while (countBracket > 0)
                {
                    if (Regex.Matches(statement.Substring(0, statement.IndexOf("[")), " ").Count > 1)
                    {
                        var arrayname = statement.Substring(0, statement.IndexOf("["));
                        var spaceindex = arrayname.LastIndexOf(" ");
                        statement = statement.Substring(spaceindex);
                    }

                    var indexBracketFront = statement.IndexOf("[", 0, StringComparison.Ordinal);
                    var indexBracketEnd = statement.IndexOf("]", 0, StringComparison.Ordinal);

                    var arrayIndex = statement.Substring(indexBracketFront + 1,
                                                         (indexBracketEnd - 1) - indexBracketFront);

                    // dont remember why i did this
                    if (int.TryParse(arrayIndex, out result))
                    {
                        ints.Add(new ThreeOPCode { Index = 2, Statement = "int t_" + counter });
                        var arrayName = statement.Substring(statement.IndexOf(" ", StringComparison.Ordinal),
                                                            indexBracketFront - statement.IndexOf(" ", StringComparison.Ordinal));
                        letStatement = original.Substring(0, original.IndexOf("let")) + "let t_" + counter + " = " + arrayName + "[" + arrayIndex + "]";
                        original = original.Replace(arrayName + "[" + arrayIndex + "]", " t_" + counter++);
                    }
                    else
                    {
                        ints.Add(new ThreeOPCode { Index = 2, Statement = "int t_" + counter });
                        var previousInt = "t_" + counter;
                        letStatements.Add(new ThreeOPCode
                        {
                            Index = index,
                            Statement = original.Substring(0, original.IndexOf("let")) + "let " + "t_" + counter++ + " = " +
                                arrayIndex
                        });
                        ints.Add(new ThreeOPCode { Index = 2, Statement = "int t_" + counter });
                        letStatement = original.Substring(0, original.IndexOf("let")) + "let t_" + counter + " = " + statement.Substring(statement.IndexOf(" ", StringComparison.Ordinal) + 1,
                             indexBracketFront - (statement.IndexOf(" ", StringComparison.Ordinal) + 1)) + "[" + previousInt + "]";
                        var arrayname = statement.Substring(0, indexBracketFront);
                        if (Regex.IsMatch(arrayname, " "))
                        {
                            arrayname = arrayname.Substring(arrayname.IndexOf(" "));
                        }

                        original = original.Replace(arrayname + "[" + arrayIndex + "]", " t_" + counter++);
                    }

                    if (statement.Length > indexBracketEnd + 2)
                    {
                        statement = statement.Substring(indexBracketEnd + 2);
                    }

                    letStatements.Add(new ThreeOPCode { Index = index, Statement = letStatement });

                    --countBracket;
                }
            }

            statement = original.Substring(original.IndexOf("=") + 1);
        }

        /// <summary>
        /// transforms parenthesis statements into 3OP code
        /// </summary>
        /// <param name="original">the original statement from the code</param>
        /// <param name="statement">the statement to be checked/changed</param>
        /// <param name="counter">counter for temp variable creation</param>
        /// <param name="intStatements">list of temp variables creation</param>
        /// <param name="letStatements">list of temp variables value set</param>
        /// <param name="index">where the specific line of code resides in the program</param>
        private static void ParenthesisTransformation(ref string original, ref string statement, ref int counter, List<ThreeOPCode> intStatements, List<ThreeOPCode> letStatements, int index)
        {
            if (Regex.Matches(statement, "[(]").Count > 0)
            {
                var isNested = statement.LastIndexOf("(") - statement.IndexOf("(") == 1 ? true : false;
                var elements = new List<string>();
                if (isNested)
                {
                    while (isNested)
                    {
                        ValueExtration(elements, statement.Substring(statement.LastIndexOf("(") + 1, statement.IndexOf(")") - statement.LastIndexOf("(") - 1));
                        OderOfOperations(ref original, statement.Substring(statement.LastIndexOf("("), statement.IndexOf(")") - statement.LastIndexOf("(") + 1), elements, ref counter, intStatements, letStatements, index);
                        statement = original.Substring(original.IndexOf("=") + 1);
                        isNested = statement.LastIndexOf("(") - statement.IndexOf("(") == 1 ? true : false;
                    }

                    ValueExtration(elements, statement.Substring(statement.IndexOf("(") + 1, statement.IndexOf(")") - statement.IndexOf("(") - 1));
                    OderOfOperations(ref original, statement.Substring(statement.IndexOf("("), statement.IndexOf(")") - statement.IndexOf("(") + 1), elements, ref counter, intStatements, letStatements, index);

                    statement = original.Substring(original.IndexOf("=") + 1);
                }
                else
                {
                    for (int i = 0; i <= Regex.Matches(statement, "[(]").Count; i++)
                    {
                        ValueExtration(elements, statement.Substring(statement.IndexOf("(") + 1, statement.IndexOf(")") - statement.IndexOf("(") - 1));
                        OderOfOperations(ref original, statement.Substring(statement.IndexOf("("), statement.IndexOf(")") - statement.IndexOf("(") + 1), elements, ref counter, intStatements, letStatements, index);
                        statement = original.Substring(original.IndexOf("=") + 1);
                    }
                }
            }
        }

        /// <summary>
        /// does order of operations for all statements that are not 3OP already
        /// </summary>
        /// <param name="original">the original statement from the code</param>
        /// <param name="statement">the statement to be checked/changed</param>
        /// <param name="elements">the list of variables/constants</param>
        /// <param name="counter">counter for temp variable creation</param>
        /// <param name="intStatements">list of temp variables creation</param>
        /// <param name="letStatements">list of temp variables value set</param>
        /// <param name="index">where the specific line of code resides in the program</param>
        /// <param name="type">the type of statement being sent in</param>
        private static void OderOfOperations(ref string original, string statement, List<string> elements, ref int counter, List<ThreeOPCode> intStatements, List<ThreeOPCode> letStatements, int index, string type = "")
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
                original = original.Replace(statement, changedString);
                statement = statement.Replace(statement, changedString);

                if (Regex.Matches(original.Substring(original.IndexOf("=") + 1), "[-+/*%]").Count <= 1 && type != "if")
                {
                    changedString = changedString.Replace("(", "");
                    changedString = changedString.Replace(")", "");
                    original = original.Replace(statement, changedString);
                    elements.Clear();
                    return;
                }

                if (elements[i] == "*" || elements[i] == "/" || elements[i] == "%")
                {
                    intStatements.Add(new ThreeOPCode { Index = 2, Statement = "int t_" + counter });
                    letStatements.Add(new ThreeOPCode { Index = index, Statement = original.Substring(0, original.IndexOf(type)) + "let t_" + counter + " = " + ((i - 1) < 0 ? previousInt : elements[i - 1]) + elements[i] + elements[i + 1] });
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

            original = original.Replace(statement, changedString);
            statement = statement.Replace(statement, changedString);

            if (Regex.Matches(original.Substring(original.IndexOf("=") + 1), "[-+/*%]").Count <= 1 && type != "if")
            {
                changedString = changedString.Replace("(", "");
                changedString = changedString.Replace(")", "");
                original = original.Replace(statement, changedString);
                elements.Clear();
                return;
            }

            i = 0;
            while (elements.Count > 1 && elements.Count > i)
            {
                original = original.Replace(statement, changedString);
                statement = statement.Replace(statement, changedString);

                if (Regex.Matches(original.Substring(original.IndexOf("=") + 1), "[-+/*%]").Count <= 1 && type != "if")
                {
                    changedString = changedString.Replace("(", "");
                    changedString = changedString.Replace(")", "");
                    original = original.Replace(statement, changedString);
                    elements.Clear();
                    return;
                }

                if (elements[i] == "+" || elements[i] == "-")
                {
                    intStatements.Add(new ThreeOPCode { Index = 2, Statement = "int t_" + counter });
                    letStatements.Add(new ThreeOPCode { Index = index, Statement = original.Substring(0, original.IndexOf(type)) + "let t_" + counter + " = " + ((i - 1) < 0 ? previousInt : elements[i - 1]) + elements[i] + elements[i + 1] });
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

            original = original.Replace(statement, previousInt);
        }

        /// <summary>
        /// creates a list of the variables/constants in a non 3OP statement
        /// </summary>
        /// <param name="words">returned list of the statement</param>
        /// <param name="statment">statement of variables/constants to be separated</param>
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
         
        /// <summary>
        /// finds the logical operator and returns all the information about it
        /// </summary>
        /// <param name="statement">the statement that has a logical operator</param>
        /// <returns>returns a filled logical operator structure</returns>
        private static LogicalOperator EquatorTypeAmount(string statement)
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

            return new LogicalOperator { Type = type, Amount = amount, SpacesBefore = spacesBefore, SpacesAfter = spacesAfter };
        }

        /// <summary>
        /// holds information about a logical operator
        /// </summary>
        private struct LogicalOperator
        {
            /// <summary>
            /// the actual representation of the logical operator
            /// </summary>
            public string Type { get; set; }
            public int Amount { get; set; }
            /// <summary>
            /// the amount of spaces that come before it from (
            /// </summary>
            public int SpacesBefore { get; set; }
            /// <summary>
            /// the amount of spaces after operator but before )
            /// </summary>
            public int SpacesAfter { get; set; }
        }
    }

    /// <summary>
    /// class that holds index and statement of the temp variables
    /// </summary>
    public class ThreeOPCode
    {
        /// <summary>
        /// Where the index in the program where the new statement will go
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// the three op statement
        /// </summary>
        public string Statement { get; set; }
    }
}
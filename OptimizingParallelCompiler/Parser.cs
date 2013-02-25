using System;
using System.Collections.Generic;
using System.Linq;

namespace OptimizingParallelCompiler
{
    public class Parser
    {
        /// <summary>
        /// Transforms O'Neil code to C#
        /// </summary>
        /// <param name="code">Takes in a (referenced) list of type string of the code in the txtOneil textbox</param>
        public static void Transform(List<string> code)
        {
            var listOfEndFors = new List<string>();

            //replaces title with // and always inserts the using statements
            code[0] += @"using System;\nclass Program{";
            code[0] = code[0].Replace("title", "//");

            var labelCounter = 0;

            //one loop that transforms the code to c#
            code.ForEach(s =>
                {
                    //creates an unmodified instance of the current line
                    var other = s;
                    //removes all trailing spaces and tabs
                    s = s.TrimEnd('\t', ' ');
                    //counts the number of tabs or spaces for pretty code
                    var count = s.Count(x => x.Equals('\t') || x.Equals(' '));
                    //trims beginning of sentence
                    s = s.TrimStart('\t', ' ');

                    //does the while loop
                    if (s.IndexOf("while", StringComparison.Ordinal) == 0)
                    {
                       WhileTransform(count, other, s, code, ref labelCounter);
                    }
                    else if (s.IndexOf("endfor", StringComparison.Ordinal) == 0)
                    {
                        var index = code.IndexOf(other);
                        //take the last string off the list
                        code[index] = listOfEndFors[listOfEndFors.Count - 1];
                        listOfEndFors.RemoveAt(listOfEndFors.Count - 1);
                    }
                    else if (s.IndexOf("for", StringComparison.Ordinal) == 0)
                    {
                        ForTransform(s, code, other, ref labelCounter, listOfEndFors);
                    }
                    else if (s.IndexOf("end", StringComparison.Ordinal) == 0 && s.Length == 3)
                    {
                        var index = code.IndexOf(other);
                        code[index] = "\t}\n}";
                    }
                    else if (s.IndexOf("prompt", StringComparison.Ordinal) == 0 || s.IndexOf("print", StringComparison.Ordinal) == 0)
                    {
                        var sentence = s;
                        int index;
                        if (s.IndexOf("prompt", StringComparison.Ordinal) == 0)
                        {
                            sentence = sentence.Replace("prompt", "Console.Write(");
                            index = code.IndexOf(other);
                        }
                        else
                        {
                            sentence = sentence.Replace("print", "Console.Write(");
                            index = code.IndexOf(other);
                        }
                        sentence += ");";
                        code[index] = code[index].Replace(s, sentence);
                    }
                    else if (s.IndexOf("rem", StringComparison.Ordinal) == 0)
                    {
                        var sentence = s;
                        sentence = sentence.Replace("rem", "//");
                        var index = code.IndexOf(other);
                        code[index] = code[index].Replace(s, sentence);
                    }

                    else if (s.IndexOf("list", StringComparison.Ordinal) == 0)
                    {
                        var sentence = s;
                        var arrayBegin = sentence.IndexOf("[", StringComparison.Ordinal) + 1;
                        var arrayEnd = sentence.IndexOf("]", StringComparison.Ordinal);
                        var value = sentence.Substring(arrayBegin, arrayEnd - arrayBegin);
                        sentence = sentence.Substring(arrayEnd + 1, sentence.Length - (arrayEnd + 1));
                        sentence = "int[] " + sentence + " = new int[" + value + "];";
                        var index = code.IndexOf(other);
                        code[index] = code[index].Replace(s, sentence);
                    }
                    else if (s.IndexOf("let", StringComparison.Ordinal) == 0)
                    {
                        var sentence = s;
                        sentence = sentence.Substring("let".Length, sentence.Length - "let".Length);
                        sentence = sentence + ";";
                        var index = code.IndexOf(other);
                        code[index] = code[index].Replace(s, sentence);
                    }
                    else if (s.IndexOf("input", StringComparison.Ordinal) == 0)
                    {
                        var sentence = s;
                        sentence = sentence.Replace("input", "");
                        var index = code.IndexOf(other);
                        code[index] = code[index].Replace(s, sentence + " = Convert.ToInt32(Console.ReadLine());");
                    }
                    else if (s.IndexOf("int", StringComparison.Ordinal) == 0)
                    {
                        var index = code.IndexOf(other);
                        code[index] = code[index].Replace(s, s + ";");
                    }
                    else if (s.IndexOf("if", StringComparison.Ordinal) == 0 && s.Contains("then"))
                    {
                        var index = code.IndexOf(other);
                        var statement = s.Substring(s.IndexOf("then", StringComparison.Ordinal) + "then".Length,
                                                    s.Length -
                                                    (s.IndexOf("then", StringComparison.Ordinal) + "then".Length));

                        var equator = s.Substring(0, s.IndexOf(")", StringComparison.Ordinal) + 1);

                        if (equator.Contains("!="))
                        {
                            equator = equator.Replace("!=", "==");
                        }
                        else if (equator.Contains("=="))
                        {
                            equator = equator.Replace("==", "!=");
                        }
                        else if (equator.Contains("<") && !equator.Contains("<="))
                        {
                            equator = equator.Replace("<", ">=");
                        }
                        else if (equator.Contains(">") && !equator.Contains(">="))
                        {
                            equator = equator.Replace(">", "<=");
                        }
                        else if (equator.Contains("<="))
                        {
                            equator = equator.Replace("<=", ">");
                        }
                        else if (equator.Contains(">="))
                        {
                            equator = equator.Replace(">=", "<");
                        }

                        if (statement.Contains("goto"))
                        {
                            var sentence = s;
                            sentence = sentence.Replace(statement, "");
                            sentence = sentence.Replace("then", statement);
                            sentence += ";";
                            code[index] = code[index].Replace(s, sentence);
                        }
                        else if (statement.Contains("print"))
                        {
                            statement = statement.Replace("print", "Console.Write(");
                            var label = "label" + labelCounter;
                            statement = " goto " + label + "; " + statement;
                            statement += ");";
                            equator += statement;
                            code[index] = code[index].Replace(s, equator);
                            statement = code[index + 2] + "\n" + label + ":";
                            code[index + 2] = code[index + 2].Replace(code[index + 2], statement);
                            ++labelCounter;
                        }
                        else if (statement.Contains("prompt"))
                        {
                            statement = statement.Replace("prompt", "Console.Write(");
                            var label = "label" + labelCounter;
                            statement = " goto " + label + "; " + statement;
                            statement += ");";
                            equator += statement;
                            code[index] = code[index].Replace(s, equator);
                            statement = code[index + 2] + "\n" + label + ":";
                            code[index + 2] = code[index + 2].Replace(code[index + 2], statement);
                            ++labelCounter;
                        }
                        else if (statement.Contains("let"))
                        {
                            statement = statement.Replace("let", "");
                            var label = "label" + labelCounter;
                            statement = " goto " + label + "; " + statement;
                            statement += ";";
                            equator += statement;
                            code[index] = code[index].Replace(s, equator);
                            statement = code[index + 2];
                            statement += "\n" + label + ":";
                            code[index + 2] = code[index + 2].Replace(code[index + 2], statement);
                            ++labelCounter;
                        }
                        else
                        {
                            var label = "label" + labelCounter;
                            statement += "goto " + label + ";";
                            equator += statement;
                            code[index] = code[index].Replace(s, equator);
                            statement = code[index + 2];
                            statement += statement + "\n" + label + ":";
                            code[index + 2] = code[index + 2].Replace(code[index + 2], statement);
                            ++labelCounter;
                        }
                    }
                    else if (s.IndexOf("goto", StringComparison.Ordinal) == 0)
                    {
                        var sentence = s;
                        sentence += ";";
                        var index = code.IndexOf(other);
                        code[index] = code[index].Replace(s, sentence);
                    }
                    else if (s.IndexOf("label", StringComparison.Ordinal) == 0 && !s.Contains(":"))
                    {
                        var sentence = s;
                        var index = code.IndexOf(other);
                        sentence += ":";
                        if (s.Contains(" "))
                        {
                            sentence = sentence.Replace("label", "");
                            sentence = sentence.Replace(" ", "");
                        }
                        code[index] = code[index].Replace(s, sentence);
                    }
                });
        }

        private static void ForTransform(string s, List<string> code, string other, ref int labelCounter, List<string> listOfEndFors)
        {
            var index = code.IndexOf(other);

            //variable for list of statements
            string endForString;

            var id = s.IndexOf("for", StringComparison.Ordinal) + 4;
            var end = s.IndexOf("to", StringComparison.Ordinal) - 1;
            var value = "\t\t" + s.Substring(id, end - id) + ";\n";
            var value1 = "\t" + value.Substring(2, value.IndexOf("=", StringComparison.Ordinal) - 2) +
                            "=" +
                            value.Substring(2, value.IndexOf("=", StringComparison.Ordinal) - 2) + " + 1;";
            string bound;

            var a1 = s.IndexOf("to", StringComparison.Ordinal) + 2;
            var a2 = s.Length - 1;
            var a3 = s.IndexOf("to", StringComparison.Ordinal) + 1;
            var a4 = a2 - a3;

            bound = s.Substring(a1, a4);
            var label = "Label" + labelCounter;
            var endForStringPart1 = value1;

            var sentence = value + "\t" + label + ":";

            //Oneil Code                    //Translation
            //for idx = 0 to bound – 1      let idx = 0
            //                              label L_0
            //let array[idx] = -1           let array[idx] = -1 
            //endfor                        let idx = idx + 1 
            //                              if (idx <= bound – 1) then goto L_0

            //for i = 1 to size -1          "\t\ti = 1;\n\tLabel3:"
            //statements                    statements
            //endfor                        "\ti =i  + 1;" + "\n"      //this is endForStringPart1
            //                               + "\n"     this is endForStringPart2

            //idx
            var idx = value.Substring(2, value.IndexOf("=", StringComparison.Ordinal) - 2);

            var endForStringPart2 = "if (" + idx + " <= " + bound + ") goto " + label + ";";
            endForStringPart2 += "\n";

            code[index] = sentence;
            endForString = endForStringPart1 + "\n";

            endForString += "\t" + endForStringPart2;

            listOfEndFors.Add(endForString);
            ++labelCounter;
        }

        private static void WhileTransform(int count, string other, string s, List<string> code, ref int labelCounter)
        {
            string tab = null;
            for (var i = 0; i < count; i++)
            {
                tab += "\t";
            }

            var label = "label" + labelCounter;
            var newif = tab + label + ":\n" + s;
            ++labelCounter;
            var sentence = "goto " + label + ";\n";
            var statement = "label" + labelCounter + ":";
            newif = newif.Replace("while", "if");
            newif += " goto label" + labelCounter + ";";
            statement = tab + statement;

            sentence += statement;
            var index = code.IndexOf(other);
            code[index] = code[index].Replace(s, sentence);
            ++labelCounter;

            var whilecount = code.Count(s1 => s1.Contains("while") && !s1.Contains("endwhile"));
            var space = index;
            while (whilecount > 0)
            {
                var whileindex = code.FindIndex(space,
                                                s1 => s1.Contains("while") && !s1.Contains("endwhile"));
                var endwhile = code.FindIndex(index, s1 => s1.Contains("endwhile"));

                if (whilecount > 1)
                {
                    endwhile = code.FindIndex(endwhile + 1, s1 => s1.Contains("endwhile"));
                }

                if (whileindex > index && whileindex < endwhile)
                {
                    space = whileindex;

                    var tcount = code[whileindex].Count(x => x.Equals('\t'));
                    string nestedTabCount = null;
                    for (var i = 0; i < tcount; ++i)
                    {
                        nestedTabCount += "\t";
                    }

                    label = "label" + labelCounter;
                    var whileif = nestedTabCount + label + ":\n" + code[whileindex];
                    ++labelCounter;
                    sentence = "goto " + label + ";\n";
                    statement = "label" + labelCounter + ":";
                    whileif = whileif.Replace("while", "if");
                    whileif += " goto label" + labelCounter + ";";
                    sentence = nestedTabCount + sentence + statement;

                    code[whileindex] = sentence;
                    ++labelCounter;

                    code[endwhile] = whileif;
                }

                --whilecount;
            }

            var endindex = code.FindIndex(x => x.Contains("endwhile"));
            code[endindex] = newif;
        }
    }
}
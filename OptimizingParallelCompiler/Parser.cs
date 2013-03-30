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
        public static void Transform(List<string> code, List<ThreeOPCreation> lets, List<ThreeOPCreation> intStatements)
        {
            var listOfEndFors = new List<string>();

            //replaces title with // and always inserts the using statements
            code[0] += Environment.NewLine + "using System;\nclass Program{";
            code[0] = code[0].Replace("title", "//");

            var labelCounter = 0;

            var ifs = new List<int>();

            //one loop that transforms the code to c#
            code.ForEach(s =>
                {
                    //creates an unmodified instance of the current line
                    var notModified = s;
                    //removes all trailing spaces and tabs
                    s = s.TrimEnd('\t', ' ');
                    //trims beginning of sentence
                    s = s.TrimStart('\t', ' ');

                    var index = code.IndexOf(notModified);

                    if (s.IndexOf("end", StringComparison.Ordinal) == 0 && s.Length == 3)
                    {
                        code[index] = "\t}\n}";
                    }
                    else if (s.IndexOf("prompt", StringComparison.Ordinal) == 0 || s.IndexOf("print", StringComparison.Ordinal) == 0)
                    {
                        code[index]= (s.StartsWith("print") ? code[index].Replace("print", "Console.Write(") : code[index].Replace("prompt", "Console.Write("));
                        code[index] += ");";
                    }
                    else if (s.IndexOf("rem", StringComparison.Ordinal) == 0)
                    {
                        code[index] = code[index].Replace("rem", "//");
                    }
                    else if (s.IndexOf("list", StringComparison.Ordinal) == 0 || s.StartsWith("table", StringComparison.Ordinal) || s.StartsWith("box", StringComparison.Ordinal))
                    {
                        var sentence = s;
                        var arrayBegin = sentence.IndexOf("[", StringComparison.Ordinal) + 1;
                        var arrayEnd = sentence.IndexOf("]", StringComparison.Ordinal);
                        var value = sentence.Substring(arrayBegin, arrayEnd - arrayBegin);
                        sentence = sentence.Substring(arrayEnd + 1, sentence.Length - (arrayEnd + 1));
                        sentence = "int[] " + sentence + " = new int[" + value + "];";
                        code[index] = code[index].Replace(s, sentence);

                        //InformationOutput.InformationPrint("Line number " + code.IndexOf(notModified) + " type is list sentence " + notModified);
                    }
                    else if (s.IndexOf("input", StringComparison.Ordinal) == 0)
                    {
                        code[index] = code[index].Replace("input", "");
                        code[index] += " = Convert.ToInt32(Console.ReadLine());";
                    }
                    else if (s.StartsWith("if") && s.Contains("then"))
                    {
                        IfMethod(code, ref labelCounter, s, notModified, index, ifs);

                        var something = lets.Find(x => x.Index == index);

                        //InformationOutput.InformationPrint(something != null ? something.ToString() : "not it");

                        //InformationOutput.InformationPrint("Line number " + code.IndexOf(notModified) + " type is if sentence " + notModified);
                    }
                    else if (s.IndexOf("goto", StringComparison.Ordinal) == 0)
                    {
                        code[index] += ";";
                    }
                    else if (s.IndexOf("label", StringComparison.Ordinal) == 0 && !s.Contains(":"))
                    {
                        code[index] = code[index].Replace("label", "");
                        code[index] += ":";
                    }
                    else if (s.IndexOf("begin", StringComparison.Ordinal) == 0)
                    {
                        code[index] = "";
                    }
                    else if (s.IndexOf("var", StringComparison.Ordinal) == 0)
                    {
                        code[index] = "\tstatic void Main()\n\t{";
                    }
                });

            var i = 0;
            while(i < ifs.Count)
            {
                for (int k = 0; k < lets.Count; k++)
                {
                    if (lets[k].Index > ifs[i])
                    {
                        //InformationOutput.InformationPrint(lets[k].Index.ToString());
                        lets[k].Index++;
                        //ifs.RemoveAt(i);
                        //i = 0;
                        //InformationOutput.InformationPrint(lets[k].Index.ToString());
                    }
                }
                ++i;
            }

            i = 0;
            foreach (var item in lets)
            {
                code.Insert(item.Index + i, item.Statement);
                ++i;
            }

            intStatements.Reverse();

            i = 0;
            foreach (var item in intStatements)
            {
                code.Insert(item.Index + i, item.Statement);
            }

            code.ForEach(s =>
                {
                    //creates an unmodified instance of the current line
                    var notModified = s;
                    //removes all trailing spaces and tabs
                    s = s.Trim('\t', ' ');
                    //counts the number of tabs or spaces for pretty code
                    var count = s.Count(x => x.Equals('\t') || x.Equals(' '));

                    var index = code.IndexOf(notModified);

                    if (s.IndexOf("let", StringComparison.Ordinal) == 0)
                    {
                        //var sentence = s;
                        //sentence = sentence.Replace("let", "");
                        //sentence += ";";
                        //var index = code.IndexOf(notModified);
                        code[index] = code[index].Replace("let", "");
                        code[index] += ";";

                        //InformationOutput.InformationPrint("Line number " + index + " type is let sentence " + notModified + " new sentence " + sentence);
                    }
                    //does the while loop
                    if (s.IndexOf("while", StringComparison.Ordinal) == 0)
                    {
                        WhileTransform(count, notModified, s, code, ref labelCounter);

                        //InformationOutput.InformationPrint("Line number " + index + " type is while sentence " + notModified);
                    }
                    else if (s.IndexOf("endfor", StringComparison.Ordinal) == 0)
                    {
                        //var index = index;
                        //take the last string off the list
                        code[index] = listOfEndFors[listOfEndFors.Count - 1];
                        listOfEndFors.RemoveAt(listOfEndFors.Count - 1);

                        //InformationOutput.InformationPrint("Line number " + index + " type is endfor sentence " + notModified);
                    }
                    else if (s.IndexOf("for", StringComparison.Ordinal) == 0)
                    {
                        ForTransform(s, code, notModified, ref labelCounter, listOfEndFors);

                        //InformationOutput.InformationPrint("Line number " + index + " type is for sentence " + notModified);
                    }
                    else if (s.StartsWith("int") && !s.EndsWith(";"))
                    {
                        //code[index] = code[index].Replace(s, notModified.Substring(0, notModified.IndexOf("int")) + s + ";");
                        code[index] += ";";

                        //InformationOutput.InformationPrint("Line number " + index + " type is int sentence " + notModified);
                    }
                });
        }

        private static void IfMethod(List<string> code, ref int labelCounter, string s, string notModified, int index, List<int> ifs)
        {
            //var statement = s.Substring(s.IndexOf("then", StringComparison.Ordinal) + "then".Length,
            //                            s.Length -
            //                            (s.IndexOf("then", StringComparison.Ordinal) + "then".Length));

            //var equator = s.Substring(0, s.IndexOf(")", StringComparison.Ordinal) + 1);

            if (code[index].Contains("!="))
            {
                code[index] = code[index].Replace("!=", "==");
            }
            else if (code[index].Contains("=="))
            {
                code[index] = code[index].Replace("==", "!=");
            }
            else if (code[index].Contains("<") && !code[index].Contains("<="))
            {
                code[index] = code[index].Replace("<", ">=");
            }
            else if (code[index].Contains(">") && !code[index].Contains(">="))
            {
                code[index] = code[index].Replace(">", "<=");
            }
            else if (code[index].Contains("<="))
            {
                code[index] = code[index].Replace("<=", ">");
            }
            else if (code[index].Contains(">="))
            {
                code[index] = code[index].Replace(">=", "<");
            }

            //if (statement.Contains("goto"))
            //{
            //    var sentence = s;
            //    sentence = sentence.Replace(statement, "");
            //    sentence = sentence.Replace("then", statement);
            //    sentence += ";";
            //    code[index] = code[index].Replace(s, sentence);
            //}
            //else if (statement.Contains("print"))
            //{
            //    statement = statement.Replace("print", "Console.Write(");
            //    var label = "label" + labelCounter;
            //    statement = " goto " + label + "; " + statement;
            //    statement += ");";
            //    equator += statement;
            //    code[index] = code[index].Replace(s, equator);
            //    statement = code[index + 2] + Environment.NewLine + label + ":";
            //    code[index + 2] = code[index + 2].Replace(code[index + 2], statement);
            //    ++labelCounter;

            //    ifs.Add(index + 2);
            //}
            //else if (statement.Contains("prompt"))
            //{
            //    statement = statement.Replace("prompt", "Console.Write(");
            //    var label = "label" + labelCounter;
            //    //statement = " goto " + label + "; " + statement;
            //    statement += ");";
            //    equator += " goto " + label + ";";
            //    code[index] = code[index].Replace(s, equator);
            //    code.Insert(index + 1, statement + Environment.NewLine + label + ":");
            //    //statement = code[index + 2] + Environment.NewLine + label + ":";
            //    //code[index + 2] = code[index + 2].Length == 0 ? code[index + 2] = statement : code[index + 2].Replace(code[index + 2], statement);

            //    ifs.Add(index + 2);
            //    ++labelCounter;
            //}
            //else if (statement.Contains("let"))
            //{
            //    statement = statement.Replace("let", "");
            //    var label = "label" + labelCounter;
            //    statement = " goto " + label + "; " + statement;
            //    statement += ";";
            //    equator += statement;
            //    code[index] = code[index].Replace(s, equator);
            //    statement = code[index + 2];
            //    statement += Environment.NewLine + label + ":";
            //    code[index + 2] = code[index + 2].Replace(code[index + 2], statement);
            //    ++labelCounter;

            //    ifs.Add(index + 2);
            //}
            //else
            //{
                var label = "label" + labelCounter;
                code[index] += " goto " + label + ";";
                code[index] = code[index].Replace("then", "");
                //equator += statement;
                //code[index] = code[index].Replace(s, notModified.Substring(0, notModified.IndexOf("if")) + equator);
                //statement = code[index + 2];
                //statement = label + ":";
                code.Insert(index + 2, notModified.Substring(0, notModified.IndexOf("if")) + label + ":");
                ++labelCounter;

                ifs.Add(index + 1);
            //}
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
            endForStringPart2 += Environment.NewLine;

            code[index] = sentence;
            endForString = endForStringPart1 + Environment.NewLine;

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
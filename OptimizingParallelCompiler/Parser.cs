using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace OptimizingParallelCompiler
{
    public class Parser
    {
        public static void Change(ref List<string> rtblines, List<string> keywords, string code)
        {
            var lines = new string(code.ToCharArray());
            foreach (var keyword in keywords)
            {
                MatchCollection match;
                var count = WordCount(keyword, ref lines, out match);

                Console.WriteLine(count + " : word " + keyword);

                foreach (Group m in match)
                {
                    Console.WriteLine(m.Index + " : " + keyword);

                    if (keyword.Equals("begin") || keyword.Equals("var"))
                    {
                        rtblines = rtblines.Select(x => x.Replace(keyword, "")).ToList();
                    }
                    else if (keyword.Equals("title"))
                    {
                        rtblines = rtblines.Select(x => x.Replace(keyword, "//")).ToList();
                        const string usingstatements = "using System;\n" + "class Program\n" + "{\n"
                            + "\tstatic void Main()\n\t{";
                        rtblines.Insert(1, usingstatements);
                    }
                }

                /*
                var start = 0;

                for (int i = 0; i < count; i++)
                {
                    rtblines.
                    if (keyword as "title")
                    {
                        var sentence = test[i];
                        sentence = sentence.Substring(reserveWord.Count(), sentence.Length - reserveWord.Count());
                        sentence = "//" + sentence;
                        test[i] = sentence;
                        if (i == 0)
                        {
                            const string usingstatements = "using System;\n" + "class Program\n" + "{";
                            test.Insert(1, usingstatements);
                        }
                    }
                        
                    else if (reserveWord.Equals("while", StringComparison.Ordinal) && !test[i].Contains("end"))
                    {
                        txtError.AppendText(Environment.NewLine + reserveWord + " : " +
                                            test[i] + " : " + string.Equals(reserveWord, "while"));
                        var statement = test[i];
                        var label = "label" + _labelCounter;
                        ++_labelCounter;
                        var label1 = "label" + _labelCounter;
                        var sentence = "goto " + label + ";\n";
                        sentence += label1 + ":";
                        test[i] = sentence;
                        var other = new List<string>(test.GetRange(0, test.Count));
                        for (var x = 0; x < other.Count; ++x)
                        {
                            other[x] = other[x].Trim('\t', ' ');
                        }
                        var endWhile = other.IndexOf("endwhile");

                        test[endWhile] = label + ":";
                        statement = statement.Replace("while", "if");
                        statement = statement.TrimEnd(' ');
                        statement = statement + " goto " + label1 + ";";

                        test[endWhile] += "\n" + statement;

                        ++_labelCounter;
                    }
                    else if (reserveWord.Equals("for"))
                    {
                        test[i] = test[i].Replace("\t", string.Empty);
                        var index = test[i].IndexOf("for") + reserveWord.Count() + 1;
                        var end = test[i].IndexOf("to") - 1;
                        var value = "\t\t" + test[i].Substring(index, end - index) + ";\n";
                        var value1 = "\t" + value.Substring(2, value.IndexOf("=") - 2) + "=" +
                                        value.Substring(2, value.IndexOf("=") - 2) + " + 1;";
                        var label = "Label" + _labelCounter.ToString();
                        var sentence = value + "\t" + label + ":";
                        var number = test[i].IndexOf("to") + 2;
                        test[i] = test[i].TrimEnd(' ');
                        var lastvalue = test[i].Last();
                        var number1 = test[i].IndexOf(lastvalue);
                        var last = "\tif( " + value.Substring(2, value.IndexOf("=") - 2) + " <= " +
                                      test[i].Substring(number, number1 - number + 1) +
                                      " ) goto " + label + ";";

                        test[i] = sentence;
                        test.Insert(i + 2, value1);
                        test.Insert(i + 3, last);

                        ++_labelCounter;
                    }
                    else if (reserveWord.Equals("var"))
                    {
                        test[i] = "\tstatic void Main()\n\t{";
                    }
                    else if (reserveWord.Equals("end") && test[i].Equals(reserveWord))
                    {
                        test[i] = test[i].Replace("\t", "");
                        test[i] = "\t}\n}";
                    }
                    else if (reserveWord.Equals("prompt") || reserveWord.Equals("print"))
                    {
                        var sentence = test[i];
                        sentence = sentence.Replace("\t", string.Empty);
                        sentence = sentence.Replace(";", "");
                        sentence = sentence.TrimEnd(' ');
                        sentence = sentence.Substring(reserveWord.Count(), sentence.Length - reserveWord.Count());
                        sentence = "\tConsole.WriteLine(" + sentence + ");";
                        test[i] = sentence;
                    }
                    else if (reserveWord.Equals("rem"))
                    {
                        var sentence = test[i];
                        sentence = sentence.Replace("\t", string.Empty);
                        sentence = sentence.Substring(reserveWord.Count(), sentence.Length - reserveWord.Count());
                        sentence = "\t//" + sentence;
                        test[i] = sentence;
                    }
                    else if (reserveWord.Equals("list"))
                    {
                        var sentence = test[i];
                        sentence = sentence.Replace("\t", string.Empty);
                        var arrayBegin = sentence.IndexOf("[") + 1;
                        var arrayEnd = sentence.IndexOf("]");
                        var value = sentence.Substring(arrayBegin, arrayEnd - arrayBegin);
                        var number = sentence.IndexOf(" ");
                        sentence = sentence.Substring(number, sentence.Length - number);
                        sentence = "\tint[]" + sentence + " = new int[" + value + "];";
                        test[i] = sentence;
                    }
                    else if (reserveWord.Equals("let"))
                    {
                        var sentence = test[i];
                        sentence = sentence.Replace("\t", string.Empty);
                        sentence = sentence.Substring(reserveWord.Count(), sentence.Length - reserveWord.Count());
                        sentence = "\t" + sentence + ";";
                        test[i] = sentence;
                    }
                    else if (reserveWord.Equals("endfor"))
                    {
                        test[i] = "";
                    }
                    else if (reserveWord.Equals("input"))
                    {
                    }
                    else if (reserveWord.Equals("int"))
                    {
                        var sentence = test[i];
                        sentence = sentence.Replace("\t", string.Empty);
                        sentence = "\t" + sentence + ";";
                        test[i] = sentence;
                    }
                    else if (reserveWord.Equals("if") && !test[i].Contains("goto"))
                    {
                        var label = "label" + _labelCounter;
                        test[i] = test[i].Trim(' ', '\t');
                        var sentence = test[i].Substring(0, test[i].Count() - "then".Length);
                        sentence += "goto " + label + ";";

                        test[i] = sentence;
                        test.Insert(i + 2, label + ":");

                        ++_labelCounter;
                    }
                    else if (reserveWord.Equals("=="))
                    {
                        test[i] = test[i].Replace("==", "!=");
                    }
                         * */
            }
        }

        private static int WordCount(string word, ref string code, out MatchCollection match)
        {
            match = Regex.Matches(code, word + "\\s+");
            return Regex.Matches(code, word + "\\s+").Count;
        }
    }
}
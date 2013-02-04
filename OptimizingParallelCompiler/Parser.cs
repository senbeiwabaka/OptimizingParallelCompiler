using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace OptimizingParallelCompiler
{
    public class Parser
    {
        public static void Change(ref List<string> rtblines, List<string> keywords, string code)
        {
            var lines = new string(code.ToCharArray());

            rtblines.TrimExcess();

            for (var i = 0; i < rtblines.Count; i++)
            {
                rtblines[i] = rtblines[i].TrimStart(' ', '\t');
                rtblines[i] = rtblines[i].TrimEnd(' ', '\t');
            }

            foreach (var keyword in keywords)
            {
                MatchCollection match;

                var count = WordCount(keyword, ref lines, out match);

                foreach (Group m in match)
                {
                    //Console.WriteLine(m.Index + " : " + keyword);

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
                    else if (keyword.Equals("let"))
                    {
                        Console.WriteLine(m.Index + " : " + m.Value);
                        
                        Console.WriteLine(rtblines.FindAll(x => x == "let").ToList());
                    }
                }
            }
        }

        private static int WordCount(string word, ref string code, out MatchCollection match)
        {
            match = Regex.Matches(code, word + "\\s+");
            return Regex.Matches(code, word + "\\s+").Count;
        }
    }
}
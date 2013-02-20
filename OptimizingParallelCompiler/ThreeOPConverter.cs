﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace OptimizingParallelCompiler
{
    class ThreeOPConverter
    {

        public static void Transform(List<string> code)
        {
            var list = code.ToList();
            var lines = new List<string>(list);

            var threeOP = new List<ThreeOPAnalysis>(lines.Count(x => x.Contains("let")));

            var counter = 0;

            var beginStatement = new List<string>();
            var varStatement = new List<string>();

            lines.ForEach(x =>
            {
                x = x.Trim(' ', '\t');

                if (x.IndexOf("let", StringComparison.Ordinal) == 0)
                {
                    var countBracket = Regex.Matches(x, "[[]").Count;

                    var statement = x.Substring(x.IndexOf("=", StringComparison.Ordinal) + 1, x.Length - (x.IndexOf("=", StringComparison.Ordinal) + 1));

                    if (countBracket > 0)
                    {
                        var indexBracketFront = x.IndexOf("[", 0, StringComparison.Ordinal);
                        var indexBracketEnd = x.IndexOf("]", 0, StringComparison.Ordinal);

                        varStatement.Add("int t_" + counter);
                        beginStatement.Add("let t_" + counter + " = " +
                                           x.Substring(indexBracketFront + 1, (indexBracketEnd - 1) - indexBracketFront));

                        countBracket = Regex.Matches(statement, "[[]").Count;

                        threeOP.Add(new ThreeOPAnalysis("t_",false,0,code.IndexOf(x),true,counter.ToString(),
                            "", x.Substring(indexBracketFront + 1, (indexBracketEnd - 1) - indexBracketFront)));

                        while (countBracket > 1)
                        {
                            indexBracketEnd = 0;
                            indexBracketFront = 0;
                            indexBracketFront = statement.IndexOf("[", indexBracketFront + 1, StringComparison.Ordinal);
                            indexBracketEnd = statement.IndexOf("]", indexBracketEnd + 1, StringComparison.Ordinal);
                            var arrayIndex = statement.Substring(indexBracketFront + 1,
                                                                 (indexBracketEnd - 1) - indexBracketFront);

                            if (Regex.Matches(arrayIndex, "[-+*/]").Count > 0)
                            {
                                //var match = 
                                //threeOP.Add(new ThreeOPAnalysis("t_", false,
                                ++counter;
                            }

                            --countBracket;
                        }
                    }

                    //if

                    Console.WriteLine(threeOP);
                }
            });

            foreach (var init in varStatement)
            {
                var index = code.IndexOf("\tvar");
                code.Insert(index, init);
            }

            foreach (var let in beginStatement)
            {
                var index = code.IndexOf("\tbegin");
                code.Insert(index, let);
            }
        }
    }
}
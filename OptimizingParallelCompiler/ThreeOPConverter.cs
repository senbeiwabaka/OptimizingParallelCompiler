using System;
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

            int index;
            lines.ForEach(x =>
                {
                    var nonModifiedStatement = x;

                    x = x.Trim(' ', '\t');

                    var statement = x.Substring(x.IndexOf("=", StringComparison.Ordinal) + 1,
                                                    x.Length - (x.IndexOf("=", StringComparison.Ordinal) + 1));

                    if (x.IndexOf("let", StringComparison.Ordinal) == 0)
                    {
                        if (Regex.Matches(x, "[[]").Count > 0)
                        {
                            var indexBracketFront = x.IndexOf("[", 0, StringComparison.Ordinal);
                            var indexBracketEnd = x.IndexOf("]", 0, StringComparison.Ordinal);

                            var countBracket = Regex.Matches(statement, "[[]").Count;

                            threeOP.Add(new ThreeOPAnalysis("t_" + counter.ToString(),
                                                            x.Substring(indexBracketFront + 1,
                                                                        (indexBracketEnd - 1) - indexBracketFront),
                                                            false,
                                                            countBracket, code.IndexOf(nonModifiedStatement), true,
                                                            new List<string>
                                                                {
                                                                    x.Substring(x.IndexOf("t") + 2,
                                                                                (x.IndexOf("[")) - (x.IndexOf("t") + 2))
                                                                },
                                                            new List<string>(), new List<string>()));

                            ++counter;

                            while (countBracket > 0)
                            {
                                indexBracketEnd = 0;
                                indexBracketFront = 0;
                                indexBracketFront = statement.IndexOf("[", indexBracketFront + 1,
                                                                      StringComparison.Ordinal);
                                indexBracketEnd = statement.IndexOf("]", indexBracketEnd + 1, StringComparison.Ordinal);
                                var arrayIndex = statement.Substring(indexBracketFront + 1,
                                                                     (indexBracketEnd - 1) - indexBracketFront);


                                if (Regex.Matches(arrayIndex, "[-+*/]").Count > 0)
                                {
                                    var codeIndex = code.IndexOf(nonModifiedStatement);
                                    index = threeOP.FindIndex(s => s.Index == codeIndex);
                                    threeOP[index].ArrayName.Add(statement.Substring(statement.IndexOf(" ") + 1,
                                                                                     indexBracketFront -
                                                                                     (statement.IndexOf(" ") + 1)));
                                    threeOP[index].ArrayTempName.Add("t_" + counter);
                                    threeOP[index].ArrayVariableName.Add(arrayIndex);

                                    ++counter;
                                }

                                if (statement.Length < indexBracketEnd + 2)
                                {
                                    statement = statement.Substring(indexBracketEnd + 2);
                                }

                                --countBracket;
                            }
                        }
                        else if(Regex.Matches(statement, "[-+/*]").Count > 2)
                        {
                            var operandAmount = Regex.Matches(statement, "[-+/*]").Count;

                            index = code.IndexOf(nonModifiedStatement);

                            var threeOPIndex =
                                (from opAnalysis in threeOP
                                 where opAnalysis.Index == index
                                 select threeOP.IndexOf(opAnalysis)).FirstOrDefault();


                        }
                    }
                });

            var addition = threeOP.Aggregate("",
                                             (current, opAnalysis) =>
                                             current + ("int " + opAnalysis.Name + "\n"));

            index = code.IndexOf("\tvar");

            code.Insert(index + 1, addition);
        }
    }
}

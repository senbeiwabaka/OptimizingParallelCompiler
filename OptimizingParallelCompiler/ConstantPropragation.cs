using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace OptimizingParallelCompiler
{
    public static class ConstantPropragation
    {
        public static string[] Constants(List<string> code)
        {
            var labelCounter = 0;
            var listOfEndFors = new List<string>();

            var codeVariables = code.FindAll(x =>
            {
                x = x.Trim(' ', '\t');
                if (x.StartsWith("int", StringComparison.Ordinal) || x.StartsWith("list", StringComparison.Ordinal))
                {
                    return true;
                }
                return false;
            });

            var lets = code.FindAll(x => 
                {
                    x = x.Trim(' ', '\t');
                    if (x.StartsWith("let", StringComparison.Ordinal))
                    {
                        return true;
                    }
                    return false;
                });

            //var count = code.RemoveAll(x =>
            //    {
            //        x = x.Trim(' ', '\t');

            //        if (x.StartsWith("int") || x.StartsWith("list"))
            //        {
            //            return true;
            //        }

            //        return false;
            //    });

            var sentence = string.Join(string.Empty, lets);

            InformationOutput.InformationPrint(sentence);

            for (int j = 0; j < codeVariables.Count; j++)
            {
                codeVariables[j] = codeVariables[j].Substring(codeVariables[j].LastIndexOf(" "));
            }

            //code.ForEach(x =>
            //    {
            //        var original = x;
            //        x.Trim(' ', '\t');

            //        if (x.StartsWith("for", StringComparison.Ordinal))
            //        {
            //            ForTransform(x, code, original, ref labelCounter, listOfEndFors);
            //        }
            //        else if (x.StartsWith("endfor", StringComparison.Ordinal))
            //        {
            //            //var index = index;
            //            //take the last string off the list
            //            code[code.IndexOf(original)] = listOfEndFors[listOfEndFors.Count - 1];
            //            listOfEndFors.RemoveAt(listOfEndFors.Count - 1);
            //        }
            //    });

            var i = 0;
            while(i < codeVariables.Count)
            {
                var count = Regex.Matches(sentence, codeVariables[i]).Count;

                if (count > 1 || count <= 0)
                {
                    codeVariables.RemoveAt(i);
                    --i;
                }

                ++i;
            }

            return code.ToArray();
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

            //Oneil Code                    Translation
            //for idx = 0 to bound – 1      let idx = 0
            //                              label L_0
            //let array[idx] = -1           let array[idx] = -1 
            //endfor                        let idx = idx + 1 
            //                              if (idx <= bound – 1) then goto L_0

            //for i = 1 to size -1          "\t\ti = 1;\n\tLabel3:"
            //statements                    statements
            //endfor                        "\ti =i  + 1;" + "\n"      //this is endForStringPart1
            //                               + "\n"     this is endForStringPart2

            var idx = value.Substring(2, value.IndexOf("=", StringComparison.Ordinal) - 2);

            var endForStringPart2 = "if (" + idx + " <= " + bound + ") goto " + label + ";";
            endForStringPart2 += Environment.NewLine;

            code[index] = sentence;
            endForString = endForStringPart1 + Environment.NewLine;

            endForString += "\t" + endForStringPart2;

            listOfEndFors.Add(endForString);
            ++labelCounter;
        }
    }
}

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
            var codeVariables = new List<Variables>();

            code.ForEach(x =>
                {
                    var index = code.IndexOf(x);
                    var original = x;
                    x = x.Trim(' ', '\t');

                    if (x.StartsWith("if", StringComparison.Ordinal) && x.Contains("then"))
                    {
                        if (x.Contains("let") || x.Contains("goto") || x.Contains("print") || x.Contains("prompt"))
                        {
                            code.Insert(index + 1, original.Substring(0, original.IndexOf("if")) + " " + x.Substring(x.IndexOf("then") + "then".Length));

                            original = original.Replace(x.Substring(x.IndexOf("then") + "then".Length), "");
                            x = x.Substring(0, x.IndexOf("then") + "then".Length);

                            code[index] = original;
                        }
                    }
                });

            var varabiles = code.FindAll(x =>
            {
                x = x.Trim(' ', '\t');
                if (x.StartsWith("int", StringComparison.Ordinal) || x.StartsWith("list", StringComparison.Ordinal))
                {
                    return true;
                }
                return false;
            });

            foreach (var item in varabiles)
            {
                codeVariables.Add(new Variables { Name = item });
            }

            var lets = code.FindAll(x => 
                {
                    x = x.Trim(' ', '\t');
                    if (x.StartsWith("let", StringComparison.Ordinal))
                    {
                        return true;
                    }
                    return false;
                });

            codeVariables.RemoveAll(x => x.Name.Contains("list"));

            for (var j = 0; j < lets.Count; j++)
            {
                var s = lets[j];
                lets[j] = lets[j].Substring(0, lets[j].IndexOf("="));
            }

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
                codeVariables[j].Name = codeVariables[j].Name.Substring(codeVariables[j].Name.LastIndexOf(" "));
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
                var count = Regex.Matches(sentence, codeVariables[i].Name).Count;

                if (count > 1 || count <= 0)
                {
                    codeVariables.RemoveAt(i);
                    --i;
                }

                ++i;
            }

            for (i = 0; i < codeVariables.Count; i++)
            {
                InformationOutput.InformationPrint(code.IndexOf(codeVariables[i].Name).ToString() + Environment.NewLine);
                code.ForEach(x =>
                    {
                        if (x.Contains(codeVariables[i].Name) && x.Contains("let"))
                        {
                            InformationOutput.InformationPrint(code.IndexOf(x).ToString() + Environment.NewLine);
                            codeVariables[i].value = code[code.IndexOf(x)].Substring(code[code.IndexOf(x)].IndexOf("=") + 2);
                            InformationOutput.InformationPrint(codeVariables[i].value);
                            code.Remove(x);
                        }
                        else if (x.Contains(codeVariables[i].Name) && x.Contains("int"))
                        {
                            code.Remove(x);
                        }
                    });
            }

            foreach (var item in codeVariables)
            {
                code.ForEach(x =>
                    {
                        var index = code.IndexOf(x);
                        x = x.Replace(item.Name, item.value);
                        code[index] = x;
                    });
            }

            return code.ToArray();
        }

        private class Variables
        {
            public string Name { get; set; }
            public string value { get; set; }
        }
    }
}

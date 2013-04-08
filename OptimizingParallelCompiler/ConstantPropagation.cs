using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace OptimizingParallelCompiler
{
    public static class ConstantPropagation 
    {
        public static string[] Constants(List<string> code)
        {
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
                    if (x.StartsWith("let", StringComparison.Ordinal) || x.StartsWith("input", StringComparison.Ordinal))
                    {
                        return true;
                    }
                    return false;
                });

            codeVariables.RemoveAll(x => x.Name.Contains("list"));

            for (var j = 0; j < lets.Count; j++)
            {
                var s = lets[j];
                if (lets[j].Contains("let"))
                {
                    lets[j] = lets[j].Substring(0, lets[j].IndexOf("="));
                }
            }

            var sentence = string.Join(string.Empty, lets);

            for (int j = 0; j < codeVariables.Count; j++)
            {
                codeVariables[j].Name = codeVariables[j].Name.Substring(codeVariables[j].Name.LastIndexOf(" "));
            }

            var i = 0;
            while(i < codeVariables.Count)
            {
                var pattern = @"\b" + codeVariables[i].Name;
                var count = Regex.Matches(sentence, pattern).Count;

                if (count > 1 || count <= 0)
                {
                    codeVariables.RemoveAt(i);
                    --i;
                }
                else if (count == 1)
                {
                    var index = sentence.IndexOf(codeVariables[i].Name);
                    var statement = sentence.Substring(index - 1 - "input".Length <= 0 ? 0 : index - 1 - "input".Length, "input".Length);
                    if (statement == "input")
                    {
                        codeVariables.RemoveAt(i);
                        --i;
                    }
                }

                ++i;
            }

            for (i = 0; i < codeVariables.Count; i++)
            {
                InformationOutput.InformationPrint(code.IndexOf(codeVariables[i].Name).ToString() + Environment.NewLine);
                code.ForEach(x =>
                    {
                        if (x.Contains("let"))
                        {
                            var beforeEquals = x.Substring(0, x.IndexOf("="));
                            if (beforeEquals.Contains(codeVariables[i].Name))
                            {
                                codeVariables[i].Value = code[code.IndexOf(x)].Substring(code[code.IndexOf(x)].IndexOf("=") + 2);
                                code.Remove(x);
                            }
                        }
                        else if (x.Contains(codeVariables[i].Name) && x.Contains("int"))
                        {
                            code.Remove(x);
                        }
                    });
            }

            i = 0;
            while (i <= codeVariables.Count - 1)
            {
                var j =  0;
                while (j <= codeVariables.Count - 1)
                {
                    var s = codeVariables[j].Name;
                    s = s.Trim(' ');
                    if (Regex.Matches(codeVariables[i].Value, @"\b"+s+@"\b").Count > 0)
                    {
                        codeVariables[i].Value = Regex.Replace(codeVariables[i].Value, s, codeVariables[j].Value, RegexOptions.IgnorePatternWhitespace | RegexOptions.Singleline | RegexOptions.IgnoreCase);
                    }

                    ++j;
                }

                ++i;
            }

            foreach (var item in codeVariables)
            {
                code.ForEach(x =>
                    {
                        var index = code.IndexOf(x);
                        //x = x.Replace(item.Name, item.value);
                        var pattern = @"\b" + item.Name + @"\b";
                        x = Regex.Replace(x, pattern, item.Value, RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);
                        code[index] = x;
                    });
            }

            return code.ToArray();
        }

        private class Variables
        {
            public string Name { get; set; }
            public string Value { get; set; }
        }
    }
}

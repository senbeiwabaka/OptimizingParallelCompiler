using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace OptimizingParallelCompiler
{
    public static class DependencyOutputGenerator
    {
        public static string[] Generator(List<string> code)
        {
            var forLoops = new List<string>();

            var variables = new List<VariableInformation>();

            var codeVariables = code.FindAll(x =>
                {
                    x = x.Trim(' ', '\t');
                    if (x.StartsWith("int", StringComparison.Ordinal) || x.StartsWith("list", StringComparison.Ordinal))
                    {
                        return true;
                    }
                    return false;
                });

            //var i = 0;
            foreach (var item in codeVariables)
            {
                var index = codeVariables.IndexOf(item);
                var s = item.Trim(' ', '\t');
                if (s.StartsWith("list", StringComparison.Ordinal))
                {
                    variables.Add(new VariableInformation { IsArray = true, Name = s.Substring(s.IndexOf(" ", StringComparison.Ordinal) + 1), Type = 1, VariablePosition = index });
                }
                else if (s.StartsWith("int", StringComparison.Ordinal))
                {
                    variables.Add(new VariableInformation { IsArray = false, Name = s.Substring(s.IndexOf(" ", StringComparison.Ordinal) + 1), Type = 0, VariablePosition = index });
                }
            }

            int count = 0;
            code.ForEach(x =>
                {
                    x.Trim(' ', '\t');
                    if (x.StartsWith("for", StringComparison.Ordinal))
                    {
                        ++count;
                    }
                });

            code.ForEach(x =>
                {
                    var index = code.IndexOf(x);

                    x = x.Trim(' ', '\t');

                    //var count

                    if (x.StartsWith("for", StringComparison.Ordinal))
                    {
                        //code.RemoveAt(index);
                    }
                    
                });

            //code.RemoveAll(x => !x.Contains("for"));

            return forLoops.ToArray();
        }

        /// <summary>
        /// Variable information that helps generate the reads and writes
        /// </summary>
        private struct VariableInformation
        {
            // Name of the variable
            public string Name { get; set; }
            // the integer type such as list or box
            public int Type { get; set; }
            // the integer position in the list of variables
            public int VariablePosition { get; set; }
            // is it an array
            public bool IsArray { get; set; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace MiniCompiler
{
    public struct Variable
    {
        public string Type;
        public string Id;
        public int Line;
        public int Column;
        public List<int> Dimensions;
    }

    public struct Loop
    {
        public string StartLabel;
        public string EndLabel;
    }

    public static class Context
    {
        private static bool CompliationFailed = false;
        
        private static int _i;

        private static readonly List<Dictionary<string, Variable>> VariablesStack =
            new List<Dictionary<string, Variable>>();
        
        public static readonly List<Loop> NestedLoops = new List<Loop>();

        private static readonly List<string> Errors = new List<string>();

        public static string GetNewId()
        {
            return $"v{_i++}";
        }

        public static void PushVariableStack()
        {
            VariablesStack.Add(new Dictionary<string, Variable>());
        }

        public static void PopVariableStack()
        {
            VariablesStack.RemoveAt(VariablesStack.Count - 1);
        }

        public static string AddVariable(string name, string type, int line, int column, List<int> dimensions=null)
        {
            // generate new llvm id
            var id = GetNewId();

            // check if variable exists in context
            if (VariablesStack.Last().ContainsKey(name))
            {
                var variable = VariablesStack.Last()[name];
                AddError(line,
                    $"variable '{name}' already declared at line {variable.Line} column {variable.Column}.");
            }
            else
            {
                // store variable in context
                VariablesStack.Last().Add(name, new Variable
                {
                    Type = type,
                    Id = id,
                    Line = line,
                    Column = column,
                    Dimensions = dimensions
                });
            }

            return id;
        }


        public static Variable GetVariable(SyntaxInfo variable, bool isTabVar=false)
        {
            // Go through all stacks, starting at top, if at any point you find the variable return it.
            // If none of the stacks contain the variable, add compilation error 
            for (var i = VariablesStack.Count - 1; i >= 0; i--)
            {
                var variables = VariablesStack[i];

                if (variables.ContainsKey(variable.Text))
                {
                    var v = variables[variable.Text];
                    if (isTabVar && v.Dimensions == null)
                    {
                        AddError(variable.Line, $"variable {variable.Text} is a scalar, not an array (maybe remove indexing?)");
                    }
                    else if (!isTabVar && v.Dimensions != null)
                    {
                        AddError(variable.Line, $"variable {variable.Text} is an array, not a scalar (maybe you are missing indexing?)");
                    }
                    return v;
                }
            }

            AddError(variable.Line, $"variable '{variable.Text}' was not declared!");
            var id = GetNewId();
            var newVariable = new Variable
            {
                Type = "i32",
                Id = id,
                Line = -1,
                Column = -1
            };
            VariablesStack.Last().Add(variable.Text, newVariable);
            return newVariable;
        }

        public static void PushNestedLoop(string startLabel, string endLabel)
        {
            NestedLoops.Add(new Loop
            {
                StartLabel = startLabel,
                EndLabel = endLabel
            });
        }

        public static void PopNestedLoop()
        {
            NestedLoops.RemoveAt(NestedLoops.Count - 1);
        }

        public static void AddError(int line, string message)
        {
            Errors.Add($"[{line}] ERROR: {message}.");
        }

        public static bool HasErrors()
        {
            return CompliationFailed || Errors.Count != 0;
        }

        public static void PrintErrors()
        {
            foreach (var error in Errors)
            {
                Console.WriteLine(error);
            }
        }

        public static void SetCompilationErrors()
        {
            CompliationFailed = true;
        }
    }
}
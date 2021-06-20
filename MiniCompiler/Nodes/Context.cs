using System;
using System.Collections.Generic;

namespace MiniCompiler
{
    public struct Variable
    {
        public string Type;
        public string Id;
        public int Line;
        public int Column;
    }

    public static class Context
    {
        private static int _i;
        private static readonly Dictionary<string, Variable> Variables = new Dictionary<string, Variable>();
        private static readonly List<string> Errors = new List<string>();

        public static string GetNewId()
        {
            return $"v{_i++}";
        }


        public static string AddVariable(string name, string type, int line, int column)
        {
            // generate new llvm id
            var id = GetNewId();

            // check if variable exists in context
            if (Variables.ContainsKey(name))
            {
                var variable = Variables[name];
                AddError(line,
                    $"ERROR: variable '{name}' already declared at line {variable.Line} column {variable.Column}.");
            }
            else
            {
                // store variable in context
                Variables.Add(name, new Variable
                {
                    Type = type,
                    Id = id,
                    Line = line,
                    Column = column
                });
            }

            return id;
        }


        public static Variable GetVariable(SyntaxInfo variable)
        {
            // if variable doesn't exist, create a dummy one, add error and proceed
            if (!Variables.ContainsKey(variable.Text))
            {
                AddError(variable.Line, $"ERROR: variable '{variable.Text}' was not declared!");
                var id = GetNewId();
                var newVariable = new Variable
                {
                    Type = "i32",
                    Id = id,
                    Line = -1,
                    Column = -1
                };
                Variables.Add(variable.Text, newVariable);
                return newVariable;
            }

            return Variables[variable.Text];
        }

        public static void AddError(int line, string message)
        {
            Errors.Add($"[{line}] ERROR: {message}.");
        }

        public static bool HasErrors()
        {
            return Errors.Count != 0;
        }

        public static void PrintErrors()
        {
            foreach (var error in Errors)
            {
                Console.WriteLine(error);
            }
        }
    }
}
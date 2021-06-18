using System.Collections.Generic;

namespace MiniCompiler
{
    public struct Variable
    {
        public string Name;
        public string Type;
        public string Id;
        public int Line;
        public int Column;
    }

    public static class Context
    {
        private static int _i = 0;
        private static Dictionary<string, Variable> _variables = new Dictionary<string, Variable>();
        public static List<string> Errors = new List<string>();

        public static string GetNewId()
        {
            return $"v{_i++}";
        }


        public static string AddVariable(string name, string type, int line, int column)
        {
            // generate new llvm id
            var id = GetNewId();
            
            // check if variable exists in context
            if (_variables.ContainsKey(name))
            {
                var variable = _variables[name];
                Errors.Add( $"[{line}, {column}] ERROR: variable '{name}' already declared at line {variable.Line} column {variable.Column}.");
            }
            else
            {

                // store variable in context
                _variables.Add(name, new Variable
                {
                    Name = name,
                    Type = type,
                    Id = id,
                    Line = line,
                    Column = column
                });
            }

            return id;
        }


        public static Variable GetVariable(SyntaxNode variable)
        {
            // if variable doesn't exist, create a dummy one, add error and proceed
            if (!_variables.ContainsKey(variable.Text))
            {
                Errors.Add($"[]");
                var id = GetNewId();
                var newVariable = new Variable
                {
                    Name = variable.Text,
                    Type = "unknown",
                    Id = id,
                    Line = -1,
                    Column = -1
                };
                _variables.Add(variable.Text, newVariable);
                return newVariable;
            }

            return _variables[variable.Text];
        }
    }
}
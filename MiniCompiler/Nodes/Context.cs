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

        private static string GetNewId()
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
    }
}
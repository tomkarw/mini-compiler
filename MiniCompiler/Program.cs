using System;
using System.IO;
using System.Text;
using GardensPoint;

namespace MiniCompiler
{
    internal class Compiler
    {
        public static int Main(string[] args)
        {
            var filename = args[0];
            var source = new FileStream(filename, FileMode.Open);
            var scanner = new Scanner(source);
            var parser = new Parser(scanner,  new ProgramNode());
            var success = parser.Parse();

            // syntax errors
            if (!success)
            {
                return 1;
            }
            
            var stringBuilder = new StringBuilder();
            parser.program.GenCode(ref stringBuilder);

            // semantic errors
            if (Context.Errors.Count != 0)
            {
                foreach (var error in Context.Errors)
                {
                    Console.WriteLine(error);
                }
                return 2;
            }
            
            var output = Path.ChangeExtension(filename, ".ll");
            File.WriteAllText(output, stringBuilder.ToString());
            source.Close();
            
            return 0;
        }
    }
}
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
            if (args.Length < 1)
            {
                Console.WriteLine("usage: <exe> <filename>");
            }
            var filename = args[0];
            var source = new FileStream(filename, FileMode.Open);
            var scanner = new Scanner(source);
            var parser = new Parser(scanner, new ProgramNode());
            var success = parser.Parse();
            source.Close();

            // syntax errors
            if (!success)
            {
                return 1;
            }

            var stringBuilder = new StringBuilder();
            parser.program.GenCode(ref stringBuilder);

            // semantic errors
            if (Context.HasErrors())
            {
                Context.PrintErrors();
                return 2;
            }

            var output = $"{filename}.ll";
            File.WriteAllText(output, stringBuilder.ToString());

            return 0;
        }
    }
}
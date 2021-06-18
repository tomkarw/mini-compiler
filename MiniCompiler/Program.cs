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
            var stringBuilder = new StringBuilder();
            parser.program.GenCode(ref stringBuilder);

            var output = $"{filename}.ll";
            File.WriteAllText(output, stringBuilder.ToString());
            source.Close();
            Console.WriteLine(success);
            return 0;
        }
    }
}
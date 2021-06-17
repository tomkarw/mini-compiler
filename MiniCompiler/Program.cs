using System;
using System.IO;
using GardensPoint;

namespace MiniCompiler
{
    internal class Compiler
    {
        public static int Main(string[] args)
        {
            var source = new FileStream(args[0], FileMode.Open);
            var scanner = new Scanner(source);
            var program = new ProgramNode();
            var parser = new Parser(scanner, program);
            var success = parser.Parse();
            source.Close();
            Console.WriteLine(success);
            return 0;
        }
    }
}
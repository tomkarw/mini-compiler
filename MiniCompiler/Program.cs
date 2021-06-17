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
            var parser = new Parser(scanner);
            var success = parser.Parse();
            source.Close();
            Console.WriteLine(success);
            return 0;
        }
    }
}
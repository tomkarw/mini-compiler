namespace MiniCompiler
{
    public static class Context
    {
        private static int _i = 0;
        
        public static object GetNewId()
        {
            return $"v{_i++}";
        }
    }
}
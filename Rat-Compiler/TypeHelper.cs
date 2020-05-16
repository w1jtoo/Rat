namespace Rat_Compiler
{
    public static class TypeHelper
    {
        public static string Unquoute(string str)
        {
            if (str.StartsWith("\"") && str.EndsWith("\"") 
                || str.StartsWith("\'") && str.EndsWith("\'"))
                return str.Substring(1, str.Length - 2);
            return str;
        }
    }
}
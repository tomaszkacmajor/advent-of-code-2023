namespace AdventOfCode2023
{
    static class Extensions
    {
        public static int RemoveWordGetInt(this string str, string word)
        {
            return int.Parse(str.Replace(word, string.Empty).Trim());
        }
    }
}

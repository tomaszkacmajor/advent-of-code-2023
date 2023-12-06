namespace AdventOfCode2023
{
    static class Extensions
    {
        public static int RemoveWordGetInt(this string str, string word)
        {
            return int.Parse(str.Replace(word, string.Empty).Trim());
        }

        public static List<int> ParseIntsWithoutWords(this string str, params string[] words)
        {
            return str.Split(words.Concat(new[] { " ", "\t" }).ToArray(), StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToList();
        }

        public static List<long> ParseLongsWithoutWords(this string str, params string[] words)
        {
            return str.Split(words.Concat(new[] { " ", "\t" }).ToArray(), StringSplitOptions.RemoveEmptyEntries)
                    .Select(long.Parse)
                    .ToList();
        }
    }
}

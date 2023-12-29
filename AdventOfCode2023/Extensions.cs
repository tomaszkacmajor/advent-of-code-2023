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

        public static char[,] ToChar2DArray(this string[] lines)
        {
            char[,] array = new char[lines.Length, lines[0].Length];
            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                    array[i, j] = lines[i][j];
            }

            return array;
        }

        public static bool SequenceEquals<T>(this T[,] a, T[,] b) => a.Rank == b.Rank
            && Enumerable.Range(0, a.Rank).All(d => a.GetLength(d) == b.GetLength(d))
            && a.Cast<T>().SequenceEqual(b.Cast<T>());

    }
}

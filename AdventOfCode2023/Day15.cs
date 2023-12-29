namespace AdventOfCode2023
{
    public class Day15
    {
        public void Solution1()
        {
            var line = File.ReadAllLines(@"..\..\..\inputs\input15-1.txt")[0];

            int sum = 0;
            foreach (var str in line.Split(','))
                sum += GetHash(str);

            Console.WriteLine(sum);
            Console.ReadKey();
        }

        public void Solution2()
        {
            var line = File.ReadAllLines(@"..\..\..\inputs\input15-1.txt")[0];

            Dictionary<int, List<Lens>> dict = new();

            foreach (var str in line.Split(','))
            {
                var label = str.Split('=', '-')[0];
                var hash = GetHash(label);
                dict.TryGetValue(hash, out var list);
                if (list == null)
                {
                    list = new();
                    dict.Add(hash, list);
                }
                var foundLabelIndex = list.FindIndex(x => x.Label == label);

                if (str.Contains('='))
                {
                    var newLens = new Lens(label, int.Parse(str.Split('=', '-')[1]));
                    if (foundLabelIndex >= 0)
                    {
                        list.RemoveAt(foundLabelIndex);
                        list.Insert(foundLabelIndex, newLens);
                    }
                    else
                        list.Add(newLens);
                }
                else if (str.Contains('-') && foundLabelIndex >= 0)
                    list.RemoveAt(foundLabelIndex);
                
                if (!list.Any())
                    dict.Remove(hash);
            }

            int sum = 0;
            foreach (var key in dict.Keys)
            {
                for (int i = 0; i < dict[key].Count; i++)
                {
                    Lens lens = dict[key][i];
                    sum += (key + 1) * (i + 1) * lens.Length;
                }
            }

            Console.WriteLine(sum);
            Console.ReadKey();
        }

        private static int GetHash(string str)
        {
            int hash = 0;
            foreach (char c in str)
            {
                hash += c;
                hash *= 17;
                hash %= 256;
            }

            return hash;
        }

        private record Lens(string Label, int Length)
        {
        }
    }
}

namespace AdventOfCode2023
{
    public class Day3
    {
        readonly List<char> Symbols;

        public Day3()
        {
            Symbols = GetSymbols();
        }

        public void Solution1()
        {
            int sum = 0;
            string[] lines = GetPaddedInput(File.ReadAllLines(@"..\..\..\inputs\input3-1.txt"));
            List<Number> numbers = ExtractNumbers(lines);

            foreach (var number in numbers)
            {
                foreach (var pos in number.AdjacentPos)
                {
                    if (Symbols.Contains(lines[pos.Item1][pos.Item2]))
                    {
                        sum += number.Value;
                        break;
                    }
                }
            }

            Console.WriteLine(sum);
            Console.ReadKey();
        }

        public void Solution2()
        {
            int sum = 0;
            string[] lines = GetPaddedInput(File.ReadAllLines(@"..\..\..\inputs\input3-1.txt"));
            List<Number> numbers = ExtractNumbers(lines);
            Dictionary<(int, int), List<int>> engines = new();

            foreach (var number in numbers)
            {
                foreach (var pos in number.AdjacentPos)
                {
                    if (lines[pos.Item1][pos.Item2] == '*')
                    {
                        if (engines.ContainsKey((pos.Item1, pos.Item2)))
                            engines[(pos.Item1, pos.Item2)].Add(number.Value);
                        else
                            engines.Add((pos.Item1, pos.Item2), new List<int> { number.Value });
                    }
                }
            }

            foreach (var engine in engines.Keys)
            {
                var listOfNumbers = engines[engine];
                if (listOfNumbers.Count == 2)
                    sum += listOfNumbers[0] * listOfNumbers[1];
            }

            Console.WriteLine(sum);
            Console.ReadKey();
        }

        private List<Number> ExtractNumbers(string[] lines)
        {
            List<Number> numbers = new();

            int numStartPos = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                int num = 0;

                for (int j = 0; j < lines[i].Length; j++)
                {
                    var c = lines[i][j];

                    if (!IsNumber(c))
                    {
                        if (num > 0)
                            numbers.Add(new Number(num, numStartPos, j - 1, i));
                        num = 0;
                        continue;
                    }

                    if (num == 0)
                        numStartPos = j;

                    num = num * 10 + int.Parse(c.ToString());
                }
            }

            return numbers;
        }

        private static string[] GetPaddedInput(string[] lines)
        {
            string[] output = new string[lines.Length + 2];

            int newLineLength = lines[0].Length + 2;
            output[0] = output[lines.Length + 1] = new string('.', newLineLength);
            for (int i = 1; i < newLineLength - 1; i++) 
            {
                output[i] = "." + lines[i-1] + ".";
            }
            return output;
        }

        private bool IsNumber(char c)
        {
            return !Symbols.Contains(c) && c != 46;
        }

        private static List<char> GetSymbols()
        {
            return Enumerable.Range(0, 127)
                .Where(x => (x > 32 && x < 48 && x != 46)
                         || (x > 57 && x < 65)
                         || (x > 90 && x < 97)
                         || (x > 122 && x < 127))
                .Select(x => (char)x)
                .ToList();
        }
    }

    record Number(int Value, int StartPos, int EndPos, int Row)
    {
        public List<(int, int)> AdjacentPos
        {
            get
            {
                List<(int, int)> list = new();
                var XPositions = Enumerable.Range(StartPos - 1, EndPos - StartPos + 3);
                list.AddRange(XPositions.Select(x => (Row - 1, x)));
                list.AddRange(XPositions.Select(x => (Row + 1, x)));
                list.Add((Row, StartPos - 1));
                list.Add((Row, EndPos + 1));
                return list;
            }
        }
    }
}

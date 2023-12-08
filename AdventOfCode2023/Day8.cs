namespace AdventOfCode2023
{
    public class Day8
    {
        Dictionary<string, (string, string)> mappings = new();

        public void Solution1()
        {
            string[] lines = File.ReadAllLines(@"..\..\..\inputs\input8-1.txt");
            var directions = lines[0];

            foreach (var line in lines[2..])
            {
                var split = line.Split(new[] { " ", "\t", "=", ",", "(", ")" }, StringSplitOptions.RemoveEmptyEntries);
                mappings.Add(split[0], (split[1], split[2]));
            }

            bool endFound = false;
            int i = 0;
            var currNode = "AAA";

            while (!endFound)
            {
                var dir = directions[i % directions.Length];
                currNode = NextNode(currNode, dir);

                if (currNode == "ZZZ")
                    endFound = true;
                i++;
            }

            Console.WriteLine(i);
            Console.ReadKey();
        }

        private string NextNode(string currNode, char dir)
        {
            if (dir == 'L')
                currNode = mappings[currNode].Item1;
            else
                currNode = mappings[currNode].Item2;
            return currNode;
        }

        public void Solution2()
        {
            string[] lines = File.ReadAllLines(@"..\..\..\inputs\input8-1.txt");
            var directions = lines[0];

            foreach (var line in lines[2..])
            {
                var split = line.Split(new[] { " ", "\t", "=", ",", "(", ")" }, StringSplitOptions.RemoveEmptyEntries);
                mappings.Add(split[0], (split[1], split[2]));
            }

            var currNodes = mappings.Keys.Where(x => x.EndsWith("A")).ToList();

            long[] values = new long[currNodes.Count];

            for (int nodeInd = 0; nodeInd < currNodes.Count; nodeInd++)
            {
                bool endFound = false;
                int i = 0;

                while (!endFound)
                {
                    var dir = directions[i % directions.Length];
                    currNodes[nodeInd] = NextNode(currNodes[nodeInd], dir);

                    if (currNodes[nodeInd].EndsWith("Z"))
                        endFound = true;
                    i++;
                }

                values[nodeInd] = i;
            }

            Console.WriteLine(Lcm(values));
            Console.ReadKey();
        }

        static long Gcd(long a, long b)
        {
            if (b == 0)
                return a;
            else
                return Gcd(b, a % b);
        }

        public static long Lcm(long[] values)
        {
            return values.Aggregate((a, b) => a * b / Gcd(a, b));
        }
    }
}

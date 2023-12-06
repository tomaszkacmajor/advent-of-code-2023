namespace AdventOfCode2023
{
    public class Day6
    {
        public void Solution1()
        {
            string[] lines = File.ReadAllLines(@"..\..\..\inputs\input6-1.txt");

            var times = lines[0].ParseLongsWithoutWords("Time:");
            var distances = lines[1].ParseLongsWithoutWords("Distance:");
            int[] counts = new int[times.Count];

            for (int raceInd = 0; raceInd < times.Count; raceInd++)
            {
                var time = times[raceInd];
                var dist = distances[raceInd];

                for (int i = 1; i < time; i++)
                {
                    if ((time - i) * i > dist)
                        counts[raceInd]++;
                }
            }

            Console.WriteLine(counts.Aggregate((x, y) => x * y));
            Console.ReadKey();
        }

        public void Solution2()
        {
            int cnt = 0;
            string[] lines = File.ReadAllLines(@"..\..\..\inputs\input6-1.txt");

            var time = long.Parse(lines[0].Replace("Time:", "").Replace(" ", "").Trim());
            var dist = long.Parse(lines[1].Replace("Distance:", "").Replace(" ", "").Trim());

            for (long i = 1; i < time; i++)
            {
                if ((time - i) * i > dist)
                    cnt++;
            }

            Console.WriteLine(cnt);
            Console.ReadKey();
        }
    }
}

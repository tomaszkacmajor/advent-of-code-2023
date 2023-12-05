namespace AdventOfCode2023
{
    public class Day5
    {
        public void Solution1()
        {
            string[] lines = File.ReadAllLines(@"..\..\..\inputs\input5-1.txt");

            var seeds = lines[0].Split().Where(x => x != string.Empty && x != "seeds:").Select(x => long.Parse(x)).ToList();

            bool[] changed = new bool[seeds.Count];
            foreach (var line in lines[1..])
            {
                if (line == "" || line.Contains("map"))
                {
                    for (int i = 0; i < changed.Length; i++)
                        changed[i] = false;
                    continue;
                }

                var map = line.Split().Where(x => x != string.Empty).Select(x => long.Parse(x)).ToList();
                for (int i = 0; i < seeds.Count; i++)
                {
                    var seed = seeds[i];
                    if (seed >= map[1] && seed < map[1] + map[2] && !changed[i])
                    {
                        seeds[i] += map[0] - map[1];
                        changed[i] = true;
                    }
                }
            }

            Console.WriteLine(seeds.Min());
            Console.ReadKey();
        }

        public void Solution2()
        {
            string[] lines = File.ReadAllLines(@"..\..\..\inputs\input5-1.txt");

            List<(long, long)> seedRanges = new();
            var seedsRangesInput = lines[0].Split().Where(x => x != string.Empty && x != "seeds:").Select(x => long.Parse(x)).ToList();
            for (int i = 0; i < seedsRangesInput.Count; i+=2)
            {
                seedRanges.Add((seedsRangesInput[i], seedsRangesInput[i + 1]));
            }

            List<(long, long)> seedRangesChanged = new();
            List<(long, long)> seedRangesTemp = new();

            foreach (var line in lines[1..])
            {
                seedRangesTemp = new();
                if (line == "" || line.Contains("map"))
                {
                    foreach(var seedRange in seedRangesChanged)
                        seedRanges.Add((seedRange.Item1, seedRange.Item2));
                    seedRangesChanged = new();
                    continue;
                }

                var map = line.Split().Where(x => x != string.Empty).Select(x => long.Parse(x)).ToList();
                foreach (var seedRange in seedRanges)
                {
                    var rangeStart = map[1];
                    var rangeStop = map[1] + map[2] - 1;
                    var shift = map[0] - map[1];

                    var seedStart = seedRange.Item1;
                    var seedStop = seedRange.Item1 + seedRange.Item2 - 1;

                    long commonRangeStart = 0;
                    long commonRangeStop = 0;

                    if (rangeStart < seedStart && rangeStop >= seedStart & rangeStop <= seedStop)
                    {
                        commonRangeStart = seedStart;
                        commonRangeStop = rangeStop;
                        seedRangesChanged.Add((commonRangeStart + shift, commonRangeStop - commonRangeStart + 1));
                        if (commonRangeStop + 1 <= seedStop)
                            seedRangesTemp.Add((commonRangeStop + 1, seedStop - (commonRangeStop + 1) + 1));
                    }
                    else if (rangeStart >= seedStart && rangeStop <= seedStop)
                    {
                        commonRangeStart = rangeStart;
                        commonRangeStop = rangeStop;
                        seedRangesChanged.Add((commonRangeStart + shift, commonRangeStop - commonRangeStart + 1));
                        if (commonRangeStop + 1 <= seedStop)
                            seedRangesTemp.Add((commonRangeStop + 1, seedStop - (commonRangeStop + 1) + 1));
                        if (commonRangeStart - 1 >= seedStart)
                            seedRangesTemp.Add((seedStart, commonRangeStart - 1 - (seedStart) + 1));
                    }
                    else if(rangeStart >= seedStart && rangeStart <= seedStop && rangeStop > seedStop)
                    {
                        commonRangeStart = rangeStart;
                        commonRangeStop = seedStop;
                        seedRangesChanged.Add((commonRangeStart + shift, commonRangeStop - commonRangeStart + 1));
                        if (commonRangeStart - 1 >= seedStart)
                            seedRangesTemp.Add((seedStart, commonRangeStart - 1 - (seedStart) + 1));
                    }
                    else if (rangeStart < seedStart && rangeStop > seedStop)
                    {
                        commonRangeStart = seedStart;
                        commonRangeStop = seedStop;
                        seedRangesChanged.Add((commonRangeStart + shift, commonRangeStop - commonRangeStart + 1));
                    }
                    else
                    {
                        seedRangesTemp.Add((seedStart, seedStop - seedStart + 1));
                    }
                }

                seedRanges = seedRangesTemp;
            }

            foreach (var seedRange in seedRangesChanged)
                seedRanges.Add((seedRange.Item1, seedRange.Item2));

            Console.WriteLine(seedRanges.Min(x => x.Item1));
            Console.ReadKey();
        }
    }
}

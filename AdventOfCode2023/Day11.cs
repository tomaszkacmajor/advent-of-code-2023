namespace AdventOfCode2023
{
    public class Day11
    {
        public void Solution1()
        {
            var array = File.ReadAllLines(@"..\..\..\inputs\input11-1.txt").ToChar2DArray();
            long sum = 0;
            int[] emptyRowIdx = FindEmptyRows(array);
            int[] emptyColsIdx = FindEmptyCols(array);
            var galaxiesPos = GetGalaxies(array);

            for (int i = 0; i < galaxiesPos.Count - 1; i++)
            {
                for (int j = i + 1; j < galaxiesPos.Count; j++)
                {
                    sum += GetDistanceWithExpansion(galaxiesPos[i], galaxiesPos[j], emptyRowIdx, emptyColsIdx, 2);
                }
            }

            Console.WriteLine(sum);
            Console.ReadKey();
        }

        public void Solution2()
        {
            var array = File.ReadAllLines(@"..\..\..\inputs\input11-1.txt").ToChar2DArray();
            long sum = 0;
            int[] emptyRowIdx = FindEmptyRows(array);
            int[] emptyColsIdx = FindEmptyCols(array);
            var galaxiesPos = GetGalaxies(array);

            for (int i = 0; i < galaxiesPos.Count - 1; i++)
            {
                for (int j = i + 1; j < galaxiesPos.Count; j++)
                {
                    sum += GetDistanceWithExpansion(galaxiesPos[i], galaxiesPos[j], emptyRowIdx, emptyColsIdx, 1000000);
                }
            }

            Console.WriteLine(sum);
            Console.ReadKey();
        }

        private long GetDistanceWithExpansion((int, int) value1, (int, int) value2, int[] emptyRowIdx, int[] emptyColsIdx, int expFactor)
        {
            int colDiff = Math.Abs(value1.Item1 - value2.Item1);
            int rowDiff = Math.Abs(value1.Item2 - value2.Item2);
            int colMin = Math.Min(value1.Item1, value2.Item1);
            int rowMin = Math.Min(value1.Item2, value2.Item2);

            var emptyRowsBetween = emptyRowIdx.Where(x => x > colMin && x < colMin + colDiff).ToList();
            var emptyColsBetween = emptyColsIdx.Where(x => x > rowMin && x < rowMin + rowDiff).ToList();

            return colDiff + rowDiff + emptyRowsBetween.Count * (expFactor - 1) + emptyColsBetween.Count * (expFactor - 1);
        }

        private List<(int, int)> GetGalaxies(char[,] array)
        {
            List<(int, int)> galaxies = new();
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (array[i, j] == '#')
                        galaxies.Add((i, j));
                }
            }

            return galaxies;
        }

        private int[] FindEmptyCols(char[,] array)
        {
            List<int> cols = new();
            for (int i = 0; i < array.GetLength(1); i++)
            {
                bool galaxyFound = false;
                for (int j = 0; j < array.GetLength(0); j++)
                {
                    if (array[j, i] == '#')
                    {
                        galaxyFound = true;
                        break;
                    }
                }
                if (!galaxyFound)
                    cols.Add(i);
            }

            return cols.ToArray();
        }

        private int[] FindEmptyRows(char[,] array)
        {
            List<int> rows = new();
            for (int i = 0; i < array.GetLength(0); i++)
            {
                bool galaxyFound = false;
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (array[i,j] == '#')
                    {
                        galaxyFound = true;
                        break;
                    }
                }
                if (!galaxyFound)
                    rows.Add(i);
            }

            return rows.ToArray();
        }
    }
}

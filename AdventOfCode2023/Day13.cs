namespace AdventOfCode2023
{
    public class Day13
    {
        public void Solution1()
        {
            var allRows = File.ReadAllLines(@"..\..\..\inputs\input13-1.txt");
            var blocks = GetBlocks(allRows);

            long sum = 0;

            foreach (var block in blocks)
            {
                GetSplits(block, out var colsSplitInd, out var rowsSplitInd);
                sum += colsSplitInd + rowsSplitInd * 100;
            }

            Console.WriteLine(sum);
            Console.ReadKey();
        }

        public void Solution2()
        {
            var allRows = File.ReadAllLines(@"..\..\..\inputs\input13-1.txt");
            var blocks = GetBlocks(allRows);

            long sum = 0;
            foreach (var block in blocks)
            {
                GetSplits(block, out var orgColsSplitInd, out var orgRowsSplitInd);

                var tempBlock = new string[block.Length];
                bool changeFound = false;
                for (int i = 0; i < block.Length && !changeFound; i++)
                {
                    for (int j = 0; j < block[i].Length; j++)
                    {
                        block.CopyTo(tempBlock, 0);
                        var ch = block[i].ToCharArray();
                        if (ch[j] == '#')
                            ch[j] = '.';
                        else 
                            ch[j] = '#';

                        tempBlock[i] = new string(ch);

                        var rows = tempBlock;
                        var cols = GetColumns(tempBlock);
                        var colsSplitInd = GetSplitInd(cols).Where(x => x != orgColsSplitInd).FirstOrDefault();
                        var rowsSplitInd = GetSplitInd(rows).Where(x => x != orgRowsSplitInd).FirstOrDefault();

                        if (colsSplitInd == 0 && rowsSplitInd == 0)
                            continue;

                        if (colsSplitInd > 0 || rowsSplitInd > 0)
                        {
                            sum += colsSplitInd + rowsSplitInd * 100;
                            changeFound = true;
                            break;
                        }
                    }
                }
            }

            Console.WriteLine(sum);
            Console.ReadKey();
        }

        private void GetSplits(string[] block, out int colsSplitInd, out int rowsSplitInd)
        {
            var rows = block;
            var cols = GetColumns(block);
            colsSplitInd = GetSplitInd(cols).FirstOrDefault();
            rowsSplitInd = GetSplitInd(rows).FirstOrDefault();
        }

        private static List<string[]> GetBlocks(string[] allRows)
        {
            List<string[]> blocks = new List<string[]>();
            var emptyRowsIdx = Enumerable.Range(0, allRows.Length).Where(i => string.IsNullOrEmpty(allRows[i])).ToList();
            emptyRowsIdx.Add(allRows.Length);
            int beginInd = 0;
            foreach (var emptyInd in emptyRowsIdx)
            {
                var length = emptyInd - beginInd;
                string[] result = new string[length];
                Array.Copy(allRows, beginInd, result, 0, length);
                blocks.Add(result);
                beginInd = emptyInd + 1;
            }

            return blocks;
        }

        private static List<int> GetSplitInd(string[] rows)
        {
            List<int> foundSplitIdx = new();
            for (int splitInd = 1; splitInd < rows.Length; splitInd++)
            {
                bool splitFound = true;
                for (int rightInd = splitInd; rightInd < rows.Length; rightInd++)
                {
                    int leftInd = 2 * splitInd - rightInd - 1;
                    if (leftInd < 0)
                        break;

                    if (rows[rightInd] != rows[leftInd])
                    {
                        splitFound = false;
                        break;
                    }
                }

                if (splitFound)
                    foundSplitIdx.Add(splitInd);
            }

            return foundSplitIdx;
        }

        private string[] GetColumns(string[] rows)
        {
            string[] cols = new string[rows[0].Length];
            for (int i = 0; i < rows[0].Length; i++)
            {
                var tempArr = new char[rows.Length];
                for (int j = 0; j < rows.Length; j++)
                    tempArr[j] = rows[j][i];
                cols[i] = string.Join(string.Empty, tempArr);
            }
            return cols;
        }

    }
}

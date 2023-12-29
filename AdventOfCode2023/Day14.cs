namespace AdventOfCode2023
{
    public class Day14
    {
        static int arrayHeight;
        static int arrayWidth;

        public void Solution1()
        {
            var array = File.ReadAllLines(@"..\..\..\inputs\input14-1.txt").ToChar2DArray();

            var cols = GetColumns(array);
            int sum = 0;

            foreach (var col in cols)
            {
                var hashesIdx = Enumerable.Range(0, col.Length).Where(i => col[i] == '#').ToList();
                var splits = col.Split('#');


                sum += GetWeight(splits[0].Count(x => x == 'O'), -1, col.Length);
                for (int i = 1; i < splits.Length; i++)
                {
                    sum += GetWeight(splits[i].Count(x => x == 'O'), hashesIdx[i - 1], col.Length);
                }
            }

            Console.WriteLine(sum);
            Console.ReadKey();
        }

        public void Solution2()
        {
            var array = File.ReadAllLines(@"..\..\..\inputs\input14-1.txt").ToChar2DArray();
            int goalCyclesNo = 1000000000;

            arrayHeight = array.GetLength(0);
            arrayWidth = array.GetLength(1);

            var list = new List<char[,]>();

            int cyclesInd = 0;
            int repetitiveCyclesCnt = 0;
            while (cyclesInd < goalCyclesNo && repetitiveCyclesCnt == 0)
            {
                cyclesInd++;
                array = TiltOneCycle(array);
                for (int i = 0; i < list.Count; i++)
                {
                    if (array.SequenceEquals(list[i]))
                    {
                        repetitiveCyclesCnt = cyclesInd - i - 1;
                        break;
                    }
                }

                list.Add(array);
            }

            var restCyclesNo = (goalCyclesNo - cyclesInd) % repetitiveCyclesCnt;
            for (int i = 0; i < restCyclesNo; i++)
                array = TiltOneCycle(array);

            int sum = 0;
            for (int i = 0; i < arrayHeight; i++)
                for (int j = 0; j < arrayWidth; j++)
                    sum += array[i, j] == 'O' ? arrayHeight - i : 0;

            Console.WriteLine(sum);
            Console.ReadKey();
        }

        private static char[,] TiltOneCycle(char[,] array)
        {
            array = TransposeDiagonal(array);
            array = SlideLeft(array);
            array = TransposeDiagonal(array);
            array = SlideLeft(array);
            array = TransposeCounterDiagonal(array);
            array = SlideLeft(array);
            array = TransposeCounterDiagonal(array);
            array = TransposeVertical(array);
            array = SlideLeft(array);
            array = TransposeVertical(array);
            return array;
        }

        private static char[,] TransposeDiagonal(char[,] array)
        {
            
            char[,] newArray = new char[arrayHeight, arrayWidth];
            for (int i = 0; i < arrayHeight; i++)
                for (int j = 0; j < arrayWidth; j++)
                    newArray[j, i] = array[i, j];

            return newArray;
        }

        private static char[,] TransposeCounterDiagonal(char[,] array)
        {
            char[,] newArray = new char[arrayHeight, arrayWidth];
            for (int i = 0; i < arrayHeight; i++)
                for (int j = 0; j < arrayWidth; j++)
                    newArray[arrayHeight - j - 1, arrayWidth - i - 1] = array[i, j];

            return newArray;
        }

        private static char[,] TransposeVertical(char[,] array)
        {
            char[,] newArray = new char[arrayHeight, arrayWidth];
            for (int i = 0; i < arrayHeight; i++)
                for (int j = 0; j < arrayWidth; j++)
                    newArray[i, arrayWidth - j - 1] = array[i, j];

            return newArray;
        }

        private static char[,] SlideLeft(char[,] array)
        {
            char[,] newArray = new char[arrayHeight, arrayWidth];
            for (int rowInd = 0; rowInd < arrayHeight; rowInd++)
            {
                var row = string.Join(string.Empty, GetRow(array, rowInd));
                var hashesIdx = Enumerable.Range(0, row.Length).Where(i => row[i] == '#').ToList();
                var splits = row.Split('#');

                int colInd = 0;
                for (int i = 0; i < splits.Length; i++)
                {
                    var cnt = splits[i].Count(x => x == 'O');
                    for (int k = 0; k < cnt; k++)
                    {
                        newArray[rowInd, colInd] = 'O';
                        colInd++;
                    }
                    for (int k = cnt; k < splits[i].Length; k++)
                    {
                        newArray[rowInd, colInd] = '.';
                        colInd++;
                    }

                    if (i == splits.Length - 1)
                        break;

                    newArray[rowInd, colInd] = '#';
                    colInd++;
                }
            }

            return newArray;
        }

        private static char[] GetRow(char[,] array, int rowInd)
        {
            var tempArr = new char[arrayWidth];
            for (int j = 0; j < arrayWidth; j++)
                tempArr[j] = array[rowInd, j];
            return tempArr;
        }

        private static int GetWeight(int rocksCnt, int hashInd, int colLength)
        {
            int sum = 0;
            for (int i = 0; i < rocksCnt; i++)
            {
                sum += (colLength - hashInd - 1 - i);
            }
            return sum;
        }

        private static string[] GetColumns(char[,] array)
        {
            string[] cols = new string[array.GetLength(1)];
            for (int i = 0; i < array.GetLength(1); i++)
            {
                var tempArr = new char[array.GetLength(0)];
                for (int j = 0; j < array.GetLength(0); j++)
                    tempArr[j] = array[j, i];
                cols[i] = string.Join(string.Empty, tempArr);
            }
            return cols;
        }
    }
}

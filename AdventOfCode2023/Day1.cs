namespace AdventOfCode2023
{
    public class Day1
    {
        public void Solution1()
        {
            string[] lines = File.ReadAllLines(@"..\..\..\inputs\input1-1.txt");

            int cnt = 0;
            foreach (var s in lines)
            {
                var num1 = FindFirstNumber(s);
                var num2 = FindLastNumber(s);
                cnt += num1 * 10 + num2;
            }

            Console.WriteLine(cnt);
            Console.ReadKey();
        }

        public void Solution2()
        {
            string[] lines = File.ReadAllLines(@"..\..\..\inputs\input1-1.txt");
            Dictionary<string, string> list = new()
            {
                { "one", "1" },
                { "two", "2" },
                { "three", "3" },
                { "four", "4" },
                { "five", "5" },
                { "six", "6" },
                { "seven", "7" },
                { "eight", "8" },
                { "nine", "9" }
            };

            int cnt = 0;
            foreach (var s in lines)
            {
                string sNew = s;

                var minInd = int.MaxValue;
                string numLeft = "";
                string numLeftReplace = "";
                var maxInd = int.MinValue;
                string numRight = "";
                string numRightReplace = "";

                foreach (var x in list)
                {
                    var ind = s.IndexOf(x.Key);
                    if (ind >= 0 && ind < minInd)
                    {
                        minInd = ind;
                        numLeft = x.Key;
                        numLeftReplace = x.Value;
                    }

                    var ind2 = s.LastIndexOf(x.Key);
                    if (ind2 >= 0 && ind2 > maxInd)
                    {
                        maxInd = ind2;
                        numRight = x.Key;
                        numRightReplace = x.Value;
                    }
                }

                if (numLeft != "")
                    sNew = s.Replace(numLeft, numLeftReplace);

                var num1 = FindFirstNumber(sNew);

                if (numRight != "")
                    sNew = s.Replace(numRight, numRightReplace);

                var num2 = FindLastNumber(sNew);

                cnt += num1 * 10 + num2;
            }

            Console.WriteLine(cnt);
            Console.ReadKey();
        }

        private static int FindFirstNumber(string sNew)
        {
            for (int j = 0; j < sNew.Length; j++)
            {
                if (int.TryParse(sNew[j].ToString(), out var num))
                    return num;
            }
            return -1;
        }

        private static int FindLastNumber(string s)
        {
            for (int k = s.Length - 1; k >= 0; k--)
            {
                if (int.TryParse(s[k].ToString(), out var num))
                    return num;
            }
            return -1;
        }
    }
}

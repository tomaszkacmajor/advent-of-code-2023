namespace AdventOfCode2023
{
    public class Day9
    {
        public void Solution1()
        {
            string[] lines = File.ReadAllLines(@"..\..\..\inputs\input9-1.txt");
            long sum = 0;

            foreach (var line in lines)
            {
                var nums = line.ParseIntsWithoutWords();
                long prediction = 0;
                List<int> temp;
                while(!nums.All(x => x == 0))
                {
                    temp = new();
                    prediction += nums.Last();
                    for (int i = 0; i < nums.Count - 1; i++)
                        temp.Add(nums[i + 1] - nums[i]);
                    nums = temp;
                }
                sum += prediction;
            }

            Console.WriteLine(sum);
            Console.ReadKey();
        }

        public void Solution2()
        {
            string[] lines = File.ReadAllLines(@"..\..\..\inputs\input9-1.txt");
            long sum = 0;

            foreach (var line in lines)
            {
                var nums = line.ParseIntsWithoutWords();
                long prediction = nums.First();
                List<int> temp;
                int signToggle = -1;
                while (!nums.All(x => x == 0))
                {
                    temp = new();
                    for (int i = 0; i < nums.Count - 1; i++)
                        temp.Add(nums[i + 1] - nums[i]);
                    nums = temp;
                    prediction += signToggle * nums.First();
                    signToggle *= -1;
                }
                sum += prediction;
            }

            Console.WriteLine(sum);
            Console.ReadKey();
        }
    }
}

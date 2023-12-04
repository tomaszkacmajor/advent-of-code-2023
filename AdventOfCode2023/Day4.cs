namespace AdventOfCode2023
{
    public class Day4
    {
        public void Solution1()
        {
            string[] lines = File.ReadAllLines(@"..\..\..\inputs\input4-1.txt");

            int sum = 0;
            foreach (var line in lines)
            {
                var split = line.Split(':', '|');
                var winningNumbers = split[1].Split().Where(x => x != string.Empty);
                var numbers = split[2].Split().Where(x => x != string.Empty);
                int cardValue = 0;

                foreach (var number in numbers)
                {
                    if (winningNumbers.Contains(number))
                    {
                        if (cardValue == 0)
                            cardValue = 1;
                        else
                            cardValue *= 2;
                    }
                }
                sum += cardValue;
            }

            Console.WriteLine(sum);
            Console.ReadKey();
        }

        public void Solution2()
        {
            string[] lines = File.ReadAllLines(@"..\..\..\inputs\input4-1.txt");

            int[] cardCounts = new int[lines.Length];
            Array.Fill(cardCounts, 1);

            for (int cardInd = 0; cardInd < lines.Length; cardInd++)
            {
                var split = lines[cardInd].Split(':', '|');
                var winningNumbers = split[1].Split().Where(x => x != string.Empty).ToList();
                var numbers = split[2].Split().Where(x => x != string.Empty).ToList();
                int tempInd = cardInd;

                foreach (var number in numbers)
                {
                    if (winningNumbers.Contains(number))
                        cardCounts[++tempInd] += cardCounts[cardInd];
                }
            }

            Console.WriteLine(cardCounts.Sum());
            Console.ReadKey();
        }
    }
}

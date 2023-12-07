namespace AdventOfCode2023
{
    public class Day7
    {
        public void Solution1(bool withJokers = false)
        {
            string[] lines = File.ReadAllLines(@"..\..\..\inputs\input7-1.txt");
            List<Hand> hands = new();
            List<Hand> orderedHands = new();

            foreach (var line in lines)
            {
                var hand = line.Split()[0];
                var bid = int.Parse(line.Split()[1].Trim());
                hands.Add(new Hand(bid, hand, withJokers));
            }

            foreach (var type in Enum.GetValues<HandType>())
            {
                var subset = hands.Where(x => x.Type == type);
                orderedHands.AddRange(subset.OrderBy(o => o.MappedHand).ToList());
            }

            long sum = 0;
            for (int i = 0; i < orderedHands.Count; i++) 
            {
                Hand hand = orderedHands[i];
                sum += hand.Bid * (orderedHands.Count - i);
            }

            Console.WriteLine(sum);
            Console.ReadKey();
        }

        public void Solution2()
        {
            Solution1(withJokers: true);
        }

        class Hand
        {
            public int Bid { get; }
            public HandType Type { get; }
            public string OrgHand { get; }
            public string MappedHand { get; } = "";

            private readonly Dictionary<char, string> CardMap = new()
            {
                {'A', "a"}, 
                {'K', "b"},
                {'Q', "c"},
                {'J', "d"},
                {'T', "e"},
                {'9', "f"},
                {'8', "g"},
                {'7', "h"},
                {'6', "i"},
                {'5', "j"},
                {'4', "k"},
                {'3', "l"},
                {'2', "m"},
            };

            public Hand(int bid, string hand, bool withJokers = false)
            {
                Bid = bid;
                OrgHand = hand;

                if (!withJokers)
                {
                    Type = GetType(hand);
                }
                else
                {
                    CardMap.Remove('J');
                    CardMap.Add('J', "o");

                    Type = GetTypeJokers(hand);
                }

                foreach (var h in hand)
                    MappedHand += CardMap[h];
            }

            private HandType GetType(string hand)
            {
                Dictionary<char, int> occurrences = new();

                foreach (var h in hand)
                {
                    if (occurrences.ContainsKey(h))
                        occurrences[h]++;
                    else
                        occurrences.Add(h, 1);
                }

                if (occurrences.ContainsValue(5))
                    return HandType.Five;
                else if (occurrences.ContainsValue(4))
                    return HandType.Four;
                else if (occurrences.ContainsValue(3) && occurrences.ContainsValue(2))
                    return HandType.FullHouse;
                else if (occurrences.ContainsValue(3))
                    return HandType.Three;
                else if (occurrences.Values.Count(x => x == 2) == 2)
                    return HandType.TwoPairs;
                else if (occurrences.ContainsValue(2))
                    return HandType.OnePair;
                else
                    return HandType.HighCard;
            }

            private HandType GetTypeJokers(string hand)
            {
                int minTypeValue = int.MaxValue;
                HandType bestHandType = HandType.HighCard;

                foreach(var card in CardMap.Keys.Where(x => x != 'J'))
                {
                    var replacedHand = hand.Replace("J", card.ToString());
                    var type = GetType(replacedHand);
                    if ((int)type < minTypeValue)
                    {
                        minTypeValue = (int)type;
                        bestHandType = type;
                    }
                }

                return bestHandType;
            }
        }

        public enum HandType
        {
            Five,
            Four,
            FullHouse,
            Three,
            TwoPairs,
            OnePair,
            HighCard
        }
    }
}

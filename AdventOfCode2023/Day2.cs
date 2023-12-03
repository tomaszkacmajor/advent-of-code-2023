namespace AdventOfCode2023
{
    public class Day2
    {
        public void Solution1()
        {
            string[] lines = File.ReadAllLines(@"..\..\..\inputs\input2-1.txt");

            int cnt = 0;
            foreach (var line in lines)
            {
                var split = line.Split(':', ';');
                var gameNo = split[0].RemoveWordGetInt("Game");
                int r = 0, g = 0, b = 0;
                bool gamePossible = true;

                foreach(var draw in split[1..])
                {
                    var colors = draw.Split(',');
                    foreach (var color in colors)
                    {
                        if (color.Contains("red"))
                            r = color.RemoveWordGetInt("red");
                        if (color.Contains("green"))
                            g = color.RemoveWordGetInt("green");
                        if (color.Contains("blue"))
                            b = color.RemoveWordGetInt("blue");

                        if (r > 12 || g > 13 || b > 14)
                        {
                            gamePossible = false;
                            break;
                        }
                    }

                    if (!gamePossible)
                        break;
                }

                if (gamePossible)
                    cnt += gameNo;
            }

            Console.WriteLine(cnt);
            Console.ReadKey();
        }

        public void Solution2()
        {
            string[] lines = File.ReadAllLines(@"..\..\..\inputs\input2-1.txt");

            int cnt = 0;
            foreach (var line in lines)
            {
                var split = line.Split(':', ';');
                var gameNo = split[0].RemoveWordGetInt("Game");
                int r = 0, g = 0, b = 0;
                int maxR = 0, maxG = 0, maxB = 0;

                foreach (var draw in split[1..])
                {
                    var colors = draw.Split(',');
                    foreach (var color in colors)
                    {
                        if (color.Contains("red"))
                        {
                            r = color.RemoveWordGetInt("red");
                            if (r > maxR)
                                maxR = r;
                        }
                        if (color.Contains("green"))
                        {
                            g = color.RemoveWordGetInt("green");
                            if (g > maxG)
                                maxG = g;
                        }
                        if (color.Contains("blue"))
                        {
                            b = color.RemoveWordGetInt("blue");
                            if (b > maxB)
                                maxB = b;
                        }
                    }
                }
                cnt += maxR * maxG * maxB;
            }

            Console.WriteLine(cnt);
            Console.ReadKey();
        }
    }
}

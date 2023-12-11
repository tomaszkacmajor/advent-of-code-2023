namespace AdventOfCode2023
{
    public class Day10
    {
        Dictionary<Direction, (int, int)> dirMap = new();

        public Day10()
        {
            dirMap.Add(Direction.North, (-1, 0));
            dirMap.Add(Direction.South, (1, 0));
            dirMap.Add(Direction.West, (0, -1));
            dirMap.Add(Direction.East, (0, 1));
        }

        public void Solution1()
        {
            string[] lines = File.ReadAllLines(@"..\..\..\inputs\input10-1.txt");
            char[,] array = lines.ToChar2DArray();
            FindStartPos(lines, out var startX, out var startY);

            array[startX, startY] = '7';
            var previousDir = Direction.South;

            int x = startX;
            int y = startY;
            int cnt = 0;

            do
            {
                var symbol = array[x, y];
                var directions = GetDirections(symbol);
                var nextDir = directions.First(x => x != previousDir);
                x += dirMap[nextDir].Item1;
                y += dirMap[nextDir].Item2;
                previousDir = GetOpposite(nextDir);
                cnt++;
            }
            while (startX != x || startY != y);

            Console.WriteLine(cnt / 2);
            Console.ReadKey();
        }

        public void Solution2()
        {
            string[] lines = File.ReadAllLines(@"..\..\..\inputs\input10-1.txt");
            char[,] array = lines.ToChar2DArray();
            FindStartPos(lines, out var startX, out var startY);

            array[startX, startY] = '7';
            var previousDir = Direction.South;

            int x = startX;
            int y = startY;

            ElementType[,] elemementTypes = new ElementType[array.GetLength(0), array.GetLength(1)];
            for (int i = 0; i < elemementTypes.GetLength(0); i++)
            {
                for (int j = 0; j < elemementTypes.GetLength(1); j++)
                    elemementTypes[i, j] = ElementType.NOT_LOOP;
            }

            do
            {
                var symbol = array[x, y];
                var directions = GetDirections(symbol);
                
                elemementTypes[x, y] = directions.Any(x => x == Direction.South) ? ElementType.LOOP_CONNECTED_SOUTH : ElementType.LOOP; 

                var nextDir = directions.First(x => x != previousDir);
                x += dirMap[nextDir].Item1;
                y += dirMap[nextDir].Item2;

                previousDir = GetOpposite(nextDir);
            }
            while (startX != x || startY != y);

            int cnt = 0;
            for (int i = 0; i < elemementTypes.GetLength(0); i++)
            {
                bool inside = false;
                for (int j = 0; j < elemementTypes.GetLength(1); j++)
                {
                    if (elemementTypes[i, j] == ElementType.LOOP_CONNECTED_SOUTH)
                        inside = !inside;
                    if (elemementTypes[i, j] == ElementType.NOT_LOOP && inside)
                        cnt++;

                }
            }

            Console.WriteLine(cnt);
            Console.ReadKey();
        }

        private static void FindStartPos(string[] lines, out int startX, out int startY)
        {
            startX = 0;
            startY = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                var j = lines[i].IndexOf('S');
                if (j >= 0)
                {
                    startX = i;
                    startY = j;
                    break;
                }
            }
        }

        private Direction GetOpposite(Direction nextDir)
        {
            switch(nextDir)
            {
                case Direction.North: return Direction.South;
                case Direction.South: return Direction.North;
                case Direction.West: return Direction.East;
                case Direction.East: return Direction.West;
                default: throw new ArgumentException();
            }
        }

        private List<Direction> GetDirections(char symbol)
        {
            switch (symbol)
            {
                case '|':
                    return new List<Direction> { Direction.North, Direction.South};
                case '-':
                    return new List<Direction> { Direction.East, Direction.West };
                case 'L':
                    return new List<Direction> { Direction.North, Direction.East };
                case 'J':
                    return new List<Direction> { Direction.North, Direction.West };
                case '7':
                    return new List<Direction> { Direction.South, Direction.West };
                case 'F':
                    return new List<Direction> { Direction.South, Direction.East };
                default: return new List<Direction>();
            }
        }

        private enum Direction
        {
            West, East, North, South
        }

        private enum ElementType
        {
            //Element doesn't belong to the loop
            NOT_LOOP,

            //element belongs to the loop and the pipe is connected to the south,
            LOOP_CONNECTED_SOUTH,

            //other element that belongs to the loop
            LOOP
        }
    }
}

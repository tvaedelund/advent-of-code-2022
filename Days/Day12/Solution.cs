using AdventOfCode.Helpers;

namespace AdventOfCode2022.Days.Day12;

public class Solution : ISolver
{
    public object PartOne(string input)
    {
        return GetShortesRoute(GetMap(input));
    }

    public object PartTwo(string input) => 0;

    int GetShortesRoute(Dictionary<Pos, int> map)
    {
        var startPos = map.Single(m => m.Value == 'S').Key;
        var endPos = map.Single(m => m.Value == 'E').Key;

        map[startPos] = 'a';
        map[endPos] = 'z';

        var pathLength = new Dictionary<Pos, int>();
        pathLength[startPos] = 0;

        var pq = new PriorityQueue<Pos, int>();
        pq.Enqueue(startPos, 0);

        while (true)
        {
            var current = pq.Dequeue();
            if (current == endPos)
            {
                break;
            }

            var adjecent = GetAdjacent(current).Where(a => map.ContainsKey(a));
            foreach (var a in adjecent)
            {
                if (map[a] <= map[current] + 1)
                {
                    var accumulatedLength = pathLength[current] + 1;
                    if (accumulatedLength < pathLength.GetValueOrDefault(a, int.MaxValue))
                    {
                        pathLength[a] = accumulatedLength;
                        pq.Enqueue(a, accumulatedLength);
                    }
                }
            }
        }

        return pathLength[endPos];
    }

    Dictionary<Pos, int> GetMap(string input)
    {
        var inputSplit = input.Split("\r\n");
        var ySize = inputSplit.Length;
        var xSize = inputSplit[0].Length;

        var map = from y in Enumerable.Range(0, ySize)
                  from x in Enumerable.Range(0, xSize)
                  select new KeyValuePair<Pos, int>(new Pos(x, y), inputSplit[y][x]);

        return map.ToDictionary(x => x.Key, x => x.Value);
    }

    IEnumerable<Pos> GetAdjacent(Pos pos)
    {
        return new[]
        {
            new Pos(pos.x, pos.y + 1),
            new Pos(pos.x + 1, pos.y),
            new Pos(pos.x, pos.y - 1),
            new Pos(pos.x - 1, pos.y),
        };
    }

    record Pos(int x, int y);
}

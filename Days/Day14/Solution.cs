using System.Text;
using AdventOfCode.Helpers;

namespace AdventOfCode2022.Days.Day14;

public class Solution : ISolver
{
    public object PartOne(string input)
    {
        ParseRockStructures(input);
        // PrintCave();
        DropSand();
        return rockStructures.Count(rs => rs.Value == 'O');
    }

    public object PartTwo(string input) => 0;

    record Pos(int x, int y);
    Dictionary<Pos, char> rockStructures = new();

    void DropSand()
    {
        while (true)
        {
            var current = new Pos(500, 0);
            // var stop = rockStructures.Where(rs => rs.Key.x == current.x).Select(rs => rs.Key).Min(rsk => rsk.y);
            while (true)
            {
                if (IsOutOfBounds(current))
                {
                    return;
                }

                if (CanDropDown(current))
                {
                    current = current with { x = current.x, y = current.y + 1 };
                }
                else if (CanDropLeft(current))
                {
                    current = current with { x = current.x - 1, y = current.y };
                }
                else if (CanDropRight(current))
                {
                    current = current with { x = current.x + 1, y = current.y };
                }
                else
                {
                    rockStructures[current] = 'O';
                    // PrintCave();
                    break;
                }
            }
        }
    }

    bool CanDropDown(Pos pos)
    {
        return !rockStructures.ContainsKey(new(pos.x, pos.y + 1));
    }

    bool CanDropLeft(Pos pos)
    {
        return !rockStructures.ContainsKey(new(pos.x - 1, pos.y + 1));
    }

    bool CanDropRight(Pos pos)
    {
        return !rockStructures.ContainsKey(new(pos.x + 1, pos.y + 1));
    }

    bool IsOutOfBounds(Pos pos)
    {
        return pos.x < rockStructures.Min(rs => rs.Key.x) || pos.x > rockStructures.Max(rs => rs.Key.x) || pos.y > rockStructures.Max(rs => rs.Key.y);
    }

    void ParseRockStructures(string input)
    {
        var rocks = input.Split("\r\n")
            .Distinct() // There's A LOT of duplicates
            .Select(x => x.Split(" -> ")
                .Select(y => y.Split(',')
                    .Select(int.Parse)
                    .ToArray())
                .ToArray())
               .Select(r => r.Select((p, i) => i < r.Count() - 1 ? (start: p, end: r[i + 1]) : (start: p, end: null))
                .SkipLast(1).ToArray())
            .ToArray();


        foreach (var rock in rocks)
        {
            foreach (var point in rock)
            {
                var diffX = point.start[0] - point.end[0];
                var diffY = point.start[1] - point.end[1];

                if (diffX > 0)
                {
                    for (int x = point.start[0]; x >= point.end[0]; x--)
                    {
                        rockStructures[new(x, point.start[1])] = '#';
                    }
                }

                if (diffX < 0)
                {
                    for (int x = point.start[0]; x <= point.end[0]; x++)
                    {
                        rockStructures[new(x, point.start[1])] = '#';
                    }
                }

                if (diffY > 0)
                {
                    for (int y = point.start[1]; y >= point.end[1]; y--)
                    {
                        rockStructures[new(point.start[0], y)] = '#';
                    }
                }

                if (diffY < 0)
                {
                    for (int y = point.start[1]; y <= point.end[1]; y++)
                    {
                        rockStructures[new(point.start[0], y)] = '#';
                    }
                }
            }
        }
    }

    void PrintCave()
    {
        var maxX = rockStructures.Max(r => r.Key.x);
        var maxY = rockStructures.Max(r => r.Key.y);
        var minX = rockStructures.Min(r => r.Key.x);
        var minY = rockStructures.Min(r => r.Key.y);

        var sb = new StringBuilder();

        for (int y = 0; y <= maxY; y++)
        {
            for (int x = minX; x <= maxX; x++)
            {
                var current = new Pos(x, y);
                sb.Append(rockStructures.ContainsKey(current) ? rockStructures[current] : '.');
            }

            sb.AppendLine();
        }

        Console.WriteLine(sb.ToString());
    }
}

using System.Text.RegularExpressions;
using AdventOfCode.Helpers;

namespace AdventOfCode2022.Days.Day09;

public class Solution : ISolver
{
    public object PartOne(string input) => CrossBridge(input).Distinct().Count();

    public object PartTwo(string input) => 0;

    record struct Pos(int x, int y);

    List<Pos> Visited = new();

    IEnumerable<Pos> CrossBridge(string input)
    {
        var head = new Pos(0, 0);
        var tail = new Pos(0, 0);
        var visited = new List<Pos>();

        foreach (var step in GetSteps(input))
        {
            var currentHead = head;

            head = step switch
            {
                "U" => head with { x = head.x, y = head.y - 1 },
                "D" => head with { x = head.x, y = head.y + 1 },
                "L" => head with { x = head.x - 1, y = head.y },
                "R" => head with { x = head.x + 1, y = head.y },
                _ => head
            };

            if ((GetDistance(head, tail) == 2 && !IsDiagonal(head, tail)) || GetDistance(head, tail) > 2)
            {
                tail = currentHead;
            }

            visited.Add(tail);
        }

        return visited;
    }

    bool IsDiagonal(Pos head, Pos tail)
    {
        return head.x != tail.x && head.y != tail.y;
    }

    int GetDistance(Pos head, Pos tail)
    {
        return Math.Abs(head.x - tail.x) + Math.Abs(head.y - tail.y);
    }

    IEnumerable<string> GetSteps(string input)
    {
        return input.Split("\r\n")
            .Select(x => x.Split(' '))
            .Select(x => (x[0], int.Parse(x[1])))
            .Select(x => Enumerable.Range(1, x.Item2)
                .Select(y => x.Item1))
            .SelectMany(x => x);
    }
}

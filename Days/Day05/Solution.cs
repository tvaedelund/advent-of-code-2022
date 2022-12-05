using System.Text.RegularExpressions;
using AdventOfCode.Helpers;

namespace AdventOfCode2022.Days.Day05;

public class Solution : ISolver
{
    public object PartOne(string input) => GetTopmostCrates(input);

    public object PartTwo(string input) => GetTopmostCrates(input, true);

    private string GetTopmostCrates(string input, bool isPartTwo = false)
    {
        var parts = input.Split("\r\n\r\n");
        var rows = parts[1].Split("\r\n");
        var stacks = ParseStacks(parts[0].Split("\r\n"));
        foreach (var row in rows)
        {
            var pattern = @"move (\d+) from (\d+) to (\d+)";
            var match = Regex.Match(row, pattern);
            var command = (count: int.Parse(match.Groups[1].Value), source: int.Parse(match.Groups[2].Value) - 1, target: int.Parse(match.Groups[3].Value) - 1);
            if (isPartTwo)
            {
                ExecuteCommand9001(stacks[command.source], stacks[command.target], command.count);
            }
            else
            {
                ExecuteCommand9000(stacks[command.source], stacks[command.target], command.count);
            }
        }
        
        return string.Join("", stacks.Select(x => x.Peek()));
    }

    private void ExecuteCommand9000(Stack<char> source, Stack<char> target, int count)
    {
        for (int i = 0; i < count; i++)
        {
            target.Push(source.Pop());
        }
    }

    private void ExecuteCommand9001(Stack<char> source, Stack<char> target, int count)
    {
        var temporary = new Stack<char>();
        ExecuteCommand9000(source, temporary, count);
        ExecuteCommand9000(temporary, target, count);
    }

    private List<Stack<char>> ParseStacks(string[] rows)
    {
        var stacks = rows.Last().Chunk(4).Select(c => new Stack<char>()).ToList();
   
        foreach (var row in rows.Reverse().Skip(1))
        {
            var crates = row.Chunk(4).ToList();
            for (int i = 0; i < crates.Count; i++)
            {
                if (crates[i][1] != ' ')
                {
                    stacks[i].Push(crates[i][1]);
                }
            }
        }

        return stacks;
    }
}

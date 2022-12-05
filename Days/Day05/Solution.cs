using System.Text.RegularExpressions;
using AdventOfCode.Helpers;

namespace AdventOfCode2022.Days.Day05;

public class Solution : ISolver
{
    public object PartOne(string input) => GetTopmostCrates(input);

    public object PartTwo(string input) => GetTopmostCrates(input, true);

    private string GetTopmostCrates(string input, bool isPartTwo = false)
    {
        var stacks = ParseStacks(input);
        var rows = input.Split("\r\n");
        foreach (var row in rows.SkipWhile(r => !string.IsNullOrWhiteSpace(r)).Skip(1))
        {
            var pattern = @"move (\d+) from (\d+) to (\d+)";
            var matches = Regex.Matches(row, pattern);
            var command = (int.Parse(matches[0].Groups[1].Value), int.Parse(matches[0].Groups[2].Value), int.Parse(matches[0].Groups[3].Value));
            stacks = isPartTwo ? ExecuteCommand9001(stacks, command) : ExecuteCommand(stacks, command);
        }
        
        return string.Join("", stacks.Select(x => x.Peek()));
    }

    private List<Stack<char>> ExecuteCommand9001(List<Stack<char>> stacks, (int count, int from, int to) command)
    {
        var cratesToMove = new List<char>();
        for (int i = 0; i < command.count; i++)
        {
            cratesToMove.Add(stacks[command.from - 1].Pop());
        }

        cratesToMove.Reverse();

        foreach (var crate in cratesToMove)
        {
            stacks[command.to - 1].Push(crate);
        }

        return stacks;
    }

    private List<Stack<char>> ExecuteCommand(List<Stack<char>> stacks, (int count, int from, int to) command)
    {
        for (int i = 0; i < command.count; i++)
        {
            var crateToMove = stacks[command.from - 1].Pop();
            stacks[command.to - 1].Push(crateToMove);
        }

        return stacks;
    }

    private List<Stack<char>> ParseStacks(string input)
    {
        var rows = input.Split("\r\n");
        var stacks = new List<Stack<char>>();
    
        foreach (var row in rows.TakeWhile(x => !string.IsNullOrWhiteSpace(x)).Reverse().Skip(1))
        {
            var crates = row.Select((x, i) => (x, i)).GroupBy(x => x.i / 4).Select(x => x.ToList()).ToList();
            for (int i = 0; i < crates.Count; i++)
            {
                if (stacks.Count < i + 1)
                {
                    stacks.Add(new Stack<char>());
                }
                
                if (crates[i][1].x == ' ')
                {
                    continue;
                }

                stacks[i].Push(crates[i][1].x);
            }
        }

        return stacks;
    }
}

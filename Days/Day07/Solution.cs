using System.Text.RegularExpressions;
using AdventOfCode.Helpers;

namespace AdventOfCode2022.Days.Day07;

public class Solution : ISolver
{
    Dictionary<string, int> fileSystem = new();

    public object PartOne(string input) => GetFileSystem(input)
        .Where(x => x.Value <= 100000)
        .Sum(x => x.Value);

    public object PartTwo(string input)
    {
        var max = GetFileSystem(input).Max(x => x.Value);
        return GetFileSystem(input)
            .Where(x => x.Value >= max - 40000000)
            .OrderBy(x => x.Value)
            .First().Value;
    }

    Dictionary<string, int> GetFileSystem(string input)
    {
        var fileSystem = new Dictionary<string, int>();
        var currentPath = new Stack<string>();

        foreach (var row in input.Split("\r\n"))
        {
            if (row == "$ cd ..")
            {
                _ = currentPath.Pop();
            }
            else if (row.StartsWith("$ cd"))
            {
                currentPath.Push(string.Join("", currentPath) + row.Split(' ')[2]);
            }
            else if (!row.StartsWith("$") && !row.StartsWith("dir"))
            {
                var size = int.Parse(row.Split(' ')[0]);
                foreach (var subpath in currentPath)
                {
                    fileSystem[subpath] = fileSystem.GetValueOrDefault(subpath) + size;
                }
            }
        }

        return fileSystem;
    }
}

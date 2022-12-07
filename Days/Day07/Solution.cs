using System.Text.RegularExpressions;
using AdventOfCode.Helpers;

namespace AdventOfCode2022.Days.Day07;

public class Solution : ISolver
{
    // < 18929272 (to high)
    // > 1680 (to low)
    // > 3487907 (to low)

    Dictionary<string, int> fileSystem = new();

    public object PartOne(string input) => GetFileSystem(input)
        .Where(x => x.Value <= 100000)
        .Sum(x => x.Value);

    public object PartTwo(string input) => GetFileSystem(input)
        .Where(x => x.Value >= (GetFileSystem(input).Max(x => x.Value) - 40000000))
        .OrderBy(x => x.Value)
        .First().Value;

    Dictionary<string, int> GetFileSystem(string input)
    {
        if (!fileSystem.Any())
        {
            var currentPath = "/";

            foreach (var row in input.Split("\r\n").Skip(1))
            {
                var test = row.Split(' ');
                if (test is [_, _, var path])
                {
                    if (path == "..")
                    {
                        currentPath = string.Join("/", currentPath.Split('/').SkipLast(1));
                    }
                    else
                    {
                        currentPath = $"{currentPath}/{path}";
                    }
                }
                else
                {
                    if (test[0] is not "dir" and not "$")
                    {
                        AddSizes(fileSystem, currentPath, int.Parse(test[0]));
                    }
                }
            }
        }

        return fileSystem;
    }

    void AddSizes(Dictionary<string, int> structure, string path, int size)
    {
        var pathSplit = path.Split('/').Skip(1).ToArray();
        var pathItem = string.Empty;
        for (int i = 0; i < pathSplit.Length; i++)
        {
            pathItem += $"/{string.Join("/", pathSplit.Skip(i).Take(1))}";
            if (structure.ContainsKey(pathItem))
            {
                structure[pathItem] += size;
            }
            else
            {
                structure[pathItem] = size;
            }
        }
    }
}

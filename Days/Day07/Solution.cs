using System.Text.RegularExpressions;
using AdventOfCode.Helpers;

namespace AdventOfCode2022.Days.Day07;

public class Solution : ISolver
{
    public object PartOne(string input) => GetSize(input);

    public object PartTwo(string input) => 0;

    int GetSize(string input)
    {
        return GetStructure(input).Where(x => x.Value <= 100000).Sum(x => x.Value);
    }

    Dictionary<string, int> GetStructure(string input)
    {
        var result = new Dictionary<string, int>();
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
                    AddSizes(result, currentPath, int.Parse(test[0]));
                }
            }
        }

        return result;
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

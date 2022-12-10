using System.Text.RegularExpressions;
using AdventOfCode.Helpers;

namespace AdventOfCode2022.Days.Day10;

public class Solution : ISolver
{
    public object PartOne(string input) => GetSignals(input).Where(x => x.tick is 20 or 60 or 100 or 140 or 180 or 220).Select(x => x.tick * x.X).Sum();

    public object PartTwo(string input) => 0;

    private IEnumerable<(int X, int tick)> GetSignals(string input)
    {
        var X = 1;
        var tick = 1;
        foreach (var line in input.Split("\r\n"))
        {
            var lineSplit = line.Split(' ');
            if (lineSplit.Length == 1)
            {
                yield return (X, tick++);
            }
            else
            {
                yield return (X, tick++);
                yield return (X, tick++);
                X += int.Parse(lineSplit[1]);
            }
        }
    }
}
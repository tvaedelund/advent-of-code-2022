using System.Text;
using System.Text.RegularExpressions;
using AdventOfCode.Helpers;

namespace AdventOfCode2022.Days.Day10;

public class Solution : ISolver
{
    public object PartOne(string input) => GetSignals(input).Where(x => x.cycle is 20 or 60 or 100 or 140 or 180 or 220).Select(x => x.cycle * x.X).Sum();

    public object PartTwo(string input)
    {
        var pixels = GetSignals(input);
        var screen = PrintScreen(pixels);
        return $"\r\n{screen}";
    }

    record Cycle(int X, int cycle, char pixel);

    private IEnumerable<Cycle> GetSignals(string input)
    {
        var X = 1;
        var cycle = 1;
        foreach (var line in input.Split("\r\n"))
        {
            var lineSplit = line.Split(' ');
            if (lineSplit.Length == 1)
            {
                yield return new(X, cycle++, DrawPixel(X, cycle));
            }
            else
            {
                yield return new(X, cycle++, DrawPixel(X, cycle));
                yield return new(X, cycle++, DrawPixel(X, cycle));
                X += int.Parse(lineSplit[1]);
            }
        }
    }

    private char DrawPixel(int X, int pos)
    {
        pos -= 2;
        pos %= 40;
        return (pos >= (X - 1) && pos <= (X + 1)) ? '#' : '.';
    }

    private string PrintScreen(IEnumerable<Cycle> cycles)
    {
        var sb = new StringBuilder();
        var rows = cycles.Chunk(40);
        foreach (var row in rows)
        {
            for (int i = 0; i < row.Length; i++)
            {
                sb.Append(row[i].pixel);
            }

            sb.AppendLine();
        }

        return sb.ToString();
    }
}
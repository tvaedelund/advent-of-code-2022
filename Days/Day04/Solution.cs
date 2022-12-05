using AdventOfCode.Helpers;

namespace AdventOfCode2022.Days.Day04;

public class Solution : ISolver
{
    public object PartOne(string input) => GetOverlaps(input).Where(x => x).Count();

    public object PartTwo(string input) => GetPartialOverlaps(input).Where(x => x).Count();

    private IEnumerable<bool> GetOverlaps(string input) =>
        from row in input.Split("\r\n")
        let pairs = (row.Split(',')[0].Split('-').Select(int.Parse).ToList(), row.Split(',')[1].Split('-').Select(int.Parse).ToList())
        let overlaps = (pairs.Item1[0] >= pairs.Item2[0] && pairs.Item1[1] <= pairs.Item2[1]) || (pairs.Item2[0] >= pairs.Item1[0] && pairs.Item2[1] <= pairs.Item1[1])
        select overlaps;

    private IEnumerable<bool> GetPartialOverlaps(string input) =>
        from row in input.Split("\r\n")
        let pairs = (row.Split(',')[0].Split('-').Select(int.Parse).ToList(), row.Split(',')[1].Split('-').Select(int.Parse).ToList())
        let overlaps = Math.Max(pairs.Item1[0], pairs.Item2[0]) <= Math.Min(pairs.Item1[1], pairs.Item2[1])
        select overlaps;
}

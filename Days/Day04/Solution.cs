using AdventOfCode.Helpers;

namespace AdventOfCode2022.Days.Day04;

public class Solution : ISolver
{
    public object PartOne(string input) => GetOverlaps(input).Where(x => x).Count();

    public object PartTwo(string input) => GetPartialOverlaps(input).Where(x => x).Count();

    private IEnumerable<bool> GetOverlaps(string input) =>
        from row in input.Split("\r\n")
        let pairs = (row.Split(',')[0].Split('-').Select(int.Parse).ToList(), row.Split(',')[1].Split('-').Select(int.Parse).ToList())
        let ranges = (Enumerable.Range(pairs.Item1[0], pairs.Item1[1] - pairs.Item1[0] + 1), Enumerable.Range(pairs.Item2[0], pairs.Item2[1] - pairs.Item2[0] + 1))
        let overlaps = ranges.Item1.Intersect(ranges.Item2).Count() == ranges.Item1.Count() || ranges.Item2.Intersect(ranges.Item1).Count() == ranges.Item2.Count()
        select overlaps;

    private IEnumerable<bool> GetPartialOverlaps(string input) =>
        from row in input.Split("\r\n")
        let pairs = (row.Split(',')[0].Split('-').Select(int.Parse).ToList(), row.Split(',')[1].Split('-').Select(int.Parse).ToList())
        let ranges = (Enumerable.Range(pairs.Item1[0], pairs.Item1[1] - pairs.Item1[0] + 1), Enumerable.Range(pairs.Item2[0], pairs.Item2[1] - pairs.Item2[0] + 1))
        let overlaps = ranges.Item1.Intersect(ranges.Item2).Count() > 0 || ranges.Item2.Intersect(ranges.Item1).Count() > 0
        select overlaps;
}

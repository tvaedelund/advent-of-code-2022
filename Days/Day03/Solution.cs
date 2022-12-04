using AdventOfCode.Helpers;

namespace AdventOfCode2022.Days.Day03;

public class Solution : ISolver
{
    public object PartOne(string input) => GetPriorities(input).Sum();

    public object PartTwo(string input) => GetBadgePriorities(input).Sum();

    private IEnumerable<int> GetPriorities(string input) =>
        from rucksack in input.Split("\r\n")
        let compartments = (rucksack[..(rucksack.Length / 2)], rucksack[(rucksack.Length / 2)..])
        let common = compartments.Item1.Intersect(compartments.Item2)
        from type in common
        let priority = type - 96 < 0 ? type - 38 : type - 96
        select priority;

    private IEnumerable<int> GetBadgePriorities(string input) =>
        from groups in input.Split("\r\n").Select((x, i) => new { Index = i, Value = x}).GroupBy(x => x.Index / 3)
        let first = groups.First(x => x.Index % 3 == 0).Value
        let second = groups.First(x => x.Index % 3 == 1).Value
        let third = groups.First(x => x.Index % 3 == 2).Value
        let temp = first.Intersect(second)
        let common = temp.Intersect(third)
        from badge in common
        let priority = badge - 96 < 0 ? badge - 38 : badge - 96
        select priority;
}

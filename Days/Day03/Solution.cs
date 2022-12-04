using AdventOfCode.Helpers;

namespace AdventOfCode2022.Days.Day03;

public class Solution : ISolver
{
    public object PartOne(string input) => GetPriorities(input).Sum();

    public object PartTwo(string input) => 0;

    private IEnumerable<int> GetPriorities(string input)
    {
        return from rucksack in input.Split("\r\n")
               let compartments = (rucksack[..(rucksack.Length / 2)], rucksack[(rucksack.Length / 2)..])
               let common = compartments.Item1.Intersect(compartments.Item2)
               from type in common
               let priority = type - 96 < 0 ? type - 38 : type - 96
               select priority;
    }
}

using AdventOfCode.Helpers;

namespace AdventOfCode2022.Days.Day03;

public class Solution : ISolver
{
    public object PartOne(string input) => GetPriorities(input).Sum();

    public object PartTwo(string input) => 0;

    private IEnumerable<int> GetPriorities(string input)
    {
        var rucksacks = from rucksack in input.Split("\r\n")
                        let compartments = (rucksack[..(rucksack.Length / 2)], rucksack[(rucksack.Length / 2)..])
                        select compartments;

        var commonItemTypes = from rucksack in rucksacks
                              let common = GetCommonItemTypes(rucksack)
                              select common.Distinct();

        var priorities = from type in commonItemTypes.SelectMany(x => x)
                         let priority = type - 96 < 0 ? type - 38 : type - 96
                         select priority;

        return priorities;
    }

    private IEnumerable<char> GetCommonItemTypes((string, string) rucksack)
    {
        foreach (var item in rucksack.Item1)
        {
            if (rucksack.Item2.Any(x => x == item))
            {
                yield return item;
            }
        }
    }
}

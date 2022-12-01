using AdventOfCode.Helpers;

namespace AdventOfCode2022.Days.Day01;

public class Solution : ISolver
{
    public object PartOne(string input) => GetCalories(input).First();

    public object PartTwo(string input) => GetCalories(input).Take(3).Sum();

    private IEnumerable<int> GetCalories(string input) =>
        from elf in input.Split($"{Environment.NewLine}{Environment.NewLine}")
        let calories = elf.Split(Environment.NewLine).Select(int.Parse).Sum()
        orderby calories descending
        select calories;
}
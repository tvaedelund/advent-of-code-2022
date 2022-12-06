using System.Text.RegularExpressions;
using AdventOfCode.Helpers;

namespace AdventOfCode2022.Days.Day06;

public class Solution : ISolver
{
    string inputTest = "nppdvjthqldpwncqszvftbrmjlhg";

    public object PartOne(string input) => GetPositionOfPacket(input, 4);

    public object PartTwo(string input) => GetPositionOfPacket(input, 14);

    private int GetPositionOfPacket(string input, int length)
    {
        return input.Select((c, i) => (((i + length < input.Length ? input[i..(i + length)] : "").Distinct().Count() == length), i + length)).First(x => x.Item1).Item2;
    }
}

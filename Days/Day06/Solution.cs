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
        // Sliding window of size 'length'
        // Check length of each distinct window, match = True, else False
        // Select first window that is True and its Index
        return input.Select((c, i) => (window: ((i + length < input.Length ? input[i..(i + length)] : "").Distinct().Count() == length), index: i + length)).First(x => x.window).index;
    }
}

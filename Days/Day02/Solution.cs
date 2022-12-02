using AdventOfCode.Helpers;

namespace AdventOfCode2022.Days.Day02;

public class Solution : ISolver
{
    public object PartOne(string input) => GetTotalScore(input).Sum();

    public object PartTwo(string input) => 0;

    private IEnumerable<int> GetTotalScore(string input)
    {
        return from round in input.Split("\r\n")
                     let score = GetScoreOfRound(round[0], round[2])
                     select score;
    }

    private int GetScoreOfRound(char Opponent, char Me) => (Opponent, Me) switch
    {
        ('A', 'X') => 1 + 3,
        ('A', 'Y') => 2 + 6,
        ('A', 'Z') => 3 + 0,
        ('B', 'X') => 1 + 0,
        ('B', 'Y') => 2 + 3,
        ('B', 'Z') => 3 + 6,
        ('C', 'X') => 1 + 6,
        ('C', 'Y') => 2 + 0,
        ('C', 'Z') => 3 + 3,
        _ => 0
    };
}

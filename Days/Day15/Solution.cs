using System.Text.RegularExpressions;
using AdventOfCode.Helpers;

namespace AdventOfCode2022.Days.Day15;

public class Solution : ISolver
{
    public object PartOne(string input) => Solve(input, 2000000);
    public object PartTwo(string input) => 0;

    int Solve(string input, int yLevel)
    {
        var readings = from line in input.Split("\r\n")
                       let groups = Regex.Match(line, @"Sensor at x=(-?\d+), y=(-?\d+): closest beacon is at x=(-?\d+), y=(-?\d+)").Groups
                       let reading = new Reading(new Pos(int.Parse(groups[1].Value), int.Parse(groups[2].Value)), new Pos(int.Parse(groups[3].Value), int.Parse(groups[4].Value)), yLevel)
                       select reading;

        var readingsCoveringRow = readings.Where(reading => reading.CoverageOfRow > 0)
            .SelectMany(reading => reading.Coverage)
            .Distinct()
            .ToList();

        return Math.Abs(readingsCoveringRow.Min(pos => pos.x)) + Math.Abs(readingsCoveringRow.Max(pos => pos.x));
    }

    record Pos(int x, int y);

    record Reading(Pos sensor, Pos beacon, int yLevel)
    {
        public int DistanceBetween => Math.Abs(sensor.x - beacon.x) + Math.Abs(sensor.y - beacon.y);
        public int DistanceToRow => Math.Abs(sensor.y - yLevel);
        public int CoverageOfRow => (DistanceBetween * 2 + 1) - DistanceToRow * 2;

        public IEnumerable<Pos> Coverage => Enumerable.Range(sensor.x - CoverageOfRow / 2, CoverageOfRow)
            .Select(x => new Pos(x, sensor.y));
    }
}

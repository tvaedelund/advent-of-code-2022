using System.Diagnostics;

namespace AdventOfCode2022.Days;

internal static class Day01
{
    internal static void Solve()
    {
        var sw = Stopwatch.StartNew();

        var data = File.ReadAllLines("Days\\Day01.txt");

        var sums = new List<int>(); 
        var sum = 0;

        foreach (var row in data)
        {
            if (string.IsNullOrEmpty(row))
            {
                sums.Add(sum);
                sum = 0;
                continue;
            }

            sum += int.Parse(row);
        }

        sums.Add(sum);
        
        var result = sums.Max();
        var resultTwo = sums.OrderByDescending(x => x).Take(3).Sum();

        sw.Stop();

        Console.WriteLine($"ResultOne: {result}");
        Console.WriteLine($"ResultTwo: {resultTwo}");
        Console.WriteLine($"Time: {sw.ElapsedMilliseconds} ms");
    }
}
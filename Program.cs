using System.Reflection;
using AdventOfCode.Helpers;

var day = "Day02";

Console.WriteLine($"Advent of Code 2022: {day}");

var solvers = from t in Assembly.GetExecutingAssembly().GetTypes()
                where t.GetInterfaces().Contains(typeof(ISolver))
                         && t.GetConstructor(Type.EmptyTypes) != null
                select Activator.CreateInstance(t) as ISolver;

var solution = solvers.First(x => x.ToString()!.Contains(day));
var input = File.ReadAllText(@$"days\{day}\input.txt");

if (input.EndsWith("\r\n"))
{
    input = input.Substring(0, input.Length - 2);
}

Console.WriteLine($"PartOne: {solution.PartOne(input)}");
Console.WriteLine($"PartTwo: {solution.PartTwo(input)}");

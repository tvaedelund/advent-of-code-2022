﻿using System.Diagnostics;
using System.Reflection;
using AdventOfCode.Helpers;

var day = "Day03";
var isTest = false;
var fileName = isTest ? @$"days\{day}\input-test.txt" : @$"days\{day}\input.txt";

Console.WriteLine($"Advent of Code 2022: {day}");

var solvers = from t in Assembly.GetExecutingAssembly().GetTypes()
                where t.GetInterfaces().Contains(typeof(ISolver))
                         && t.GetConstructor(Type.EmptyTypes) != null
                select Activator.CreateInstance(t) as ISolver;

var solution = solvers.First(x => x.ToString()!.Contains(day));
var input = File.ReadAllText(fileName);

if (input.EndsWith("\r\n"))
{
    input = input.Substring(0, input.Length - 2);
}

var sw = Stopwatch.StartNew();

Console.WriteLine($"PartOne: {solution.PartOne(input)}");
Console.WriteLine($"TimeOne: {sw.ElapsedMilliseconds}ms");

Console.WriteLine($"PartTwo: {solution.PartTwo(input)}");
Console.WriteLine($"TimeTwo: {sw.ElapsedMilliseconds}ms");

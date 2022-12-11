using System.Text;
using System.Text.RegularExpressions;
using AdventOfCode.Helpers;

namespace AdventOfCode2022.Days.Day11;

public class Solution : ISolver
{
    public object PartOne(string input)
    {
        var monkeys = Parse(input).ToList();
        
        for (int i = 0; i < 20; i++)
        {
            InspectItems(monkeys);
        }

        var topMonkeys = monkeys.Select(monkey => monkey.inspections.Sum()).OrderByDescending(sum => sum).Take(2).ToList();

        return topMonkeys[0] * topMonkeys[1];
    }

    public object PartTwo(string input) => 0;

    record Monkey(Queue<long> items, int test, int ifTrue, int ifFalse, string operation, List<int> inspections);

    void InspectItems(List<Monkey> monkeys)
    {
        foreach (var monkey in monkeys)
        {
            while (monkey.items.Any())
            {
                var item = monkey.items.Dequeue();
                var newItem = DoMonkeyBusiness(item, monkey.operation) / 3;
                monkeys[newItem % monkey.test == 0 ? monkey.ifTrue : monkey.ifFalse].items.Enqueue(newItem);
                monkey.inspections.Add(1);
            }
        }
    }

    long DoMonkeyBusiness(long old, string operation)
    {
        var opSplit = operation.Split(' ');
        var end = opSplit[2] == "old" ? old : long.Parse(opSplit[2]);

        return opSplit[1] switch
        {
            "+" => old + end,
            "-" => old - end,
            "*" => old * end,
            "/" => old / end,
            _ => old
        };
    }

    IEnumerable<Monkey> Parse(string input)
    {
        var monkeys = input.Split("\r\n\r\n");

        foreach (var monkey in monkeys)
        {
            var monkeyRows = monkey.Split("\r\n");
            var startingItems = new Queue<long>();
            var operation = string.Empty;
            var test = 0;
            var ifTrue = 0;
            var ifFalse = 0;
            foreach (var row in monkeyRows)
            {
                if (row.Contains("Starting items:"))
                {
                    foreach (var item in row.Split(": ")[1].Split(", ").Select(int.Parse).ToList())
                    {
                        startingItems.Enqueue(item);
                    }
                }
                else if (row.Contains("Operation:"))
                {
                    operation = row.Replace("  Operation: new = ", "");
                }
                else if (row.Contains("Test:"))
                {
                    test = int.Parse(row.Replace("  Test: divisible by ", ""));
                }
                else if (row.Contains("If true:"))
                {
                    ifTrue = int.Parse(row.Last().ToString());
                }
                else if (row.Contains("If false:"))
                {
                    ifFalse = int.Parse(row.Last().ToString());
                }
            }

            yield return new Monkey(startingItems, test, ifTrue, ifFalse, operation, new());
        }
    }
}
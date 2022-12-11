using AdventOfCode.Helpers;

namespace AdventOfCode2022.Days.Day11;

public class Solution : ISolver
{
    public object PartOne(string input)
    {
        var monkeys = Parse(input).ToList();

        for (int i = 0; i < 20; i++)
        {
            InspectItems(monkeys, worryLevel => worryLevel / 3);
        }

        var topMonkeys = monkeys.OrderByDescending(m => m.inspections).Take(2).ToList();

        return topMonkeys[0].inspections * topMonkeys[1].inspections;
    }

    public object PartTwo(string input)
    {
        var monkeys = Parse(input).ToList();

        // https://en.wikipedia.org/wiki/Chinese_remainder_theorem
        var mod = 1;
        foreach (var m in monkeys)
        {
            mod *= m.test;
        }

        for (int i = 0; i < 10000; i++)
        {
            InspectItems(monkeys, worryLevel => worryLevel % mod);
        }

        var topMonkeys = monkeys.OrderByDescending(m => m.inspections).Take(2).ToList();

        return (long)topMonkeys[0].inspections * (long)topMonkeys[1].inspections;
    }


    record Monkey(Queue<long> items, int test, int ifTrue, int ifFalse, Func<long, long> operation)
    {
        public int inspections { get; set; }
    }

    void InspectItems(List<Monkey> monkeys, Func<long, long> calcWorryLevel)
    {
        foreach (var monkey in monkeys)
        {
            while (monkey.items.Any())
            {
                var item = monkey.items.Dequeue();
                var newItem = calcWorryLevel(monkey.operation(item));
                monkeys[newItem % monkey.test == 0 ? monkey.ifTrue : monkey.ifFalse].items.Enqueue(newItem);
                monkey.inspections++;
            }
        }
    }

    IEnumerable<Monkey> Parse(string input)
    {
        var monkeys = input.Split("\r\n\r\n");

        foreach (var monkey in monkeys)
        {
            var instructions = monkey.Split("\r\n");
            var startingItems = new Queue<long>(instructions[1].Split("Starting items: ")[1].Split(", ").Select(long.Parse));

            Func<long, long> operation;
            if (instructions[2].Contains("Operation: new = old * old"))
            {
                operation = (long old) => old * old;
            }
            else if (instructions[2].Contains("Operation: new = old * "))
            {
                var end = int.Parse(instructions[2].Split("Operation: new = old * ")[1]);
                operation = (long old) => old * end;
            }
            else
            {
                var end = int.Parse(instructions[2].Split("Operation: new = old + ")[1]);
                operation = (long old) => old + end;
            }

            var test = int.Parse(instructions[3].Split("Test: divisible by ")[1]);
            var ifTrue = int.Parse(instructions[4].Split("If true: throw to monkey ")[1]);
            var ifFalse = int.Parse(instructions[5].Split("If false: throw to monkey ")[1]);

            yield return new Monkey(startingItems, test, ifTrue, ifFalse, operation);
        }
    }
}
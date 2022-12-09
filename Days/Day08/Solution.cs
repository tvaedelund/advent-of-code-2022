using System.Text.RegularExpressions;
using AdventOfCode.Helpers;

namespace AdventOfCode2022.Days.Day08;

public class Solution : ISolver
{
    public object PartOne(string input) => GetVisibleTrees(input);

    public object PartTwo(string input) => 0;

    record Tree(int x, int y, int height);

    int GetVisibleTrees(string input)
    {
        var forest = input.Split("\r\n")
            .SelectMany((treeLine, x) => treeLine.Select((tree, y) => new Tree(x, y, int.Parse(tree.ToString()))))
            .ToHashSet();

        var maxX = forest.Max(tree => tree.x);
        var maxY = forest.Max(tree => tree.y);

        var visibleTrees = forest.Where(tree => tree.x > 0 && tree.x < maxX && tree.y > 0 && tree.y < maxY)
            .Select(tree => IsVisible(tree, forest))
            .Count(tree => tree);

        return visibleTrees + 2 * maxX + 2 * maxY;
    }

    bool IsVisible(Tree tree, IEnumerable<Tree> trees)
    {
        var maxHeightDown = trees
            .Where(t => t.x > tree.x && t.y == tree.y)
            .Max(ttc => ttc.height);

        var maxHeightLeft = trees
            .Where(t => t.x == tree.x && t.y < tree.y)
            .Max(ttc => ttc.height);

        var maxHeightRight = trees
            .Where(t => t.x == tree.x && t.y > tree.y)
            .Max(ttc => ttc.height);

        var maxHeightUp = trees
            .Where(t => t.x < tree.x && t.y == tree.y)
            .Max(ttc => ttc.height);

        return tree.height > maxHeightDown || tree.height > maxHeightLeft || tree.height > maxHeightRight || tree.height > maxHeightUp;
    }
}

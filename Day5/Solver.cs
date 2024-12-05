using SuperLinq;

namespace AdventOfCode2024.Day5;

internal class Solver
{
    readonly List<(int, int)> rules = [];
    readonly List<(int[], bool)> updates = [];

    public Solver(bool sample)
    {
        var fileName = sample ? "sample.txt" : "input.txt";
        var rawInput = File.ReadAllText($"./Day5/{fileName}");
        var data = rawInput.ToLines(options: StringSplitOptions.TrimEntries);
        var storingRules = true;
        foreach (var line in data)
        {
            if (string.IsNullOrEmpty(line))
            {
                storingRules = false;
                continue;
            }
            if (storingRules)
            {
                var rule = line.Split('|');
                rules.Add((int.Parse(rule[0]), int.Parse(rule[1])));
            }
            else
            {
                updates.Add((line.Split(',').Select(x => int.Parse(x)).ToArray(), true));
            }
        }
        foreach (var rule in rules)
        {
            CheckRuleAgainstUpdates(rule);
        }
    }

    private void CheckRuleAgainstUpdates((int, int) rule)
    {
        for (var i = 0; i < updates.Count; i++)
        {
            if (updates[i].Item2)
            {
                var page1Index = updates[i].Item1.IndexOf(rule.Item1);
                var page2Index = updates[i].Item1.IndexOf(rule.Item2);
                if (page1Index != -1 && page2Index != -1)
                {
                    if (page1Index > page2Index)
                    {
                        var update = updates[i];
                        update.Item2 = false;
                        updates[i] = update;
                    }
                }
            }
        }
    }

    public string Part1()
    {
        var result = 0;
        foreach (var update in updates.Where(u => u.Item2))
        {
            result += FindMiddleNumber(update.Item1);
        }
        return result.ToString();
    }

    public string Part2()
    {
        var result = 0;
        var broken = updates.Where(u => !u.Item2).Select(u => u.Item1);
        foreach (var item in broken)
        {
            result += FindMiddleNumber([.. item.Order(new UpdateComparer(rules))]);
        }
        return result.ToString();
    }

    private static int FindMiddleNumber(int[] update)
    {
        var middleIndex = (update.Length - 1) / 2;
        return update[middleIndex];
    }

}

public class UpdateComparer(List<(int, int)> rules) : IComparer<int>
{
    public int Compare(int x, int y)
    {
        if (rules.Contains((x, y)))
        {
            return -1;
        }
        return 1;
    }
}
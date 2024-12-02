using SuperLinq;

namespace AdventOfCode2024.Day2;

internal class Solver
{
    string[] data;
    public Solver(bool sample)
    {
        var fileName = sample ? "sample.txt" : "input.txt";
        var rawInput = File.ReadAllText($"./Day2/{fileName}");
        data = rawInput.ToLines();
    }

    public string Part1() => data.Select(line => line.Split()
                                                     .Select(level => int.Parse(level))
                                                     .ToArray())
                                 .Count(IsSafe)
                                 .ToString();

    public string Part2() => data.Select(line => line.Split()
                                                     .Select(level => int.Parse(level))
                                                     .ToArray())
                                 .Count(IsSafeSkipOne)
                                 .ToString();

    private static bool IsSafe(int[] levels)
    {
        if (levels.Window(2).All(w => w[0] - w[1] is >= 1 and <= 3))
        {
            return true;
        }
        if (levels.Window(2).All(w => w[1] - w[0] is >= 1 and <= 3))
        {
            return true;
        }
        return false;
    }

    private static bool IsSafeSkipOne(int[] levels)
    {
        for (var i = 0; i < levels.Length; i++)
        {
            if (IsSafe([.. levels[..i], .. levels[(i + 1)..]]))
            {
                return true;
            }
        }
        return false;
    }
}

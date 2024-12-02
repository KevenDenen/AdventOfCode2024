namespace AdventOfCode2024.Day1;

internal class Solver
{
    readonly List<long> left = [];
    readonly List<long> right = [];

    public Solver(bool sample)
    {
        var fileName = sample ? "sample.txt" : "input.txt";
        var rawInput = File.ReadAllText($"./Day1/{fileName}");
        foreach (var line in rawInput.ToLines())
        {
            var split = line.Split(' ', options: StringSplitOptions.RemoveEmptyEntries);
            left.Add(long.Parse(split[0]));
            right.Add(long.Parse(split[1]));
        }             
    }

    public string Part1()
    {
        left.Sort();
        right.Sort();
        return left.Zip(right, (l, r) => Math.Abs(l - r)).Sum().ToString();
    }

    public string Part2()
    {
        var countRight = right.CountBy(r => r).ToDictionary();
        return left.Select(l => l * countRight.GetValueOrDefault(l, 0)).Sum().ToString();
    }
}

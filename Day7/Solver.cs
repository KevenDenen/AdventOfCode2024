namespace AdventOfCode2024.Day7;

internal class Solver
{

    readonly string[] data;
    public Solver(bool sample)
    {
        var fileName = sample ? "sample.txt" : "input.txt";
        var rawInput = File.ReadAllText($"./Day7/{fileName}");
        data = rawInput.ToLines();
    }

    public string Solve(bool part2)
    {
        var result = 0L;
        foreach (var line in data)
        {
            var target = long.Parse(line.Split(':')[0]);
            var operands = line.Split(":")[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(s => long.Parse(s)).ToArray();
            if (IsValid(target, operands, part2))
            {
                result += target;
            }
        }
        return result.ToString();
    }

    private static bool IsValid(long target, long[] operands, bool part2)
    {
        if (operands.Length == 0)
        {
            return target == 0;
        }
        if (operands.Length == 1)
        {
            return target == operands[0];
        }
        if (IsValid(target, [(operands[0] + operands[1]), .. operands[2..]], part2))
        {
            return true;
        }
        if (IsValid(target, [(operands[0] * operands[1]), .. operands[2..]], part2))
        {
            return true;
        }
        if (part2 && IsValid(target, [ConcatLongs(operands[0], operands[1]), .. operands[2..]], part2))
        {
            return true;
        }
        return false;
    }

    private static long ConcatLongs(long v1, long v2)
    {
        return long.Parse(v1.ToString() + v2.ToString());
    }
}

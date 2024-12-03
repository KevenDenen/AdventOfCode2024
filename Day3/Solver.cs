using System.Text.RegularExpressions;
using System.Linq;

namespace AdventOfCode2024.Day3;

internal partial class Solver
{
    readonly string data;

    [GeneratedRegex(@"mul\((\d+),(\d+)\)")]
    internal static partial Regex Part1Regex();
    [GeneratedRegex(@"mul\((?<first>\d+),(?<second>\d+)\)|(?<disable>don't\(\))|(?<enable>do\(\))")]
    internal static partial Regex Part2Regex();

    public Solver(bool sample)
    {
        var fileName = sample ? "sample.txt" : "input.txt";
        data = File.ReadAllText($"./Day3/{fileName}");
    }

    public string Part1()
    {
        return Part1Regex().Matches(data)
                           .Select(match => int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value))
                           .Sum()
                           .ToString();
    }

    public string Part2()
    {
        var matchList = Part2Regex().Matches(data);

        var enabled = true;
        int sum = 0;
        foreach (Match match in matchList)
        {
            if (match.Groups["enable"].Success)
            {
                enabled = true;
            }
            else if (match.Groups["disable"].Success)
            {
                enabled = false;
            }
            else if (enabled)
            {
                sum += int.Parse(match.Groups["first"].Value) * int.Parse(match.Groups["second"].Value);
            }
        }
        return sum.ToString();
    }

}

namespace AdventOfCode2024;

internal static class Shared
{
    public static string[] ToLines(this string input,
                                   StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
    {
        return input.Split(["\r\n", "\r", "\n"], options);
    }
}

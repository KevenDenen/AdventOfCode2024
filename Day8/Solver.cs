namespace AdventOfCode2024.Day8;

internal class Solver
{

    readonly string[] data;
    HashSet<(int x, int y, char c)> antennas = [];
    private readonly int width;
    private readonly int height;

    public Solver(bool sample)
    {
        var fileName = sample ? "sample.txt" : "input.txt";
        var rawInput = File.ReadAllText($"./Day8/{fileName}");
        data = rawInput.ToLines();
        width = data[0].TrimEnd().Length;
        height = data.Length;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (char.IsLetterOrDigit(data[y][x]))
                {
                    antennas.Add((x, y, data[y][x]));
                }
            }
        }
    }

    public string Part1()
    {
        HashSet<(int x, int y)> antinodes = [];

        foreach (var firstAntenna in antennas)
        {
            foreach (var otherAntenna in antennas)
            {
                if (firstAntenna == otherAntenna || firstAntenna.c != otherAntenna.c)
                {
                    continue;
                }
                
                int differenceInX = otherAntenna.x - firstAntenna.x;
                int differenceInY = otherAntenna.y - firstAntenna.y;
                int newXPosition = firstAntenna.x - differenceInX;
                int newYPosition = firstAntenna.y - differenceInY;

                if (newXPosition >= 0 && newXPosition < width && newYPosition >= 0 && newYPosition < height)
                {
                    antinodes.Add((newXPosition, newYPosition));
                }
            }
        }

        return antinodes.Count.ToString();
    }

    public string Part2()
    {
        HashSet<(int x, int y)> antinodes = [];

        int width = data[0].TrimEnd().Length;
        int height = data.Length;

        foreach (var firstAntenna in antennas)
        {
            foreach (var otherAntenna in antennas)
            {
                if (firstAntenna == otherAntenna || firstAntenna.c != otherAntenna.c)
                {
                    continue;
                }

                int differenceInX = otherAntenna.x - firstAntenna.x;
                int differenceInY = otherAntenna.y - firstAntenna.y;
                int newXPosition = firstAntenna.x - differenceInX;
                int newYPosition = firstAntenna.y - differenceInY;

                antinodes.Add((otherAntenna.x, otherAntenna.y));

                int i = 1;
                while (newXPosition >= 0 && newXPosition < width && newYPosition >= 0 && newYPosition < height)
                {
                    antinodes.Add((newXPosition, newYPosition));
                    i++;
                    newXPosition = firstAntenna.x - differenceInX * i;
                    newYPosition = firstAntenna.y - differenceInY * i;
                }
            }
        }

        return antinodes.Count.ToString();
    }
}

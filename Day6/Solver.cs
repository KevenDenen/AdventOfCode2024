namespace AdventOfCode2024.Day6;

internal class Solver
{
    readonly char[,] data;
    HashSet<(int, int)> mainPath;

    public Solver(bool sample)
    {
        var fileName = sample ? "sample.txt" : "input.txt";
        var rawInput = File.ReadAllText($"./Day6/{fileName}");
        var rawData = rawInput.ToLines();
        data = new char[rawData.Length, rawData[0].Length];
        for (int i = 0; i < rawData.Length; i++)
        {
            for (int j = 0; j < rawData[i].Length; j++)
            {
                data[i, j] = rawData[i][j];
            }
        }
        mainPath = [];
    }

    public string Part1()
    {
        var location = FindGuard();
        mainPath.Add(location);
        (int, int) facing = (-1, 0);

        var leftArea = false;
        while (!leftArea)
        {
            (location, facing, leftArea) = CheckAndMove(data, location, facing);
            mainPath.Add(location);
        }

        return mainPath.Count.ToString();
    }

    public string Part2()
    {
        var count = 0;
        foreach (var location in mainPath)
        {
            (int row, int col) = location;
            if (data[row, col] != '.')
            {
                continue;
            }
            var guard = FindGuard();
            (int, int) facing = (-1, 0);

            var coveredPart2 = new HashSet<((int, int), (int, int))>
            {
                (guard, facing)
            };

            var original = data[row, col];
            data[row, col] = '#';
            while (true)
            {
                (guard, facing, var leftArea) = CheckAndMove(data, guard, facing);
                if (leftArea)
                {
                    break;
                }
                if (!coveredPart2.Add((guard, facing)))
                {
                    count++;
                    break;
                }
            }
            data[row, col] = original;
        }
        return count.ToString();
    }

    private static ((int, int) location, (int, int) facing, bool leftArea) CheckAndMove(char[,] data, (int, int) location, (int, int) facing)
    {
        (int, int) newFacing;
        (int, int) newLocation;
        (int, int) lookingAt = (location.Item1 + facing.Item1, location.Item2 + facing.Item2);
        if (lookingAt.Item1 < 0 || lookingAt.Item1 >= data.GetLength(0) || lookingAt.Item2 < 0 || lookingAt.Item2 >= data.GetLength(0))
        {
            return (location, facing, true);
        }
        if (data[lookingAt.Item1, lookingAt.Item2] == '#')
        {
            newFacing = GetNextFacing(facing);
            newLocation = location;
        }
        else
        {
            newFacing = facing;
            newLocation = lookingAt;
        }
        return (newLocation, newFacing, false);
    }

    private static (int, int) GetNextFacing((int, int) current) => current switch
    {
        (-1, 0) => (0, 1),
        (0, 1) => (1, 0),
        (1, 0) => (0, -1),
        (0, -1) => (-1, 0),
        _ => (-1, 0)
    };

    private (int, int) FindGuard()
    {
        for (int row = 0; row < data.GetLength(0); row++)
        {
            for (int col = 0; col < data.GetLength(1); col++)
            {
                if (data[row, col] == '^')
                {
                    return (row, col);
                }

            }
        }
        return (0, 0);
    }

}

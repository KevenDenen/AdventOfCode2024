using SuperLinq;

namespace AdventOfCode2024.Day4;

internal class Solver
{
    string[] data;
    public Solver(bool sample)
    {
        var fileName = sample ? "sample.txt" : "input.txt";
        var rawInput = File.ReadAllText($"./Day4/{fileName}");
        data = rawInput.ToLines();
    }

    public string Part1()
    {
        int count = 0;
        for (int row = 0; row < data.Length; row++) 
        {
            for (int col = 0; col < data[row].Length; col++)
            {
                if (data[row][col] == 'X')
                {
                    count += CheckForXMAS(row, col);
                }
            }
        }
        return count.ToString();
    }

    private int CheckForXMAS(int row, int col)
    {
        return CheckDirection(row, col, -1, -1) +
               CheckDirection(row, col, 0, -1) +
               CheckDirection(row, col, 1, -1) +
               CheckDirection(row, col, 1, 0) +
               CheckDirection(row, col, 1, 1) +
               CheckDirection(row, col, 0, 1) +
               CheckDirection(row, col, -1, 1) +
               CheckDirection(row, col, -1, 0);
    }

    private int CheckDirection(int row, int col, int rowOffset, int colOffset)
    {
        if(CheckLetter(row, col, rowOffset, colOffset, 'M') &&
               CheckLetter(row, col, rowOffset * 2, colOffset * 2, 'A') &&
               CheckLetter(row, col, rowOffset * 3, colOffset * 3, 'S'))
        {
            return 1;
        }
        return 0;
    }

    private bool CheckLetter(int row, int col, int rowOffset, int colOffset, char letter)
    {
        if (row + rowOffset < 0 || row + rowOffset >= data.Length)
        {
            return false;
        }
        if (col + colOffset < 0 || col + colOffset >= data[row + rowOffset].Length)
        {
            return false;
        }
        return data[row + rowOffset][col + colOffset] == letter;
    }
    public string Part2() 
    {
        int count = 0;
        for (int row = 0; row < data.Length; row++)
        {
            for (int col = 0; col < data[row].Length; col++)
            {
                if (data[row][col] == 'A')
                {
                    count += CheckForMAS(row, col);
                }
            }
        }
        return count.ToString();
    }

    private int CheckForMAS(int row, int col)
    {
        var downRight = CheckLetter(row, col, -1, -1, 'M') && CheckLetter(row, col, 1, 1, 'S') ||
                        CheckLetter(row, col, 1, 1, 'M') && CheckLetter(row, col, -1, -1, 'S');
        var upRight = CheckLetter(row, col, 1, -1, 'M') && CheckLetter(row, col, -1, 1, 'S') ||
                      CheckLetter(row, col, -1, 1, 'M') && CheckLetter(row, col, 1, -1, 'S');
        return downRight && upRight ? 1 : 0;
    }
}

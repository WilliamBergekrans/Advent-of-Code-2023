namespace AdventOfCode2023.Solutions;

public class Day9: ISolution
{
    public int SolutionPartOne(List<string> textInput)
    {
        List<List<int>> numberInput = textInput.Select(row =>
        {
            return row.Split(' ').Select(c => int.Parse(c.Trim())).ToList();
        }).ToList();
        
        int sum = numberInput.Select(GetNextNumber).Sum();
        return sum;
    }

    public int SolutionPartTwo(List<string> textInput)
    {
        List<List<int>> numberInput = textInput.Select(row =>
        {
            return row.Split(' ').Select(c => int.Parse(c.Trim())).ToList();
        }).ToList();
        
        int sum = numberInput.Select(GetNextNumberPart2).Sum();
        return sum;
    }

    private int GetNextNumber(List<int> numbers)
    {
        if (numbers.All(n => n == 0))
        {
            return 0;
        }

        List<int> differences = new();
        for (int i = 0; i < numbers.Count-1; i++)
        {
             differences.Add(numbers[i+1] - numbers[i]);
        }

        return numbers.Last() + GetNextNumber(differences);
    }
    
    private int GetNextNumberPart2(List<int> numbers)
    {
        if (numbers.All(n => n == 0))
        {
            return 0;
        }

        List<int> differences = new();
        for (int i = 0; i < numbers.Count-1; i++)
        {
            differences.Add(numbers[i+1] - numbers[i]);
        }

        return numbers.First() - GetNextNumberPart2(differences);
    }
}
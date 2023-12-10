namespace AdventOfCode2023;
using System.IO;

public static class Program
{
    private static Task Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine($"What day to you want to run this for?");
            return Task.CompletedTask;
        }
        int dayNumber = int.Parse(args[0]);
        string filename = $"day{dayNumber}";
        Console.WriteLine(filename);
        if (File.Exists(filename))
        {
            List<string> input = File.ReadAllLines(filename).ToList();

            // Dynamically create an instance of the correct day class
            Type? dayType = Type.GetType($"AdventOfCode2023.Solutions.Day{dayNumber}");
            if (dayType != null && typeof(ISolution).IsAssignableFrom(dayType))
            {
                ISolution dayInstance = (ISolution)Activator.CreateInstance(dayType)!;
                int resultPartOne = dayInstance.SolutionPartOne(input);
                int resultPartTwo = dayInstance.SolutionPartTwo(input);

                Console.WriteLine($"Part One: {resultPartOne}");
                Console.WriteLine($"Part Two: {resultPartTwo}");
            }
            else
            {
                Console.WriteLine($"Day{dayNumber} class not found or does not implement ISolution.");
            }
        }
        else
        {
            Console.WriteLine("Input file not found.");
        }

        return Task.CompletedTask;
    }
}

public interface ISolution {
    int SolutionPartOne(List<string> textInput);
    int SolutionPartTwo(List<string> textInput);
}
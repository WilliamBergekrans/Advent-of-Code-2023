using System.Net.Sockets;

namespace AdventOfCode2023.Solutions;

public class Day5: ISolution
{
    private Dictionary<int, List<RangeMapping>> allMappings;
    public int SolutionPartOne(List<string> textInput)
    {
        List<long> seeds = textInput[0].Split(':')[1].Split(' ')
            .Where(numString => numString != "")
            .Select(numString => long.Parse(numString))
            .ToList();

        List<string> allMaps = textInput.Skip(1).Where(row => row != "").ToList();

        Dictionary<int, List<RangeMapping>> mappingsInOrder = new ();
        
        int mapNumber = 0;
        foreach (string row in allMaps)
        {
            // Rows with characters only mean that we move on
            if (!char.IsNumber(row.First()))
            {
                mapNumber++;
                continue;
            }

            List<long> rowMap = row.Split(' ').Where(s => s != "").Select(s => long.Parse(s)).ToList();
            RangeMapping rowStruct = new (rowMap[1], rowMap[0], rowMap[2]);

            if (!mappingsInOrder.ContainsKey(mapNumber))
            {
                mappingsInOrder.Add(mapNumber, new List<RangeMapping>());
            }
            mappingsInOrder[mapNumber].Add(rowStruct);
        }

        allMappings = mappingsInOrder;
        
        List<long> locations = seeds.Select(seed => FindLocation(seed, 1)).ToList();
        Console.WriteLine($"Lowest Part 1: {locations.Min()}");
        return 0;
    }

    public int SolutionPartTwo(List<string> textInput)
    {
        List<long> seeds = textInput[0].Split(':')[1].Split(' ')
            .Where(numString => numString != "")
            .Select(numString => long.Parse(numString))
            .ToList();

        List<long> allSeedNumbers = new();
        Dictionary<long, long> seedWithRange = new();

        bool secondInPair = false;
        long firstNumber = 0;
        foreach (long seed in seeds)
        {
            if (secondInPair)
            {
                seedWithRange.Add(firstNumber, seed);
                secondInPair = false;
            }
            else
            {
                firstNumber = seed;
                secondInPair = true;
            }
        }
        
        List<string> allMaps = textInput.Skip(1).Where(row => row != "").ToList();

        Dictionary<int, List<RangeMapping>> mappingsInOrder = new ();
        
        int mapNumber = 0;
        foreach (string row in allMaps)
        {
            // Rows with characters only mean that we move on
            if (!char.IsNumber(row.First()))
            {
                mapNumber++;
                continue;
            }

            List<long> rowMap = row.Split(' ').Where(s => s != "").Select(s => long.Parse(s)).ToList();
            RangeMapping rowStruct = new (rowMap[1], rowMap[0], rowMap[2]);

            if (!mappingsInOrder.ContainsKey(mapNumber))
            {
                mappingsInOrder.Add(mapNumber, new List<RangeMapping>());
            }
            mappingsInOrder[mapNumber].Add(rowStruct);
        }

        allMappings = mappingsInOrder;
        long? lowest = null;
        foreach (var (key, value) in seedWithRange)
        {
            long currentSeed = key;
            long max = key + value;
            while (currentSeed <= max)
            {
                long location = FindLocation(currentSeed, 1);
                if (location < lowest || lowest is null)
                {
                    lowest = location;
                    Console.WriteLine(lowest);
                }
                currentSeed++;
            }
        }
        Console.WriteLine($"Lowest Part 2: {lowest}");
        return 0;
    }

    private long FindLocation(long number, int n)
    {
        int max = allMappings.Count;
        while (n <= max)
        {
            number = FindNextNumber(number, n);
            n++;
        }

        return number;
    }

    private long FindNextNumber(long number, int n)
    {
        RangeMapping mapping = allMappings[n].FirstOrDefault(mapping =>
            mapping.SourceNumber <= number && mapping.SourceNumber + mapping.Range >= number);

        return mapping.DestinationNumber - mapping.SourceNumber + number;
    }
}

public struct RangeMapping
{
    public long SourceNumber;
    public long DestinationNumber;
    public long Range;

    public RangeMapping(long source, long destination, long range)
    {
        SourceNumber = source;
        DestinationNumber = destination;
        Range = range;
    }
}

public class Range
{
    public long Start { get; set; }
    public long End { get; set; }

    public Range(long start, long end)
    {
        Start = start;
        End = end;
    }
}
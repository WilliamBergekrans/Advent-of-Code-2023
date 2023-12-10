namespace AdventOfCode2023.Solutions;

public class Day6: ISolution
{
    public int SolutionPartOne(List<string> textInput)
    {
        List<int> times = textInput[0].Split(':')[1].Split(' ').Where(s => s != "").Select(s => int.Parse(s)).ToList();
        List<int> distances = textInput[1].Split(':')[1].Split(' ').Where(s => s != "").Select(s => int.Parse(s)).ToList();

        List<int> waysPerRace = new();
        for (int i = 0; i < times.Count; i++)
        {
            int waysToWin = 0;
            int time = times[i];
            int distance = distances[i];

            for (int j = 0; j <= time; j++)
            {
                int currentDistance = j * (time - j);
                if (currentDistance > distance)
                {
                    waysToWin++;
                }
            }
            
            waysPerRace.Add(waysToWin);
        }

        return waysPerRace.Aggregate(1, (total, next) => total * next);
    }

    public int SolutionPartTwo(List<string> textInput)
    {
        List<int> times = textInput[0].Split(':')[1].Split(' ').Where(s => s != "").Select(s => int.Parse(s)).ToList();
        List<int> distances = textInput[1].Split(':')[1].Split(' ').Where(s => s != "").Select(s => int.Parse(s)).ToList();
        
        List<int> waysPerRace = new();
        int waysToWin = 0;
        long time = long.Parse(times.Select(time => time.ToString()).Aggregate("", (s, s1) => s + s1));
        long distance = long.Parse(distances.Select(distance => distance.ToString()).Aggregate("", (s, s1) => s + s1));

        for (long j = 0; j <= time; j++)
        {
            long currentDistance = j * (time - j);
            if (currentDistance > distance)
            {
                waysToWin++;
            }
        }
        
        waysPerRace.Add(waysToWin);

        return waysPerRace.Aggregate(1, (total, next) => total * next);
    }
}
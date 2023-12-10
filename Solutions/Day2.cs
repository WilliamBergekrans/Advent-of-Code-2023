namespace AdventOfCode2023.Solutions;

public class Day2: ISolution
{
    public Day2()
    {
        
    }
    
    public int SolutionPartOne(List<string> textInput)
    {
        Dictionary<string, int> bagLimits = new()
        {
            { "red", 12 }, { "green", 13 }, { "blue", 14 }
        };
        int id = 0;
        int sum = textInput.Select(row =>
        {
            id++;
            string[] firstSplit = row.Split(':');
            string[] allDraws = firstSplit[1].Split(';');
            
            foreach (string draw in allDraws)
            {
                string[] colors = draw.Split(',');
                foreach (string color in colors)
                { 
                    string[] pair = color.TrimStart().Split(' ');
                    int number = Int32.Parse(pair[0]);
                    if (bagLimits[pair[1]] < number)
                    {
                        return 0;
                    }
                }
            }
            return id;
        }).Sum();
        return sum;
    }

    public int SolutionPartTwo(List<string> textInput)
    {
        Dictionary<string, int> bagLimits = new()
        {
            { "red", 12 }, { "green", 13 }, { "blue", 14 }
        };
        int id = 0;
        int sum = textInput.Select(row =>
        {
            id++;
            string[] firstSplit = row.Split(':');
            string[] allDraws = firstSplit[1].Split(';');

            Dictionary<string, int> minimumColors = new()
            {
                { "red", 0 }, { "green", 0 }, { "blue", 0 }
            };
            
            foreach (string draw in allDraws)
            {
                string[] colors = draw.Split(',');
                foreach (string colorAndNumber in colors)
                { 
                    string[] pair = colorAndNumber.TrimStart().Split(' ');
                    int number = Int32.Parse(pair[0]);
                    string color = pair[1];

                    if (minimumColors[color] < number)
                    {
                        minimumColors[color] = number;
                    }
                }
            }

            int power = 0;
            List<int> values = minimumColors.Values.ToList();
            return values[0] * values[1] * values[2];
        }).Sum();
        return sum;
    }
}
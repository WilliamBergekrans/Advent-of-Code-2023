namespace AdventOfCode2023.Solutions;

public class Day8: ISolution
{
    private Dictionary<string, Node> instructions;
    private string stepDirections;
    public int SolutionPartOne(List<string> textInput)
    {
        instructions = new Dictionary<string, Node>();
        stepDirections = textInput[0].Trim();
        
        foreach (string row in textInput.Skip(2))
        {
            string key = row.Split('=')[0].Trim();
            string left = row.Split('=')[1].Trim().Trim('(').Trim(')').Split(',')[0].Trim();
            string right = row.Split('=')[1].Trim().Trim('(').Trim(')').Split(',')[1].Trim();
            
            instructions.Add(key, new Node(left, right));
        }
        
        int steps = 0;  
        int nextDirectionNumber = 0;
        string nextNode = "AAA";
        
        while (true)
        {
            if (nextNode == "ZZZ")
            {
                break;
            }
            Node node = instructions[nextNode];
            char nextStep = stepDirections[nextDirectionNumber];

            if (nextDirectionNumber == stepDirections.Length-1)
            {
                nextDirectionNumber = 0;
            }
            else
            {
                nextDirectionNumber++;
            }

            steps++;
            nextNode = nextStep == 'L' ? node.Left : node.Right;
        }

        return steps;
    }

    public int SolutionPartTwo(List<string> textInput)
    {
        string[] startNodes = textInput.Skip(2).ToList().Select(row =>
        {
            string key = row.Split('=')[0].Trim();
            return key.Last() == 'A' ? key : "";
        }).Where(s => s != "").ToArray();

        Dictionary<int, bool> leftAtInstruction = new();
        int test = 0;
        foreach (char stepDirection in stepDirections)
        {
            leftAtInstruction.Add(test, stepDirection == 'L');
            test++;
        }

        List<long> cycleSteps = new();
        foreach (string startNode in startNodes)
        {
            int steps = 0;  
            int nextDirectionNumber = 0;
            string nextNode = startNode;
        
            while (true)
            {
                if (nextNode.Last() == 'Z')
                {
                    break;
                }
                Node node = instructions[nextNode];
                char nextStep = stepDirections[nextDirectionNumber];

                if (nextDirectionNumber == stepDirections.Length-1)
                {
                    nextDirectionNumber = 0;
                }
                else
                {
                    nextDirectionNumber++;
                }

                steps++;
                nextNode = nextStep == 'L' ? node.Left : node.Right;
            }
            cycleSteps.Add(steps);
        }

        long lcm = LCM(cycleSteps.ToArray());
        Console.WriteLine($"LCM: {lcm}");
        return 0;
    }
    
    static long LCM(long[] numbers)
    {
        long result = numbers[0];
        for (int i = 1; i < numbers.Length; i++)
        {
            result = LCM(result, numbers[i]);
        }
        return result;
    }

    static long LCM(long a, long b)
    {
        return Math.Abs(a * b) / GCD(a, b);
    }
    
    static long GCD(long a, long b)
    {
        while (b != 0)
        {
            long temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }
}
public record Node(string Left, string Right);
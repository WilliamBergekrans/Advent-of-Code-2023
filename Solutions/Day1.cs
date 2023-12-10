namespace AdventOfCode2023.Solutions;

public class Day1: ISolution
{
    public int SolutionPartOne(List<string> textInput)
    {
        int sum = textInput.Select(row =>
        {
            List<char>? onlyNumbers = row.Where(rowChar => char.IsNumber(rowChar)).ToList();
            char? first = onlyNumbers?.First();
            char? last = onlyNumbers?.Last();
            string numberString = first.ToString() + last.ToString();
            return Int32.Parse(numberString);
        }).Sum();
        return sum;
    }

    public int SolutionPartTwo(List<string> textInput)
    {
        string[] stringNumbers =new [] {"one", "two", "three", "four", "five", "six", "seven", "eight", "nine"};
        Dictionary<string, string> numberMap = new()
        {
            { "one", "1" }, { "two", "2" }, { "three", "3" }, { "four", "4" },
            { "five", "5" }, { "six", "6" }, { "seven", "7" }, { "eight", "8" }, { "nine", "9" }
        };
        
        int sum = textInput.Select(row =>
        {
            string newRow = "";
            string chars = "";
            foreach (char c in row)
            {
                if (char.IsNumber(c))
                {
                    newRow += c.ToString();
                    chars = "";
                }
                else
                {
                    chars += c.ToString();
                    
                    foreach (string stringNumber in stringNumbers)
                    {
                        bool isNumberString = chars.Contains(stringNumber);
                        if (!isNumberString) continue;
                        string numberToAdd = numberMap[stringNumber];
                        newRow += numberToAdd;
                        chars = chars.Length > 1 ? chars.Substring(chars.Length - 1) : "";
                    }
                }
            }
            Console.WriteLine($"Row: {row}");
            Console.WriteLine($"Numbers: {newRow}");
            char first = newRow.First();
            char last = newRow.Last();
            string numberString = first.ToString() + last.ToString();
            Console.WriteLine($"Returned: {numberString}");
            return int.Parse(numberString);
        }).Sum();
        return sum;
    }
}
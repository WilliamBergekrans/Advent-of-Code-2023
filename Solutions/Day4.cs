using System.Data;

namespace AdventOfCode2023.Solutions;

public class Day4: ISolution
{
    public int SolutionPartOne(List<string> textInput)
    {
        int sum = (int)textInput.Select(row =>
        {
            string[] splitOnColon = row.Split(":");
            string myNumbers = splitOnColon[1].Split("|")[0];
            string winningNumbers = splitOnColon[1].Split("|")[1];

            List<int> numbers = myNumbers.Split(" ").Where(s => s != "").Select(s => int.Parse(s)).ToList();
            List<int> winning = winningNumbers.Split(" ").Where(s => s != "").Select(s => int.Parse(s)).ToList();

            int numberOfWins = 0;
            
            numbers.ForEach(number =>
            {
                if (winning.Contains(number))
                {
                    numberOfWins++;
                }
            });
            if (numberOfWins == 0) return 0;
            return Math.Pow(2, numberOfWins - 1);

        }).Sum();
        return sum;
    }

    public int SolutionPartTwo(List<string> textInput)
    {
        Dictionary<int, int> copiesPerRow = new();
        for (int i = 0; i < textInput.Count; i++)
        {
            copiesPerRow.Add(i, 0);
        }
        int rowNumber = 0;
        textInput.ForEach(row =>
        {
            // Add the original to the copies count
            copiesPerRow[rowNumber]++;

            // Find number of matches
            int matches = GetMatchesForRow(textInput, rowNumber);

            // Add copies to upcoming rows
            for (int i = 1; i <= matches; i++)
            {
                copiesPerRow[rowNumber + i] = copiesPerRow[rowNumber] + copiesPerRow[rowNumber + i];
            }

            rowNumber++;
        });
        int sum = copiesPerRow.Values.Sum();
        return sum;
    }

    private int GetMatchesForRow(List<string> textInput, int rowNumber)
    {
        string row = textInput[rowNumber];
        string[] splitOnColon = row.Split(":");
        string myNumbers = splitOnColon[1].Split("|")[0];
        string winningNumbers = splitOnColon[1].Split("|")[1];

        List<int> numbers = myNumbers.Split(" ").Where(s => s != "").Select(s => int.Parse(s)).ToList();
        List<int> winning = winningNumbers.Split(" ").Where(s => s != "").Select(s => int.Parse(s)).ToList();
            
        int matches = 0;
            
        numbers.ForEach(number =>
        {
            if (winning.Contains(number))
            {
                matches++;
            }
        });

        return matches;
    }
}
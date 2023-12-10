namespace AdventOfCode2023.Solutions;

public class Day3: ISolution
{
    public int SolutionPartOne(List<string> textInput)
    {
        int rowNumber = 0;
        var sum = textInput.Select(row =>
        {
            bool previousNumberHadAdjacentSymbol = false;
            string currentNumber = "";
            int rowSum = 0;
            int rowPosition = -1;
            foreach (char character in row)
            {
                rowPosition++;
                
                // 
                if (!char.IsNumber(character) && previousNumberHadAdjacentSymbol && currentNumber != "")
                {
                    rowSum += int.Parse(currentNumber);
                    currentNumber = "";
                }
                
                // If Dot
                if (character == '.')
                {
                    // Check adjacent straight up
                    if (rowNumber > 0 && !char.IsNumber(textInput[rowNumber-1][rowPosition])
                                      && textInput[rowNumber-1][rowPosition] != '.')
                    {
                        previousNumberHadAdjacentSymbol = true;
                    } else if (rowNumber < textInput.Count-1 && !char.IsNumber(textInput[rowNumber+1][rowPosition])
                                                        && textInput[rowNumber+1][rowPosition] != '.')
                    {
                        previousNumberHadAdjacentSymbol = true;
                    }
                    else
                    {
                        previousNumberHadAdjacentSymbol = false;
                    }

                    if (previousNumberHadAdjacentSymbol && currentNumber != "")
                    {
                        rowSum += int.Parse(currentNumber);
                    }
                    
                    currentNumber = "";
                    continue;
                }
                
                // If another number, add and move on
                if (char.IsNumber(character))
                {
                    // Check adjacent
                    if (rowNumber > 0 && !char.IsNumber(textInput[rowNumber-1][rowPosition])
                                      && textInput[rowNumber-1][rowPosition] != '.')
                    {
                        previousNumberHadAdjacentSymbol = true;
                    } else if (rowNumber < textInput.Count-1 && !char.IsNumber(textInput[rowNumber+1][rowPosition])
                                                             && textInput[rowNumber+1][rowPosition] != '.')
                    {
                        previousNumberHadAdjacentSymbol = true;
                    }
                    
                    currentNumber += character.ToString();
                    if (rowPosition == row.Length-1)
                    {
                        if (previousNumberHadAdjacentSymbol && currentNumber != "")
                        {
                            rowSum += int.Parse(currentNumber);
                            currentNumber = "";
                        }
                    }
                }
                else
                {
                    // If Symbol
                    previousNumberHadAdjacentSymbol = true;
                    if (previousNumberHadAdjacentSymbol && currentNumber != "")
                    {
                        rowSum += int.Parse(currentNumber);
                    }
                    currentNumber = "";
                }
                
            }

            rowNumber++;
            return rowSum;
        }).Sum();

        return sum;
    }

    public int SolutionPartTwo(List<string> textInput)
    {
        int rowNumber = 0;
        int sum = textInput.Select(row =>
        {
            int rowSum = 0;
            int rowPosition = -1;
            foreach (char c in row)
            {
                rowPosition++;
                // Only care about gears
                if (c != '*') continue;

                if (HasExactlyTwoAdjacentNumbers(textInput, rowNumber, rowPosition, out Dictionary<int, bool> positions))
                {
                    Console.Write("Hej");
                    int product = FindNumbers(textInput, rowNumber, rowPosition, positions);
                    rowSum += product;
                }
            }

            rowNumber++;
            return rowSum;
        }).Sum();
        return sum;
    }

    private int FindNumbers(List<string> textInput, int rowNumber, int rowPosition, Dictionary<int, bool> positions)
    {

        List<int> numbers = new ();
        
        // Left and right
        string left = MirrorString(FindNumberToTheLeft(textInput[rowNumber], rowPosition - 1));
        if (left != "") numbers.Add(int.Parse(left));

        string right = FindNumberToTheRight(textInput[rowNumber], rowPosition + 1);
        if (right != "") numbers.Add(int.Parse(right));

        // Row above
        List<string> numbersUp = FindNumbersInRow(textInput[rowNumber - 1], rowPosition);
        numbers.AddRange(numbersUp.Where(num => num != "").Select(s => int.Parse(s)));

        // Row bellow
        List<string> numbersDown =FindNumbersInRow(textInput[rowNumber + 1], rowPosition);
        numbers.AddRange(numbersDown.Where(num => num != "").Select(s => int.Parse(s)));

        if (numbers.Count != 2)
        {
            Console.WriteLine("SOMETHING IS OFF");
        }
        return numbers.First() * numbers.Last();
    }

    private List<string> FindNumbersInRow(string row, int middlePosition)
    {
        List<string> numbers = new ();
        // If middle is number
        if (char.IsNumber(row[middlePosition]))
        {
            string leftPart = MirrorString(FindNumberToTheLeft(row, middlePosition - 1));
            string rightPart = FindNumberToTheRight(row, middlePosition + 1);
            
            numbers.Add(leftPart + row[middlePosition].ToString() + rightPart);
        }
        else
        {
            numbers.Add(MirrorString(FindNumberToTheLeft(row, middlePosition -1)));
            numbers.Add(FindNumberToTheRight(row, middlePosition +1));
        }

        return numbers.Where(number => number != "").ToList();
    }

    private string FindNumberToTheLeft(string row, int startPosition)
    {
        if (!char.IsNumber(row[startPosition])) return "";
        string numberString = row[startPosition].ToString();
        if (startPosition > 0)
        {
            numberString += FindNumberToTheLeft(row, startPosition - 1);
        }

        return numberString;
    }
    
    private string FindNumberToTheRight(string row, int startPosition)
    {
        if (!char.IsNumber(row[startPosition])) return "";
        string numberString = row[startPosition].ToString();
        if (startPosition < row.Length -1)
        {
            numberString += FindNumberToTheRight(row, startPosition + 1);
        }

        return numberString;
    }
    
    public string MirrorString(string input)
    {
        // Convert the string to a char array and reverse it
        char[] reversedArray = input.ToCharArray();
        Array.Reverse(reversedArray);

        // Create a new string from the reversed char array and concatenate
        return new string(reversedArray);
    }

    private bool HasExactlyTwoAdjacentNumbers(List<string> textInput, int rowNumber, int rowPosition, out Dictionary<int, bool> positions)
    {
        Dictionary<int, bool> positionHasNumber = new()
        {
            { 1, false },
            { 2, false },
            { 3, false },
            { 4, false },
            { 5, false },
            { 6, false },
            { 7, false },
            { 8, false }
        };
        
        int adjacentTo = 0;
        // Upper left
        if (rowNumber > 0 && rowPosition > 0)
        {
            char c = textInput[rowNumber - 1][rowPosition - 1];
            if (char.IsNumber(c))
            {
                positionHasNumber[1] = true;
            }
        }
        
        // Straight left
        if (rowPosition > 0)
        {
            char c = textInput[rowNumber][rowPosition - 1];
            if (char.IsNumber(c))
            {
                positionHasNumber[4] = true;
            }
        }
        
        // Lower left
        if (rowNumber < textInput.Count-1 && rowPosition > 0)
        {
            char c = textInput[rowNumber + 1][rowPosition - 1];
            if (char.IsNumber(c))
            {
                positionHasNumber[6] = true;
            }
        }
        
        // Straight down
        if (rowNumber < textInput.Count-1)
        {
            char c = textInput[rowNumber+1][rowPosition];
            if (char.IsNumber(c))
            {
                positionHasNumber[7] = true;
            }
        }
        
        // Lower right
        if (rowNumber < textInput.Count-1 && rowPosition < textInput[rowNumber+1].Length-1)
        {
            char c = textInput[rowNumber + 1][rowPosition + 1];
            if (char.IsNumber(c))
            {
                positionHasNumber[8] = true;
            }
        }
        
        // Straight right
        if (rowPosition < textInput[rowNumber+1].Length-1)
        {
            char c = textInput[rowNumber][rowPosition + 1];
            if (char.IsNumber(c))
            {
                positionHasNumber[5] = true;
            }
        }
        
        // Upper right
        if (rowNumber > 0 && rowPosition < textInput[rowNumber-1].Length-1)
        {
            char c = textInput[rowNumber-1][rowPosition + 1];
            if (char.IsNumber(c))
            {
                positionHasNumber[3] = true;
            }
        }
        
        // Straight up
        if (rowNumber > 0)
        {
            char c = textInput[rowNumber-1][rowPosition];
            if (char.IsNumber(c))
            {
                positionHasNumber[2] = true;
            }
        }

        adjacentTo = 0;
        bool nextToEachOther = false;
        foreach ((int key, bool value) in positionHasNumber)
        {
            // Position 4 and 5 always their own numbers
            if (key is 4 or 5 && value)
            {
                nextToEachOther = false;
                adjacentTo++;
            }
            else
            {
                if (value && !nextToEachOther)
                {
                    nextToEachOther = true;
                    adjacentTo++;
                }
                else if (!value)
                {
                    nextToEachOther = false;
                }
            }
            
        }

        positions = positionHasNumber;
        return adjacentTo == 2;
    }
}
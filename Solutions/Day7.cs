namespace AdventOfCode2023.Solutions;

public class Day7: ISolution
{
    public int SolutionPartOne(List<string> textInput)
    {
        Dictionary<string, BidAndRank> handWithRank = new();
        textInput.ForEach(row =>
        {
            string hand = row.Split(' ')[0].Trim();
            int bid = int.Parse(row.Split(' ')[1].Trim());
            int rank = GetRank(hand);
            
            handWithRank.Add(hand, new BidAndRank(bid, rank));
        });
        List<KeyValuePair<string, BidAndRank>> sortedList = handWithRank.ToList();
        sortedList.Sort(new CompareCards());

        int rank = 1;
        int totalSum = sortedList.Select(hand =>
        {
            int rowSum = rank * hand.Value.Bid;
            rank++;
            return rowSum;
        }).Sum();
        
        return totalSum;
    }

    public int SolutionPartTwo(List<string> textInput)
    {
        Dictionary<string, BidAndRank> handWithRank = new();
        textInput.ForEach(row =>
        {
            string hand = row.Split(' ')[0].Trim();
            int bid = int.Parse(row.Split(' ')[1].Trim());
            int rank = GetRankPart2(hand);
            
            handWithRank.Add(hand, new BidAndRank(bid, rank));
        });
        List<KeyValuePair<string, BidAndRank>> sortedList = handWithRank.ToList();
        sortedList.Sort(new CompareCardsPart2());

        int rank = 1;
        int totalSum = sortedList.Select(hand =>
        {
            int rowSum = rank * hand.Value.Bid;
            rank++;
            return rowSum;
        }).Sum();
        
        return totalSum;
    }
    
    private int GetRankPart2(string handString)
    {
        Dictionary<char, int> hand = new();
        int amountOfJ = 0;
        foreach (char c in handString)
        {
            if (!hand.ContainsKey(c))
            {
                hand.Add(c, 1);
            }
            else
            {
                hand[c]++;
            }

            if (c == 'J')
            {
                amountOfJ++;
            }
        }

        if (hand.First().Key == 'J' && hand.Count == 1)
        {
            
        }
        else
        {
            hand.Remove('J');
        }

        // Five of a kind
        if (hand.Any(h => h.Value + amountOfJ >= 5))
        {
            return 7;
        }

        // Four of a kind
        if (hand.Any(h => h.Value + amountOfJ == 4))
        {
            return 6;
        }
        
        // Full house
        if (hand.Count == 2 && hand.Any(h => h.Value + amountOfJ == 3))
        {
            return 5;
        }

        // Three of a kind or two pair
        if (hand.Any(h => h.Value + amountOfJ == 3) ||
            (hand.Count(h => h.Value + amountOfJ == 2) == 2))
        {
            // Three of a kind
            if (hand.Any(h => h.Value + amountOfJ == 3))
            {
                return 4;
            }
            // Two pair
            return 3;
        }
        
        // One pair
        if (hand.Any(h => h.Value + amountOfJ == 2))
        {
            return 2;
        }
        
        // High card
        return 1;
    }

    private int GetRank(string handString)
    {
        Dictionary<char, int> hand = new();
        foreach (char c in handString)
        {
            if (!hand.ContainsKey(c))
            {
                hand.Add(c, 1);
            }
            else
            {
                hand[c]++;
            }
        }

        // Five of a kind
        if (hand.Count == 1)
        {
            return 7;
        }

        // Four of a kind or Full house
        if (hand.Count == 2)
        {
            // Four of a kind
            if (hand.First().Value == 1 || hand.First().Value == 4)
            {
                return 6;
            }
            
            // Full house
            return 5;
        }

        // Three of a kind or two pair
        if (hand.Count == 3)
        {
            // Three of a kind
            if (hand.Any(h => h.Value == 3))
            {
                return 4;
            }
            // Two pair
            return 3;
        }
        
        // One pair
        if (hand.Count == 4)
        {
            return 2;
        }
        
        // High card
        return 1;
    }
}

public record BidAndRank(int Bid, int Rank);

public class CompareCardsPart2 : IComparer<KeyValuePair<string, BidAndRank>>
{
    private readonly Dictionary<char, int> _valueByChar = new ()
    {
        { 'A', 5 },
        { 'K', 4 },
        { 'Q', 3 },
        { 'T', 2 },
        { 'J', 1 },
    };
    
    public int Compare(KeyValuePair<string, BidAndRank> x, KeyValuePair<string, BidAndRank> y)
    {
        if (x.Value.Rank > y.Value.Rank)
        {
            return 1;
        } 
        
        if (x.Value.Rank < y.Value.Rank)
        {
            return -1;
        }

        return GetHighestHand(x.Key, y.Key);
    }

    private int GetHighestHand(string a, string b)
    {
        for (int i = 0; i < a.Length; i++)
        {
            if (a[i] == b[i])
            {
                continue;
            }
            // a is number
            if (char.IsNumber(a[i]))
            {
                // Both are numbers
                if (char.IsNumber(b[i]))
                {
                    if (a[i] > b[i])
                    {
                        return 1;
                    }

                    return -1;
                }

                // A higher than b
                if (b[i] == 'J')
                {
                    return 1;
                }
                // b higher than a
                return -1;
            }
            
            if (char.IsNumber(b[i]))
            {
                // Both are numbers
                if (char.IsNumber(a[i]))
                {
                    if (b[i] > a[i])
                    {
                        return -1;
                    }

                    return 1;
                }
                
                // b larger than a
                if (a[i] == 'J')
                {
                    return -1;
                }
                // a higher than a
                return 1;
            }

            if (_valueByChar[a[i]] > _valueByChar[b[i]])
            {
                return 1; 
            }

            return -1;
        }
        Console.WriteLine("SHOULD NOT GET HERE???");
        return 0;
    }
}
public class CompareCards : IComparer<KeyValuePair<string, BidAndRank>>
{
    private readonly Dictionary<char, int> _valueByChar = new ()
    {
        { 'A', 5 },
        { 'K', 4 },
        { 'Q', 3 },
        { 'J', 2 },
        { 'T', 1 }
    };
    
    public int Compare(KeyValuePair<string, BidAndRank> x, KeyValuePair<string, BidAndRank> y)
    {
        if (x.Value.Rank > y.Value.Rank)
        {
            return 1;
        } 
        
        if (x.Value.Rank < y.Value.Rank)
        {
            return -1;
        }

        return GetHighestHand(x.Key, y.Key);
    }

    private int GetHighestHand(string a, string b)
    {
        for (int i = 0; i < a.Length; i++)
        {
            if (a[i] == b[i])
            {
                continue;
            }
            // a is number
            if (char.IsNumber(a[i]))
            {
                // Both are numbers
                if (char.IsNumber(b[i]))
                {
                    if (a[i] > b[i])
                    {
                        return 1;
                    }

                    return -1;
                }

                // b higher than a
                return -1;
            }
            
            if (char.IsNumber(b[i]))
            {
                // Both are numbers
                if (char.IsNumber(a[i]))
                {
                    if (b[i] > a[i])
                    {
                        return -1;
                    }

                    return 1;
                }
                
                // b higher than a
                return 1;
            }

            if (_valueByChar[a[i]] > _valueByChar[b[i]])
            {
                return 1; 
            }

            return -1;
        }
        Console.WriteLine("SHOULD NOT GET HERE???");
        return 0;
    }
}

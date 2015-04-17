namespace hrThe_Quickest_Way_Up
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class Program
    {
        static void Main()
        {
            var numCases = Int32.Parse(Console.ReadLine());
            while (numCases-- > 0)
            {
                var numLadders = Int32.Parse(Console.ReadLine());
                var ladders = new Dictionary<int, int>();
                for (int i = 0; i < numLadders; ++i)
                {
                    var nums = Console.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
                    ladders.Add(nums[0], nums[1]);
                }
                var numSnakes = Int32.Parse(Console.ReadLine());
                var snakes = new Dictionary<int, int>();
                for (int i = 0; i < numSnakes; ++i)
                {
                    var nums = Console.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
                    snakes.Add(nums[0], nums[1]);
                }
                var adjList = new List<List<int>> {new List<int>()};
                for (int i = 1; i < 100; i++)
                {
                    adjList.Add(new List<int>());
                    for (int j = 1; j <= 6; j++)
                    {
                        var newVal = i + j;
                        if (ladders.ContainsKey(newVal))
                        {
                            adjList[i].Add(ladders[newVal]);
                        }
                        else if (snakes.ContainsKey(newVal))
                        {
                            adjList[i].Add(snakes[newVal]);
                        }
                        else if (i + j <= 100)
                        {
                            adjList[i].Add(i + j);
                        }
                    }
                    adjList[i] = adjList[i].OrderBy(x => x).ToList();
                }
                adjList.Add(new List<int>());
                var s = new Stack<KeyValuePair<int, int>>();
                s.Push(new KeyValuePair<int, int>(1, 0));
                var memo = new int[101];
                for (int i = 0; i < 101; ++i)
                    memo[i] = int.MaxValue;
                while (s.Count > 0)
                {
                    var curState = s.Pop();
                    var curVertex = curState.Key;
                    var totalMoves = curState.Value;
                    if (totalMoves < memo[curVertex])
                    {
                        memo[curVertex] = totalMoves;
                    }
                    if (curVertex == 100)
                    {
                        continue;
                    }
                    for (int j = 0; j < adjList[curVertex].Count; j++)
                    {
                        if (totalMoves + 1 < memo[adjList[curVertex][j]])
                        {
                            s.Push(new KeyValuePair<int, int>(adjList[curVertex][j], totalMoves + 1));
                        }
                    }
                }
                Console.WriteLine(memo[100] != Int32.MaxValue ? memo[100] : -1);
            }
        }
    }
}

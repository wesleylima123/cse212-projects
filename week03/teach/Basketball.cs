/*
 * CSE 212 Lesson 6C 
 * 
 * This code will analyze the NBA basketball data and create a table showing
 * the players with the top 10 career points.
 * 
 * Note about columns:
 * - Player ID is in column 0
 * - Points is in column 8
 * 
 * Each row represents the player's stats for a single season with a single team.
 */

using Microsoft.VisualBasic.FileIO;

public class Basketball
{
    public static void Run()
    {
        // Map (Dictionary) to store total points for each player
        var players = new Dictionary<string, int>();

        using var reader = new TextFieldParser("../../../basketball.csv");
        reader.TextFieldType = FieldType.Delimited;
        reader.SetDelimiters(",");
        reader.ReadFields(); // ignore header row
        
        while (!reader.EndOfData) 
        {
            var fields = reader.ReadFields()!;
            var playerId = fields[0];
            var points = int.Parse(fields[8]);
            
            // Add points to the player's total
            if (players.ContainsKey(playerId))
            {
                players[playerId] += points;
            }
            else
            {
                players[playerId] = points;
            }
        }

        Console.WriteLine($"Total unique players: {players.Count}");
        Console.WriteLine();

        // Convert dictionary to list of (playerId, totalPoints) and sort by points (descending)
        var sortedPlayers = players
            .OrderByDescending(kvp => kvp.Value)  // Sort by total points (highest first)
            .Take(10)                              // Take only top 10
            .ToList();

        // Display the top 10 players
        Console.WriteLine("Top 10 Players by Career Points:");
        Console.WriteLine("==================================");
        Console.WriteLine("Rank  Player ID                                    Total Points");
        Console.WriteLine("----  ------------------------------------------  ------------");
        
        for (int i = 0; i < sortedPlayers.Count; i++)
        {
            // Align player ID (some are long, so keep them readable)
            string playerId = sortedPlayers[i].Key;
            int totalPoints = sortedPlayers[i].Value;
            
            // Format with rank, player ID (truncate if too long for display)
            if (playerId.Length > 40)
            {
                playerId = playerId.Substring(0, 37) + "...";
            }
            
            Console.WriteLine($"{i + 1,4}  {playerId,-42}  {totalPoints,11:N0}");
        }
    }
}
using System.Text.Json;

public static class SetsAndMaps
{
    /// <summary>
    /// The words parameter contains a list of two character 
    /// words (lower case, no duplicates). Using sets, find an O(n) 
    /// solution for returning all symmetric pairs of words.  
    ///
    /// For example, if words was: [am, at, ma, if, fi], we would return :
    ///
    /// ["am & ma", "if & fi"]
    ///
    /// The order of the array does not matter, nor does the order of the specific words in each string in the array.
    /// at would not be returned because ta is not in the list of words.
    ///
    /// As a special case, if the letters are the same (example: 'aa') then
    /// it would not match anything else (remember the assumption above
    /// that there were no duplicates) and therefore should not be returned.
    /// </summary>
    /// <param name="words">An array of 2-character words (lowercase, no duplicates)</param>
    public static string[] FindPairs(string[] words)
    {
        HashSet<string> set = new HashSet<string>(words);
        List<string> result = new List<string>();

        foreach (string w in words)
        {
            if (w[0] < w[1])
            {
                string rev = new string(new[] { w[1], w[0] });
                if (set.Contains(rev))
                {
                    result.Add($"{w} & {rev}");
                }
            }
        }

        return result.ToArray();
    }

    /// <summary>
    /// Read a census file and summarize the degrees (education)
    /// earned by those contained in the file.  The summary
    /// should be stored in a dictionary where the key is the
    /// degree earned and the value is the number of people that 
    /// have earned that degree.  The degree information is in
    /// the 4th column of the file.  There is no header row in the
    /// file.
    /// </summary>
    /// <param name="filename">The name of the file to read</param>
    /// <returns>fixed array of divisors</returns>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();

        if (!File.Exists(filename))
        {
            string altPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", filename);
            if (File.Exists(altPath))
            {
                filename = altPath;
            }
            else
            {
                return new Dictionary<string, int>();
            }
        }

        foreach (var line in File.ReadLines(filename))
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            var fields = line.Split(",");

            // A primeira coluna é 0, então education está no índice 3
            if (fields.Length > 3)
            {
                string degree = fields[3].Trim();

                if (!string.IsNullOrEmpty(degree) && degree != "?")
                {
                    if (degrees.ContainsKey(degree))
                    {
                        degrees[degree]++;
                    }
                    else
                    {
                        degrees[degree] = 1;
                    }
                }
            }
        }

        // NÃO ordenar! Retornar na ordem que foi encontrada
        return degrees;
    }

    /// <summary>
    /// Determine if 'word1' and 'word2' are anagrams.  An anagram
    /// is when the same letters in a word are re-organized into a 
    /// new word.  A dictionary is used to solve the problem.
    /// 
    /// Examples:
    /// is_anagram("CAT","ACT") would return true
    /// is_anagram("DOG","GOOD") would return false because GOOD has 2 O's
    /// 
    /// Important Note: When determining if two words are anagrams, you
    /// should ignore any spaces.  You should also ignore cases.  For 
    /// example, 'Ab' and 'Ba' should be considered anagrams
    /// 
    /// Reminder: You can access a letter by index in a string by 
    /// using the [] notation.
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        string cleanedWord1 = word1.Replace(" ", "").ToLower();
        string cleanedWord2 = word2.Replace(" ", "").ToLower();

        if (cleanedWord1.Length != cleanedWord2.Length)
        {
            return false;
        }

        Dictionary<char, int> letterCounts = new Dictionary<char, int>();
        foreach (char c in cleanedWord1)
        {
            if (letterCounts.ContainsKey(c))
            {
                letterCounts[c]++;
            }
            else
            {
                letterCounts[c] = 1;
            }
        }

        foreach (char c in cleanedWord2)
        {
            if (!letterCounts.ContainsKey(c))
            {
                return false;
            }

            letterCounts[c]--;

            if (letterCounts[c] < 0)
            {
                return false;
            }
        }

        foreach (var count in letterCounts.Values)
        {
            if (count != 0)
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// This function will read JSON (Javascript Object Notation) data from the 
    /// United States Geological Service (USGS) consisting of earthquake data.
    /// The data will include all earthquakes in the current day.
    /// 
    /// JSON data is organized into a dictionary. After reading the data using
    /// the built-in HTTP client library, this function will return a list of all
    /// earthquake locations ('place' attribute) and magnitudes ('mag' attribute).
    /// Additional information about the format of the JSON data can be found 
    /// at this website:  
    /// 
    /// https://earthquake.usgs.gov/earthquakes/feed/v1.0/geojson.php
    /// 
    /// </summary>
    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        using var client = new HttpClient();
        using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        using var jsonStream = client.Send(getRequestMessage).Content.ReadAsStream();
        using var reader = new StreamReader(jsonStream);
        var json = reader.ReadToEnd();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowReadingFromString
        };

        var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

        List<string> summaries = new List<string>();

        if (featureCollection?.Features != null)
        {
            foreach (var feature in featureCollection.Features)
            {
                if (feature?.Properties != null)
                {
                    string place = feature.Properties.Place ?? "Unknown location";
                    double magnitude = feature.Properties.Mag;

                    // FORMATO CORRETO que o teste espera: " - Mag "
                    summaries.Add($"{place} - Mag {magnitude:F1}");
                }
            }
        }

        return summaries.ToArray();
    }
}
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;

public static class SetsAndMaps
{
    /// <summary>
    /// Problem 1: Find symmetric pairs of two-letter words using a set.
    // Time Complexity: O(n) - using HashSet for O(1) lookups
    // Space Complexity: O(n) - storing words in a set

    /// Approach:
    // 1. Add all words to a HashSet for O(1) lookup
    // 2. For each word, reverse it (e.g., "am" -> "ma")
    // 3. Check if the reversed word exists in the set
    // 4. Only add pairs where word comes before reversed alphabetically (to avoid duplicates)
    // 5. Skip words that are the same when reversed (like "aa")
 
    // Example: ["am", "at", "ma", "if", "fi"] -> ["am & ma", "if & fi"]
    /// </summary>
    public static string[] FindPairs(string[] words)
    {
        // Create a HashSet for O(1) lookups
        HashSet<string> wordSet = new HashSet<string>(words);
        
        // Use HashSet for results to automatically avoid duplicates
        HashSet<string> resultSet = new HashSet<string>();
        
        foreach (string word in words)
        {
            // Reverse the 2-character word efficiently
            string reversed = new string(new char[] { word[1], word[0] });
            
            // Check if reversed exists AND word comes before reversed alphabetically
            // This ensures each pair is added only once (e.g., "am & ma" not "ma & am")
            if (wordSet.Contains(reversed) && string.CompareOrdinal(word, reversed) < 0)
            {
                resultSet.Add($"{word} & {reversed}");
            }
        }
        
        // Convert HashSet to array for return
        string[] result = new string[resultSet.Count];
        resultSet.CopyTo(result);
        return result;
    }

    /// <summary>
    /// Problem 2: Read a census file and summarize degrees earned.
    /// The degree is located in column 4 (index 3) of the CSV file.
    /// 
    /// Approach:
    // 1. Read the file line by line
    // 2. Split each line by commas
    // 3. Extract the degree from column 4 (index 3)
    // 4. Use a dictionary to count occurrences of each degree
    // 5. Return the dictionary with degree names as keys and counts as values
    /// 
    /// Example output: {"Bachelors": 5355, "HS-grad": 10501, ...}
    /// </summary>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();
        
        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(",");
            
            // Column 4 is index 3 (0-based indexing)
            if (fields.Length > 3)
            {
                string degree = fields[3].Trim();
                
                if (!string.IsNullOrEmpty(degree))
                {
                    if (degrees.ContainsKey(degree))
                        degrees[degree]++;
                    else
                        degrees[degree] = 1;
                }
            }
        }

        return degrees;
    }

    /// <summary>
    /// Problem 3: Determine if two words are anagrams using a dictionary.
     
    /// Approach:
    // 1. Remove all spaces and convert to lowercase (case-insensitive, ignore spaces)
    // 2. If lengths differ, they cannot be anagrams
    // 3. Use a dictionary to count characters in the first word
    // 4. Subtract counts using the second word
    // 5. If any count goes negative or a character is missing, return false
    // 6. Return true if all counts cancel out to zero
    /// 
    /// Examples:
    ///   "CAT" and "ACT" -> true
    ///   "DOG" and "GOOD" -> false (different letter counts)
    ///   "A Decimal Point" and "Im a Dot in Place" -> true (ignores spaces and case)
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        // Remove spaces and convert to lowercase for case-insensitive comparison
        string cleaned1 = word1.Replace(" ", "").ToLower();
        string cleaned2 = word2.Replace(" ", "").ToLower();
        
        // Different lengths cannot be anagrams
        if (cleaned1.Length != cleaned2.Length)
            return false;
        
        // Dictionary to count character frequencies
        Dictionary<char, int> charCount = new Dictionary<char, int>();
        
        // Count characters in the first word
        foreach (char c in cleaned1)
        {
            if (charCount.ContainsKey(c))
                charCount[c]++;
            else
                charCount[c] = 1;
        }
        
        // Subtract counts using the second word
        foreach (char c in cleaned2)
        {
            if (!charCount.ContainsKey(c))
                return false;
            
            charCount[c]--;
            
            if (charCount[c] < 0)
                return false;
        }
        
        return true;
    }

    /// <summary>
    /// Problem 5: Get earthquake summary from USGS API.
    
    /// Approach:
    // 1. Send HTTP GET request to USGS GeoJSON API
    // 2. Deserialize JSON response into FeatureCollection object
    // 3. Extract 'place' and 'mag' properties from each feature
    // 4. Format each earthquake as "place - Mag magnitude"
    // 5. Return array of formatted strings
    
    /// Data source: https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson
    /// Example output: ["1km NE of Pahala, Hawaii - Mag 2.36", ...]
    /// </summary>
    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        
        using var client = new HttpClient();
        client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0");
        
        var response = client.GetAsync(uri).Result;
        response.EnsureSuccessStatusCode();
        
        var json = response.Content.ReadAsStringAsync().Result;
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

        List<string> results = new List<string>();
        
        if (featureCollection?.Features != null)
        {
            foreach (var feature in featureCollection.Features)
            {
                if (feature?.Properties != null)
                {
                    string place = feature.Properties.Place ?? "Unknown";
                    double? mag = feature.Properties.Mag;
                    
                    if (mag.HasValue)
                    {
                        results.Add($"{place} - Mag {mag.Value}");
                    }
                }
            }
        }
        
        return results.ToArray();
    }
}
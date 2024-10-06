    using System.Text.Json;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

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
            List<string> results = new List<string> {};
            Dictionary<string, string> result_dict = new Dictionary<string, string> {};
            foreach (var word in words) {
                char[] tmp = word.ToCharArray();
                Array.Reverse(tmp);
                string rev_word = new string(tmp);
                if (result_dict.ContainsKey(rev_word)) { // This should only happen if the key is a reverse
                    results.Add(String.Format("{0} & {1}", rev_word, word));
                } else {
                    result_dict[word] = word;
                }
            }
            return results.ToArray();
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
            foreach (var line in File.ReadLines(filename))
            {
                var fields = line.Split(",");
                //Field 3 is the degree I think field 1 is the count
                if (degrees.ContainsKey(fields[3])) {
                    degrees[fields[3]] += 1;
                } else {
                    degrees[fields[3]] = 1;
                }
            }

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
            Dictionary<char, int> values = new Dictionary<char, int> {};
            foreach (char ch in word1.ToLower()) {
                if (ch != ' ') {
                    if (values.ContainsKey(ch)) {
                        values[ch] += 1;
                    } else {
                        values[ch] = 1;
                    }
                }
            }

            foreach (char ch in word2.ToLower()) {
                if (ch != ' ') {
                    if (values.ContainsKey(ch)) {
                        if (values[ch] == 0) {
                            return false; // There are more of this char in the second word
                        } else {
                            values[ch] -= 1;
                        }
                    } else {
                        return false; // There is a char missing from the first word
                    }
                }
            }

            foreach (var thing in values) {
                if (thing.Value != 0) {
                    return false; // There were leftover chars from the first word.
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
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

            String[] result = new string[featureCollection.features.Count];

            foreach (var feature in featureCollection.features) {
                
                result[featureCollection.features.IndexOf(feature)] = String.Format("{0} - Mag {1}", feature.properties.place, feature.properties.mag);
            }

            return result;
        }
    }
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using WpfControls;

namespace HttpRequestComposer
{
    public class SuggestionFromResource : ISuggestionProvider
    {
        public HashSet<string> suggestions;
        public SuggestionFromResource(string resource)
        {
            using (var userAgentStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource))
            using (var reader = new StreamReader(userAgentStream))
            {
                var resourceJson = reader.ReadToEnd();
                suggestions = JsonConvert.DeserializeObject<HashSet<string>>(resourceJson);
            }
        }

        public IEnumerable GetSuggestions(string filter)
        {
            var suggest = new List<string>();

            if (filter.Length < 3)
                return suggest;

            var filters = filter.ToLower(CultureInfo.InvariantCulture).Split(' ');
            var take = 10;
            foreach (var suggestion in suggestions)
            {
                if (take <= 0) break;

                if (filters.All(s => suggestion.ToLower(CultureInfo.InvariantCulture).Contains(s)))
                {
                    suggest.Add(suggestion);
                    take--;
                }
            }
            return suggest;
        }
    }
}
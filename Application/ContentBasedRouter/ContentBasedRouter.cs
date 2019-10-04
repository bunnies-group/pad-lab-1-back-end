using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Application.ContentBasedRouter
{
    public class ContentBasedRouter : IContentBasedRouter
    {
        private readonly IDictionary<string, IEnumerable<string>> _topics = new Dictionary<string, IEnumerable<string>>
        {
            {
                "sports", new List<string>
                {
                    "sports",
                    "football",
                    "volleyball"
                }
            }
        };

        private readonly Regex _punctuationRegex = new Regex(@"[\s.,!?\-]+");

        public IEnumerable<string> ComputeAdditionalRoutes(string message)
        {
            var words = _punctuationRegex.Split(message)
                .Select(it => it.ToLowerInvariant())
                .Distinct();

            return _topics
                .Where(topic => words.Any(word => topic.Value.Contains(word)))
                .Select(topic => topic.Key);
        }
    }
}
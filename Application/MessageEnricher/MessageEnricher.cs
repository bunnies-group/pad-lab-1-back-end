using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Application.MessageEnricher
{
    public class MessageEnricher : IMessageEnricher
    {
        private readonly Regex _punctuationRegex = new Regex(@"[\s.,!?\-]+");

        private readonly IDictionary<string, string> _dictionary = new Dictionary<string, string>
        {
            { "hi", "Hello" },
            { "lol", "laughing out loud" },
            { "imho", "in my horrible opinion" },
            { "ussr", "Union of Soviet Socialist Republics" },
            { "ok", "okay" },
            { "utm", "utm is meh" }
        };

        public string Translate(string message)
        {
            return _punctuationRegex.Split(message)
                .Select(it => it.ToLowerInvariant())
                .Distinct()
                .Where(word => _dictionary.ContainsKey(word))
                .Aggregate(message, (current, word) =>
                    Regex.Replace(current, word, _dictionary[word], RegexOptions.IgnoreCase));
        }
    }
}
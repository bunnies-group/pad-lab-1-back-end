using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Application.MessageEnricher;

namespace Application.MessageTranslator
{
    public class MessageEnricher : IMessageEnricher
    {
        private readonly Regex _punctuationRegex = new Regex(@"[\s.,!?\-]+");

        private readonly IDictionary<string, string> _dictionary = new Dictionary<string, string>
        {
            { "hi", "Hello" },
            { "lol", "laughing out loud" },
            { "imho", "in my horrible opinion" },
            { "USSR", "Union of Soviet Socialist Republics" },
        };

        public string Translate(string message)
        {
            return _punctuationRegex.Split(message)
                .Where(word => _dictionary.ContainsKey(word))
                .Aggregate(message, (current, word) => current.Replace(word, _dictionary[word]));
        }
    }
}
using System;

namespace CodeBase.Services.Card
{
    public class Card
    {
        public string Term { get; }
        public string Definition { get; }

        public Card(string term, string definition)
        {
            Term = term ?? throw new ArgumentNullException(nameof(term));
            Definition = definition ?? throw new ArgumentNullException(nameof(definition));
        }
    }
}

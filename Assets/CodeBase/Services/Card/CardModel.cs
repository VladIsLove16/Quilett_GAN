using System;

namespace CodeBase.Services.Card
{
    public class CardModel
    {
        public string Term { get; }
        public string Definition { get; }
        public bool IsFlipped { get; private set; }
        public Language Language { get; }

        public CardModel(string term, string definition, Language language)
        {
            Term = term ?? throw new ArgumentNullException(nameof(term));
            Definition = definition ?? throw new ArgumentNullException(nameof(definition));
            Language = language;
            IsFlipped = false;
        }

        public void FlipCard() => IsFlipped = !IsFlipped;
    }
}

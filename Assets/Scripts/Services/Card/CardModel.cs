using System;

namespace CodeBase.Services.Card
{
    [Serializable]
    public class CardModel
    {
        private string term;
        private string definition;
        public string Term { get { return term; } }
        public string Definition { get { return definition; } }
        public bool IsFlipped { get; private set; }
        public Language Language { get; }

        public CardModel(string term, string definition, Language language)
        {
            this.term = term ?? throw new ArgumentNullException(nameof(term));
            this.definition = definition ?? throw new ArgumentNullException(nameof(definition));
            Language = language;
            IsFlipped = false;
        }

        public void FlipCard() => IsFlipped = !IsFlipped;
    }
}

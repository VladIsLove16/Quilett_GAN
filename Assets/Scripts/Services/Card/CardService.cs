using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeBase.Services.Card
{
    public class CardService : ICardManager
    {
        private string _currentCardSetName;
        private List<CardModel> _cards = new ();
        private List<CardModel> Cards => _cards;
            private Language _currentLanguage;

        public void SetLanguage(Language language) => _currentLanguage = language;

        public bool TryCreateCard(string term, string definition, Language language)
        {
            if (Cards.FirstOrDefault(c => c.Term == term) != null)
                return false;
            if (string.IsNullOrEmpty(term) || string.IsNullOrEmpty(definition))
                throw new ArgumentException("Term and definition cannot be empty.");

            Cards.Add(new CardModel(term, definition, language));
            return true;
        }

        public void DeleteCard(string term)
        {
            var card = Cards.FirstOrDefault(c => c.Term == term);
            if (card != null)
            {
                Cards.Remove(card);
            }
        }

        public List<CardModel> GetAllCards() => Cards;

        public bool IsCardCompleted(CardModel card) => Cards.Contains(card);
        public void Load(CardSet cardSet)
        {
            _currentCardSetName = cardSet.Name;
            _cards = cardSet.Cards;
        }
        public CardSet GetCurrentCardSet()
        {
            return new CardSet(_currentCardSetName,_cards);
        }
    }
}

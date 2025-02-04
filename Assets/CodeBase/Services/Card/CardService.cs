using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeBase.Services.Card
{

    public class CardService : ICardManager
        {
            private List<CardModel> _cards = new List<CardModel>();
            private Language _currentLanguage;

            public void SetLanguage(Language language) => _currentLanguage = language;

            public void CreateCard(string term, string definition, Language language)
            {
                if (string.IsNullOrEmpty(term) || string.IsNullOrEmpty(definition))
                    throw new ArgumentException("Term and definition cannot be empty.");

                _cards.Add(new CardModel(term, definition, language));
            }

            public void DeleteCard(string term)
            {
                var card = _cards.FirstOrDefault(c => c.Term == term);
                if (card != null)
                {
                    _cards.Remove(card);
                }
            }

            public List<CardModel> GetAllCards() => _cards;

            public bool IsCardCompleted(CardModel card) => _cards.Contains(card);
        }

    public interface ICardManager
    {
        void CreateCard(string term, string definition, Language language);
        void DeleteCard(string term);
        List<CardModel> GetAllCards();
        bool IsCardCompleted(CardModel card);  // Добавлен метод для проверки завершенности
        void SetLanguage(Language language);
    }

}


using System.Collections.Generic;

namespace CodeBase.Services.Card
{
    public interface ICardManager
    {
        void CreateCard(string term, string definition, Language language);
        void DeleteCard(string term);
        List<CardModel> GetAllCards();
        bool IsCardCompleted(CardModel card);  // Добавлен метод для проверки завершенности
        void SetLanguage(Language language);
    }

}


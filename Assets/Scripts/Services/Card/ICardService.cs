using System.Collections.Generic;

namespace CodeBase.Services.Card
{
    public interface ICardService
    {
        void CreateCard(string term, string definition);
        void DeleteCard(string term);
        List<Card> GetAllCards();
    }
}
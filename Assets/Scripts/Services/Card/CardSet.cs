using System.Collections.Generic;
using CodeBase.Services.Card;

[System.Serializable]
public class CardSet
{
    private string _name;
    public string Name => _name;
    public List<CardModel> Cards => _cards;
    private List<CardModel> _cards = new List<CardModel>();
    public CardSet(string name, List<CardModel> cards)
    {
        _name = name;
        _cards = cards;
    }

    public void SetName(string newName)
    {
        _name = newName;
    }
    public void Add(CardModel card)
    {
        _cards.Add(card);
    }
}


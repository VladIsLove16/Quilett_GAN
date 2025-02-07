using CodeBase.Services.Card;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DataManager : MonoBehaviour
{
    string fileName = "PlayerCardSet";
    private ICardManager _cardManager;
    [Inject]
    public void Construct(ICardManager cardManager)
    {
        _cardManager = cardManager;
    }
    private void Awake()
    {
        var data = JsonSaveSystem.Load<CardSet>(fileName);
        if (data == null)
        {
            data = CreateDefaultCardSet();
        }
        _cardManager.Load(data);
        AndroidBridge.SaveRandomWord("english word");
        AndroidBridge.SaveRandomWord("english word second one");
    }
    private void OnApplicationQuit()
    {
        JsonSaveSystem.Save(_cardManager.GetCurrentCardSet(), fileName);
    }
    private CardSet CreateDefaultCardSet()
    {
        List<CardModel> cards = new List<CardModel>()
        {
            new CardModel("Hallo","Hello",Language.Deutch),
            new CardModel("Name","Name",Language.Deutch),
            new CardModel("My","Mein",Language.Deutch),
        };
        CardSet cardSet = new CardSet("newCardSet", cards);
        return cardSet;
    }
}
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeBase.Services.Card;
using Zenject;
using CodeBase.UI;

public class CardsScroller : MonoBehaviour
{
    [SerializeField] private Button _right;
    [SerializeField] private Button _left;
    [SerializeField] private CardView cardPrefab; // Добавляем публичное поле для префаба
    [SerializeField] private Transform cardContainer; // Контейнер для размещения карточек
    private CardView[] _childComponents;

    private uint _index;
    private List<CardModel> _cardsToReview = new List<CardModel>(); // Список карточек для просмотра

    private ICardManager _cardManager;

    [Inject]
    public void Construct(ICardManager cardManager)
    {
        _cardManager = cardManager;
    }

    public void Initialize(List<CardModel> allCards)
    {
        _cardsToReview = new List<CardModel>();

        foreach (var card in allCards)
        {
            if (!_cardManager.IsCardCompleted(card)) // Проверка на пройденные карточки
            {
                _cardsToReview.Add(card);
            }
        }

        UpdateScroll();
    }

    public void UpdateScroll()
    {
        _childComponents = GetComponentsInChildren<CardView>();

        // Убираем все старые карточки перед добавлением новых
        foreach (var child in _childComponents)
        {
            Destroy(child.gameObject);
        }

        // Создаем и инициализируем карточки
        foreach (var card in _cardsToReview)
        {
            var cardViewInstance = Instantiate(cardPrefab, cardContainer);
            cardViewInstance.InitializeCard(card);
        }
    }

    private void ScrollRight()
    {
        if (_index < _cardsToReview.Count - 1)
        {
            _index++;
            UpdateScroll();
        }
    }

    private void ScrollLeft()
    {
        if (_index > 0)
        {
            _index--;
            UpdateScroll();
        }
    }
}

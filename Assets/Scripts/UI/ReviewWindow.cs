using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeBase.Services.Card;
using TMPro;
using Unity.VisualScripting;
using System;
using Zenject;
using System.Linq;

namespace CodeBase.UI
{
    public class ReviewWindow : MonoBehaviour
    {
        [SerializeField] private Button nextButton;  // Кнопка для следующей карточки
        [SerializeField] private Button previousButton;  // Кнопка для предыдущей карточки
        //[SerializeField] private CardView cardViewPrefab;  // Префаб для отображения карточки
        [SerializeField] private CardView _cardView;  // Отображение карточки
        //[SerializeField] private Transform cardContainer;  // Контейнер для карточек
        [SerializeField] private TextMeshProUGUI currentIndexText;
        [SerializeField] private Image progressBar;  // Текущий индекс карточки
        [SerializeField] private Button closeButton;
        private ICardManager _cardManager;
        private List<CardModel> _allCards;  // Список всех карточек
        private int _currentIndex = 0;  // Текущий индекс карточки
        [Inject]
        public void Construct(ICardManager cardManager)
        {
            _cardManager = cardManager;
        }
        private void Start()
        {
            nextButton.onClick.AddListener(ShowNextCard);
            previousButton.onClick.AddListener(OnPreviousButtonClicked);
            closeButton.onClick.AddListener(Close);
            _cardView.DeleteButtonClicked += OnCardView_DeleteButtonClicked;
        }

        private void OnCardView_DeleteButtonClicked()
        {
            string currentTerm = _cardView.GetCurrentTerm();
            _cardManager.DeleteCard(currentTerm);
            //CardModel currentCard = _allCards.First(x => x.Term == currentTerm);
            //_allCards.Remove(currentCard);
            ShowNextCard();
        }

        // Инициализация окна с карточками
        public void Initialize(List<CardModel> allCards)
        {
            _allCards = allCards;
            DisplayCard();
        }

        private void Close()
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }

        // Обновление отображения текущей карточки
        //private void DisplayCard()
        //{
        //    if (_allCards == null || _allCards.Count == 0) return;

        //    // Удаляем старые карточки из контейнера
        //    foreach (Transform child in cardContainer)
        //    {
        //        Destroy(child.gameObject);
        //    }

        //    // Создаем новую карточку
        //    var cardView = Instantiate(cardViewPrefab, cardContainer);
        //    cardView.InitializeCard(_allCards[_currentIndex]);

        //    // Обновляем отображение индекса текущей карточки
        //    currentIndexText.text = $"{_currentIndex + 1} / {_allCards.Count}";
        //}
        // Обновление отображения текущей карточки
        private void DisplayCard()
        {
            if (_allCards == null || _allCards.Count == 0) return;

            _cardView.InitializeCard(_allCards[_currentIndex]);

            currentIndexText.text = $"{_currentIndex + 1} / {_allCards.Count}";
            progressBar.fillAmount = (float)(_currentIndex + 1) / _allCards.Count;
        }

        // Обработчик нажатия на кнопку "Следующая"
        private void ShowNextCard()
        {
            if (_allCards == null || _allCards.Count == 0) return;

            _currentIndex = (_currentIndex + 1) % _allCards.Count;
            DisplayCard();
        }

        // Обработчик нажатия на кнопку "Предыдущая"
        private void OnPreviousButtonClicked()
        {
            if (_allCards == null || _allCards.Count == 0) return;

            _currentIndex = (_currentIndex - 1 + _allCards.Count) % _allCards.Count;
            DisplayCard();
        }
    }
}

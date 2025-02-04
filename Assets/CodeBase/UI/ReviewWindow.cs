using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeBase.Services.Card;
using TMPro;

namespace CodeBase.UI
{
    public class ReviewWindow : MonoBehaviour
    {
        [SerializeField] private Button nextButton;  // Кнопка для следующей карточки
        [SerializeField] private Button previousButton;  // Кнопка для предыдущей карточки
        [SerializeField] private CardView cardViewPrefab;  // Префаб для отображения карточки
        [SerializeField] private Transform cardContainer;  // Контейнер для карточек
        [SerializeField] private TextMeshProUGUI currentIndexText;  // Текущий индекс карточки
        [SerializeField] private Button closeButton;

        private List<CardModel> _allCards;  // Список всех карточек
        private int _currentIndex = 0;  // Текущий индекс карточки

        private void Start()
        {
            nextButton.onClick.AddListener(OnNextButtonClicked);
            previousButton.onClick.AddListener(OnPreviousButtonClicked);
            closeButton.onClick.AddListener(Close);
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
        private void DisplayCard()
        {
            if (_allCards == null || _allCards.Count == 0) return;

            // Удаляем старые карточки из контейнера
            foreach (Transform child in cardContainer)
            {
                Destroy(child.gameObject);
            }

            // Создаем новую карточку
            var cardView = Instantiate(cardViewPrefab, cardContainer);
            cardView.InitializeCard(_allCards[_currentIndex]);

            // Обновляем отображение индекса текущей карточки
            currentIndexText.text = $"{_currentIndex + 1} / {_allCards.Count}";
        }

        // Обработчик нажатия на кнопку "Следующая"
        private void OnNextButtonClicked()
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

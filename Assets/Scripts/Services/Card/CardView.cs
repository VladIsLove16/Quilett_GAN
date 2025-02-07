using UnityEngine;
using TMPro;
using UnityEngine.UI;
using CodeBase.Services.Card;
using System;
using Zenject;

namespace CodeBase.UI
{
    public class CardView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI cardText;  // Текст для термина или определения
        [SerializeField] private Button flipButton;  // Кнопка для переворачивания карточки
        [SerializeField] private Button deleteButton;  // Кнопка для удаления карточки из набора
        private CardModel _currentCard;  // Текущая карточка
        public Action DeleteButtonClicked;
        // Инициализация карточки
        public void InitializeCard(CardModel card)
        {
            _currentCard = card;
            UpdateCardDisplay();
        }

        // При старте добавляем слушателя на кнопку
        private void Start()
        {
            flipButton.onClick.AddListener(OnCardClicked);
            deleteButton.onClick.AddListener(DeleteCard);
        }

        private void DeleteCard()
        {
            DeleteButtonClicked.Invoke();
        }

        // Обработчик нажатия на кнопку для переворота карточки
        private void OnCardClicked()
        {
            if (_currentCard == null) return;

            _currentCard.FlipCard();  // Переворачиваем карточку
            UpdateCardDisplay();  // Обновляем отображение
        }

        // Обновляем отображение карточки в зависимости от состояния
        private void UpdateCardDisplay()
        {
            if (_currentCard == null) return;

            // Если карточка перевернута, показываем определение, если нет - термин
            cardText.text = _currentCard.IsFlipped ? _currentCard.Definition : _currentCard.Term;
        }

        internal string GetCurrentTerm()
        {
            return _currentCard.Term;
        }

        internal CardModel GetCurrentCardModel()
        {
            return _currentCard;
        }
    }
}

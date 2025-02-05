using CodeBase.Services.Card;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CodeBase.UI
{
    public class ProgressView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI progressText;
        private List<CardModel> _cards = new List<CardModel>();
        private int _currentIndex;
        private int _completedCount;
        private int _correctAnswers;

        private void Awake() => Initialize();

        public void Initialize()
        {
            _cards.Clear();
            _currentIndex = 0;
            _completedCount = 0;
            _correctAnswers = 0;
            ShowCard();
            UpdateProgress();
        }

        public void OnCardClicked()
        {
            if (_cards.Count == 0) return;
            _completedCount++;
            _currentIndex = (_currentIndex + 1) % _cards.Count;
            ShowCard();
            UpdateProgress();
        }

        public void OnCorrectAnswer()
        {
            _correctAnswers++;
            UpdateProgress();
        }

        private void ShowCard()
        {
            if (_cards.Count == 0) return;
            var currentCard = _cards[_currentIndex];
            // Logic for showing the current card's definition.
        }

        private void UpdateProgress()
        {
            progressText.text = $"Progress: {_correctAnswers}/{_cards.Count}";
        }
    }
}

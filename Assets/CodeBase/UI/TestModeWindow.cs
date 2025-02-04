using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using CodeBase.Services.Card;
using System.Linq;
using Zenject;

namespace CodeBase.UI
{
    public class TestModeWindow : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI questionText;
        [SerializeField] private Button[] answerButtons;
        [SerializeField] private TextMeshProUGUI[] answerTexts;
        [SerializeField] private Button closeButton;
        //[Inject]private CardPresenter _cardPresenter;
        [SerializeField] private ProgressView progress;

        private List<CardModel> _cards;
        private CardModel _currentCard;
        private int _currentIndex = 0;
        private VibrationService _vibrationService;

        public void Initialize(List<CardModel> cards, Language selectedLanguage)
        {
            _cards = cards.Where(card => card.Language == selectedLanguage).ToList();
            _currentIndex = 0;
            ShowNextQuestion();
        }

        private void Start()
        {
            closeButton.onClick.AddListener(Close);
            foreach (var button in answerButtons)
            {
                button.onClick.AddListener(() => OnAnswerSelected(button));
            }
            bool isVibrationEnabled = PlayerPrefs.GetInt("VibrationEnabled", 1) == 1;
            _vibrationService = new VibrationService(isVibrationEnabled);
        }

        private void ShowNextQuestion()
        {
            if (_currentIndex >= _cards.Count)
            {
                Close();
                return;
            }

            _currentCard = _cards[_currentIndex];
            questionText.text = _currentCard.Definition;  // ���������� �����������

            var shuffledCards = new List<CardModel>(_cards);
            shuffledCards.Shuffle();  // ������������ ��������

            shuffledCards.Remove(_currentCard);
            shuffledCards.Insert(Random.Range(0, 4), _currentCard);  // ��������� ������� �������� � ��������� �����

            for (int i = 0; i < answerButtons.Length; i++)
            {
                answerTexts[i].text = shuffledCards[i].Term;  // ���������� ������� � shuffledCards
                answerButtons[i].interactable = true;
            }
        }
        private void OnAnswerSelected(Button button)
        {
            int index = System.Array.IndexOf(answerButtons, button);

            if (answerTexts[index].text == _currentCard.Term)
            {
                Debug.Log("Correct!");
                _vibrationService.Vibrate(true);
                progress.OnCorrectAnswer();  // ����������� ����� ��������
            }
            else
            {
                _vibrationService.Vibrate(false);
                Debug.Log("Wrong!");
            }
            
            _currentIndex++;
            ShowNextQuestion();
        }

        private void Close()
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
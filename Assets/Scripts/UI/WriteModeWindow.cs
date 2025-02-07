using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using CodeBase.Services.Card;
using System.Linq;
using Zenject;
using System;


namespace CodeBase.UI
{
    public class WriteModeWindow : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI definitionText;
        [SerializeField] private TMP_InputField answerInput;
        [SerializeField] private Button submitButton;
        //[SerializeField] private TextMeshProUGUI resultText;
        [SerializeField] private IncorrectPopup _incorrectPopup;
        [SerializeField] private Button closeButton;
        //[Inject] private CardPresenter _cardPresenter;
        [SerializeField] private TextMeshProUGUI progress;
        [SerializeField] private Image progressBar;
        private List<CardModel> _availableCards;
        private CardModel _currentCard;
        private int _currentQuestionIndex;
        private int _totalQuestions;
        private int _correctAnswers;
        private VibrationService _vibrationService;
        [SerializeField] private TextMeshProUGUI TestResultText;
        public void Initialize(List<CardModel> allCards, Language selectedLanguage)
        {
            _availableCards = new List<CardModel>(allCards);
            _availableCards = allCards.Where(card => card.Language == selectedLanguage).ToList();
            _totalQuestions = _availableCards.Count;
            _currentQuestionIndex = 0;
            _correctAnswers = 0;
            ShowNextQuestion();
        }
        private void Start()
        {
            submitButton.onClick.AddListener(OnSubmitClicked);
            closeButton.onClick.AddListener(Close);
            bool isVibrationEnabled = PlayerPrefs.GetInt("VibrationEnabled", 1) == 1;
            _vibrationService = new VibrationService(isVibrationEnabled);
            _incorrectPopup.onPopupClosed += ShowNextQuestion;
        }
        private void OnDestroy()
        {
            _incorrectPopup.onPopupClosed -= ShowNextQuestion;  
        }
        private void Close()
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }

        private void ShowNextQuestion()
        {
            Debug.Log("WHO IS CALLING IT");
            if (_currentQuestionIndex >= _totalQuestions)
            {
                FinishTest();
                return;
            }
            _currentCard = _availableCards[_currentQuestionIndex];
            progress.text = $"{_currentQuestionIndex + 1} / {_availableCards.Count}";
            progressBar.fillAmount = (float)(_currentQuestionIndex + 1) / _availableCards.Count;
            definitionText.text = _currentCard.Definition;  // Показываем определение
            answerInput.text = "";
            _currentQuestionIndex++;
        }

        private void OnSubmitClicked()
        {
            bool result = IsCorrectAnswer();
            _vibrationService.Vibrate(result);
            _correctAnswers += result ? 1 : 0;
            ShowPopup(IsCorrectAnswer(), _currentCard.Term);
        }

        private void ShowPopup(bool isCorrect, string correctAnswer)
        {
            _incorrectPopup.Show(isCorrect, correctAnswer);
        }

        private bool IsCorrectAnswer()
        {
            return answerInput.text.Trim().ToLower() == _currentCard.Term.Trim().ToLower();
        }
        private void FinishTest()
        {
            TestResultText.text = $"Test completed! Good Job! \n {_correctAnswers} correct answers ";
            TestResultText.gameObject.SetActive(true);
            submitButton.gameObject.SetActive(false);
            answerInput.gameObject.SetActive(false);
        }
    }
}
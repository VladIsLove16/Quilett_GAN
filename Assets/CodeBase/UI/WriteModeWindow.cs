using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using CodeBase.Services.Card;
using System.Linq;
using Zenject;


namespace CodeBase.UI
{
    public class WriteModeWindow : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI questionText;
        [SerializeField] private TMP_InputField answerInput;
        [SerializeField] private Button submitButton;
        [SerializeField] private TextMeshProUGUI resultText;
        [SerializeField] private Button closeButton;
        //[Inject] private CardPresenter _cardPresenter;
        [SerializeField] private ProgressView progress;
        private List<CardModel> _availableCards;
        private CardModel _currentCard;
        private int _currentQuestionIndex;
        private int _totalQuestions;
        private int _correctAnswers;
        private VibrationService _vibrationService;



        public void Initialize(List<CardModel> allCards, Language selectedLanguage)
        {
            _availableCards = new List<CardModel>(allCards);
            _availableCards = allCards.Where(card => card.Language == selectedLanguage).ToList();
            _totalQuestions = _availableCards.Count;
            _currentQuestionIndex = 0;
            _correctAnswers = 0;


            ShowNextQuestion();
        }

        private void Close()
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }

        private void ShowNextQuestion()
        {
            if (_currentQuestionIndex >= _totalQuestions)
            {
                FinishTest();
                return;
            }

            _currentCard = _availableCards[_currentQuestionIndex];
            _currentQuestionIndex++;

            questionText.text = _currentCard.Definition;  // Показываем определение
            answerInput.text = "";
            resultText.text = "";
        }
        private void Start()
        {
            submitButton.onClick.AddListener(OnSubmitClicked);
            closeButton.onClick.AddListener(Close);
            bool isVibrationEnabled = PlayerPrefs.GetInt("VibrationEnabled", 1) == 1;
            _vibrationService = new VibrationService(isVibrationEnabled);
        }

        private void OnSubmitClicked()
        {
            if (answerInput.text.Trim().ToLower() == _currentCard.Term.ToLower())
            {
                _correctAnswers++;
                resultText.text = "Правильно!";
                //_cardPresenter.OnCorrectAnswer();  // Увеличиваем общий прогресс
                progress.OnCorrectAnswer();
            }
            else
            {
                resultText.text = $"Ошибка! Правильный ответ: {_currentCard.Term}";
            }

            bool isCorrect = answerInput.text.Trim().ToLower() == "правильный ответ"; // Пример

            

            _vibrationService.Vibrate(isCorrect);

            Invoke(nameof(ShowNextQuestion), 1.5f);
        }


        private void FinishTest()
        {
            questionText.text = "Тест завершен!";
            resultText.text = $"Правильных ответов: {_correctAnswers} из {_totalQuestions}";
            submitButton.gameObject.SetActive(false);
            answerInput.gameObject.SetActive(false);
        }
    }
}
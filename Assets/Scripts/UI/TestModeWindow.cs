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
        private const int NOTSELECTEDBUTTONINDEX = -1;
        private const int MINCARDSCOUNT = 4;
        [SerializeField] private TextMeshProUGUI questionText;
        [SerializeField] private Button[] answerButtons;
        [SerializeField] private TextMeshProUGUI[] answerTexts;
        [SerializeField] private Outline[] Outlines;
        [SerializeField] private Button closeButton;
        [SerializeField] private Button confirmButton;
        [SerializeField] Color confrimButtonColorDisabled = new Color(15, 80, 156, 0.25f);
        [SerializeField] Color confrimButtonColorActive = new Color(15, 80, 156, 1);
        [SerializeField] private IncorrectPopup _incorrectPopup;
        [SerializeField] CookieAnimator animator;
        //[Inject]private CardPresenter _cardPresenter;
        [SerializeField] private TextMeshProUGUI progress;
        [SerializeField] private Image progressBar;
        [SerializeField] private TextMeshProUGUI TestResultText;

        private List<CardModel> _cards;
        private CardModel _currentCard;
        private int _currentIndex = 0;
        private VibrationService _vibrationService;
        private int selectedButtonIndex;
        private int correctAnswers;
        [SerializeField] private Color unselectedColor = Color.white;
        [SerializeField] private Color selectedColor = new Color();
        public bool Initialize(List<CardModel> cards, Language selectedLanguage)
        {
            if (cards.Count < MINCARDSCOUNT)
            {
                Debug.LogWarning("Cant Initilize. Enter More cards");
                return false;
            }
            //_cards = cards.Where(card => card.Language == selectedLanguage).ToList();
            _cards = cards.ToList();
            _currentIndex = 0;
            selectedButtonIndex = NOTSELECTEDBUTTONINDEX;
            correctAnswers = 0;
            ShowNextQuestion();
            return true;
        }

        private void Start()
        {
            closeButton.onClick.AddListener(Close);
            confirmButton.onClick.AddListener(ConfirmSelection);
            foreach (var button in answerButtons)
            {
                button.onClick.AddListener(() => OnAnswerSelected(button));
            }
            bool isVibrationEnabled = PlayerPrefs.GetInt("VibrationEnabled", 1) == 1;
            _vibrationService = new VibrationService(isVibrationEnabled);
            _incorrectPopup.onPopupClosed += ShowNextQuestion;
        }
        private void OnDestroy()
        {
            _incorrectPopup.onPopupClosed -= ShowNextQuestion;
        }
        private void ShowNextQuestion()
        {
            if (_currentIndex >= _cards.Count)
            {
                FinishTest();
                return;
            }

            _currentCard = _cards[_currentIndex];
            questionText.text = _currentCard.Definition; 

            var shuffledCards = new List<CardModel>(_cards);
            shuffledCards.Shuffle();  

            shuffledCards.Remove(_currentCard);
            shuffledCards.Insert(Random.Range(0, 4), _currentCard);

            for (int i = 0; i < answerButtons.Length; i++)
            {
                answerTexts[i].text = shuffledCards[i].Term;  
                answerButtons[i].interactable = true;
            }
            animator.SetState(CookieAnimator.State.Hello);
            progress.text = $"{_currentIndex + 1} / {_cards.Count}";
            progressBar.fillAmount = (float)(_currentIndex + 1) / _cards.Count;
            _currentIndex++;
        }
        private void OnAnswerSelected(Button button)
        {
            confirmButton.image.color = confrimButtonColorActive;
            UnSelectSelectedButton();
            selectedButtonIndex = System.Array.IndexOf(answerButtons, button);
            Outlines[selectedButtonIndex].enabled = false;
            button.image.color = selectedColor;
        }

        private void ConfirmSelection()
        {
            if (selectedButtonIndex < 0)
                return;
            bool isCorrectAnswer = answerTexts[selectedButtonIndex].text == _currentCard.Term;
            _vibrationService.Vibrate(isCorrectAnswer);
            correctAnswers += isCorrectAnswer ? 1 : 0;
            ShowResultMenu(isCorrectAnswer, _currentCard.Term);
            UnSelectSelectedButton();
            selectedButtonIndex = NOTSELECTEDBUTTONINDEX;
            confirmButton.image.color = confrimButtonColorDisabled  ;
        }
        private Button GetSelectedButton()
        {
            if (selectedButtonIndex < answerButtons.Count() && selectedButtonIndex >= 0)
                return answerButtons[selectedButtonIndex];
            else
                return null;
        }
        private void UnSelectSelectedButton()
        {
            Button btn = GetSelectedButton();
            if (btn == null)
                return;
            Outlines[selectedButtonIndex].enabled = true;
            btn.image.color = unselectedColor;
        }

        private void ShowResultMenu(bool isCorrectAnswer,string term)
        {
            animator.SetState(isCorrectAnswer ? CookieAnimator.State.Happy : CookieAnimator.State.Sad);
            _incorrectPopup.Show(isCorrectAnswer, term);
        }
        private void FinishTest()
        {
            TestResultText.text = $"Test completed! Good Job! \n {correctAnswers} correct answers ";    
            TestResultText.gameObject.SetActive(true);
            foreach (var btn in answerButtons)
            {
                btn.gameObject.SetActive(false);
            }
        }
        private void Close()
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}

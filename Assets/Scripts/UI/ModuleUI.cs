using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CodeBase.Services.Card;
using Zenject;
using System;

namespace CodeBase.UI
{
    public class ModuleUI : MonoBehaviour
    {
        [SerializeField] private InputField termInputField;
        [SerializeField] private InputField definitionInputField;
        [SerializeField] private Button createCardButton;
        [SerializeField] private Button startReviewButton;
        [SerializeField] private Button startTestingModeButton;
        [SerializeField] private Button startTypingModeButton;
        [SerializeField] private Transform cardContainer;
        [SerializeField] private CardView cardPrefab;
        [SerializeField] private GameObject reviewWindowParent;
        [SerializeField] private GameObject testWindowParent;  
        [SerializeField] private GameObject TypingModeWindowParent;  
        [SerializeField] private TMP_Dropdown languageDropdown;
        private Language _language;
        [SerializeField] private Button openSettingsButton; // Кнопка для открытия настроек
        private SettingsWindow _settingsWindow;

        private ICardManager _cardManager;
        private ReviewWindow _reviewWindowPrefab;
        private TestModeWindow _testWindowPrefab;  // ������ ��������� ����
        private WriteModeWindow _typingModeWindowPrefab;
        [Inject]
        public void Construct(ICardManager cardManager, ReviewWindow reviewWindowPrefab, TestModeWindow testWindowPrefab, WriteModeWindow writeModeWindowPrefab, SettingsWindow settingsWindow)
        {
            _cardManager = cardManager;
            _reviewWindowPrefab = reviewWindowPrefab;
            _testWindowPrefab = testWindowPrefab;
            _typingModeWindowPrefab = writeModeWindowPrefab;
            _settingsWindow = settingsWindow;
        }

        private void Start()
        {
            createCardButton.onClick.AddListener(OnCreateCardClicked);
            startReviewButton.onClick.AddListener(OnStartReviewClicked);
            startTestingModeButton.onClick.AddListener(OnStartTestClicked);  // �������� �� ���� �� ������ �����
            startTypingModeButton.onClick.AddListener(OnStartTypingModeClicked);
            languageDropdown.onValueChanged.AddListener(OnLanguageChanged);
            openSettingsButton.onClick.AddListener(OpenSettings);
        }

        private void OpenSettings()
        {
            Instantiate(_settingsWindow, reviewWindowParent.transform);
        }

        private void OnLanguageChanged(int index)
        {
            Language selectedLanguage = (Language)index;
            SetLanguage(selectedLanguage);
        }

        private void SetLanguage(Language language)
        {
            // Pass the language setting to the corresponding windows
            _language = language;
            _cardManager.SetLanguage(language);
        }
        private void OnCreateCardClicked()
        {
            string term = termInputField.text;
            string definition = definitionInputField.text;

            if (!string.IsNullOrEmpty(term) && !string.IsNullOrEmpty(definition))
            {
                _cardManager.CreateCard(term, definition, _language);  // �������� ��������� ����
                termInputField.text = "";
                definitionInputField.text = "";
            }
        }


        private void OnStartReviewClicked()
        {
            if (reviewWindowParent == null)
            {
                Debug.LogError("Review window parent not assigned!");
                return;
            }

            var allCards = _cardManager.GetAllCards();
            var reviewWindowInstance = Instantiate(_reviewWindowPrefab, reviewWindowParent.transform);
            reviewWindowInstance.Initialize(allCards);
            reviewWindowInstance.gameObject.SetActive(true);
        }

        private void OnStartTypingModeClicked()
        {
            if (TypingModeWindowParent == null)
            {
                Debug.LogError("Typing mode window parent not assigned!");
                return;
            }

            var allCards = _cardManager.GetAllCards();
            var typingModeWindowInstance = Instantiate(_typingModeWindowPrefab, TypingModeWindowParent.transform);
            typingModeWindowInstance.Initialize(allCards, _language);  // �������� ��������� ����
            typingModeWindowInstance.gameObject.SetActive(true);
        }

        private void OnStartTestClicked()
        {
            if (testWindowParent == null)
            {
                Debug.LogError("Test window parent not assigned!");
                return;
            }

            CreateTestWindow();
        }

        private void CreateTestWindow()
        {
            var allCards = _cardManager.GetAllCards();
            var testWindowInstance = Instantiate(_testWindowPrefab, testWindowParent.transform);
            testWindowInstance.Initialize(allCards, _language);  // �������� ��������� ����
            testWindowInstance.gameObject.SetActive(true);
        }
    }
}
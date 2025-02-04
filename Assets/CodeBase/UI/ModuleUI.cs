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
        [SerializeField] private Button startTestButton;  // ��������� ������ ��� ������� �����
        [SerializeField] private Transform cardContainer;
        [SerializeField] private CardView cardPrefab;
        [SerializeField] private GameObject reviewWindowParent;
        [SerializeField] private GameObject testWindowParent;  // ������ ������ ��� ��������� ����
        [SerializeField] private Button startWriteModeButton;
        [SerializeField] private GameObject writeModeWindowParent;  // �������� ��� ����
        [SerializeField] private TMP_Dropdown languageDropdown;
        private Language _language;
        [SerializeField] private Button settingsButton; // Кнопка для открытия настроек
        private SettingsWindow _settingsWindow;

        private ICardManager _cardManager;
        private ReviewWindow _reviewWindowPrefab;
        private TestModeWindow _testWindowPrefab;  // ������ ��������� ����
        private WriteModeWindow _writeModeWindowPrefab;
        [Inject]
        public void Construct(ICardManager cardManager, ReviewWindow reviewWindowPrefab, TestModeWindow testWindowPrefab, WriteModeWindow writeModeWindowPrefab, SettingsWindow settingsWindow)
        {
            _cardManager = cardManager;
            _reviewWindowPrefab = reviewWindowPrefab;
            _testWindowPrefab = testWindowPrefab;
            _writeModeWindowPrefab = writeModeWindowPrefab;
            _settingsWindow = settingsWindow;
        }

        private void Start()
        {
            createCardButton.onClick.AddListener(OnCreateCardClicked);
            startReviewButton.onClick.AddListener(OnStartReviewClicked);
            startTestButton.onClick.AddListener(OnStartTestClicked);  // �������� �� ���� �� ������ �����
            startWriteModeButton.onClick.AddListener(OnStartWriteModeClicked);
            languageDropdown.onValueChanged.AddListener(OnLanguageChanged);
            settingsButton.onClick.AddListener(OpenSettings);
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

        private void OnStartWriteModeClicked()
        {
            if (writeModeWindowParent == null)
            {
                Debug.LogError("Write mode window parent not assigned!");
                return;
            }

            var allCards = _cardManager.GetAllCards();
            var writeModeWindowInstance = Instantiate(_writeModeWindowPrefab, writeModeWindowParent.transform);
            writeModeWindowInstance.Initialize(allCards, _language);  // �������� ��������� ����
            writeModeWindowInstance.gameObject.SetActive(true);
        }

        private void OnStartTestClicked()
        {
            if (testWindowParent == null)
            {
                Debug.LogError("Test window parent not assigned!");
                return;
            }

            var allCards = _cardManager.GetAllCards();
            var testWindowInstance = Instantiate(_testWindowPrefab, testWindowParent.transform);
            testWindowInstance.Initialize(allCards, _language);  // �������� ��������� ����
            testWindowInstance.gameObject.SetActive(true);
        }
    }
}
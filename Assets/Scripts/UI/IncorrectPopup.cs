using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    internal class IncorrectPopup : MonoBehaviour
    {

        [SerializeField] private TextMeshProUGUI popupStatusText;
        [SerializeField] private TextMeshProUGUI correctAnswerText;
        [SerializeField] private Sprite correctAnswerSprite;
        [SerializeField] private Sprite incorrectAnswerSprite;
        [SerializeField] private Image correctnessAnswerImage;
        [SerializeField] private Button _confirmButton;
        public Action onPopupClosed;
        private void Start()
        {
            _confirmButton.onClick.AddListener(OnConfirmClick);
        }
         private void OnConfirmClick()
        {
            Hide();
            onPopupClosed.Invoke();
        }

        public void Show(bool isCorrectAnswer, string correctAnswer)
        {
            UpdateCorrectAnswerText(correctAnswer);
            UpdateStatus(isCorrectAnswer);
            UpdateCorrectnessImage(isCorrectAnswer);
            UpdateConfrimButtonColor(isCorrectAnswer);
            Show();
        }

        private void UpdateConfrimButtonColor(bool isCorrectAnswer)
        {
            _confirmButton.image.color = isCorrectAnswer ? Color.green : Color.red;
        }

        private void Show()
        {
            gameObject.SetActive(true);
            //maybe animation
        }

        private void UpdateCorrectnessImage(bool isCorrectAnswer)
        {
            correctnessAnswerImage.sprite = isCorrectAnswer ? correctAnswerSprite : incorrectAnswerSprite;
        }

        private void UpdateStatus(bool isCorrectAnswer)
        {
            popupStatusText.text = GetTextByStatus(isCorrectAnswer);
        }

        private void UpdateCorrectAnswerText(string correctAnswer)
        {
            correctAnswerText.text = correctAnswer;
        }

        private string GetTextByStatus(bool isCorrectAnswer)
        {
            if (isCorrectAnswer)
                return "Correct";
            else
                return "Incorrect";
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            //maybe animation
        }

    }
}
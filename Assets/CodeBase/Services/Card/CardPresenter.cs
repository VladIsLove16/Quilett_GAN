using CodeBase.Services.Card;
using CodeBase.UI;
using System.Collections.Generic;
using Zenject;

public class CardPresenter
{
    //private readonly ProgressView _progressView;
    private List<CardModel> _cards;
    private int _currentIndex;
    private int _completedCount;
    private int _correctAnswers;  // Новый счетчик для правильных ответов
    //[Inject] private IProgressSlider slider;
  //  [Inject]
    public CardPresenter()
    {
      //  _progressView = progressView;
        _cards = new List<CardModel>();
    }

    public void LoadCards(List<CardModel> cards, ProgressView progress)
    {
        _cards = cards;
        _currentIndex = 0;
        _completedCount = 0;
        _correctAnswers = 0;  // Сбрасываем счетчик правильных ответов
        ShowCard();
        UpdateProgress();
    }

    public void OnCardClicked(ProgressView progress)
    {
        if (_cards.Count == 0) return;

        _completedCount++;
        _currentIndex = (_currentIndex + 1) % _cards.Count;
        ShowCard();
        UpdateProgress();
    }

    // Обработчик правильного ответа
    public void OnCorrectAnswer()
    {
        _correctAnswers++;
        UpdateProgress();
    }

    private void ShowCard()
    {
        var currentCard = _cards[_currentIndex];
      //  progress.UpdateProgress(_correctAnswers, _cards.Count);  // Обновление прогресса с учетом правильных ответов
    }

    private void UpdateProgress()
    {
      //  progress.UpdateProgress(_correctAnswers, _cards.Count);  // Обновление прогресса
    }
}

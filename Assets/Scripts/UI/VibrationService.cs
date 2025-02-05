using UnityEngine;

public class VibrationService
{
    private bool _isVibrationEnabled;

    public VibrationService(bool isVibrationEnabled)
    {
        _isVibrationEnabled = isVibrationEnabled;
    }

    public void Vibrate(bool isCorrect)
    {
        if (!_isVibrationEnabled) return;

        if (isCorrect)
        {
            Handheld.Vibrate(); // Вибрация при правильном ответе
        }
        else
        {
            // Имитация короткой вибрации при неправильном ответе
            Handheld.Vibrate();
            System.Threading.Thread.Sleep(100);
            Handheld.Vibrate();
        }
    }
}
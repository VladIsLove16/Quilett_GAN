using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace CodeBase.UI
{
    public class SettingsWindow : MonoBehaviour
    {
        [SerializeField] private Toggle musicToggle;
        [SerializeField] private Slider musicSlider;
        [SerializeField] private Toggle vibrationToggle;
        [SerializeField] private Button closeButton;
        private float musicValue;
        private bool _isVibrationEnabled = true;
        private bool _isMusicEnabled = true;

        private void Start()
        {
            musicToggle.onValueChanged.AddListener(ToggleMusic);
            musicSlider.onValueChanged.AddListener(MusicValue);
            vibrationToggle.onValueChanged.AddListener(ToggleVibration);
            closeButton.onClick.AddListener(Close);

            LoadSettings();
        }
        public void MusicValue(float amount)
        {
            musicValue = amount;
            PlayerPrefs.SetFloat("MusicValue", amount);
        }
        private void ToggleMusic(bool isOn)
        {
            _isMusicEnabled = isOn;
            // Логика для включения/выключения музыки
            AudioListener.volume = isOn ? 1 : 0;
            PlayerPrefs.SetInt("MusicEnabled", isOn ? 1 : 0);
        }

        private void ToggleVibration(bool isOn)
        {
            _isVibrationEnabled = isOn;
            PlayerPrefs.SetInt("VibrationEnabled", isOn ? 1 : 0);
        }

        private void LoadSettings()
        {
            _isMusicEnabled = PlayerPrefs.GetInt("MusicEnabled", 1) == 1;
            _isVibrationEnabled = PlayerPrefs.GetInt("VibrationEnabled", 1) == 1;

            musicValue = PlayerPrefs.GetFloat("MusicValue", 0.7f);
            musicToggle.isOn = _isMusicEnabled;
            vibrationToggle.isOn = _isVibrationEnabled;
        }

        private void Close()
        {
            gameObject.SetActive(false);
        }
        public float Volume => musicValue;
        public bool IsVibrationEnabled() => _isVibrationEnabled;
    }

    
}
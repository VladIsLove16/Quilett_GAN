using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using System;

public class Translator : MonoBehaviour
{
    //[SerializeField]
    private string apiUrl = "https://api.mymemory.translated.net/get";  // Базовый URL

    [SerializeField]
    string customText;

    public void TranslateText(string text, System.Action<string> callback)
    {
        Debug.Log(text);
        if (!string.IsNullOrEmpty(customText))
            text = customText;

        StartCoroutine(SendTranslationRequest(text, callback));
    }

    IEnumerator SendTranslationRequest(string text, System.Action<string> callback)
    {
        // Формируем правильный URL с параметром q=текст для перевода
        string url = $"{apiUrl}?q={Uri.EscapeDataString(text)}&langpair=en|de";

        Debug.Log($"Отправляем запрос с текстом: {text}");  // Логируем отправляемый текст

        using (UnityWebRequest www = UnityWebRequest.Get(url)) // Используем GET запрос
        {
            yield return www.SendWebRequest();

            Debug.Log($"Статус код: {www.responseCode}");
            Debug.Log($"Ответ сервера: {www.downloadHandler.text}");

            if (www.result == UnityWebRequest.Result.Success)
            {
                string jsonResponse = www.downloadHandler.text;
                try
                {
                    TranslationResponse response = JsonUtility.FromJson<TranslationResponse>(jsonResponse);
                    if (response != null && response.responseData != null && !string.IsNullOrEmpty(response.responseData.translatedText))
                    {
                        Debug.Log($"Переведенный текст: {response.responseData.translatedText}");  // Логируем переведённый текст
                        callback?.Invoke(response.responseData.translatedText);
                    }
                    else
                    {
                        Debug.LogError("Ответ не содержит перевода!");
                        callback?.Invoke(null);
                    }
                }
                catch (System.Exception ex)
                {
                    Debug.LogError("Ошибка при разборе JSON: " + ex.Message);
                    callback?.Invoke(null);
                }
            }
            else
            {
                Debug.LogError($"Ошибка перевода: {www.responseCode} {www.downloadHandler.text}");
                callback?.Invoke(null);
            }
        }
    }

    [System.Serializable]
    public class TranslationResponse
    {
        public ResponseData responseData;
        public int responseStatus;
    }

    [System.Serializable]
    public class ResponseData
    {
        public string translatedText;
    }
}

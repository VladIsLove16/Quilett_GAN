using UnityEngine;

public class AndroidBridge : MonoBehaviour
{
    // Проверка, работает ли на Android
    private static bool IsAndroid()
    {
        return Application.platform == RuntimePlatform.Android;
    }

    // Метод для сохранения случайного слова на Android
    public static void SaveRandomWord(string word)
    {
        if (IsAndroid())
        {
            using (AndroidJavaClass javaClass = new AndroidJavaClass("com.example.mywidget.UnityBridge"))
            {
                javaClass.CallStatic("saveRandomWord", word);
            }
        }
    }

    // Метод для получения слова на Android
    public static string GetRandomWord()
    {
        if (IsAndroid())
        {
            using (AndroidJavaClass javaClass = new AndroidJavaClass("com.example.mywidget.UnityBridge"))
            {
                return javaClass.CallStatic<string>("getRandomWord");
            }
        }
        return "Нет данных";
    }
}

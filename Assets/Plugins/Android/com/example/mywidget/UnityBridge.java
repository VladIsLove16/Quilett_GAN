package com.example.mywidget;

import android.content.Context;
import android.content.SharedPreferences;

public class UnityBridge {

    private static final String PREFS_NAME = "RandomWordPrefs";
    private static final String KEY_WORD = "random_word";

    // Сохраняем слово в SharedPreferences
    public static void saveRandomWord(Context context, String word) {
        SharedPreferences preferences = context.getSharedPreferences(PREFS_NAME, Context.MODE_PRIVATE);
        SharedPreferences.Editor editor = preferences.edit();
        editor.putString(KEY_WORD, word);
        editor.apply();
    }

    // Получаем слово из SharedPreferences
    public static String getRandomWord(Context context) {
        SharedPreferences preferences = context.getSharedPreferences(PREFS_NAME, Context.MODE_PRIVATE);
        return preferences.getString(KEY_WORD, "Слово не найдено");
    }
}

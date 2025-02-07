package com.example.mywidget;

import android.app.PendingIntent;
import android.appwidget.AppWidgetManager;
import android.appwidget.AppWidgetProvider;
import android.content.ComponentName;
import android.content.Context;
import android.content.Intent;
import android.widget.RemoteViews;
import java.util.Random;

public class RandomWordWidget extends AppWidgetProvider {

    private static final String[] WORDS = {"Привет", "Слово", "Виджет", "Андроид", "Unity"};

    @Override
    public void onUpdate(Context context, AppWidgetManager appWidgetManager, int[] appWidgetIds) {
        for (int appWidgetId : appWidgetIds) {
            RemoteViews views = new RemoteViews(context.getPackageName(), R.layout.widget_layout);
            
            // Генерация случайного слова
            String randomWord = WORDS[new Random().nextInt(WORDS.length)];
            views.setTextViewText(R.id.widget_text, randomWord);

            // Обновление виджета
            appWidgetManager.updateAppWidget(appWidgetId, views);
        }
    }
}

using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine;
public static class JsonSaveSystem
{
    public static void Save<T>(T data, string filename= null) where T : class
    {
        if (string.IsNullOrEmpty(filename))
            filename = typeof(T).ToString();
        string path = Application.persistentDataPath + "/" + filename + ".json";
        try
        {
            // Сериализация данных в JSON
            string json = JsonConvert.SerializeObject(data, Formatting.Indented);

            // Запись JSON в файл
            File.WriteAllText(path, json);
            Debug.Log("Saved");
        }
        catch (Exception ex)
        {
            Debug.LogError("Failed to save file at " + path + ": " + ex.Message);
        }
    }
    public static T Load<T>(string filename = null) where T : class
    {
        if (string.IsNullOrEmpty(filename))
            filename = typeof(T).ToString();
        string path = Application.persistentDataPath + "/" + filename + ".json";

        if (File.Exists(path))
        {
            try
            {
                string json = File.ReadAllText(path);
                T data = JsonConvert.DeserializeObject<T>(json);
                return data;
            }
            catch (Exception ex)
            {
                Debug.LogError("Failed to load file at " + path + ": " + ex.Message);
            }
        }
        else
        {
            Debug.Log("Save file not found in " + path);
        }

        return null;
    }
}

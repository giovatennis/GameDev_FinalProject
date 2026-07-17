using UnityEngine;

public static class SaveSystem
{
    private const string SaveKey = "RuinsExpedition_Save";

    public static void Save(SaveData data)
    {
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(SaveKey, json);
        PlayerPrefs.Save();
    }

    public static SaveData Load()
    {
        if(!HasSave())
        {
            return null;
        }
        string json = PlayerPrefs.GetString(SaveKey);
        return JsonUtility.FromJson<SaveData>(json);
    }

    public static bool HasSave()
    {
        return PlayerPrefs.HasKey(SaveKey);
    }

    public static void DeleteSave()
    {
        PlayerPrefs.DeleteKey(SaveKey);
        PlayerPrefs.Save();
    }
}
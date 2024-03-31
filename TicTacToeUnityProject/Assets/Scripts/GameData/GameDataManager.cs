using System.IO;
using UnityEngine;

public static class GameDataManager 
{
    static string path = Application.dataPath;
    public static void Save(GameStateData state)
    {
        string fullpath = Path.Combine(path, "SavedGame.json");
        Directory.CreateDirectory(Path.GetDirectoryName(fullpath));
        string data = JsonUtility.ToJson(state);

        using FileStream stream = new FileStream(fullpath, FileMode.Create);
        using StreamWriter writer = new StreamWriter(stream);
        writer.Write(data);
    }

    public static GameStateData Load()
    {
        GameStateData loadedData = new GameStateData();
        string fullpath = Path.Combine(path, "SavedGame.json");
        if (File.Exists(fullpath))
        {
            string dataToLoad = string.Empty;
            using (FileStream stream = new FileStream(fullpath, FileMode.Open))
            {
                using StreamReader writer = new StreamReader(stream);
                dataToLoad = writer.ReadToEnd();
            }

            loadedData = JsonUtility.FromJson<GameStateData>(dataToLoad);
        }

        return loadedData;
    }
}



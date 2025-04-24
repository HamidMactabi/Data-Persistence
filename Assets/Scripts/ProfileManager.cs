using System;
using System.IO;
using UnityEngine;

public class ProfileManager : MonoBehaviour
{
    public string playerName;
    public int bestScore;
    public static ProfileManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
    public void RegisterName(string name)
    {
        playerName = name;
    }

    internal class SaveData
    {
        public string PlayerName;
        public int BestScore;
    }

    public void Save()
    {
        var data = new SaveData
        {
            PlayerName = playerName,
            BestScore = bestScore
        };

        var json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/profile" , json);
    }

    public void Load()
    {
        var path = Application.persistentDataPath + "/profile";
        if (File.Exists(path))
        {
            var json = File.ReadAllText(path);
            var data = JsonUtility.FromJson<SaveData>(json);
            playerName = data.PlayerName;
            bestScore = data.BestScore;
        }
    }
}

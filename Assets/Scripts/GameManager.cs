using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public string playerName;
    public int bestScore;
    public string bestScoreName;

    public void Awake()
    {
        if(Instance!=null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
     
    }
    [System.Serializable]
    public class SaveData
    {
        SaveData data = new SaveData();
            data.playerName=playerName;
            data.bestScoreName=bestScoreName;
            data.bestScore=bestScore;

            string json = JsonUtility.ToJson(data);
            File.WriteAllText(Application.PresistantDataPath+"/savefile.json",json);
    }
    public void LoadData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            playerName = data.playerName;
            bestScore = data.bestScore;
            bestScoreName = data.bestScoreName;
        }
    }


}
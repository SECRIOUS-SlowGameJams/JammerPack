using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SavedDataManager : MonoBehaviour
{
    public List<SavedGame> savedGames;

    [SerializeField]
    string saveLocation;

    [SerializeField]
    string defaultSaveGameName;

    SavedGame activeGame;

    public static event Action DataLoadedEvent;

    private void Awake()
    {
        saveLocation = Application.dataPath;
        savedGames = new List<SavedGame>();
        LoadAllSavedFiled();
    }

    public void SaveData(string saveFileName)
    {
        string fullfilePath = saveLocation + "\\" + saveFileName + ".json";
        string data = JsonUtility.ToJson(activeGame);
        File.WriteAllText(fullfilePath, data);
    }

    SavedGame LoadData(string fileLocation)
    {
        if (File.Exists(fileLocation))
        {
            string loadedData = File.ReadAllText(fileLocation);
            if (!string.IsNullOrEmpty(loadedData))
            {
                Debug.Log("Data loaded");
                DataLoadedEvent?.Invoke();
                return JsonUtility.FromJson<SavedGame>(loadedData);
            }
            else return null;
        }
        else return null;
    }

    void LoadAllSavedFiled()
    {
        string fileFormat = ".json";
        string[] allfiles = Directory.GetFiles(saveLocation, fileFormat, SearchOption.TopDirectoryOnly);


        foreach (var item in allfiles)
        {
            SavedGame savedGame = LoadData(item);
            if (savedGame != null)
            {
                savedGames.Add(savedGame);
            }
        }
    }

}



public class SavedGame
{
    public string savedFileName;
    //add your data here;

}
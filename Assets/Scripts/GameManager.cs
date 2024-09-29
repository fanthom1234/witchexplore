using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    // Event/Action //

    // Menu //
    public SceneUtils sceneUtils;
    // Data //
    public List<Customers> customers;
    public bool hasSave 
    {
        get 
        {
            return ES3.KeyExists("wasSaved");
        }  
        private set {}
    }
    // Singleton //
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    public void NewGame()
    {
        DeleteSaveGame();
        ES3.Save("wasSaved", true);
        sceneUtils.LoadScene("MainGame", null);
    }

    public void LoadGame()
    {
        if(hasSave)
            sceneUtils.LoadScene("MainGame", () => ES3AutoSaveMgr.Current.Load());
    }

    public void SaveGame()
    {
        ES3AutoSaveMgr.Current.Save();
        ES3.Save("wasSaved", true);
        Debug.Log("Saved!!");
    }

    public void DeleteSaveGame()
    {
        if(hasSave)
        {
            ES3.DeleteKey("wasSaved");
            ES3.DeleteFile("SaveFile.es3");
        }
    }
}

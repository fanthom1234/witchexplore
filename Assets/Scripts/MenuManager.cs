using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject newGameButton; 
    public GameObject loadGameButton; 
    public GameObject settingGameButton; 
    public GameObject exitGameButton; 

    private void OnEnable() {
        if (ES3.KeyExists("wasSaved"))
            loadGameButton.SetActive(true);
        else 
            loadGameButton.SetActive(false);
    }

    private void OnDisable() {
        
    }

    public void OnSetting()
    {
        
    }

    public void OnQuit()
    {
        Application.Quit();
    }
}

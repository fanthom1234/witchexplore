using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneUtils : MonoBehaviour
{
    public Inventory playerInventory;

    public void ReloadCurrentScene()
    {
        Scene currentScene = SceneManager.GetActiveScene(); // Get the current scene
        SceneManager.LoadScene(currentScene.buildIndex); // Reload the current scene using its build index
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName, null));
    }

    public void LoadScene(string sceneName, Action OnLoadSceneDone)
    {
        StartCoroutine(LoadSceneAsync(sceneName, OnLoadSceneDone));
    }

    private IEnumerator LoadSceneAsync(string sceneName, Action OnLoadSceneDone)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        OnLoadSceneDone?.Invoke();
    }

    // Save/Load/New

    public void SaveGame()
    {
        if (playerInventory)
            ES3.Save("playerInventory", playerInventory.gameObject);

        if (GameManager.Instance != null)
            GameManager.Instance.SaveGame();
        else
            Debug.Log("No GameManager/ SaveGame");
    }

    public void LoadGame()
    {
        if (ES3.KeyExists("playerInventory"))
            ES3.Load("playerInventory", playerInventory.gameObject);

        if (GameManager.Instance != null)
            GameManager.Instance.LoadGame();
        else
            Debug.Log("No GameManager/ LoadGame");
    }

    public void NewGame()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.NewGame();
        else
            Debug.Log("No GameManager/ NewGame");
    }
}

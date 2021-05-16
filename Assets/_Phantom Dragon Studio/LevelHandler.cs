using Zenject;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelHandler : MonoBehaviour
{
    private AsyncOperation preLoadedScene;
    private Dictionary<int, string> scenes = new Dictionary<int, string>();
    private string scenePath;

    void Awake()
    { 
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            Debug.Log(scenePath);
            scenes.Add(i, scenePath);
        }
    }

    public void PreLoadLevel(int sceneIndex)
    {

        preLoadedScene = SceneManager.LoadSceneAsync(scenes[sceneIndex], LoadSceneMode.Additive);
        preLoadedScene.allowSceneActivation = true;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode.ToString());
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettingsManager : MonoBehaviour, IGameManager{

    LevelHandler _levelHandler;
    public GameObject inGameGUI;

    [SerializeField]
    public  GameSettings myGameSettings;
    public bool isGamePaused;

    [HideInInspector]
    public float defaultTimeScale, currentTimeScale;

    public GameSettings GameSettings => myGameSettings;
    public bool IsGamePaused => isGamePaused;

    public void Awake() {

        inGameGUI.SetActive(false);
        myGameSettings.GameSetup();
        _levelHandler = GetComponent<LevelHandler>();
        isGamePaused = false;
        defaultTimeScale = Time.timeScale;      //Cache the default timescale
        currentTimeScale = defaultTimeScale;    //Cache the timeScale we plan to alter for pausing the game.
    }

    public bool PauseGame()
    {
        if (isGamePaused == true) //start
        {
            Debug.Log("Resuming Game");
            isGamePaused = false;
            currentTimeScale = defaultTimeScale;
            Time.timeScale = defaultTimeScale;
            return isGamePaused;

        }
        else //stop
        {
            Debug.Log("Pausing game....");
            isGamePaused = true;
            currentTimeScale = 0f;
            Time.timeScale = currentTimeScale;
            return isGamePaused;
        }
    }
}

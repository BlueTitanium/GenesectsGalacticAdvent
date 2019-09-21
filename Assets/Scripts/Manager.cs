using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI; //Need this for calling UI scripts
using UnityEngine.SceneManagement;
using TMPro;


public class Manager : MonoBehaviour {

    [SerializeField]
    Transform UIPanel = null; //Will assign our panel to this variable so we can enable/disable it
    public TextMeshProUGUI time;
    public TextMeshProUGUI score;
    public TextMeshProUGUI multiplier;
    private int scoreVal = 0;
    private int multiplierVal = 1;
    private float seconds = 0;
    public GameObject player;
    bool isPaused; //Used to determine paused state

    void Start ()
    {
        UIPanel.gameObject.SetActive(false); //make sure our pause menu is disabled when scene starts
        isPaused = false; //make sure isPaused is always false when our scene opens
        time.SetText("Time: " + 0 + " seconds");
        score.SetText("Score: " + scoreVal);
        multiplier.SetText("Multiplier: x" + multiplierVal);
    }

    void Update ()
    {        
        //If player presses escape and game is not paused. Pause game. If game is paused and player presses escape, unpause.
        if(Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        Pause();
        else if(Input.GetKeyDown(KeyCode.Escape) && isPaused)
        UnPause();
        seconds += Time.deltaTime;
        time.SetText("Time: " + Math.Round(seconds,2));
        scoreVal = (int)player.GetComponent<PlayerController>().score;
        multiplierVal = (int)player.GetComponent<PlayerController>().multiplier;
        score.SetText("Score: " + scoreVal);
        multiplier.SetText("Multiplier: x" + multiplierVal);
    }

    public void Pause()
    {
        isPaused = true;
        UIPanel.gameObject.SetActive(true); //turn on the pause menu
        Time.timeScale = 0f; //pause the game
    }

    public void UnPause()
    {
        isPaused = false;
        UIPanel.gameObject.SetActive(false); //turn off pause menu
        Time.timeScale = 1f; //resume game
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }
}
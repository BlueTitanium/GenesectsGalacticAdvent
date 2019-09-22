using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI; //Need this for calling UI scripts
using UnityEngine.SceneManagement;
using TMPro;


public class Manager : MonoBehaviour {

    [SerializeField]
    Transform pauseScreen = null;
    [SerializeField]
    Transform VictoryScreen = null;
    [SerializeField]
    Transform TrueVictoryScreen = null; 
    [SerializeField]
    Transform CrashScreen = null;//Will assign our panel to this variable so we can enable/disable it
    public TextMeshProUGUI time;
    public TextMeshProUGUI score;
    public TextMeshProUGUI[] scoreDiffs;
    public TextMeshProUGUI multiplier;
    private long scoreVal = 0;
    private long multiplierVal = 1;
    private float seconds = 0;
    private long[] scoreDiffVals;
    public GameObject player;
    bool isPaused; //Used to determine paused state

    void Start ()
    {   
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        pauseScreen.gameObject.SetActive(false); //make sure our pause menu is disabled when scene starts
        VictoryScreen.gameObject.SetActive(false);
        TrueVictoryScreen.gameObject.SetActive(false);
        CrashScreen.gameObject.SetActive(false);
        isPaused = false; //make sure isPaused is always false when our scene opens
        time.SetText("Time: " + 0 + " seconds");
        score.SetText("Score: " + scoreVal);
        multiplier.SetText("Multiplier: x" + multiplierVal);
        for(long i = 0; i < scoreDiffs.Length; i++)
            scoreDiffs[i].SetText("");
        scoreDiffVals = new long[scoreDiffs.Length];
    }

    void Update ()
    {   

        //If player presses escape and game is not paused. Pause game. If game is paused and player presses escape, unpause.
        if(Input.GetKeyDown(KeyCode.Escape) && !isPaused && !player.GetComponent<PlayerController>().victory && !player.GetComponent<PlayerController>().truevictory && !player.GetComponent<PlayerController>().crashed)
        Pause();
        else if(Input.GetKeyDown(KeyCode.Escape) && isPaused && !player.GetComponent<PlayerController>().victory && !player.GetComponent<PlayerController>().truevictory && !player.GetComponent<PlayerController>().crashed)
        UnPause();
        if(player.GetComponent<PlayerController>().victory)
            dispVictory();
        if(!player.GetComponent<PlayerController>().victory && VictoryScreen.gameObject.activeSelf == true){
            VictoryScreen.gameObject.SetActive(false); //turn off pause menu
            Time.timeScale = 1f;
        }
        if(player.GetComponent<PlayerController>().truevictory)
            dispTrueVictory();
        if(!player.GetComponent<PlayerController>().truevictory && TrueVictoryScreen.gameObject.activeSelf == true){
            TrueVictoryScreen.gameObject.SetActive(false); //turn off pause menu
            Time.timeScale = 1f;
        }
        if(player.GetComponent<PlayerController>().crashed){
            dispCrashLanding();
        }
        if(!player.GetComponent<PlayerController>().crashed && CrashScreen.gameObject.activeSelf == true){
            CrashScreen.gameObject.SetActive(false); //turn off pause menu
            Time.timeScale = 1f;
        }
        seconds += Time.deltaTime;
        time.SetText("Time: " + Math.Round(seconds,2));
        long scoreTMP = scoreVal;
        scoreVal = (long)player.GetComponent<PlayerController>().score;
        scoreDiffVals[0] = scoreVal - scoreTMP;
        long counter = 0;
        if(scoreDiffVals[0] != 0){
            if(scoreDiffVals[0] > 0)scoreDiffs[counter].SetText("+" + scoreDiffVals[0]);
            if(scoreDiffVals[0] < 0)scoreDiffs[counter].SetText("" + scoreDiffVals[0]);
            scoreDiffs[counter].transform.rotation = Quaternion.Euler(scoreDiffs[counter].transform.eulerAngles.x,
                                                            scoreDiffs[counter].transform.eulerAngles.y,
                                                            UnityEngine.Random.Range(-30f,30f));
            scoreDiffs[counter].GetComponent<Animation>().Play();
            counter++;
            if(counter == scoreDiffs.Length)counter = 0;
        }
        multiplierVal = (long)player.GetComponent<PlayerController>().multiplier;
        if(scoreVal > -10000)
            score.SetText("Score: " + scoreVal);
        else score.SetText("Score: " + Math.Abs(scoreVal));
        multiplier.SetText("Multiplier: x" + multiplierVal);    
    }

    public void Pause()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        isPaused = true;
        pauseScreen.gameObject.SetActive(true); //turn on the pause menu
        Time.timeScale = 0f; //pause the game
    }

    public void UnPause()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        isPaused = false;
        pauseScreen.gameObject.SetActive(false); //turn off pause menu
        Time.timeScale = 1f; //resume game
    }
    public void dispVictory(){
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        VictoryScreen.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }
    public void dispTrueVictory(){
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        TrueVictoryScreen.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }
    public void dispCrashLanding(){
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        CrashScreen.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }
    public void QuitToMainScreen(){
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1f;
    }

    public void Restart()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene("Main");
        Time.timeScale = 1f;
    }
}
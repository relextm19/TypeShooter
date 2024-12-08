using System;
using UnityEngine;
using UnityEngine.UI;

public class StatsScreen : MonoBehaviour{
    public static StatsScreen instance;
    [SerializeField]
    private Text enemiesKilledText, scoreText, timeText, wordsTypedText, wordsPerMinuteText;

    void Start(){
        instance = this;
        gameObject.SetActive(false);
    }

    public void Setup(){
        EndScreen.instance.hide();
        setUpTexts();
        gameObject.SetActive(true);
    }

    public void setUpTexts(){
        enemiesKilledText.text = "Enemies Killed: " + GameController.instance.getEnemiesKilled();
        scoreText.text = "Score: " + GameController.instance.getScore();
        timeText.text = "Time: " + Math.Round(GameController.instance.getTime()) + "s";
        wordsTypedText.text = "Words Typed: " + GameController.instance.getWordsTyped();
        wordsPerMinuteText.text = "Words Per Minute: " + (int)(GameController.instance.getWordsTyped() / GameController.instance.getTime() * 60);
    }

    public void hide(){
        gameObject.SetActive(false);
    }

    public void showEndScreen(){
        gameObject.SetActive(false);
        EndScreen.instance.Setup();
    }
}

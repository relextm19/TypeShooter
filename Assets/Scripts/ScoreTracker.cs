using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTracker : MonoBehaviour{
    public static ScoreTracker instance;
    Text text;
    private int score = 0;

    void Awake(){
        instance = this;
        text = GetComponent<Text>();
    }

    void Start(){
        GameController.onScoreUpdate += UpdateScoreText;
    }

    void OnDestroy(){
        if (GameController.instance != null){
            GameController.onScoreUpdate -= UpdateScoreText;
        }
    }

    void UpdateScoreText(int newScore){
        score = newScore;
        text.text = score.ToString();
    }
}
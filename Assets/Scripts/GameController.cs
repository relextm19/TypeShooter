using UnityEngine;

public class GameController : MonoBehaviour{
    public static GameController instance;
    public delegate void OnScoreUpdate(int score);
    public static event OnScoreUpdate onScoreUpdate;
    private int score;
    private int enemiesKilled;
    private int wordsTyped;
    private static Vector2 screenBounds;

    void Awake(){
        instance = this;
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
    }

    public void addScore(int score){
        this.score += score;
        if(onScoreUpdate != null) onScoreUpdate(this.score);
    }
    public void addEnemyKill(){
        enemiesKilled++;
    }
    public void addWordTyped(){
        wordsTyped++;
    }
    
    public int getScore(){
        return score;
    }
    public int getEnemiesKilled(){
        return enemiesKilled;
    }
    public float getTime(){
        return Time.timeSinceLevelLoad;
    }
    public int getWordsTyped(){
        return wordsTyped;
    }
    public static Vector2 getScreenBounds(){
        return screenBounds;
    }
}
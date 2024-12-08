using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour{
    [SerializeField]
    private Text scoreText;
    public static EndScreen instance;
    void Start(){
        instance = this;
        hide();
    }
    public void Setup(){
        StatsScreen.instance.hide();
        gameObject.SetActive(true);
        scoreText.text = "Score: " + GameController.instance.getScore();
    }

    public void showStats(){
        hide();
        StatsScreen.instance.Setup();
    }

    public void hide(){
        gameObject.SetActive(false);
    }

    public void Restart(){
        SceneManager.LoadScene("MainScene");
    }
}

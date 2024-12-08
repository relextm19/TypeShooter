using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour, IDemagable{
    [SerializeField]
    private int health = 3;
    private Image[] hearts;
    void Start(){
        hearts = GetComponentsInChildren<Image>();
    }
    [ContextMenu("Take Damage")]
    public void TakeDamageFromContextMenu(){
        takeDamage(1); 
    }

    public void takeDamage(int amount){
        health--;
        if(health <= 0){
            endGame();
        }
        for(int i = 0; i < hearts.Length; i++){
            if(i < health){
                hearts[i].enabled = true;
            } else{
                hearts[i].enabled = false;
            }
        }        
    }
    private void endGame(){
        EndScreen.instance.Setup();
        health = 0;
        GameObject enemySpawner = GameObject.Find("EnemySpawner");
        if (enemySpawner != null) {
            foreach(GameObject enemy in EnemySpawner.instance.getEnemyDict().Values){
                if (enemy != null) {
                    enemy.GetComponent<Enemy>().stop();
                }
            }
            Destroy(enemySpawner);
        }
    }
    [ContextMenu("Whole lotta health")]
    private void WholeLottaHealth(){
        health = 999;
    }
}

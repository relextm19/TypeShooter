using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour{
    private GameObject enemyPrefab;
    private Dictionary<int, GameObject> enemyDict;
    public static EnemySpawner instance;
    private float screenTop, screenBottom, screenRight;
    private static int spawnCount = 0;
    private float timer = 0;
    [SerializeField]
    private float spawnDelay = 3;
    void Start(){
        enemyPrefab = Resources.Load<GameObject>("Prefabs/Enemy");
        enemyDict = new Dictionary<int, GameObject>();
        Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        screenTop = screenBounds.y;
        screenBottom = -screenBounds.y;
        screenRight = screenBounds.x;
        instance = this;
    }

    void Update(){
        if(spawnDelay > 0.5f) spawnDelay -= Time.deltaTime * 0.05f;  
    
        if(timer > spawnDelay){
            spawnEnemy();
            timer = 0;
        } else{
            timer += Time.deltaTime;
        }
    }

    [ContextMenu("Spawn Enemy")]
    void spawnEnemy(){
        float y = Random.Range(screenBottom, screenTop);
        float x = screenRight;
        Vector3 position = new Vector3(x, y, 1);
        enemyDict.Add(++spawnCount, Instantiate(enemyPrefab, position, Quaternion.identity));
        Debug.Log(enemyDict.Count);
    }

    public static int getID(){
        return spawnCount;
    }

    public Transform getClosestEnemyTransform() {
        foreach (var enemy in enemyDict.Values) {
            if (enemy == null) continue;
            return enemy.transform;
        }
        return null;
    }

    
    public void removeEnemy(int ID){
        if(enemyDict.ContainsKey(ID)){
            enemyDict.Remove(ID);
        }
    }
    public Dictionary<int, GameObject> getEnemyDict(){
        return enemyDict;
    }
}

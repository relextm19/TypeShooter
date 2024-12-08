using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour{
    private EnemySpawner enemySpawner;
    private Transform target;
    private GameObject bulletPrefab;
    private AudioSource audioSource;
    private float angle;
    private Vector3 direction;
    [SerializeField]
    private float fireRate = 0.5f;
    private float timer = 0;
    public static Gun Instance;
    private int bullets = 0;
    public event Action<int> OnBulletCountChanged;
    private float spriteWidth;
    [SerializeField]
    private float yOffset = 0;

    void Awake(){
        enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");
        audioSource = GetComponent<AudioSource>();
        Instance = this;
        spriteWidth = GetComponent<SpriteRenderer>().bounds.extents.x;
    }

    void FixedUpdate(){
        timer += Time.fixedDeltaTime;

        rotate();
        fire();
    }

    private void fire(){
        if(timer > fireRate && target && bullets > 0){
            audioSource.Play();
            Instantiate(bulletPrefab, new Vector3(transform.position.x + spriteWidth / 2, transform.position.y + yOffset, transform.position.z), Quaternion.identity);
            timer = 0;
            bullets--;
            OnBulletCountChanged?.Invoke(bullets);
        }
    }

    private void rotate(){
        target = enemySpawner.getClosestEnemyTransform();
        if(target){
            direction = target.position - transform.position;
            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    public float getAngle(){
        return angle;
    }
 
    public Vector3 getDirection(){
        return direction;
    }

    public void addBullets(int amount){
        bullets += amount;
        OnBulletCountChanged?.Invoke(bullets);
    }

    public int getBullets(){
        return bullets;
    }   
    [ContextMenu("Whole lotta bullets")]
    private void godMode(){
        bullets = 999;
    }
}

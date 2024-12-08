using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IAttacker, IDemagable{
    [SerializeField]
    private float speed;
    [SerializeField]
    private int health = 1;
    private int attackDamage = 1;
    private int score = 1;
    private Rigidbody2D rb;
    private float width;
    private float timer = 0;
    private float minDelay = 0.5f, maxDelay = 1.5f;
    private float swingDelay = 1;
    private HealthBar HealthBar;
    private EnemySpawner enemySpawnerInstance;
    private bool isStopped = false;
    private int instanceID;
    void Awake(){
        rb = GetComponent<Rigidbody2D>();
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        rb.velocity = new Vector2(speed, 0);
        HealthBar = GameObject.Find("HealthBar").GetComponent<HealthBar>();
        enemySpawnerInstance = EnemySpawner.instance;
        width = sr.bounds.extents.x;
        instanceID = EnemySpawner.getID();
    }

    void FixedUpdate(){
        if(isStopped) return;
        move();
        outOfBoundsBehaviour();
    }

    void move(){
        if(timer > swingDelay){
            swing();
            timer = 0;       
        } else{
            timer += Time.fixedDeltaTime;
            swingDelay = UnityEngine.Random.Range(minDelay, maxDelay);
        }
    }

    void swing(){
        float r = UnityEngine.Random.Range(0, 2);
        // 0 = up, 1 = down
        if(r == 0) rb.velocity = new Vector2(rb.velocity.x, speed);
        else if (r == 1) rb.velocity = new Vector2(rb.velocity.x, -speed); 
    }

    void outOfBoundsBehaviour(){
        if (transform.position.y >= GameController.getScreenBounds().y || transform.position.y <= -GameController.getScreenBounds().y){ 
            rb.velocity = new Vector2(rb.velocity.x, -rb.velocity.y);
        } if(transform.position.x < -GameController.getScreenBounds().x - width){
            attack(HealthBar);
            takeDamage(health);
        }
    }

    public void attack(IDemagable target){
        target.takeDamage(attackDamage);
    }
    public void takeDamage(int damage){
        health -= damage;
        if(health <= 0){
            GameController.instance.addScore(score);
            GameController.instance.addEnemyKill();
            enemySpawnerInstance.removeEnemy(instanceID);
            Destroy(gameObject);
        }
    }
    public void stop(){
        rb.velocity = Vector2.zero;
        isStopped = true;
    }
}

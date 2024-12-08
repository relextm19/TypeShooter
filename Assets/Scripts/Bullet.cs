using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IAttacker{
    [SerializeField]
    private float speed = 40;
    private Rigidbody2D rb;
    private int damage = 1;
    private Gun gun;
    void Awake(){
        gun = GameObject.Find("Gun").GetComponent<Gun>();
        rb = GetComponent<Rigidbody2D>();
        fire();
    }

    void Update(){
        if(transform.position.x > 15 || transform.position.x < -15 || transform.position.y > 10 || transform.position.y < -10){
            Destroy(gameObject);
        }
    }

    void fire(){
        transform.rotation = Quaternion.Euler(0, 0, gun.getAngle() - 90);
        rb.velocity = gun.getDirection().normalized * speed;
    }

    void OnTriggerEnter2D(Collider2D colider){
        IDemagable target = colider.GetComponent<IDemagable>();
        if(target != null){
            attack(target);
            Destroy(gameObject);
        }
    }
    public void attack(IDemagable target){
        target.takeDamage(damage);
    }
}

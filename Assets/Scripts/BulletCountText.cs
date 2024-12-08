using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletCountText : MonoBehaviour{
    private Text text;
    private AudioSource audioSource;
    void Start(){
        text = GetComponent<Text>();
        audioSource = GetComponent<AudioSource>();
        Gun.Instance.OnBulletCountChanged += UpdateBulletCount;
        text.color = Color.red;
        text.text = Gun.Instance.getBullets().ToString();
    }

    void UpdateBulletCount(int count){
        text.text = count.ToString();
        if(count == 0){
            text.color = Color.red;
            audioSource.Play();
        } else {
            text.color = Color.white;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anim : MonoBehaviour {
    public Animator ani;
    public GameObject tmp;
    AudioSource gunFireAudio;           //放置槍聲的容器

    // Use this for initialization
    void Start()
    {
        //取得物件身上的音源
        gunFireAudio = GetComponent<AudioSource>();
        //InvokeRepeating("AutoShooting",1,shootFrequency); //重複射擊，自動射擊 = O
    }
    // Update is called once per frame
    void Update() {
          ani.SetInteger("attack_r", 0);
        if (Input.GetKeyDown("r"))
        {
            gunFireAudio.Stop();
            gunFireAudio.Play();
            ani.SetInteger("attack_r", 1);
        }
   
    }
}

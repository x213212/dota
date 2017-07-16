using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_hp : MonoBehaviour {
    public float MaxHP;
    public float HP;
    public Animator ani;
    // Use this for initialization
    void Start () {
 
        MaxHP = 30000;
        HP = 30000;
        ani.SetFloat("MAXHP", MaxHP);
        ani.SetFloat("HP", HP);
    }
	
	// Update is called once per frame
	void Update () {
        if (!ani)
            return;
        MaxHP=ani.GetFloat("MAXHP");
        HP=ani.GetFloat("HP");
      
        this.transform.localPosition = new Vector3(-1*(-2f+ 2f * (HP / MaxHP)), 0.0f, 0.0f);
	}
}

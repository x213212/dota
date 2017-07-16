using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hp_face : MonoBehaviour {
    public Transform playpos;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        playpos = GameObject.Find("Main Camera").GetComponent<Transform>();
        if (!playpos)
            return;
        this.transform.LookAt(playpos.position);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_health : MonoBehaviour {
    public GameObject tmp;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 targetv = Camera.main.WorldToScreenPoint(tmp.transform.position);
        GetComponent<RectTransform>().position = targetv;
	}
}

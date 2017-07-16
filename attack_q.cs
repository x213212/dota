using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack_q : MonoBehaviour {
    public GameObject tmp;
    GameObject tmp2;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0))
        {
            tmp2 = (GameObject)Instantiate(tmp, transform.position, transform.rotation);
            Destroy(tmp2.gameObject, 2);
        }

    }
}

   

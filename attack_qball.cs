using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class attack_qball : NetworkBehaviour
{
    public float speed;
    public List<GameObject> tmp;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


        transform.Translate(speed * Time.deltaTime, 0, 0);
        //  Destroy(gameObject,2);
    }

}

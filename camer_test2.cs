using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camer_test2 : MonoBehaviour {


    public GameObject bloodBar;//获取血条信息。
    public GameObject MainCamera;//获取主摄像机  
    public Vector3 offset;  //血条和人物坐标的差值,也就是血条在人物的上中方...之类的.这样理解

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //让血条信息一直处于人物的头顶某处
       // bloodBar.transform.position = transform.position + offset;

        Vector3 relativePos = transform.position - MainCamera.transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        bloodBar.transform.rotation = rotation;
        bloodBar.transform.LookAt(MainCamera.transform.position);
    }

}

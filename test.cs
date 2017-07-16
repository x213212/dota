using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public float ShotSpeed = 10;
    private float time = 1;//代表从A点出发到B经过的时长
    public Transform pointA;//点A
    public Transform pointB;//点B
    public Transform pointC;//点B
    public Transform Lookatwho;//点B
    public GameObject TEST;
    public float g = -10;//重力加速度
    // Use this for initialization
    private Vector3 speed;//初速度向量
    private Vector3 Gravity;//重力向量
    public bool enable = false;
    private Vector3 currentAngle;
    public int hight = 0;
    void Start()
    {



    }
    void Update()
    {
       
            if (enable == false)
        {

            Lookatwho.transform.GetComponent<CapsuleCollider>().enabled = false;
            dTime = 0;
            TEST = null;
            TEST = new GameObject("Cool GameObject made from Code");
            //Add Components
            pointA.LookAt(Lookatwho);
            TEST.transform.position = pointA.position;
            TEST.transform.rotation = pointA.rotation;

            float X = (transform.position.x < Lookatwho.position.x) ? X = transform.position.x - 5 : X = transform.position.x + 5;
            float Z = (transform.position.z < Lookatwho.position.z) ?Z = transform.position.z - 5 : Z = transform.position.z + 5;

            TEST.transform.position = new Vector3 (X, Lookatwho.position.y+5, Z);
            pointB = TEST.transform;
            enable = true;
        
      
            time = Vector3.Distance(pointA.position, pointB.position) / ShotSpeed;
            transform.position = pointA.position;//将物体置于A点
                                                 //通过一个式子计算初速度
            speed = new Vector3((pointB.position.x - pointA.position.x) / time,
                (pointB.position.y - pointA.position.y) / time - 0.5f * g * time, (pointB.position.z - pointA.position.z) / time);
            Gravity = Vector3.zero;//重力初始速度为0
       
        }

    }
    private float dTime = 0;
    // Update is called once per frame
    void FixedUpdate()
    {
    
        if (transform.position.y > -1.8) {

            pointA.LookAt(Lookatwho);
            Gravity.y = g * (dTime += Time.fixedDeltaTime);//v=at
                                                       //模拟位移

        transform.position += (speed + Gravity) * Time.fixedDeltaTime;

        currentAngle.x = -Mathf.Atan((speed.y + Gravity.y) / speed.z) * Mathf.Rad2Deg;

        transform.eulerAngles = currentAngle; }
        else
        {
            transform.position += Vector3.up * 1.0f;
            transform.rotation = Quaternion.Euler(0f, transform.rotation.y, transform.rotation.z);
            GetComponent<test>().enabled = false;
            GetComponent<test>().pointB = null;
            GetComponent<test>().pointA = null;
            GetComponent<test>().Lookatwho = null;
           
            enable = false;
            Destroy(TEST);
            Lookatwho.transform.GetComponent<CapsuleCollider>().enabled =true;

            //Debug.Log("okrddddddddddd");
        }


    }
}
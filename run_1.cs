using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
public class run_1 : NetworkBehaviour
{
    public Animator ani;
    public Transform tmp;

    GameObject ball_tmp;

    public Rigidbody rb;
    public  int waring = 0;
    public int q_ball = 0;
    //人物的三个状态 站立、行走、奔跑
    private const int HERO_IDLE = 0;
    private const int HERO_WALK = 1;
    private const int HERO_RUN = 2;
    float z;
    //记录当前人物的状态
    private int gameState = 0;

    //记录鼠标点击的3D坐标点
    private Vector3 point;
    private float time;
    AudioSource gunFireAudio;
    public ParticleSystem impactEffect; //用來放置撞擊的容器
    public ParticleSystem impactEffect2;	//用來放置撞擊的容器
    private object retrun;

    void Start()
    {
     rb = GetComponent<Rigidbody>(); 
        // rb = GetComponent<Rigidbody>();
        gunFireAudio = GetComponent<AudioSource>();
        //初始设置人物为站立状态
        SetGameState(HERO_IDLE);
        ani.SetInteger("now", 0);
      

    }

    void Update()
    {
        Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit2;
        if (Physics.Raycast(ray2, out hit2))
        {
            if (Input.GetKeyDown("f")) {
                point = hit2.point;
                if (Mathf.Abs(Vector3.Distance(point, transform.position)) >= 1.3f)
            {
                    /*r閃作弊*/
                    Vector3 tmp2;
                    tmp2 = transform.position;
                    transform.position = point;
                    transform.LookAt(tmp2);
    
                   //   impactEffect2.transform.position = new Vector3(transform.position.x,0f, transform.position.z);
                   impactEffect2.transform.position =tmp2;
                    impactEffect2.Stop();
                    impactEffect2.Play();
                    AudioClip tmp = (AudioClip)Resources.Load("Audio/flash");
                    gunFireAudio.clip = tmp;
                    gunFireAudio.Stop();
                    gunFireAudio.Play();
                    return;
            } }
        }

            if (transform.position.y < -1.8)
        {
            transform.position += Vector3.up * 0.1f;
        }
        if (this.ani.GetCurrentAnimatorStateInfo(0).IsName("Unarmed-Attack-Kick-R1"))
        {
            ani.SetInteger("attack_r", 0);
            GetComponent<CapsuleCollider>().height = 2;
            GetComponent<CapsuleCollider>().center = new Vector3(0.0f, 2.0f, 0.0f);
            GetComponent<CapsuleCollider>().radius = 1;
            GetComponent<CapsuleCollider>().direction = 1;

        }
        if (Input.GetKeyDown("r")&& ani.GetInteger("attack_r")==0)
        {
            if (Time.realtimeSinceStartup - time>=0.5f)
            {
                GetComponent<CapsuleCollider>().height = 8;
                GetComponent<CapsuleCollider>().center = new Vector3(0.0f, 2.0f, 2.0f);
                GetComponent<CapsuleCollider>().direction = 2;
                GetComponent<CapsuleCollider>().radius = 3;
                AudioClip tmp = (AudioClip)Resources.Load("Audio/Lee_Sin");
                gunFireAudio.clip = tmp;
                gunFireAudio.Stop();
                gunFireAudio.Play();
                ani.SetInteger("attack_r", 1);
            }
            time = Time.realtimeSinceStartup;
        }
        if (Input.GetKeyDown("v"))
        {
            waring = 1;
 
        }
        if (Input.GetKeyUp("v"))
        {
            waring = 0;

        }
        if (Input.GetKeyDown("q"))
        {
            q_ball = 1;

        }
        if (Input.GetKeyUp("q"))
        {
            q_ball = 0;

        }

        //按下鼠标左键后
        if (Input.GetMouseButtonDown(0))
        {
           // transform.localEulerAngles = new Vector3(0, 0, 0);
            //从摄像机的原点向鼠标点击的对象身上设法一条射线
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            //当射线彭转到对象时
            if (Physics.Raycast(ray, out hit))
            {
                //目前场景中只有地形
                //其实应当在判断一下当前射线碰撞到的对象是否为地形。

                //得到在3D世界中点击的坐标
                point = hit.point;
                if (waring == 1)
                {
                    AudioClip tmp = (AudioClip)Resources.Load("Audio/Ping");
                    gunFireAudio.clip = tmp;
                    gunFireAudio.Stop();
                    gunFireAudio.Play();
                    if (Mathf.Abs(Vector3.Distance(point, transform.position)) >= 1.3f)
                    {
                        impactEffect2.transform.position = point;
                    }
                    // impactEffect.transform.rotation = Quaternion.Euler(270, 0, 0);
                    impactEffect2.Stop();
                    impactEffect2.Play();
                    return;
                }
                if (q_ball == 1)
                {
                    AudioClip tmp = (AudioClip)Resources.Load("Audio/q_fly");
                    gunFireAudio.clip = tmp;
                    gunFireAudio.Stop();
                    gunFireAudio.Play();
                    if (Mathf.Abs(Vector3.Distance(point, transform.position)) >= 1.3f)
                    {
                        GameObject test = new GameObject("test");
                        test.transform.position = point;
                        test.transform.LookAt(transform);
                        Quaternion rotation = Quaternion.Euler(0f, 90f, 0f) * test.transform.rotation;

                        CmdSpawnCannonball(test, rotation);

                        Destroy(test);
               
                    }
                    // impactEffect.transform.rotation = Quaternion.Euler(270, 0, 0);
               
                    return;
                }


                //设置主角面朝这个点，主角的X 与 Z轴不应当发生旋转，   
                //注解1

                // impactEffect.transform.rotation = Quaternion.Euler(270, 0, 0);

                //
                transform.LookAt(new Vector3(point.x, transform.position.y, point.z));
                // transform.LookAt(new Vector3(point.x, transform.position.y, point.z));
                SetGameState(HERO_RUN);
        
             

                //用户是否连续点击按钮

                //连续点击 进入奔跑状态




                //记录本地点击鼠标的时间
                time = Time.realtimeSinceStartup;
            }
        }
    }

    void FixedUpdate()
    {

        switch (gameState)
        {
            case HERO_IDLE:
                ani.SetInteger("now", 0);
                break;
        

            case HERO_RUN:
                ani.SetInteger("now", 2);
                //奔跑时移动的长度为0.1
                Move(0.1f);
                break;
        }
        /*
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement);*/
    }

    void SetGameState(int state)
    {
        switch (state)
        {
            case HERO_IDLE:
                //播放站立动画
                ani.SetInteger("now", 0);
                point = transform.position;
         
                //  animation.Play("idle");
                break;
            case HERO_WALK:
                //播放行走动画
                // animation.Play("walk");
                Debug.Log("w");
                break;
            case HERO_RUN:
                //播放奔跑动画
                Debug.Log("w");
                ani.SetInteger("now", 2);
                // animation.Play("run");
                break;
        }
        gameState = state;
    }

    void Move(float speed)
    {

        //注解2
        //主角没到达目标点时，一直向该点移动
        if (Mathf.Abs(Vector3.Distance(point, transform.position)) >= 1.3f)
        {
            //  transform.LookAt(tmp);
            impactEffect.transform.position = point;
            // impactEffect.transform.rotation = Quaternion.Euler(270, 0, 0);
            impactEffect.Stop();
            impactEffect.Play();
            //
            if (ani.GetInteger("attack_r") == 1) {
                ani.SetInteger("now", 0);
                //到达目标时 继续保持站立状态。
                SetGameState(HERO_IDLE);
            }
            ani.SetInteger("now", 2);
            //得到角色控制器组件
            /*
            CharacterController controller = GetComponent<CharacterController>();
            //注解3 限制移动
            Vector3 v = Vector3.ClampMagnitude(point - transform.position, speed);
            //可以理解为主角行走或奔跑了一步
            controller.Move(v);*/
        }
        else
        {
            ani.SetInteger("now",0);
            //到达目标时 继续保持站立状态。
            SetGameState(HERO_IDLE);
        }
    }
    [Command]
    void CmdSpawnCannonball(GameObject tmp, Quaternion rotation)
    {
        //we instantiate one from Resources
  
        ball_tmp = (GameObject)Instantiate(Resources.Load("qball"), new Vector3(transform.position.x, transform.position.y, transform.position.z), rotation);

        //
        NetworkServer.Spawn(ball_tmp);
    }
}
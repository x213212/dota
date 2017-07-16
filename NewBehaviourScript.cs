using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Controller : MonoBehaviour
{
    public Animator ani;
    //人物的三个状态 站立、行走、奔跑
    private const int HERO_IDLE = 0;
    private const int HERO_WALK = 1;
    private const int HERO_RUN = 2;

    //记录当前人物的状态
    private int gameState = 0;

    //记录鼠标点击的3D坐标点
    private Vector3 point;
    private float time;

    void Start()
    {
        //初始设置人物为站立状态
        SetGameState(HERO_IDLE);

    }

    void Update()
    {
        //按下鼠标左键后
        if (Input.GetMouseButtonDown(0))
        {
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

                //设置主角面朝这个点，主角的X 与 Z轴不应当发生旋转，
                //注解1
                transform.LookAt(new Vector3(point.x, transform.position.y, point.z));

                //用户是否连续点击按钮
                if (Time.realtimeSinceStartup - time <= 0.2f)
                {
                    //连续点击 进入奔跑状态
                    SetGameState(HERO_RUN);
                }
                else
                {
                    //点击一次只进入走路状态
                    SetGameState(HERO_WALK);
                }

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

                break;
            case HERO_WALK:
                //移动主角 一次移动长度为0.05
                Move(0.05f);
                break;

            case HERO_RUN:
                //奔跑时移动的长度为0.1
                Move(0.1f);
                break;
        }

    }

    void SetGameState(int state)
    {
        switch (state)
        {
            case HERO_IDLE:
                //播放站立动画
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
            transform.localEulerAngles = new Vector3(0, 0, 0);
            //得到角色控制器组件
            CharacterController controller = GetComponent<CharacterController>();
            //注解3 限制移动
            Vector3 v = Vector3.ClampMagnitude(point - transform.position, speed);
            //可以理解为主角行走或奔跑了一步
            controller.Move(v);
        }
        else
        {
            //到达目标时 继续保持站立状态。
            SetGameState(HERO_IDLE);
        }
    }

}
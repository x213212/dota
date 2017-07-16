using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_touch : MonoBehaviour {
    public float numberOfLives = 3;   //設定塔的血有多少
    public Animator ani;
    public Rigidbody rb;
    float currentLives;               //目前血量
    string tmp;
    AudioSource damageAudio;        //音效
    bool alive = true;				//生或死
    void Start()
    {
        numberOfLives = ani.GetFloat("HP");
    }
    void Awake()
    {
        currentLives = numberOfLives;
        damageAudio = GetComponent<AudioSource>();
    }
    void FixedUpdate()
    {
     
        //Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
       // GetComponent<Rigidbody>().AddForce(input);
    }
    void OnTriggerEnter(Collider other)
    {
  
        Debug.Log(other.tag);
        if (other.tag.IndexOf("Enemyplayer") >= 0)
        {
            Debug.Log(other.tag+"arrack_r");
            if (other.GetComponent<Animator>().GetInteger("attack_r") == 1) {

            
                GetComponent<test>().enabled = true;
                GetComponent<test>().pointA = this.transform;
                GetComponent<test>().Lookatwho = other.transform;
            

                numberOfLives = ani.GetFloat("HP");

            numberOfLives -= 500;
            ani.SetFloat("HP", numberOfLives);
       
          
            }
            /*
            for(int i=0;i<5;i++)
            transform.position += transform.up* Time.deltaTime * 1;*/
       
        }
        //當敵人碰到塔就受傷
        if (other.tag != "Enemy" || !alive)
            return;
      
        // Vector3 temp = new Vector3(0, gameObject.transform.position.y+7, 0);

        //  other.gameObject.transform.position = temp;
        //Destroy(other.gameObject);
        /*
        currentLives -= 1;
		damageAudio.Play();

		//如果沒血了
		if(currentLives <= 0)
		{
			//死亡重生3秒
			alive = false;
            if (damageImage)
            {
                Color col = damageImage.color;
                col.a = 1f;
                damageImage.color = col;
            }

			//重新開始
			Invoke("Restart", 3f);
		}
        */
    }

    void Restart()
    {
        //重置畫面
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length; i++)
            Destroy(enemies[i]);

        currentLives = numberOfLives;
        alive = true;
        /*
        if (damageImage)
        {
            Color col = damageImage.color;
            col.a = 0f;
            damageImage.color = col;
        }*/
    }
}

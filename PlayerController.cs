using UnityEngine;
using UnityEngine.Networking;


public class PlayerController:NetworkBehaviour
{
    static int count=0;
	private run_1 playerCameraTransform;
	private Camera playerCamera;
	private AudioListener playerAudioListener;


    void Start()
	{
        if (isLocalPlayer)
        {
            gameObject.name = "ME";
            gameObject.tag = "Player";
            //gameObject.GetComponent<CharacterController>().enabled = false;
        }
        else
        {
            gameObject.name = "other" + count;
            gameObject.tag = "Enemyplayer";
        }

        //當角色被產生出來時，如果不是Local Player就把所有的控制項目關閉，這些角色的位置資料將由Server來同步
        /*
		fpsController = GetComponent<FirstPersonController>(); 
        //.GetComponent<ScriptA>().enabled = true / false
        ./
    
   
        playerCamera = playerCameraTransform.GetComponent<Camera>();
		playerAudioListener = playerCameraTransform.GetComponent<AudioListener>();
        */
        for (int i = 0; i < count+1; i++)
        {
                playerAudioListener = GameObject.Find("other" + i).GetComponent<AudioListener>();

               // playercher = GameObject.Find("other" + i).GetComponent<CharacterController>();
                playerCameraTransform = GameObject.Find("other" + i).GetComponent<run_1>();

            if (playerCameraTransform)
            {
                playerCameraTransform.enabled = false;
            }
     
            if (playerAudioListener)
            {
                playerAudioListener.enabled = false;
            }
        }
   
        count += 1;

    }
}


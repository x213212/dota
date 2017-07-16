using UnityEngine;

public class CameraEditorControl : MonoBehaviour
{

	public float speed = 5f;
    private float x;
    public Transform tmp;
    //偵測滑鼠移動反映到畫面
    void Update()
    {
     


        float horizontal = Input.GetAxis("Mouse X") * speed;
		float vertical = Input.GetAxis("Mouse Y") * speed;
      
        transform.Rotate(0f, horizontal, vertical, Space.Self);



    }

}

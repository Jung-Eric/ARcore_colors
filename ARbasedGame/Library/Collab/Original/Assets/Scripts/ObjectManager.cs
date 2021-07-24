using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public Camera AngleC;
    public GameObject player;
    public GameObject[] colliders;

    //유저의 위치에 따라 angle을 적용한다.
    public int player_pos;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Vector2 pos = Input.GetTouch(0).position;
            Vector3 touch = new Vector3(pos.x, pos.y, 0.0f);

            Ray ray = Camera.main.ScreenPointToRay(touch);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.gameObject.tag == "object")
                    Debug.Log(hit.collider.gameObject);
            }
        }
    

    }

    public void angle_change(float a)
    {
        var camera_script = GameObject.Find("Main Camera").GetComponent<Ingame_camera>();
        camera_script.angle = a;
    }
}
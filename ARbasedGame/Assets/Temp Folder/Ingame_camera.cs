using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingame_camera : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update

    //유저를 추적한다.
    private Vector3 distance;
    private Vector3 distance_mod;

    //X,Z 중간연산
    private float X_cal;
    private float Z_cal;

    //코사인 사인 저장
    private float C_imp;
    private float S_imp;

    //유저와 같은 현재 위치 인수를 가진다.
    public int now_world;

    //카메라 회전 정도를 결정한다.
    public float angle;
    
    //오브젝트 회전 각도
    Quaternion Rotation_imp;

    void Start()
    {
        distance = transform.position - player.transform.position;
    }

    // Update is called once per frame
    
    void Update()
    {
        if (now_world == 0)
        {
            angle = 0;
        }
        else if (now_world == 1)
        {
            angle = 180;
        }
        else if (now_world == 2)
        {
            angle = 270;
        }
        else if (now_world == 3)
        {
            angle = 190;
        }
        else if (now_world == 4)
        {
            angle = 260;
        }
        else if (now_world == 5)
        {
            angle = 325;
        }
        else if (now_world == 6)
        {
            angle = 250;
        }
        else if (now_world == 7)
        {
            angle = 30;
        }
    }
    

    private void LateUpdate()
    {
        C_imp = Mathf.Cos(-angle * 3.14f / 180);
        S_imp = Mathf.Sin(-angle * 3.14f / 180);

        X_cal = C_imp * distance.x - S_imp * distance.z;
        Z_cal = S_imp * distance.x + C_imp * distance.z;

        distance_mod = new Vector3(X_cal, distance.y, Z_cal);

        Rotation_imp = Quaternion.Euler(new Vector3(30,angle,0));

        //회전 처리
        transform.localRotation = Quaternion.Lerp(transform.rotation, Rotation_imp, 3.0f * Time.deltaTime);

        //이동 처리
        transform.position = Vector3.Lerp(transform.position, player.transform.position+ distance_mod, 3.0f * Time.deltaTime);


        //transform.position = Vector3.Lerp(transform.position, player.transform.position, 2f * Time.deltaTime)+ new Vector3(3, 5, -30);
        //transform.position = player.transform.position + distanse;
        //transform.position = Vector3.Lerp(player.transform.position, transform.position, 2f*Time.deltaTime);
    }

}

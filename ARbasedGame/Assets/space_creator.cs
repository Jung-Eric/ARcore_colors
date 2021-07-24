using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class space_creator : MonoBehaviour
{
    int mode;

    //메테오는 8종이 랜덤하게 떨어진다.
    public GameObject[] meteors;


    public GameObject circle1;
    public GameObject circle2;

    //이벤트 발생 주기
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        mode = 0;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer = timer + Time.deltaTime;
        if(timer >= 0.5)
        {
            summon_meteors();
        }

        /*
        Random rand = new Random();

        //8종의 운석 중 랜덤하게 5개를 고른다.
        int imp = Random.Range(0, 8);
        */
    }


    void create_space_1()
    {

    }

    void summon_meteors()
    {
        int avail = 1;
        //소환된 위치 선정
        //발사될 각도 선정
        int pos_x = Random.Range(-100, 100);
        int pos_y = Random.Range(-100, 100);
        int pos_z = Random.Range(-100, 100);

        if (pos_x < 20 && pos_x > -20) { avail = -1; }
        if (pos_y < 20 && pos_y > -20) { avail = -1; }
        if (pos_z < 20 && pos_z > -20) { avail = -1; }

        Vector3 pos = new Vector3(pos_x, pos_y, pos_z);


        int rot_x = Random.Range(0, 360);
        int rot_y = Random.Range(0, 360);
        int rot_z = Random.Range(0, 360);
        Vector3 rot = new Vector3(rot_x, rot_y, rot_z);

        if (avail == 1)
        {
            GameObject circle1_c = Instantiate(circle1) as GameObject;
            circle1_c.transform.position = pos;
            circle1_c.transform.eulerAngles = rot;

            Destroy(circle1_c, 3);
        }
    }

    void summon_balls()
    {
        //Instantiate(meteor1);
        //Destroy(meteor1, 10);
    }



}

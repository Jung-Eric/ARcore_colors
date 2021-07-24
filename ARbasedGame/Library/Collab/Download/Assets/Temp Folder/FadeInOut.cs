using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOut : MonoBehaviour
{
    public GameObject circular;
    public GameObject add_box;
    //public GameObject Box;
    // Start is called before the first frame update

    private GameObject circular_mask;
    private GameObject add_box_t;
    private GameObject add_box_b;
    private GameObject add_box_r;
    private GameObject add_box_l;

    new Vector3 local_pos;

    private float impli_num;
    private float scaling;
    private float scaling_num;

    public int mode;

    void Start()
    {
        scaling = 0;
        scaling_num = 0.8f;
        Fade_base();
        //mode = 0;
        //FadeOut_work();
    }

    // Update is called once per frame
    void Update()
    {
        if (mode == 1)
        {
            FadeOut_work();
        }
        else if (mode == 2)
        {
            FadeIn_work();
        }
    }

    /*
    public void FadeIn()
    {

    }
    */

    public void Fade_base()
    {
        circular_mask = Instantiate(circular, new Vector3(0, 0, 0), transform.rotation) as GameObject;
        add_box_t = Instantiate(add_box, new Vector3(0, 300, 0), transform.rotation) as GameObject;
        add_box_b = Instantiate(add_box, new Vector3(0, -300, 0), transform.rotation) as GameObject;
        add_box_l = Instantiate(add_box, new Vector3(-300, 0, 0), transform.rotation) as GameObject;
        add_box_r = Instantiate(add_box, new Vector3(300, 0, 0), transform.rotation) as GameObject;


        circular_mask.transform.parent = gameObject.transform;
        add_box_t.transform.parent = gameObject.transform;
        add_box_b.transform.parent = gameObject.transform;
        add_box_r.transform.parent = gameObject.transform;
        add_box_l.transform.parent = gameObject.transform;

        local_pos = gameObject.transform.position;
        impli_num = 300 * 2.773f;

        circular_mask.transform.position = new Vector3(local_pos.x, local_pos.y, 0);
        add_box_t.transform.position = new Vector3(local_pos.x, local_pos.y + impli_num, 0);
        add_box_b.transform.position = new Vector3(local_pos.x, local_pos.y - impli_num, 0);
        add_box_l.transform.position = new Vector3(local_pos.x - impli_num, local_pos.y, 0);
        add_box_r.transform.position = new Vector3(local_pos.x + impli_num, local_pos.y, 0);

        //최초 생성시에는 안보여야..
        circular_mask.transform.localScale = new Vector3(0, 0, 0);
        add_box_t.transform.localScale = new Vector3(0, 0, 0);
        add_box_b.transform.localScale = new Vector3(0, 0, 0);
        add_box_l.transform.localScale = new Vector3(0, 0, 0);
        add_box_r.transform.localScale = new Vector3(0, 0, 0);


    }

    //필요없어진 부분
    /*
    public void Fade_base_out()
    {

        circular_mask.transform.localScale = new Vector3(1, 1, 1);
        add_box_t.transform.localScale = new Vector3(1, 0, 1);
        add_box_b.transform.localScale = new Vector3(1, 0, 1);
        add_box_l.transform.localScale = new Vector3(0, 1, 1);
        add_box_r.transform.localScale = new Vector3(0, 1, 1);

    }

    public void Fade_base_in()
    {

        circular_mask.transform.localScale = new Vector3(0, 0, 1);
        add_box_t.transform.localScale = new Vector3(1, 1, 1);
        add_box_b.transform.localScale = new Vector3(1, 1, 1);
        add_box_l.transform.localScale = new Vector3(1, 1, 1);
        add_box_r.transform.localScale = new Vector3(1, 1, 1);

    }
    */

    public void FadeOut_work()
    {
        if (scaling <= 1)
        {
            scaling = scaling + (scaling_num) * Time.deltaTime;
        }
        else
        {
            //정지 모드로 변경
            mode = 0;
            var change_script = GameObject.Find("ChangeScene").GetComponent<Change_scene>();
            change_script.ChangeTo_AR();
        }
        circular_mask.transform.localScale = new Vector3(1-scaling, 1-scaling, 1);
        add_box_t.transform.localScale = new Vector3(1, scaling*(1.1f), 1);
        add_box_b.transform.localScale = new Vector3(1, scaling * (1.1f), 1);
        add_box_l.transform.localScale = new Vector3(scaling * (1.1f), 1, 1);
        add_box_r.transform.localScale = new Vector3(scaling * (1.1f), 1, 1);
    }

    public void FadeIn_work()
    {
        if (scaling <= 1)
        {
            scaling = scaling + (scaling_num) * Time.deltaTime;
        }
        else
        {
            //정지 모드로 변경
            mode = 0;
            scaling = 0;
            //오브젝트 제거하기 잠시 보류
            /*
            Destroy(circular_mask);
            Destroy(add_box_t);
            Destroy(add_box_b);
            Destroy(add_box_l);
            Destroy(add_box_r);
            */
            gameObject.SetActive(false);
        }
        circular_mask.transform.localScale = new Vector3(0 + scaling, 0 + scaling, 1);
        add_box_t.transform.localScale = new Vector3(1, 1-scaling, 1);
        add_box_b.transform.localScale = new Vector3(1, 1-scaling , 1);
        add_box_l.transform.localScale = new Vector3(1-scaling , 1, 1);
        add_box_r.transform.localScale = new Vector3(1-scaling , 1, 1);
    }

    
    
}

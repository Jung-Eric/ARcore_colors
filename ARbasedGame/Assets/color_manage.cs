using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class color_manage : MonoBehaviour
{
    //색 저장 DB, 팔레트를 그냥 바로 불러온다.
    private DatabaseManager mgrDB;


    // 0은 대기중, 1은 터치 시작, 2는 터치 진행
    // 3은 색 채집 처리, 4는 색을 아이템에 인벤토리처럼 저장
    // -1은 허용 범위 밖을 터치했을 경우 에러를 내보낸다.
    public int stage;

    //진행 시 이펙트와
    public GameObject progress_vpx;
    public GameObject progress_vpx2;

    //카메라 위치 받아오기
    public Camera MainCamera;


    //성공 시 이펙트
    public GameObject success_vpx;


    //실패 시 이펙트
    public GameObject fail_vpx;



    //레이캐스트 hit 관련
    RaycastHit hit;
    float MaxDistance = 15f;

    //이미지 작업 할 텍스쳐를 생성
    private Texture2D tex;

    //시간 측정 인수
    private float timer;

    //임시 터치 저장
    private Touch touch;
    private Vector2 touch_v;

    //예전 좌표 저장
    private Vector2 touch_old;

    //색 출력 관련 인수
    //progress 진행 시 띄울 이미지
    private Color progress;

    //비율 연산하기
    private float x_ratio = 120 * (0.5f);
    private float y_ratio = 56 * (0.5f);

    //int 2차원 배열, 색을 저장한다. 128x128을 저장한다.
    //터치 허용 범위, 32pixel 범위를 원하면 절반인 16pixel을 적용해야 된다.
    Color[,] color_array = new Color[32, 32];
    //int[,] c_array = new int[32, 32];
    int area_int = 16;

    // Start is called before the first frame update





    //지연된 처리를 위한 계산
    WaitForEndOfFrame frameEnd = new WaitForEndOfFrame();

    /*
    public IEnumerator TakeSnapshot()
    {
        yield return frameEnd;
        //실패하면 screen.width로 변경

        Debug.Log(Screen.width +" / "+ Screen.height);

       Texture2D tex2 = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, true);
        tex2.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0, true);

        tex2.Apply();

        Color c_imp = tex2.GetPixel((int)touch_v.x, (int)(Screen.height - touch_v.y));



        Debug.Log(c_imp.r + " / " + c_imp.g + " / " + c_imp.b);

 
    }
    */


    void Start()
    {
        stage = 0;
        timer = 0.0f;
        touch_v = new Vector2(0, 0);
        touch_old = new Vector2(0, 0);
        //tex = new Texture2D(128, 128, TextureFormat.RGB24, true);
    }

    // Update is called once per frame
    void Update()
    {

        //collect_effect_manage(1);
        //collect_effect_manage(0);

   
        
        if (Input.GetMouseButtonDown(0))
        {

            Debug.Log("stage clicked");

            touch_v = Input.mousePosition;
            timer = 0;
            stage = 1;

            //touch_v = new Vector2(300, 300);
            //call_suc();

            Debug.Log(touch_v.x+" / "+ touch_v.y);
            //Debug.Log(c_imp.r+" / "+c_imp.g + " / "+c_imp.b);

            int[] imp2 = { 50, 150, 100 };
            int[] res2 = array_sort(imp2);
            Debug.Log(res2[0] + " / " + res2[1] + " / " + res2[2] + " / " + res2[3] + " / " + res2[4]);

            Color imp = new Color( 0.2f, 0.8f, 0.9f );
            int[] res = color_counter(imp);
            Debug.Log(res[0] + " / " + res[1] + " / " + res[2] + " / " + res[3] + " / " + res[4] + " / "+ res[5]);
            //색을 받아 번호에 따라 Min : 0 / Mid : 1 / Max : 2 으로 sorting
            // 3,4에는 Max MId에 해당하는 RGB(012)를 표기


        }
        
        if (Input.GetMouseButton(0))
        {
            if (stage == 1)
            {
                timer += Time.deltaTime;
            }
            if( timer > 1)
            {
                Debug.Log("stage 2 applied");
                collect_effect_manage(1);
                collect_effect_manage(0);
                stage = 2;
            }
            if(stage == 2)
            {
                timer += Time.deltaTime;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("mouse button up");
            if(stage == 2)
            {
                collect_effect_manage(-1);
                call_epxlo();
            }
            stage = 0;
            timer = 0;

            //여기에 임시 사용해본다.
            //StartCoroutine("TakeSnapshot");

        }
        
        
        // 실제 게임
        // 코드를 stationary가 유지되면 시작되는걸로 바꾼다.
        if (Input.touchCount == 1)
        {
            Touch c_touch = Input.GetTouch(0);
            touch_v = c_touch.position;
            if (c_touch.phase == TouchPhase.Began)
            {
                stage = 1;
            }
            else if (c_touch.phase == TouchPhase.Moved)
            {

                if (stage == 2)
                {
                    //손떨림 방지, 일정 거리를 초과해야 초기화
                    if (Vector2.Distance(touch_v, touch_old) > 5)
                    {
                        collect_effect_manage(-1);
                        timer = 0;
                        stage = 0;
                        call_epxlo();
                    }
                }

            }
            else if (c_touch.phase == TouchPhase.Stationary)
            {

                if (stage == 1)
                {
                    timer += Time.deltaTime;
                    if (timer > 0.8)
                    {
                        collect_effect_manage(1);
                        collect_effect_manage(0);
                        stage = 2;
                    }
                }
                else if (stage == 2)
                {
                    touch_old = touch_v;
                    timer += Time.deltaTime;
                }
            }
            else if (c_touch.phase == TouchPhase.Ended || c_touch.phase == TouchPhase.Canceled)
            {
                collect_effect_manage(-1);
                timer = 0;
                stage = 0;

                if (stage == 2)
                {
                    collect_effect_manage(-1);
                    timer = 0;
                    stage = 0;
                    call_epxlo();
                }
            }

        }


        if (timer >= 3.5f)
        {

            collect_effect_manage(-1);
            timer = 0;
            stage = 4;
            call_suc();

        }

        if (stage == 4)
        {
            StartCoroutine("stage4");
           
            stage = 0;
        }
    }



    public IEnumerator stage4()
    {
        yield return frameEnd;

        Texture2D tex2 = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, true);
        tex2.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0, true);

        tex2.Apply();

        //여기까지는 같다.

        //Color c_imp = tex2.GetPixel((int)touch_v.x, (int)(Screen.height - touch_v.y));

        if (touch_area())
        {
            int[] imp_c = { 0, 0, 0, 0, 0, 0 };

            for (int i = 0; i < (area_int) * 2; i++)
            {
                for (int j = 0; j < (area_int) * 2; j++)
                {
                    //형태상 값이 역전된다.
                    color_array[i, j] = tex2.GetPixel((int)(touch_v.x - area_int + i), (int)(Screen.height-touch_v.y + area_int - j));

                    int[] imp_num = color_counter(color_array[i, j]);


                    for (int k = 0; k < 6; k++)
                    {
                        imp_c[k] = imp_c[k] + imp_num[k];
                    }

                }
            }


            for (int k = 0; k < 6; k++)
            {
                //대량의 수를 더했으니 총 픽셀 개수(나누기2)로 나눠준다.
                //단일 픽셀 추출보다 양이 더 많다.
                imp_c[k] = Mathf.FloorToInt(imp_c[k] / ((32 * 32) / 2));
            }
            //테스트 하기
            //call_epxlo();
            add_color(imp_c);

        }
        else
        {
            Color imp = tex2.GetPixel((int)touch_v.x, (int)(Screen.height-touch_v.y));

            int[] imp_num = color_counter(imp);

            add_color(imp_num);
            //테스트 하기
            //call_epxlo();

        }


        //Debug.Log(c_imp.r + " / " + c_imp.g + " / " + c_imp.b);


    }

    //배열을 받아 직접 외부 스크립트 조정

    private void add_color(int[] color_count)
    {
        //RGBCMY 순으로 색을 더해준다. 0~5
        //직접 회부 스크립트를 불러와서 적용해준다.
        for (int i =0; i<color_count[0]; i++)
        {
            mgrDB.AddColor("Red");
        }

        for (int i = 0; i < color_count[1]; i++)
        {
            mgrDB.AddColor("Green");
        }

        for (int i = 0; i < color_count[2]; i++)
        {
            mgrDB.AddColor("Blue");
        }

        for (int i = 0; i < color_count[3]; i++)
        {
            mgrDB.AddColor("Cyan");
        }

        for (int i = 0; i < color_count[4]; i++)
        {
            mgrDB.AddColor("Magenta");
        }

        for (int i = 0; i < color_count[5]; i++)
        {
            mgrDB.AddColor("Yellow");
        }



    }

    private bool touch_area()
    {
        bool In_area = touch_v.x >= (area_int) && touch_v.x < Screen.width - (area_int) && touch_v.y >= (area_int) && touch_v.y < Screen.height - (area_int);
        return In_area;
    }

    private void collect_effect_manage(int i)
    {
        if (i == 1)
        {
            progress_vpx.SetActive(true);
            progress_vpx2.SetActive(true);
        }
        else if (i == 0)
        {

            float cal_x_ratio = (touch_v.x - (Screen.width / 2)) / (Screen.width) * (x_ratio);
            float cal_y_ratio = (touch_v.y - (Screen.height / 2)) / (Screen.height) * (y_ratio);

            //progress_vpx.transform.localPosition = new Vector3(cal_x_ratio, cal_y_ratio, 45);
            progress_vpx.transform.localPosition = new Vector3(cal_x_ratio, cal_y_ratio, 45);
            progress_vpx2.transform.localPosition = new Vector3(cal_x_ratio, cal_y_ratio, 45);
            //progress_vpx.transform.localPosition = new Vector3(touch_v.x, touch_v.y, 45);
        }
        else if (i == -1)
        {
            progress_vpx.SetActive(false);
            progress_vpx2.SetActive(false);
        }

    }

    //허용범위 이외에는 이 에러를 출력한다.
    private void area_error()
    {
        stage = 0;
        return;
    }


    //색 연산 알고리즘, 배열을 반환한다.
    //6개의 값을 배열로 반환한다. RGBCMY(012345)를 반환한다.
    public int[] color_counter(Color imp)
    {
        //연산의 편의를 위해 250으로 변환한다.
        int imp_R = (int)(imp.r * 250);
        int imp_G = (int)(imp.g * 250);
        int imp_B = (int)(imp.b * 250);

        int[] color_arr = { imp_R, imp_G, imp_B };

        //새롭게 sorting
        int[] sorted_arr;
        sorted_arr = array_sort(color_arr);

        //채도가 높을수록 더 많은 색을 가진다.
        //총 반환할 색의 개수를 정한다.
        int imp_c = sorted_arr[2] - sorted_arr[0];
        imp_c = (imp_c * imp_c);
        imp_c = (int)(Mathf.Round(imp_c / 6000));

        //sort를 하면 동시에 계산한 Max,Mid도 반환한다.
        int RGB_Max = sorted_arr[3];
        int RGB_Mid = sorted_arr[4];

        int diff = color_arr[2] - color_arr[1];
        diff = 250 - diff;

        int dist_CMY = diff * diff;
        //int dist_RGB = (250 * 250) - dist_CMY;
        //이 dist가 작으면 CMY, dist가 크면 RGB 위주
        //Debug.Log("(dist_CMY / (250 * 250)) : " + (dist_CMY / (250 * 250)));
        //RGB로 반환할 값하고 CMY로 반환할 값을 만든다.
        int count_CMY = Mathf.FloorToInt(imp_c * dist_CMY / (250 * 250));
        int count_RGB = imp_c - count_CMY;

        //Debug.Log("CMY : "+count_CMY);
        //Debug.Log("RGB : " + count_RGB);

        int RGB_index = RGB_Max;
        int CMY_index = CMY_index_cal(RGB_Max, RGB_Mid);

        //반환 목적으로 6개의 배열을 
        int[] return_arr = { 0, 0, 0, 0, 0, 0 };

        return_arr[RGB_index] = count_RGB;
        return_arr[CMY_index] = count_CMY;

        return return_arr;

    }



    //색을 받아 번호에 따라 Min : 0 / Mid : 1 / Max : 2 으로 sorting
    // 3,4에는 Max MId에 해당하는 RGB(012)를 표기
    public int[] array_sort(int[] imp)
    {
        int[] arrays = new int[5];

        //임시 리스트
        List<int> list = new List<int>();
        list.Add(imp[0]);
        list.Add(imp[1]);
        list.Add(imp[2]);
        list.Sort();

        for (int i = 0; i < 3; i++)
        {
            //제일 큰 값
            if (imp[i] == list[2])
            {
                arrays[3] = i;
            }
            //중간 값
            if (imp[i] == list[1])
            {
                arrays[4] = i;
            }
        }

        arrays[0] = list[0];
        arrays[1] = list[1];
        arrays[2] = list[2];


        return arrays;
    }

    public int CMY_index_cal(int Max, int Mid)
    {
        // 3 : C / 4 : M / 5 : Y 에 해당한다.
        int res = 3;
        if (Max == 0)
        {
            if (Mid == 1)
            {
                res = 5;
            }
            else if (Mid == 2)
            {
                res = 4;
            }
        }
        else if (Max == 1)
        {
            if (Mid == 0)
            {
                res = 5;
            }
            else if (Mid == 2)
            {
                res = 3;
            }
        }
        else if (Max == 2)
        {
            if (Mid == 0)
            {
                res = 4;
            }
            else if (Mid == 1)
            {
                res = 3;
            }
        }


        return res;
    }


    public void call_epxlo()
    {
        GameObject exp = Instantiate(fail_vpx) as GameObject;
        exp.transform.localScale = new Vector3(5, 5, 5);
        exp.transform.position = progress_vpx.transform.position;
        exp.transform.rotation = progress_vpx.transform.rotation;

        Destroy(exp, 5.0f);
    }

    public void call_suc()
    {
        GameObject exp = Instantiate(success_vpx) as GameObject;
        exp.transform.localScale = new Vector3(7, 7, 7);
        exp.transform.position = progress_vpx.transform.position;
        Destroy(exp, 5.0f);
    }

    

}


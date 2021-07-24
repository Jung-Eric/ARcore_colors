using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class color_manage : MonoBehaviour
{
    // 0은 대기중, 1은 터치 시작, 2는 터치 진행
    // 3은 색 채집 처리, 4는 색을 아이템에 인벤토리처럼 저장
    // -1은 허용 범위 밖을 터치했을 경우 에러를 내보낸다.
    public int stage;

    public GameObject color_cylinder;

    //성공 시 이펙트와
    public GameObject cylinder_success;
    //실패 시 이펙트
    public GameObject cylinder_fail;

    //이미지 작업 할 텍스쳐를 생성
    private Texture2D tex;

    //시간 측정 인수
    private float timer;

    //임시 터치 저장
    private Touch touch;
    private Vector2 touch_v;

    //색 출력 관련 인수
    //progress 진행 시 띄울 이미지
    private Color progress;


    //int 2차원 배열, 색을 저장한다. 128x128을 저장한다.
    int[,] c_array = new int[128, 128];

    // Start is called before the first frame update
    /*
    void Start()
    {
        //Debug.Log("Hello");
        stage = 0;
        timer = 0.0f;
        tex = new Texture2D(128, 128, TextureFormat.RGB24, true);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.touchCount == 1)
        {
            Touch c_touch = Input.GetTouch(0);
            if (stage == 0)
            {
                touch_v = c_touch.position;
            }


            switch (c_touch.phase)
            {
                case TouchPhase.Began:
                    if (stage == 0)
                    {
                        timer = 0.0f;
                        stage = 1;
                    }
                    break;

                case TouchPhase.Stationary:
                    if (stage == 1)
                    {
                        timer += Time.deltaTime;
                        if (timer > 1)
                        {
                            stage = 2;
                        }
                    }
                    else if(stage == 2)
                    {
                        timer += Time.deltaTime;
                    }
                    break;
                default:
                    stage = 0;
                    break;
            }
        }
        if (Time.deltaTime >= 3.0f)
        {
            stage = 3;
            if (stage == 3)
            {
                //여기서 터치 위치에 따라 처리한다.
                //만약 허용된 위치가 너무 테두리라면 에러를 출력한다.
                //우선 측정 량은 128pixel이다.
                bool In_area = touch_v.x >= 64 && touch_v.x < Screen.width - 64 && touch_v.y >= 64 && touch_v.y < Screen.height - 64;
                if (!In_area)
                {
                    area_error();
                }
                else if (In_area)
                {
                    tex.ReadPixels(new Rect(touch_v.x-64, touch_v.x-64, 128, 128), 0, 0, true);
                    //Color imp = tex.GetPixel(touch_v.x,touch_v.y);
                }
               
            }
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
    public int [] color_counter(Color imp)
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
        int imp_c = sorted_arr[2]- sorted_arr[0];
        imp_c = (250 * 250) - (imp_c * imp_c);
        imp_c = (int) (Mathf.Round(imp_c / 6000));

        //sort를 하면 동시에 계산한 Max,Mid도 반환한다.
        int RGB_Max = sorted_arr[3];
        int RGB_Mid = sorted_arr[4];

        int diff = color_arr[2] - color_arr[1];
        diff = 250 - diff;

        int dist_CMY = diff * diff;
        //int dist_RGB = (250 * 250) - dist_CMY;
        //이 dist가 작으면 CMY, dist가 크면 RGB 위주

        //RGB로 반환할 값하고 CMY로 반환할 값을 만든다.
        int count_CMY = Mathf.FloorToInt(imp_c* (dist_CMY / (250 * 250)));
        int count_RGB = imp_c - count_CMY;

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
    public int [] array_sort(int[] imp)
    {
        int[] arrays = new int[5];

        //임시 리스트
        List<int> list = new List<int>();
        list.Add(imp[0]);
        list.Add(imp[1]);
        list.Add(imp[2]);
        list.Sort();

        for(int i = 0; i < 3; i++)
        {
            //제일 큰 값
            if(imp[i]==list[2])
            {
                arrays[3] = i;
            }
            //중간 값
            if(imp[i]==list[1])
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
   */
}

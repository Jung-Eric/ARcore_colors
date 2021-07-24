using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class WorldControl : MonoBehaviour
{
    float zoomSpeed = 0.2f;
    float imp = 0.0f;
    float scale_portion = 0.0f;
    float z_position = 0.0f;

    Vector2 touch_position; //터치 저장 위치
    Vector2 touchEnd_position; //드래그 완료 후 저장 위치
    float Result_x = 0.0f; //좌표 X변화
    float rotation_speed = 0.006f; //회전 속도

    //z변화 속도 조정
    float z_change = 0.04f;
    float s_change = 0.04f;


    // Update is called once per frame
    void Update()
    {
        //신버전 scale과 position을 동시에 조정한다.
        /*
        scale_transformation();
        z_transform();
        transform.position = new Vector3(0, -20 , z_position);
        transform.localScale = new Vector3(scale_portion, scale_portion, scale_portion);
        */

        //구버전
        //transform.localScale -= new Vector3(0.001f, 0.001f, 0.001f);
        //transform.position += new Vector3(0.0f, 0.005f, 0.0f);

        

        if (Input.touchCount == 1)
        {
            Touch dragger = Input.GetTouch(0);

            //움직인 거리를 측정
            if (dragger.phase == TouchPhase.Began)
            {
                touch_position = dragger.position - dragger.deltaPosition;
            }
            else if(dragger.phase == TouchPhase.Moved)
            {
                touchEnd_position = dragger.position - dragger.deltaPosition;
                Result_x = (touchEnd_position- touch_position).x;

                transform.Rotate(0, Result_x*rotation_speed, 0);

            }
            else if (dragger.phase == TouchPhase.Ended)
            {

            }
           
        }

            if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            //delta는 delta시간 만큼 움직인 거리가 된다.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            //현재와 과거값의 움직임의 크기 구하기
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            //위 크기를 기반으로 화면 확대 축소를 진행한다.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;



            //위에 구한 값을 기반으로 전체 오브젝트 크기를 조정한다.
            //imp = deltaMagnitudeDiff * zoomSpeed;
            //transform.localScale += new Vector3(imp,imp,imp);

            int changer = System.Convert.ToInt32(deltaMagnitudeDiff*10);

            if (changer >= 0){

                for (int i = 0; i < changer; i++)
                {
                    if (transform.localScale.x >= 0.5)
                    {
                        z_transform_minus();
                        scale_transformation_minus();
                    }
                }

            }
            else if(changer <0){
                changer = changer * (-1);
                for(int i =0; i<changer; i++)
                {
                    if (transform.localScale.x <= 4)
                    {
                        z_transform_plus();
                        scale_transformation_plus();
                    }
                }


            }

          

            transform.position = new Vector3(0, -20, z_position);
            transform.localScale = new Vector3(scale_portion, scale_portion, scale_portion);

            /*
            if (FirstPersonCamera.orthographic)
            {
                FirstPersonCamera.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;
                //너무 작아지지는 않게 만든다.
                FirstPersonCamera.orthographicSize = Mathf.Max(FirstPersonCamera.orthographicSize, 0.1f);
            }
            else
            {
                FirstPersonCamera.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;
                FirstPersonCamera.fieldOfView = Mathf.Clamp(FirstPersonCamera.fieldOfView, 0.1f, 179.9f);

            }
            */
        }

    }

    void z_transform_minus()
    {
        z_position = transform.position.z;
        z_position = z_position + 5;
        z_position = z_position  * (1.0f + z_change);
        z_position = z_position - 5;
    }

    void scale_transformation_minus()
    {
        scale_portion = transform.localScale.x;
        scale_portion = scale_portion * (1.0f - s_change);
    }

    void z_transform_plus()
    {
        z_position = transform.position.z;
        z_position = z_position + 5;
        z_position = z_position * (1.0f - z_change);
        z_position = z_position - 5;
    }

    void scale_transformation_plus()
    {
        scale_portion = transform.localScale.x;
        scale_portion = scale_portion * (1.0f + s_change);
    }


}

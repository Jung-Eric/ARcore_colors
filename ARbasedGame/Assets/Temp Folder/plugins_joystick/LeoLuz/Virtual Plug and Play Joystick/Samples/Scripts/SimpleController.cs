using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LeoLuz.PropertyAttributes;

namespace LeoLuz.PlugAndPlayJoystick
{
    public class SimpleController : MonoBehaviour
    {
        private UIManager mgrUI;
        private ObjectManager mgrObj;

        public string VerticalAxis = "Vertical";
        public string HorizontalAxis = "Horizontal";
        //public string JumpAxis = "Jump";
        //public string FireAxis = "Fire 1";
        public GameObject Projectile;
        Rigidbody rb;
        //Rigidbody2D rb;
        public float velocity;

        //현재 월드와 새로운 월드
        public int now_world;




        //중간에 걸친 상태 : 0은 걸치지 않음 / 1은 걸침
        //public int trigger_mid;

        //X,Z 중간연산
        private float X_imp;
        private float Z_imp;
        private float X_cal;
        private float Z_cal;

        

        //코사인 사인 저장
        private float C_imp;
        private float S_imp;

        //이건 360기준
        public float angle;

        new Vector3 turning;

        Animator animator;

        //public float ProjectileVelocity = 700f;
        [ReadOnly]
        public bool grounded;
        // Use this for initialization
        void Start()
        {
            turning = new Vector3(0,0,0);

            mgrUI = FindObjectOfType<UIManager>();
            mgrObj = FindObjectOfType<ObjectManager>();

            angle = 0;
            rb = GetComponent<Rigidbody>();


            //animator = GetComponent<Animator>();

            //animator.SetBool("Is_Stop", true);
        }

        

        //void OnGUI()
        //{
        //    GUILayout.BeginVertical();
        //    GUILayout.Label("Input.GetButtonDown(FireAxis)=" + Input.GetButtonDown(FireAxis));
        //    GUILayout.Label(Input.GetButtonDownList());
        //    GUILayout.EndVertical();
        //}

        // Update is called once per frame
        void Update()
        {
            

            X_imp = Input.GetAxis("Horizontal") * velocity;
            Z_imp = Input.GetAxis("Vertical") * velocity;

            /*
            if (X_imp != 0 || Z_imp != 0)
            {
                animator.SetBool("Is_Run", true);
            }
            else
            {
                animator.SetBool("Is_Stop", true);
            }
            */
            C_imp = Mathf.Cos(-angle * 3.14f / 180);
            S_imp = Mathf.Sin(-angle * 3.14f / 180);

            X_cal = C_imp * X_imp - S_imp * Z_imp;
            Z_cal = S_imp * X_imp + C_imp * Z_imp;

            
            //X_cal = X_cal / 200;
            //Z_cal = Z_cal / 200;

            //Vector3 impAngle = new Vector3(turning.x,turning.y+angle,turning.z);

            //transform.Translate(new Vector3(X_cal, 0, Z_cal));
            //transform.eulerAngles = impAngle;
            //Transform.position += new Vector3(X_cal,0, Z_cal);

            rb.velocity = new Vector3(X_cal, 0, Z_cal);
            //rb.velocity = new Vector3(Input.GetAxis("Horizontal") * velocity,0,Input.GetAxis("Vertical") * velocity);
            //rb.velocity = new Vector2(Input.GetAxis("Horizontal") * velocity, rb.velocity.y);
            //rb.velocity = new Vector2(Input.GetAxis("Horizontal") * velocity, Input.GetAxis("Vertical") * velocity);

            /*
            if (grounded && Input.GetButton(JumpAxis))
            {
                rb.velocity = new Vector2(rb.velocity.x, 10f);
                grounded = false;
            }
            if (Input.GetButtonDown(FireAxis))
            {
                GameObject obj = (GameObject)Instantiate(Projectile, transform.position + Vector3.right * 0.3f, Quaternion.identity);
                Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), GetComponent<Collider2D>());
                obj.GetComponent<Rigidbody2D>().velocity = new Vector2(ProjectileVelocity, 0f);
            }
            */

            if (now_world == 0)
            {
                angle = 0;
            }
            else if(now_world == 1)
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


        // 충돌 감지 및 저장
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log(other.gameObject.name);
            if (other.tag == "object" || other.tag == "puzzle" || other.tag == "quest" || other.tag == "script")
            {
                mgrUI.m_buttonB.SetActive(true);
                mgrObj.SetObjectName(other.gameObject.name);
                mgrObj.SetObjectTag(other.tag);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "object" || other.tag == "puzzle" || other.tag == "quest" || other.tag == "script")
            {
                mgrUI.m_buttonB.SetActive(false);
                mgrObj.ResetObject();
            }
        }


        void OnCollisionEnter(Collision col)
        {
            grounded = true;
        }

        void OnCollisionStay(Collision col)
        {
            grounded = true;
        }

        void OnCollisionExit(Collision col)
        {
            grounded = false;
        }
    }
}
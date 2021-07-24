using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LeoLuz.PropertyAttributes;

namespace LeoLuz.PlugAndPlayJoystick
{
    public class WorldTrigger : MonoBehaviour
    {
        BGMManager mgrBGM;

        //월드 이름을 저장한다.
        private int lastTriggerNum = 0;
        public int trigger_num;
        private string m_mapName = "구름 탑";

        private Dictionary<int, string> m_worldMapDic = new Dictionary<int, string>() { [0] = "구름 탑", [1] = "발걸음의 숲", [2] = "초원", [3] = "속삭임의 숲", [4] = "침묵의 황무지", [5] = "고요의 사막", [6] = "바닷바람 해변", [9] = "외딴 섬", [11] = "서리 빙하 지대", [14] = "툰드라", [16] = "오름이 쉬는 땅", [17] = "화산지대", [18] = "요정의 마르", [19] = "하늘바라기 섬", [20] = "하늘결정산", [21] = "난쟁이 버섯 숲" };

        private Text m_worldMapText;
        private Color m_color;

        private float fadeTime = 2f;
        float fade_start;
        float fade_end;
        float fade_time = 0f;


        private void Start()
        {
            mgrBGM = FindObjectOfType<BGMManager>();

            GameObject canvas = GameObject.Find("Canvas");
            m_worldMapText = canvas.transform.GetChild(1).GetComponent<Text>();
            m_worldMapText.text = m_mapName;
            StartCoroutine(ShowMapNameCoroutine());
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                var player_script = GameObject.Find("MainPlayer_cube").GetComponent<SimpleController>();
                var camera_script = GameObject.Find("Main Camera").GetComponent<Ingame_camera>();
                int now_world = player_script.now_world;

                if (now_world != trigger_num)
                {
                    lastTriggerNum = now_world;
                    player_script.now_world = trigger_num;
                    camera_script.now_world = trigger_num;
                    ShowMapName();

                    if (trigger_num == 0)
                        mgrBGM.ChangeMusic(0);
                    else if (trigger_num == 1 && lastTriggerNum != 2)
                        mgrBGM.ChangeMusic(1);
                    else if (trigger_num == 6 && lastTriggerNum != 7)
                        mgrBGM.ChangeMusic(2);
                }
            }
        }

        private void ShowMapName()
        {
            FindObjectOfType<ObjectManager>().SetLocation(trigger_num);
            if (m_worldMapDic.ContainsKey(trigger_num))
                m_mapName = m_worldMapDic[trigger_num];
            else
                m_mapName = "?????";

            m_worldMapText.text = m_mapName;
            StartCoroutine(ShowMapNameCoroutine());
        }

        IEnumerator ShowMapNameCoroutine()
        {
            yield return StartCoroutine(TextFadeOut());
            StartCoroutine(TextFadeIn());
        }

        protected IEnumerator TextFadeIn()
        {
            m_color.a = 1f;
            m_color = m_worldMapText.color;
            fade_start = 1f; fade_end = 0f; fade_time = 0f;

            while (m_color.a > 0f)
            {
                fade_time += Time.deltaTime / fadeTime;
                m_color.a = Mathf.Lerp(fade_start, fade_end, fade_time);
                m_worldMapText.color = m_color;
                yield return null;
            }
        }

        protected IEnumerator TextFadeOut()
        {
            m_color.a = 0f;
            m_color = m_worldMapText.color;
            fade_start = 0f; fade_end = 1f; fade_time = 0f;
            m_color.a = Mathf.Lerp(fade_start, fade_end, fade_time);

            while (m_color.a < 1f)
            {
                fade_time += Time.deltaTime / fadeTime;
                m_color.a = Mathf.Lerp(fade_start, fade_end, fade_time);
                m_worldMapText.color = m_color;
                yield return null;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    private ScriptManager mgrScript;
    private QuestManager mgrQuest;
    private DatabaseManager mgrDB;
    private UIManager mgrUI;

    public Camera AngleC;
    public GameObject player;
    public GameObject[] colliders;

    private string m_objectName = ""; // 상호작용 중인 오브젝트 이름
    private string m_objectTag = ""; // 상호작용 중인 오브젝트 태그
    private string m_locationName = "Center";

    //유저의 위치에 따라 angle을 적용한다.
    public int player_pos;


    private void Start()
    {
        mgrScript = FindObjectOfType<ScriptManager>();
        mgrQuest = FindObjectOfType<QuestManager>();
        mgrDB = FindObjectOfType<DatabaseManager>();
        mgrUI = FindObjectOfType<UIManager>();
    }

    public void angle_change(float a)
    {
        var camera_script = GameObject.Find("Main Camera").GetComponent<Ingame_camera>();
        camera_script.angle = a;
    }

    public void SetLocation(int num)
    {
        if (num == 0)
            m_locationName = "Center";
        else if (num <= 6 || num == 21)
            m_locationName = "Green";
        else if (num <= 15)
            m_locationName = "Blue";
        else if (num <= 20)
            m_locationName = "Red";
    }

    public void SetObjectName(string name)
    {
        m_objectName = name;
    }
    public void SetObjectTag(string tag)
    {
        m_objectTag = tag;
    }
    public void ResetObject()
    {
        m_objectName = "";
        m_objectTag = "";
    }

    public void ObjectInteraction()
    {
        if (m_objectTag == "object")
        {
            mgrScript.ShowObjectScript(m_locationName, m_objectName);
        }
        else if (m_objectTag == "script")
        {
            if (m_objectName == "책")
            {
                mgrScript.ShowScript("오브젝트", 0);
            }
        }
        else if (m_objectTag == "puzzle")
        {
            if (m_objectName == "Puzzle1")
            {
                if (FindObjectOfType<PuzzleBoardManager>().GetLevel() == 1)
                {
                    FindObjectOfType<PuzzleManager>().StartPuzzle(1);
                    mgrUI.LayerOn();
                }
            }
            else if (m_objectName == "Puzzle2")
            {
                if (FindObjectOfType<PuzzleBoardManager>().GetLevel() == 2)
                {
                    FindObjectOfType<PuzzleManager>().StartPuzzle(2);
                    mgrUI.LayerOn();
                }
            }
        }
        else if (m_objectTag == "quest")
        {
            if (m_objectName == "뱃사공")
            {
                List<LoadJson.Script> scripts = LoadJson.scriptDic["뱃사공의 부탁"];
                if (scripts[0].InnerScripts[0].finished && FindObjectOfType<PuzzleBoardManager>().GetLevel() == 3) // puzzle clear
                {
                    mgrScript.ShowScript("뱃사공의 부탁", 3);
                    mgrDB.SetEventClear("뱃사공의 부탁");
                }
                else if (scripts[0].InnerScripts[0].finished && !scripts[1].InnerScripts[0].finished)
                {
                    mgrScript.ShowScript("뱃사공의 부탁", 1);
                }
                else if (!scripts[0].InnerScripts[0].finished)
                {
                    mgrQuest.AddQuest("뱃사공의 부탁");
                    mgrScript.ShowScript("뱃사공의 부탁", 0);
                }
            }
            else if (m_objectName == "갈색 곰")
            {
                List<LoadJson.Script> scripts = LoadJson.scriptDic["동생 찾기"];
                if (scripts[0].InnerScripts[0].finished && !scripts[1].InnerScripts[0].finished)
                {
                    mgrScript.ShowScript("동생 찾기", 1);
                }
                else if (!scripts[0].InnerScripts[0].finished)
                {
                    mgrQuest.AddQuest("동생 찾기");
                    mgrScript.ShowScript("동생 찾기", 0);
                }
            }
            else if (m_objectName == "흰색 곰")
            {
                List<LoadJson.Script> scripts = LoadJson.scriptDic["동생 찾기"];
                if (mgrDB.GetEventClear("동생 찾기"))
                {
                    scripts = LoadJson.scriptDic["새로운 시도"];
                    if (scripts[0].InnerScripts[0].finished && scripts[0].InnerScripts[0].accepted && mgrDB.GetColor("Green")) // 초록색이 있다면
                    {
                        mgrScript.ShowScript("새로운 시도", 3);
                        mgrDB.SetEventClear("새로운 시도");
                    }
                    else if (scripts[0].InnerScripts[0].finished && !scripts[1].InnerScripts[0].finished)
                    {
                        mgrScript.ShowScript("새로운 시도", 1);
                    }
                    else if (!scripts[0].InnerScripts[0].finished)
                    {
                        mgrQuest.AddQuest("새로운 시도");
                        mgrScript.ShowScript("새로운 시도", 0);
                    }
                }
                else if (scripts[0].InnerScripts[0].finished && scripts[0].InnerScripts[0].accepted)
                {
                    mgrScript.ShowScript("동생 찾기", 3);
                    mgrDB.SetEventClear("동생 찾기");
                }
            }
        }
        ResetObject();
    }
}
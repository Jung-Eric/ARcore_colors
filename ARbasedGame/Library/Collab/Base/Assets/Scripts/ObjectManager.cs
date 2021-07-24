using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    private ScriptManager mgrScript;
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
        else if (m_objectTag == "puzzle")
        {
            if (m_objectName == "Puzzle1")
                FindObjectOfType<PuzzleManager>().StartPuzzle(1);
            else if (m_objectName == "Puzzle2")
                FindObjectOfType<PuzzleManager>().StartPuzzle(2);

            mgrUI.LayerOn();
        }
        else if (m_objectTag == "quest")
        {
            mgrUI.ScriptLayerOn();
            if (m_objectName == "뱃사공")
            {
                mgrScript.ShowScript("뱃사공의 부탁", 0);
            }
        }
        ResetObject();
    }
}
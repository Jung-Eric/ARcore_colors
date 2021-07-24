using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestBox : MonoBehaviour
{
    private Image m_image;
    private Text m_questName;

    private QuestManager mgrQuest;


    private void Awake()
    {
        m_image = GetComponentInChildren<Image>();
        m_questName = GetComponentInChildren<Text>();
        m_image.enabled = false;
        mgrQuest = FindObjectOfType<QuestManager>();
    }

    public void SetQuest(bool required, string name)
    {
        m_questName.text = name;
        m_image.sprite = Resources.Load<Sprite>("UI Elements/White/2x/star") as Sprite;
        if (!required)
        {
            Color color = m_image.color;
            color.a = 0.168f;
            m_image.color = color;
        }
        m_image.enabled = true;
    }

    public void SetTransform()
    {
        GetComponent<Transform>().SetParent(GameObject.Find("Canvas/Quest").transform.GetChild(1).GetComponent<Transform>(), false);
    }

    public void ClickBox()
    {
        string quest = m_questName.text;
        if (FindObjectOfType<DatabaseManager>().GetEventClear(m_questName.text))
        {
            quest = "c" + quest;
        }
        
        List<LoadJsonObjectMessage.ObjectMessage> messages = LoadJsonObjectMessage.messageDic["Quest"];
        for (int i = 0; i < messages.Count; i++)
        {
            if (messages[i].name == quest)
            {
                GameObject.Find("Canvas/Quest").transform.GetChild(2).GetComponent<Text>().text = messages[i].message;
            }
        }
    }
}
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public GameObject questPrefab;


    public void AddQuest1(/*bool required, string name*/)
    {
        if (GameObject.Find("Canvas/Quest/QuestList").transform.childCount == 5)
            Debug.Log("추가 실패"); // 임시
        else
        {
            GameObject quest = Instantiate(questPrefab);
            quest.GetComponent<QuestBox>().SetTransform();
            quest.GetComponent<QuestBox>().SetQuest(true, name);
        }
    }

    public void AddQuest2(/*bool required, string name*/)
    {
        if (GameObject.Find("Canvas/Quest/QuestList").transform.childCount == 5)
            Debug.Log("추가 실패"); // 임시
        else
        {
            GameObject quest = Instantiate(questPrefab);
            quest.GetComponent<QuestBox>().SetTransform();
            quest.GetComponent<QuestBox>().SetQuest(false, name);
        }
    }
}
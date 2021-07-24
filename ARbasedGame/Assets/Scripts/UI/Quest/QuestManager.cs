using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public GameObject questPrefab;
    public GameObject m_questList;

    private DatabaseManager mgrDB;


    private void Start()
    {
        mgrDB = FindObjectOfType<DatabaseManager>();
    }

    public void AddQuest(string name)
    {
        if (m_questList.transform.childCount == 5)
            Debug.Log("추가 실패");
        else
        {
            GameObject quest = Instantiate(questPrefab);
            quest.GetComponent<QuestBox>().SetTransform();
            quest.GetComponent<QuestBox>().SetQuest(true, name); // 무조건
            mgrDB.AddEvent(name);
        }
    }
}
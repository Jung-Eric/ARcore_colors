using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptManager : MonoBehaviour
{
    public GameObject m_scriptWindow;
    public Text[] m_texts;

    private UIManager mgrUI;


    int count = 0;

    private List<string> listSpeakers = new List<string>();
    private List<string> listSentences = new List<string>();

    private string m_scriptName = "";
    private int m_scriptNum = 0;

    private bool m_isFinished;


    void Start()
    {
        mgrUI = FindObjectOfType<UIManager>();
    }

    public void ShowScript(string script, int num)
    {
        List<LoadJson.Script> scripts = LoadJson.scriptDic[script];
        if (scripts[num].InnerScripts[0].finished) // 이미 완료했다면
        {
            Debug.Log("이미 완료한 이벤트");
            return;
        }

        m_scriptName = script;
        m_scriptNum = num;
        scripts[m_scriptNum].InnerScripts[0].finished = true;

        for (int i = 0; i < scripts[m_scriptNum].InnerScripts.Count; i++)
        {
            listSentences.Add(scripts[m_scriptNum].InnerScripts[i].script);
            listSpeakers.Add(scripts[m_scriptNum].InnerScripts[i].name);
        }

        StartCoroutine(ScriptCoroutine());
    }

    IEnumerator ScriptCoroutine()
    {
        m_texts[0].text = listSpeakers[count];
        m_isFinished = false;
        for (int i = 0; i < listSentences[count].Length; i++)
        {
            m_texts[1].text += listSentences[count][i];
            yield return new WaitForSeconds(0.03f);
        }
        m_isFinished = true;
        yield break;
    }

    private void ExitScripts()
    {
        m_texts[0].text = "";
        m_texts[1].text = "";
        count = 0;
        m_isFinished = false;
        m_scriptName = "";
        m_scriptNum = 0;

        listSpeakers.Clear();
        listSentences.Clear();

        mgrUI.ScriptLayerOff();
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0) && m_isFinished && m_scriptWindow.activeSelf)
        {
            count++;

            if (count >= listSentences.Count)
            {
                StopAllCoroutines();
                ExitScripts();
            }
            else
            {
                m_texts[1].text = "";
                StopAllCoroutines();
                StartCoroutine(ScriptCoroutine());
            }
        }
    }
}

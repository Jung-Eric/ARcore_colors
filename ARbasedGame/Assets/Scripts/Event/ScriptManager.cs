﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptManager : MonoBehaviour
{
    public GameObject m_scriptWindow;
    public Text[] m_texts;
    public GameObject m_buttonList;

    private UIManager mgrUI;


    int count = 0;

    private List<string> listSpeakers = new List<string>();
    private List<string> listSentences = new List<string>();

    private string m_scriptName = "";
    private int m_scriptNum = 0;
    private int m_selectStart = 0;

    private bool m_isFinished;
    private bool m_isSelect;
    private bool m_isObj = false;


    void Start()
    {
        mgrUI = FindObjectOfType<UIManager>();
    }

    private void ActiveButton()
    {
        m_buttonList.SetActive(true);
    }
    private void InActiveButton()
    {
        m_buttonList.SetActive(false);
    }
    private void ResetText()
    {
        for (int i = 0; i < m_texts.Length; i++)
        {
            m_texts[i].text = "";
        }
    }

    // yes -> order 1 // no -> order 2
    public void ClickButton(int order)
    {
        listSentences.Clear();
        listSpeakers.Clear();
        InActiveButton();
        ResetText();

        List<LoadJson.Script> scripts = LoadJson.scriptDic[m_scriptName];
        for (int i = m_selectStart; i < scripts[m_scriptNum].InnerScripts.Count; i++)
        {
            if (scripts[m_scriptNum].InnerScripts[i].chooseNum == order * 10)
            {
                listSentences.Add(scripts[m_scriptNum].InnerScripts[i].script);
                listSpeakers.Add(scripts[m_scriptNum].InnerScripts[i].name);
            }
        }

        if (order == 1) // 수락 시
        {
            scripts[0].InnerScripts[0].accepted = true;
            scripts[m_scriptNum + 1].InnerScripts[0].finished = true; // 다음 이벤트 true로 만듦
        }

        m_isSelect = false;
        count = 0;
        StartCoroutine(ScriptCoroutine());
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
        if (m_scriptName != "오브젝트")
            scripts[m_scriptNum].InnerScripts[0].finished = true;

        for (int i = 0; i < scripts[m_scriptNum].InnerScripts.Count; i++)
        {
            if (scripts[m_scriptNum].InnerScripts[i].chooseNum == -1)
            {
                listSentences.Add(scripts[m_scriptNum].InnerScripts[i].script);
                listSpeakers.Add(scripts[m_scriptNum].InnerScripts[i].name);
            }
            else if (scripts[m_scriptNum].InnerScripts[i].chooseNum == 0)
            {
                listSentences.Add(scripts[m_scriptNum].InnerScripts[i].script);
                listSpeakers.Add(scripts[m_scriptNum].InnerScripts[i].name);
                m_isSelect = true;
                m_selectStart = i;
            }
        }

        FindObjectOfType<UIManager>().ScriptLayerOn();
        StartCoroutine(ScriptCoroutine());
    }

    public void ShowObjectScript(string location, string name)
    {
        List<LoadJsonObjectMessage.ObjectMessage> messages = LoadJsonObjectMessage.messageDic[location];

        for (int i = 0; i < messages.Count; i++)
        {
            if (messages[i].name == name)
            {
                listSentences.Add(messages[i].message);
                break;
            }
        }
        if (listSentences.Count == 0) // 해당 오브젝트가 존재하지 않는다면
        {
            Debug.Log("No Object");
            return;
        }

        m_isObj = true;
        FindObjectOfType<UIManager>().ScriptLayerOn();
        StartCoroutine(ScriptCoroutine());
    }

    IEnumerator ScriptCoroutine()
    {
        if (!m_isObj)
            m_texts[0].text = listSpeakers[count];
        m_isFinished = false;
        for (int i = 0; i < listSentences[count].Length; i++)
        {
            if (m_isObj || listSpeakers[count] == "")
                m_texts[2].text += listSentences[count][i];
            else
                m_texts[1].text += listSentences[count][i];

            yield return new WaitForSeconds(0.03f);
        }
        m_isFinished = true;
        yield break;
    }

    private void ExitScripts()
    {
        ResetText();

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
                if (m_isSelect)
                {
                    m_isFinished = false;
                    ActiveButton();
                }
                else
                    ExitScripts();
            }
            else
            {
                m_texts[1].text = "";
                m_texts[2].text = "";
                StopAllCoroutines();
                StartCoroutine(ScriptCoroutine());
            }
        }
    }
}

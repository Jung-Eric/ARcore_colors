using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MyDefine;

public class PuzzleBoardManager : MonoBehaviour
{
    public GameObject m_treasure;

    private GameObject m_clickedBlock = null;
    private Dictionary<int, int> m_levelDic = new Dictionary<int, int>() { { 1, 8 }, { 2, 12 } };
    // 레벨에 따른 블럭의 개수

    private int m_level;
    private int m_doneBlockNum = 0;

    private ScriptManager mgrScript;


    private void Start()
    {
        mgrScript = FindObjectOfType<ScriptManager>();
        m_level = 1;
    }


    public void SetLevel(int level)
    {
        m_level = level;
    }

    private void ResetBoard()
    {
        m_doneBlockNum = 0;
        m_clickedBlock = null;
        FindObjectOfType<PuzzleManager>().ResetPuzzle(m_level);
        //FindObjectOfType<UIManager>().ScriptLayerOn();
        mgrScript.ShowScript("보물상자", m_level - 1);
    }

    public void SetClickedBlock(GameObject block)
    {
        m_clickedBlock = block;
    }

    public GameObject GetClickedBlock()
    {
        return m_clickedBlock;
    }

    public void UpdateDoneBlock()
    {
        m_doneBlockNum += 2;
        if (m_doneBlockNum == m_levelDic[m_level])
            ResetBoard();
    }
}
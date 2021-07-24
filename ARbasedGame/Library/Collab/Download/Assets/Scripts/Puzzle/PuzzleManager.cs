using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public GameObject m_puzzle;

    public void StartPuzzle(int level)
    {
        FindObjectOfType<PuzzleBoardManager>().SetLevel(level);
        m_puzzle.SetActive(true);
        m_puzzle.transform.GetChild(0).GetChild(level).gameObject.SetActive(true);
    }

    public void ResetPuzzle(int level)
    {
        m_puzzle.transform.GetChild(0).GetChild(level).gameObject.SetActive(false);
        m_puzzle.SetActive(false);
    }
}
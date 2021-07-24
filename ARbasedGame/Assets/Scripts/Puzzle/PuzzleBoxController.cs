using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static MyDefine;

public class PuzzleBoxController : MonoBehaviour
{
    public blockcolor m_color;
    private PuzzleBoardManager mgrBoard;


    private void Start()
    {
        mgrBoard = FindObjectOfType<PuzzleBoardManager>();
    }


    public void ClickBlock()
    {
        AudioManager.Instance.Play("BlockClick");
        if (mgrBoard.GetClickedBlock() == null)
        {
            mgrBoard.SetClickedBlock(gameObject);
        }
        else
        {
            GameObject clickedBlock = mgrBoard.GetClickedBlock();

            if (m_color == blockcolor.RED && clickedBlock.GetComponent<PuzzleBoxController>().m_color == blockcolor.CYAN || m_color == blockcolor.CYAN && clickedBlock.GetComponent<PuzzleBoxController>().m_color == blockcolor.RED ||
                m_color == blockcolor.BLUE && clickedBlock.GetComponent<PuzzleBoxController>().m_color == blockcolor.YELLOW || m_color == blockcolor.YELLOW && clickedBlock.GetComponent<PuzzleBoxController>().m_color == blockcolor.BLUE ||
                m_color == blockcolor.PURPLE && clickedBlock.GetComponent<PuzzleBoxController>().m_color == blockcolor.GREEN || m_color == blockcolor.GREEN && clickedBlock.GetComponent<PuzzleBoxController>().m_color == blockcolor.PURPLE ||
                m_color == blockcolor.ORANGE && clickedBlock.GetComponent<PuzzleBoxController>().m_color == blockcolor.SKY || m_color == blockcolor.SKY && clickedBlock.GetComponent<PuzzleBoxController>().m_color == blockcolor.ORANGE ||
                m_color == blockcolor.PINK && clickedBlock.GetComponent<PuzzleBoxController>().m_color == blockcolor.MINT || m_color == blockcolor.MINT && clickedBlock.GetComponent<PuzzleBoxController>().m_color == blockcolor.PINK ||
                m_color == blockcolor.NAVY && clickedBlock.GetComponent<PuzzleBoxController>().m_color == blockcolor.BROWN || m_color == blockcolor.BROWN && clickedBlock.GetComponent<PuzzleBoxController>().m_color == blockcolor.NAVY)
            {
                ClickRightBlock(clickedBlock);
            }
            else
            {
                mgrBoard.SetClickedBlock(gameObject);
            }
        }
    }

    public void ClickRightBlock(GameObject clickedBlock)
    {
        clickedBlock.GetComponent<PuzzleBoxController>().ResetColor();
        clickedBlock.GetComponent<Button>().interactable = false;
        ResetColor();
        GetComponent<Button>().interactable = false;

        mgrBoard.SetClickedBlock(null);
        mgrBoard.UpdateDoneBlock();
    }

    public void ResetColor()
    {
        m_color = blockcolor.NONE;
        Color color = gameObject.GetComponent<Image>().color;
        color.r = 255; color.g = 255; color.b = 255;
        gameObject.GetComponent<Image>().color = color;
    }
}
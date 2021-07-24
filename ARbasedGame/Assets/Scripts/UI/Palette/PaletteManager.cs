using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaletteManager : MonoBehaviour
{
    private DatabaseManager mgrDB;

    private int m_colorNum = 0;
    PaletteBox mgrBox;
    private GameObject m_box;
    public Image m_colorImage;
    public Text m_colorText;


    private void Start()
    {
        mgrDB = FindObjectOfType<DatabaseManager>();
        for (int i = 0; i < mgrDB.m_colors.Count; i++)
        {
            AddColor(mgrDB.m_colors[i]);
        }
    }

    // Red Green Blue Cyan Magenta Yellow 로 추가
    public void AddColor(string name)
    {
        mgrDB.AddColor(name);
        mgrBox.SetImage(name);
        m_colorNum = Mathf.Clamp(m_colorNum + 1, 0, 15);
        SetBox();
    }

    public void SetDetail(string name)
    {
        List<LoadJsonObjectMessage.ObjectMessage> messages = LoadJsonObjectMessage.messageDic["Color"];
        for (int i = 0; i < messages.Count; i++)
        {
            if (messages[i].name == name)
            {
                m_colorText.text = messages[i].message;
                break;
            }
        }

        m_colorImage.gameObject.SetActive(true);
        m_colorImage.sprite = Resources.Load<Sprite>("UI/Palette/Color/" + name) as Sprite;
    }

    public void ResetDetail()
    {
        m_colorText.text = "";
        m_colorImage.gameObject.SetActive(false);
        m_colorImage.sprite = null;
    }


    private void SetBox()
    {
        m_box = GameObject.Find("box" + m_colorNum.ToString());
        mgrBox = m_box.GetComponent<PaletteBox>();
    }

    private void Awake()
    {
        SetBox();
        ResetDetail();
    }
}

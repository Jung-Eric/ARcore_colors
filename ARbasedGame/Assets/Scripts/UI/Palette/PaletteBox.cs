using UnityEngine;
using UnityEngine.UI;

public class PaletteBox : MonoBehaviour
{
    private Image m_image;
    private string m_colorName;

    private PaletteManager mgrPalette;

    private void Awake()
    {
        m_colorName = "";
        m_image = GetComponentInChildren<Image>();
        m_image.enabled = false;

        mgrPalette = FindObjectOfType<PaletteManager>();
    }

    public void SetImage(string name)
    {
        m_colorName = name;
        m_image.sprite = Resources.Load<Sprite>("UI/Palette/Color/" + name) as Sprite;
        m_image.enabled = true;
    }

    public void ClickBox()
    {
        if (m_colorName == "")
            mgrPalette.ResetDetail();
        else
            mgrPalette.SetDetail(m_colorName);
    }
}
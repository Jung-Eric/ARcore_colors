using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject palette;
    public GameObject quest;
    public GameObject script;
    public GameObject m_layer;

    public GameObject Fade;

    private ScriptManager mgrScript;


    private void Start()
    {
        mgrScript = FindObjectOfType<ScriptManager>();
    }


    public void ClickPaletteButton()
    {
        palette.SetActive(true);
        m_layer.SetActive(true);
    }

    public void ClickQuestButton()
    {
        quest.SetActive(true);
        m_layer.SetActive(true);
    }
    // layer가 켜지면 layer 밖의 부분은 클릭 못하게끔

    public void ScriptLayerOn()
    {
        script.SetActive(true);
        m_layer.SetActive(true);
    }

    public void ScriptLayerOff()
    {
        script.SetActive(false);
        m_layer.SetActive(false);
    }

    public void ShowTemp()
    {
        mgrScript.ShowScript("temp", 0);
    }

    public void ClickXButton()
    {
        palette.SetActive(false);
        quest.SetActive(false);
        m_layer.SetActive(false);
    }

    public void FadeOut()
    {
        Fade.SetActive(true);
        var fade_script = GameObject.Find("FadeInOut_Mask").GetComponent<FadeInOut>();
        fade_script.mode = 1;
        
    }

}
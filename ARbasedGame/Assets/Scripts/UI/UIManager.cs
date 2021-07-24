using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject palette;
    public GameObject m_questForm;
    public GameObject m_questList;
    public Text m_questText;

    public GameObject script;
    public GameObject m_layer;

    public GameObject Fade;
    public GameObject m_joyStick;
    public GameObject m_buttonB;

    private ScriptManager mgrScript;
    private AudioManager mgrAudio;


    private void Start()
    {
        mgrScript = FindObjectOfType<ScriptManager>();
        mgrAudio = FindObjectOfType<AudioManager>();
        m_buttonB.SetActive(false);
    }


    public void ClickPaletteButton()
    {
        mgrAudio.Play("UIClick");
        palette.SetActive(true);
        LayerOn();
    }

    public void ClickQuestButton()
    {
        mgrAudio.Play("UIClick");
        m_questForm.SetActive(true);
        m_questList.SetActive(true);
        m_questText.text = "";
        LayerOn();
    }

    public void ClickBButton()
    {
        mgrAudio.Play("UIClick");
        FindObjectOfType<ObjectManager>().ObjectInteraction();
    }
    // layer가 켜지면 layer 밖의 부분은 클릭 못하게끔

    public void SetJoyStickOff()
    {
        m_joyStick.SetActive(false);
    }

    public void SetJoyStickOn()
    {
        m_joyStick.SetActive(true);
    }

    public void ScriptLayerOn()
    {
        script.SetActive(true);
        m_layer.SetActive(true);
        SetJoyStickOff();
    }

    public void ScriptLayerOff()
    {
        script.SetActive(false);
        m_layer.SetActive(false);
        SetJoyStickOn();
    }

    public void LayerOn()
    {
        m_layer.SetActive(true);
        SetJoyStickOff();
    }

    public void ClickXButton()
    {
        palette.SetActive(false);
        m_questForm.SetActive(false);
        m_questList.SetActive(false);
        m_questText.text = "";
        m_layer.SetActive(false);
        SetJoyStickOn();
    }

    public void FadeOut()
    {
        Fade.SetActive(true);
        var fade_script = GameObject.Find("FadeInOut_Mask").GetComponent<FadeInOut>();
        fade_script.mode = 1;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Change_scene : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        /*
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            var change_script = GameObject.Find("FadeInOut_Mask").GetComponent<FadeInOut>();
            change_script.FadeIn_work();
        }
        */
    }

    public void ChangeTo_Other()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            SceneManager.LoadScene("WorkIngame_v2");
        }
        else if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            SceneManager.LoadScene("WorkAR");
        }
    }

    public void ChangeTo_AR()
    {
        SceneManager.LoadScene("WorkAR");

    }

    public void ChangeTo_Ingame()
    {
        SceneManager.LoadScene("WorkIngame_v2");
    }


}
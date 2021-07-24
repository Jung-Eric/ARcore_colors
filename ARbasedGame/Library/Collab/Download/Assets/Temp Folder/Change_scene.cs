using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Change_scene : MonoBehaviour
{
    public void ChangeTo_AR()
    {
        SceneManager.LoadScene("WorkAR");

    }

    public void ChangeTo_Ingame()
    {
        SceneManager.LoadScene("WorkIngame");
    }

 
}

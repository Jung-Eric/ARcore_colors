using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Fading : MonoBehaviour
{
    private Color m_color;
    public Image m_fadeImage;

    private float fadeTime = 2.5f;
    float img_start;
    float img_end;
    float img_time = 0f;


    public void Start()
    {
        GameObject canvas = GameObject.Find("Canvas");
        m_fadeImage = canvas.transform.GetChild(0).GetComponent<Image>();
        m_fadeImage.gameObject.SetActive(true);
        StartImageFadeIn();
    }

    public void StartImageFadeIn()
    {
        StartCoroutine(ImageFadeIn());
    }

    public void StartImageFadeOut()
    {
        StartCoroutine(ImageFadeOut());
    }

    protected IEnumerator ImageFadeIn()
    {
        m_color.a = 1f;
        m_color = m_fadeImage.color;
        img_start = 1f; img_end = 0f; img_time = 0f;

        while (m_color.a > 0f)
        {
            img_time += Time.deltaTime / fadeTime;
            m_color.a = Mathf.Lerp(img_start, img_end, img_time);
            m_fadeImage.color = m_color;
            yield return null;
        }
    }


    protected IEnumerator ImageFadeOut()
    {
        m_color.a = 0f;
        m_color = m_fadeImage.color;
        img_start = 0f; img_end = 1f; img_time = 0f;
        m_color.a = Mathf.Lerp(img_start, img_end, img_time);

        while (m_color.a < 1f)
        {
            img_time += Time.deltaTime / fadeTime;
            m_color.a = Mathf.Lerp(img_start, img_end, img_time);
            m_fadeImage.color = m_color;
            yield return null;
        }
    }
}

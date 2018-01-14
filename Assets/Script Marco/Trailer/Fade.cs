using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Fade : MonoBehaviour
{
    public bool fade_In;
    private Image img;
    private Text txt;

    private void Start()
    {
        if (GetComponent<Image>() != null)
        {
            img = GetComponent<Image>();
        }
        if (GetComponent<Text>() != null)
        {
            txt = GetComponent<Text>();
        }
        StartCoroutine(FadeImage(fade_In));
    }

    public void FadeTrailer()
    {
        StartCoroutine(FadeImage(true));
    }

    public void FadeTrailer_2()
    {
        StartCoroutine(FadeImage(false));
    }

    IEnumerator FadeImage(bool fadeAway)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 2; i >= 0; i -= Time.deltaTime)
            {
                // set color with i as alpha
                img.color = new Color(1, 1, 1, i);
                yield return null;
            }

            img.color = new Color(1, 1, 1, 0);
        }
        // fade from transparent to opaque
        else
        {
            // loop over 1 second
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                // set color with i as alpha
                if (img != null)
                {
                    img.color = new Color(1, 1, 1, i);
                }

                if (txt != null)
                {
                    txt.color = new Color(1, 1, 1, i);
                }
                yield return null;
            }
        }
    }
}
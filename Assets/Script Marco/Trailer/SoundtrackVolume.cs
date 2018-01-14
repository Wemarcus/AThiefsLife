using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundtrackVolume : MonoBehaviour {

    public AudioSource source;
    public float fadeTime = 1;

    public void FadeSound()
    {
        if (fadeTime == 0)
        {
            source.volume = 0;
            return;
        }

        StartCoroutine(SoundOff());
    }

    private IEnumerator SoundOff()
    {
        float t = fadeTime;
        while (t > 0)
        {
            yield return null;
            t -= Time.deltaTime;
            source.volume = t / fadeTime;
        }
        yield break;
    }
}

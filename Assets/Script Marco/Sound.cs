using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour {

    public string tag_name;
    private AudioSource sound;
    private bool block;

    private void Start()
    {
        sound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == tag_name && !block)
        {
            block = true;
            sound.Play();

            StartCoroutine(Time(3.0f));
        }
    }

    private IEnumerator Time(float t)
    {
        yield return new WaitForSeconds(t);
        block = false;
    }
}

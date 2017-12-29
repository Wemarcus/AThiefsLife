using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashing : MonoBehaviour {

    public Light red;
    public Light blue;

	// Use this for initialization
	void Start () {
        red.enabled = true;
        blue.enabled = false;
        StartCoroutine(Flickering());
	}
	
	private IEnumerator Flickering()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            red.enabled = !red.enabled;
            blue.enabled = !blue.enabled;
        }
    }
}

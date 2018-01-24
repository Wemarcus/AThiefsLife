using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingManagerImage : MonoBehaviour {

    public GameObject[] background;
    public GameObject[] tips;
    int index;

	void Start () {
        index = Random.Range(0, background.Length);

        background[index].SetActive(true);
        tips[index].SetActive(true);
    }
	
}

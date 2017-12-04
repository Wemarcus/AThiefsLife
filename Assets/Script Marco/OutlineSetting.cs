using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineSetting : MonoBehaviour {

	void Start () {

        if (GetComponent<cakeslice.Outline>() != null)
        {
            GetComponent<cakeslice.Outline>().enabled = false;
        }
	}
	
}

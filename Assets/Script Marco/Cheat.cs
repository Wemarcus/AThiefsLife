using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheat : MonoBehaviour {

    // Classe temporanea per alcuni test..
	
	void Update () {

        if (Input.GetKeyDown(KeyCode.M))
        {
            gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            gameObject.SetActive(true);
        }

    }
}

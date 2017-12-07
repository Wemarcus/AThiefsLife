using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineSetting : MonoBehaviour {

	void Start () {

        if (GetComponent<cakeslice.Outline>() != null)
        {
			if (!this.gameObject.GetComponent<Node> ().AllySpawn) {
				GetComponent<cakeslice.Outline> ().enabled = false;
			}
        }
	}
	
}

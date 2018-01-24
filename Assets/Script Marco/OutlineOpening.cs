using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineOpening : MonoBehaviour {

    public GameObject mainCamera;
	
	// Update is called once per frame
	void Update () {

        if (!GetComponent<Cinemachine.CinemachineVirtualCamera>().enabled)
        {
            mainCamera.GetComponent<cakeslice.OutlineEffect>().enabled = true;
            Destroy(this);
        }

	}
}

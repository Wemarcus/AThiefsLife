using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTestMarco : MonoBehaviour {

    public GameObject main_camera;
    public GameObject virtual_camera;
	
	void Update () {

    }

	public void setcamera(){
		virtual_camera.GetComponent<MainCamera>().target = main_camera.GetComponent<MainCamera>().target;
		GetComponent<Cinemachine.CinemachineVirtualCamera>().LookAt = virtual_camera.GetComponent<MainCamera>().target;
	}
}

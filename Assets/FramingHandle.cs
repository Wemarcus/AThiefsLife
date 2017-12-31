using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FramingHandle : MonoBehaviour {

	public List<Camera> cameras;
	public GameObject target;
	public GameObject character;
	private Camera currentCamera;
	private Camera mainCamera;

	// Use this for initialization
	void Start () {
		SwitchCamera ();
	}
	
	// Update is called once per frame
	void Update () {
		CheckCamera ();
	}

	private void CheckCamera(){
		Debug.Log (currentCamera.name);
		RaycastHit hit;
		//Ray ray = new Ray (currentCamera.transform.position, currentCamera.transform.position - target.transform.position);
		Vector3 dir = (target.transform.position - currentCamera.transform.position);
		if (Physics.Raycast(currentCamera.transform.position,dir, out hit)) {
			Transform objectHit = hit.transform;
			Debug.Log (objectHit.name);
			if (objectHit.gameObject != character) {
				SwitchCamera ();
			}
		}

	}

	public void SwitchCamera(){
		mainCamera = Camera.current;
		bool flag = false;
		RaycastHit hit;
		//Ray ray;
		foreach (Camera camera in cameras) {
				if(!flag){
				
				//ray = new Ray (target.transform.position, camera.transform.position - target.transform.position);
				Vector3 dir = (target.transform.position - camera.transform.position);
				if (Physics.Raycast (camera.transform.position,dir, out hit)) {
					Debug.DrawRay (camera.transform.position,dir, Color.red, 500f);
					Transform objectHit = hit.transform;
					Debug.Log (objectHit.name);
					if (objectHit.gameObject == character) {
						if (currentCamera) {
							currentCamera.enabled = false;
						}
						Debug.Log ("cambio camera");
						currentCamera = camera;
						Camera.current.enabled = false;
						camera.enabled = true;
						flag = true;
					}
				}
			}
		}
	}

	public void ReleaseCamera(){
		currentCamera.enabled = false;
		mainCamera.enabled = true;
		currentCamera = null;
		mainCamera = null;
	}



}

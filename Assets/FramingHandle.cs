using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FramingHandle : MonoBehaviour {

	public List<Camera> cameras;
	public GameObject target;
	public GameObject character;
	private Camera currentCamera;
	private Camera mainCamera;
	public Camera attackCamera;
	private bool isFraming;

	// Use this for initialization
	void Start () {
		mainCamera = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
		if(isFraming && currentCamera)
		CheckCamera ();
	}

	private void CheckCamera(){
		//Debug.Log (currentCamera.name);
		RaycastHit hit;
		//Ray ray = new Ray (currentCamera.transform.position, currentCamera.transform.position - target.transform.position);
		Vector3 dir = (target.transform.position - currentCamera.transform.position);
		if (Physics.Raycast(currentCamera.transform.position,dir, out hit)) {
			Transform objectHit = hit.transform;
			Debug.Log (objectHit.name);
			if (objectHit.gameObject != character) {
				Debug.Log (objectHit.name + " in " + character.name);
				SwitchCamera ();
			}
		}

	}

	public void SwitchCamera(){
		isFraming = true;
		//mainCamera = Camera.main;
		bool flag = false;
		RaycastHit hit;
		//Ray ray;
		if (!currentCamera && cameras.Count > 0) {
			currentCamera = cameras [0];
			if (currentCamera.enabled == false) {
				currentCamera.enabled = true;
			}
		}
		foreach (Camera camera in cameras) {
				if(!flag){
				
				//ray = new Ray (target.transform.position, camera.transform.position - target.transform.position);
				Vector3 dir = (target.transform.position - camera.transform.position);
				if (Physics.Raycast (camera.transform.position,dir, out hit)) {
					Debug.DrawRay (camera.transform.position,dir, Color.red, 500f);
					Transform objectHit = hit.transform;
					//Debug.Log (objectHit.name);
					if (objectHit.gameObject == character) {
						if (currentCamera) {
							currentCamera.enabled = false;
						}
						Debug.Log ("cambio camera");
						currentCamera = camera;
						camera.enabled = true;
						if(mainCamera && mainCamera.enabled == true)
						mainCamera.enabled = false;
						flag = true;
					}
				}
			}
		}
	}

	public void LoadAttackCamera(){
		RaycastHit hit;
		Vector3 dir = (target.transform.position - attackCamera.transform.position);
		if (Physics.Raycast (attackCamera.transform.position, dir, out hit)) {
			Debug.DrawRay (attackCamera.transform.position, dir, Color.red, 500f);
			Transform objectHit = hit.transform;
			//Debug.Log (objectHit.name);
			if (objectHit.gameObject == character) {
				if (currentCamera) {
					currentCamera.enabled = false;
				}
				currentCamera = attackCamera;
				attackCamera.enabled = true;
				if (mainCamera && mainCamera.enabled == true)
					mainCamera.enabled = false;
			}
		}
	}

	public void ReleaseCamera(){
		isFraming = false;
		if(mainCamera && mainCamera.enabled == false)
		mainCamera.enabled = true;
		if(currentCamera && currentCamera.enabled == true)
		currentCamera.enabled = false;
		currentCamera = null;
	}



}

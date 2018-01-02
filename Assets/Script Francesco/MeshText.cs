using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshText : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Camera.current) {
			Camera camera = Camera.current;
			transform.LookAt (camera.transform);	
			transform.Rotate (Vector3.up - new Vector3 (0, 180, 0));
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicLayer : MonoBehaviour {

	public DynamicLayerHandler dlh;

	// Use this for initialization
	void Start () {
		dlh.AddToList (this);
	}
	
	public void ChangeLayer(int i){
		gameObject.layer = i;
	}
}

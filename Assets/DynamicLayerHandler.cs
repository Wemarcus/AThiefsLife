using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicLayerHandler : MonoBehaviour {

	public List<DynamicLayer> objectList;

	void Awake(){
		objectList = new List<DynamicLayer> ();
	}

	public void ChangeLayerType(int i){
		foreach (DynamicLayer obj in objectList) {
			obj.ChangeLayer (i);
		}
	}

	public void AddToList(DynamicLayer dl){
		objectList.Add (dl);
	}
}

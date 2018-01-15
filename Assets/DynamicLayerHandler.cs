using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicLayerHandler : MonoBehaviour {

	List<DynamicLayer> objectList;

	public void ChangeLayerType(int i){
		foreach (DynamicLayer obj in objectList) {
			obj.ChangeLayer (i);
		}
	}

	public void AddToList(DynamicLayer dl){
		objectList.Add (dl);
	}
}

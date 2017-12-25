using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeRangeHandler : MonoBehaviour {

	MapHandler mh;
	GameObject target;
	MeshRenderer mr;

	// Use this for initialization
	void Start () {
		mh = FindObjectOfType<MapHandler> ();
		mr = GetComponentInChildren<MeshRenderer> ();

		if (mh.CurrentTarget != null && mh.selectedWeapon.getWeaponType () == WeaponType.AoE && mh.inputS == InputState.Attack) {
			target = mh.CurrentTarget;
			this.gameObject.transform.position = target.transform.position;
			this.transform.localScale = new Vector3 (mh.selectedWeapon.getRange ()/2.5f, mh.selectedWeapon.getRange ()/2.5f, mh.selectedWeapon.getRange ()/2.5f);
			mr.enabled = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (mh.CurrentTarget && mh.CurrentTarget != target) {
			target = mh.CurrentTarget;
			this.gameObject.transform.position = target.transform.position;
		}
		if (mh.CurrentTarget == null || mh.inputS != InputState.Attack || mh.selectedWeapon == null || mh.selectedWeapon.getWeaponType() != WeaponType.AoE) {
			Destroy (this.gameObject);
		}
	}
}

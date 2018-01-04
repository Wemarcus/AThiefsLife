using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank_Button : MonoBehaviour {

	public Bank bank;

	public void Click(){
		GetComponentInParent<CarrierMenu> ().currentBank = bank;
	}
}

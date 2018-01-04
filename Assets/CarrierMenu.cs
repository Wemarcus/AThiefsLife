using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrierMenu : MonoBehaviour {

	public Bank currentBank;

	public void LoadScene(){
		currentBank.LoadLevel ();
	}
}

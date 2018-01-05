using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pay_Bail : MonoBehaviour {

	public void PayBail(){
		CurrentGame.cg.PayBail (CurrentGame.cg.end.caution);
	}
}

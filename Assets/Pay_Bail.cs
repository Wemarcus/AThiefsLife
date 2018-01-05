using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pay_Bail : MonoBehaviour {

	public void PayBail(){
		if (MoneyAreEnought()) { // if se i soldi sono sufficenti a pagare la cauzione
			CurrentGame.cg.PayBail (CurrentGame.cg.end.caution);
		} else { // else se non hai abbastanza soldi
			
		}
	}

	public bool MoneyAreEnought(){
		bool b = false;
		if (CurrentGame.cg.money >= CurrentGame.cg.end.caution) {
			b = true;
		}
		return b;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pay_Bail : MonoBehaviour {

    public GameObject moneyMenu; // popup non abbastanza soldi
    public GameObject bailMenu; // popup pagamento cauzione

    public GameObject fakeBackground;
    public GameObject escapeButton;
    public GameObject sentenceButton;
    public GameObject bailButton;
    public GameObject prisonDoor;

	public void PayBail(){
		if (MoneyAreEnought()) { // if se i soldi sono sufficenti a pagare la cauzione
			CurrentGame.cg.PayBail (CurrentGame.cg.end.caution);

            fakeBackground.SetActive(true);
            bailMenu.SetActive(false);
            escapeButton.GetComponent<Button>().interactable = false;
            sentenceButton.GetComponent<Button>().interactable = false;
            bailButton.GetComponent<Button>().interactable = false;
            prisonDoor.GetComponent<SlerpRotation>().OpenDoor();

        } else { // else se non hai abbastanza soldi
            bailMenu.SetActive(false);
            moneyMenu.SetActive(true);
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

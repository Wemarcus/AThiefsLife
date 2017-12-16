using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum ButtonType{
	attack1,
	attack2,
	move,
	action1,
	action2
}

public class ButtonDetailsHandler : MonoBehaviour {

	public GameObject popUp;
	private MapHandler mh;
	public ButtonType bt;
	bool isOver;

	void Start(){
		mh = FindObjectOfType<MapHandler> ();
	}

	/*public void OnPointerEnter(PointerEventData eventData)
	{
		if (!isOver) {
			Debug.Log ("Mouse enter");
			isOver = true;
			popUp.SetActive (true);
			PopUpDetails pud = popUp.GetComponent<PopUpDetails> ();
			pud.SetTitle ("void");
			SetDescriptionAndCDText (pud);
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (isOver) {
			Debug.Log ("Mouse exit");
			popUp.SetActive (false);
			isOver = false;
		}
	}*/

	public void OnMouseOverBtn(){
		Debug.Log ("over");
		if (mh.selectedPlayer != null) {
			popUp.SetActive (true);
			PopUpDetails pud = popUp.GetComponent<PopUpDetails> ();
			SetDescriptionAndCDText (pud);
		}
	}

	public void OnMouseExitBtn(){
		Debug.Log ("exit");
		popUp.SetActive (false);
	}

	void SetDescriptionAndCDText(PopUpDetails pud){
		switch (bt) {
		case ButtonType.attack1:
			pud.SetTitle (mh.selectedPlayer.GetComponent<Player>().firstWeapon.wpnName.ToString());
			pud.SetDescription (mh.selectedPlayer.GetComponent<Player> ().firstWeapon.GetDamageWithBuff().ToString () + " Damage");
			pud.SetCD (mh.selectedPlayer.GetComponent<Player> ().firstWeapon.cooldown);
			break;
		case ButtonType.attack2:
			pud.SetTitle (mh.selectedPlayer.GetComponent<Player>().secondWeapon.wpnName.ToString());
			pud.SetDescription( mh.selectedPlayer.GetComponent<Player> ().secondWeapon.wpnDescription.ToString ());
			pud.SetCD (mh.selectedPlayer.GetComponent<Player> ().secondWeapon.cooldown);
			break;
		case ButtonType.move:
			pud.SetTitle ("Movement");
			pud.SetDescription (mh.selectedPlayer.GetComponent<Player> ().getMoveRange ().ToString () + " Tiles");
			pud.SetCD (0);
			break;
		case ButtonType.action1:
			pud.SetTitle (mh.selectedPlayer.GetComponent<Player>().firstAction.actionName.ToString());
			pud.SetDescription( mh.selectedPlayer.GetComponent<Player> ().firstAction.description.ToString ());
			pud.SetCD (mh.selectedPlayer.GetComponent<Player> ().firstAction.cooldown);
			break;
		case ButtonType.action2:
			pud.SetTitle (mh.selectedPlayer.GetComponent<Player>().secondAction.actionName.ToString());
			pud.SetDescription( mh.selectedPlayer.GetComponent<Player> ().secondAction.description.ToString ());
			pud.SetCD (mh.selectedPlayer.GetComponent<Player> ().secondAction.cooldown);
			break;
		}
	}
}

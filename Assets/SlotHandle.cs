using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotHandle : MonoBehaviour {

	public int slotIndex;

	public Text bossName;

	public GameObject save;
    public GameObject empty_container;
	public Text age;
	public Text patrimony;
	public Text robberies;
	public Text arrested;

    // Aggiunte Marco
    public GameObject fakebackground;
    public GameObject delete;
    public GameObject load;
    public GameObject save_button;

	// Use this for initialization
	public void OnEnable() {
		if (gameObject) {
			bossName.text = SaveAndLoad.sal.saveList [slotIndex].bossName;
			if (SaveAndLoad.sal.saveList [slotIndex].full) {
				save.SetActive (true);
				empty_container.SetActive (false);
				age.text = SaveAndLoad.sal.saveList [slotIndex].age.ToString ();
				patrimony.text =ServiceLibrary.ReturnDotOfInt(SaveAndLoad.sal.saveList [slotIndex].money);
				robberies.text = SaveAndLoad.sal.saveList [slotIndex].robberies.ToString ();
				arrested.text = SaveAndLoad.sal.saveList [slotIndex].arrested.ToString ();
			} else {
				save.SetActive (false);
				empty_container.SetActive (true);
			}
		}
	}

	void SetSlotIndex(){
		CurrentGame.cg.actualSlot = this;
	}

	public void clickToLoadAndDelete(){
		if (SaveAndLoad.sal.saveList [slotIndex].full) {
            // Aggiunte Marco
            fakebackground.SetActive(false);
            delete.GetComponent<Button>().interactable = true;
            load.GetComponent<Button>().interactable = true;
            //
            SetSlotIndex();
		}
	}

	public void clickToSave(){
        // Aggiunte Marco
        fakebackground.SetActive(false);
        save_button.GetComponent<Button>().interactable = true;
        //
		SetSlotIndex ();
	}
}

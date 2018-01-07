using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotHandle : MonoBehaviour {

	public int slotIndex;

	public Text bossName;

	public GameObject save;
	public Text age;
	public Text patrimony;
	public Text robberies;
	public Text arrested;

	// Use this for initialization
	void OnEnable() {
		bossName.text = SaveAndLoad.sal.saveList [slotIndex].bossName;
		if (SaveAndLoad.sal.saveList [slotIndex].full) {
			save.SetActive (true);
			age.text = SaveAndLoad.sal.saveList [slotIndex].age.ToString();
			patrimony.text = SaveAndLoad.sal.saveList [slotIndex].money.ToString();
			robberies.text = SaveAndLoad.sal.saveList [slotIndex].robberies.ToString();
			arrested.text = SaveAndLoad.sal.saveList [slotIndex].arrested.ToString();
		}
	}

	void SetSlotIndex(){
		CurrentGame.cg.actualSlot = slotIndex;
	}
}

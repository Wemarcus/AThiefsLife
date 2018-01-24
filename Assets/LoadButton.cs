using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LoadButton : MonoBehaviour {

	public GameObject load_background;
	public GameObject load_menu;
	public GameObject camera_home;
	public GameObject camera_carrier;
	public GameObject npc_home;
	public GameObject npc_carrier;
	public GameObject carrier_background;
	public GameObject carrier_menu;
	public GameObject play_button;
	public GameObject delete_button;
	public GameObject boss;
	public GameObject sniper;

	public void OnClickLoad(){
		SaveAndLoad.sal.LoadGame ();
		load_background.SetActive(false);
		load_menu.SetActive(false);
		camera_home.SetActive(false);
		camera_carrier.SetActive(true);
		npc_home.SetActive(false);
		npc_carrier.SetActive(true);
		carrier_background.SetActive(true);
		carrier_menu.SetActive(true);
		play_button.GetComponent<Button>().interactable = false;
		delete_button.GetComponent<Button>().interactable = false;
		boss.GetComponent<NPC_Menu>().Start();
		sniper.GetComponent<NPC_Menu_2>().Start();
        boss.GetComponent<AgingMenu>().SetBossColor();
	}
}

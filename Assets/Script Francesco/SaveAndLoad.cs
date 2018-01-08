using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveAndLoad : MonoBehaviour {

	public static SaveAndLoad sal;

	public List<SerializableGame> saveList;

    // Aggiunte marco delete
    public GameObject back;
    public GameObject delete_popup;
    public GameObject fakebackground;

    // Aggiunte marco load

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

    // Aggiunte marco save
    public GameObject save_menu;
    public GameObject fake_background_2;
    public GameObject back_button;
    public GameObject save_button;

	void Awake () {
		if (sal == null) {
			DontDestroyOnLoad (gameObject);
			sal = this;
		}else if(sal != this){
			Destroy (gameObject);
		}
	}

	public void Start(){
		saveList = new List<SerializableGame> ();
		SerializableGame sg = new SerializableGame ();
		sg.bossName = "Empty";
		saveList.Add (sg);
		saveList.Add (sg);
		saveList.Add (sg);
		saveList.Add (sg);
		LoadData ();
	}

	public void SaveData(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/playerInfo.dat");
		SaveList data = new SaveList ();
		data.saveList = saveList;
		bf.Serialize (file, data);
		file.Close ();
	}

	public void LoadData(){
		if (File.Exists (Application.persistentDataPath + "/playerInfo.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
			SaveList data = (SaveList)bf.Deserialize (file);
			file.Close();
			saveList = data.saveList;
		}
	}
		
	public void SaveGame(){
		saveList[CurrentGame.cg.actualSlot.slotIndex] = CurrentGame.cg.SerializeGame();
		CurrentGame.cg.actualSlot.OnEnable ();
		SaveData ();
		// Aggiunta marco
		fake_background_2.SetActive(true);
		save_menu.SetActive(false);
		back_button.GetComponent<Button>().interactable = true;
		save_button.GetComponent<Button>().interactable = false;
		//
	}
		
	public void LoadGame(){
		CurrentGame.cg.LoadGame(saveList[CurrentGame.cg.actualSlot.slotIndex]);
		CurrentGame.cg.actualSlot.OnEnable ();
		// Aggiunta marco
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
		//
	}

	public void DeleteGame(){
        SerializableGame sg = new SerializableGame();
		sg.bossName = "Empty";
		saveList [CurrentGame.cg.actualSlot.slotIndex] = sg;
		SaveData ();
		CurrentGame.cg.actualSlot.OnEnable ();
		// Aggiunta marco
		fakebackground.SetActive(true);
		back.GetComponent<Button>().interactable = true;
		delete_popup.SetActive(false);
		//
	}

	void OnGUI(){
		if(saveList.Count>0)
		GUI.Label (new Rect (10,30,200,30), "Boss Name: " + saveList [0].bossName);
		if(saveList.Count>1)
		GUI.Label (new Rect (10, 50, 200, 30), "Boss Name: " + saveList [1].bossName);
		if(saveList.Count>2)
		GUI.Label (new Rect (10, 70, 200, 30), "Boss Name: " + saveList [2].bossName);
		if(saveList.Count>3)
		GUI.Label (new Rect (10, 90, 200, 30), "Boss Name: " + saveList [3].bossName);
		if (GUI.Button (new Rect (10, 320, 100, 30), "Save on slot 1")) {
			SaveGame ();
		}
		if (GUI.Button (new Rect (10, 360, 100, 30), "Delete on slot 1")) {
			DeleteGame ();
		}
		if (GUI.Button (new Rect (10, 390, 100, 30), "Load on slot 1")) {
			LoadGame ();
		}
	}
}

[Serializable]
public class SaveList{

	public List<SerializableGame> saveList;
}

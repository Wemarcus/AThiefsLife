using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveAndLoad : MonoBehaviour {

	public static SaveAndLoad sal;

	public List<SerializableGame> saveList;

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
		
	public void SaveGame(int pos){
		//cambia il salvataggio corrente di savelist e pusha il cambiamento
		saveList[pos] = CurrentGame.cg.SerializeGame();
		SaveData ();
	}
		
	public void LoadGame(int pos){
		//carica il gioco e sovrascrivi current game + passa a menu di gioco
		CurrentGame.cg.LoadGame(saveList[pos]);
	}

	public void DeleteGame(int pos){
		//sostituisci un elemento di savelist e pusha il cambiamento
		SerializableGame sg = new SerializableGame();
		saveList [pos] = sg;
		SaveData ();
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
			SaveGame (0);
		}
		if (GUI.Button (new Rect (10, 360, 100, 30), "Delete on slot 1")) {
			DeleteGame (0);
		}
		if (GUI.Button (new Rect (10, 390, 100, 30), "Load on slot 1")) {
			LoadGame (0);
		}
	}
}

[Serializable]
public class SaveList{

	public List<SerializableGame> saveList;
}

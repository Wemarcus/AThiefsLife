using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

[CreateAssetMenu(menuName = "A Thief's Life/Bank")]
[Serializable]
public class Bank : ScriptableObject {

	public string bankName;
	public int securityLevel;
	public string sceneName;


	public void IncreaseBankSecurity(){
		if (securityLevel < 3)
			securityLevel++;
	}

	public void DecreaseBankSecurity(){
		if (securityLevel > 0)
			securityLevel--;
	}

	public void LoadLevel(){
		SceneManager.LoadScene (sceneName, LoadSceneMode.Single);
	}

	public void Reset(){
		securityLevel = 0;
	}
}

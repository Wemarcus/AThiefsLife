using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailerAI : MonoBehaviour {

    public GameObject boss;
    public GameObject sniper;
    public GameObject tank;
    public GameObject doctor;
    public Transform[] target;

	void Start () {
        boss.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControlTrailer>().SetTarget(target[0]);
        sniper.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControlTrailer>().SetTarget(target[1]);
        tank.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControlTrailer>().SetTarget(target[2]);
        doctor.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControlTrailer>().SetTarget(target[3]);
    }
	
	void Update () {
		
	}
}

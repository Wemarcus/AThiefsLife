using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeTrailer : MonoBehaviour {

    public GameObject[] prefab;
    public Transform[] position;
    public Transform[] target;

    public GameObject swat_1;
    public GameObject swat_2;
    public GameObject swat_3;
    public GameObject swat_4;

    private bool block;
    private bool block_2;

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Boss" && !block)
        {
            block = true;
            Instantiate(prefab[0], position[0]).GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControlTrailer>().SetTarget(target[0]);
            Instantiate(prefab[1], position[1]).GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControlTrailer>().SetTarget(target[1]);
            Instantiate(prefab[0], position[2]).GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControlTrailer>().SetTarget(target[2]);
            Instantiate(prefab[1], position[3]).GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControlTrailer>().SetTarget(target[3]);
        }
    }

    public void SWAT()
    {
        if (!block_2)
        {
            block_2 = true;
            swat_1 = Instantiate(prefab[2], position[4]);
            swat_2 = Instantiate(prefab[2], position[5]);
            swat_3 = Instantiate(prefab[2], position[6]);
            swat_4 = Instantiate(prefab[2], position[7]);
        }
    }

}

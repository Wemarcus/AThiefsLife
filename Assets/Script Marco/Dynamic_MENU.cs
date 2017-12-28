using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamic_MENU : MonoBehaviour {

    public GameObject prefab;
    public Transform position;
    public Transform target;
    public float time;

    private bool block;
    private bool block_2;
    private GameObject tmp;
	
	void Update () {
        if (!block)
        {
            block = true;
            tmp = Instantiate(prefab, position);
            StartCoroutine(Time());
        }

        if (!block_2 && tmp.GetComponent<UnityStandardAssets.Characters.ThirdPerson.Dynamic_Menu_Swat>().floor)
        {
            block_2 = true;
            tmp.GetComponent<UnityStandardAssets.Characters.ThirdPerson.Dynamic_Menu_Swat>().agent.enabled = true;
            tmp.GetComponent<UnityStandardAssets.Characters.ThirdPerson.Dynamic_Menu_Swat>().SetTarget(target);
        }
    }

    private IEnumerator Time()
    {
        yield return new WaitForSeconds(time);
        block = false;
        block_2 = false;
    }
}

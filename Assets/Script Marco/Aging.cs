using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aging : MonoBehaviour {

    public int age; // variabile di test, sostituire con vero valore dal menu
    public GameObject hair;
    public GameObject beard;
    public GameObject eyebrows;

    private Player player;

    void Start () {
        if ((player = GetComponent<Player>()) != null)
        {
            SetBossColor();
        }
    }
    
    // Funzione Update solo per test
    /*private void Update()
    {
        SetBossColor();
    }*/

    void SetBossColor()
    {
        if (age >= 30 && age < 45)
        {
            // capelli
            hair.GetComponent<MeshRenderer>().material.SetColor("_Color", new Color32(56, 37, 24, 255));
            beard.GetComponent<MeshRenderer>().material.SetColor("_Color", new Color32(56, 37, 24, 255));
            eyebrows.GetComponent<MeshRenderer>().material.SetColor("_Color", new Color32(56, 37, 24, 255));
        }
        else if (age >= 45 && age < 60)
        {
            // capelli e passi -1
            hair.GetComponent<MeshRenderer>().material.SetColor("_Color", new Color32(71, 57, 48, 255));
            beard.GetComponent<MeshRenderer>().material.SetColor("_Color", new Color32(71, 57, 48, 255));
            eyebrows.GetComponent<MeshRenderer>().material.SetColor("_Color", new Color32(71, 57, 48, 255));
            player.moveRange = 8;
        }
        else if (age >= 60 && age < 70)
        {
            // capelli e passi -2
            hair.GetComponent<MeshRenderer>().material.SetColor("_Color", new Color32(96, 89, 84, 255));
            beard.GetComponent<MeshRenderer>().material.SetColor("_Color", new Color32(96, 89, 84, 255));
            eyebrows.GetComponent<MeshRenderer>().material.SetColor("_Color", new Color32(96, 89, 84, 255));
            player.moveRange = 7;
        }
        else if (age >= 70 && age < 90)
        {
            // capelli e passi -3
            hair.GetComponent<MeshRenderer>().material.SetColor("_Color", new Color32(173, 170, 169, 255));
            beard.GetComponent<MeshRenderer>().material.SetColor("_Color", new Color32(173, 170, 169, 255));
            eyebrows.GetComponent<MeshRenderer>().material.SetColor("_Color", new Color32(173, 170, 169, 255));
            player.moveRange = 6;
        }
        else if (age >= 90)
        {
            // capelli e passi -4
            hair.GetComponent<MeshRenderer>().material.SetColor("_Color", new Color32(255, 255, 255, 255));
            beard.GetComponent<MeshRenderer>().material.SetColor("_Color", new Color32(255, 255, 255, 255));
            eyebrows.GetComponent<MeshRenderer>().material.SetColor("_Color", new Color32(255, 255, 255, 255));
            player.moveRange = 5;
        }
    }
	
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Lightning : MonoBehaviour {

    public GameObject selected;

    void OnMouseOver()
    {
        selected.SetActive(true);
    }

    void OnMouseExit()
    {
        selected.SetActive(false);
    }
}

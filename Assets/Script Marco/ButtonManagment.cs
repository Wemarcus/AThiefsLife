using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManagment : MonoBehaviour {

    public GameObject Fake_Background;
    public Button Delete;
    public Button Play;

    public void ChangeButtonStatus()
    {
        Fake_Background.SetActive(false);
        Delete.interactable = true;
        Play.interactable = true;
    }
}

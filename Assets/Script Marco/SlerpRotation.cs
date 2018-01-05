using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlerpRotation : MonoBehaviour {

    // Script cambio menu
    public GameObject cameraNew;
    public GameObject cameraOld;
    public GameObject backgroundOld;
    public GameObject backgroundNew;
    public GameObject menuOld;
    public GameObject menuNew;
    public GameObject npcNew;

    // Script riattivazione oggetti
    public GameObject fakeBackground;
    public GameObject escapeButton;
    public GameObject sentenceButton;
    public GameObject bailButton;

    // Script Rotazione Porta
    public GameObject Door;
    public float rotation; //rotazione della porta
    public float speed;
    public bool open;

    private Vector3 defaultRot;
    private Vector3 openRot;

    public void Start()
    {
        defaultRot = Door.transform.eulerAngles;
        openRot = new Vector3(defaultRot.x, defaultRot.y + rotation, defaultRot.z);
    }

    public void Update()
    {
        if (open)
        {
            Door.transform.eulerAngles = Vector3.Slerp(Door.transform.eulerAngles, openRot, Time.deltaTime * speed);
        }
        else
        {
            Door.transform.eulerAngles = Vector3.Slerp(Door.transform.eulerAngles, defaultRot, Time.deltaTime * speed);
        }
    }

    public void OpenDoor()
    {
        open = true;

        StartCoroutine(ChangeMenu());
    }

    public void CloseDoor()
    {
        open = false;
    }

    private IEnumerator ChangeMenu()
    {
        yield return new WaitForSeconds(8.0f);

        CloseDoor();

        // cambio menu
        cameraNew.SetActive(true);
        cameraOld.SetActive(false);
        backgroundNew.SetActive(true);
        backgroundOld.SetActive(false);
        menuNew.SetActive(true);
        menuOld.SetActive(false);
        npcNew.SetActive(true);

        // riattivazioni varie
        fakeBackground.SetActive(false);
        escapeButton.GetComponent<Button>().interactable = true;
        sentenceButton.GetComponent<Button>().interactable = true;
        bailButton.GetComponent<Button>().interactable = true;
    }
}

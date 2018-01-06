using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackToMenu : MonoBehaviour {

    // Cameras
    public GameObject cameraHome;
    public GameObject cameraCarrier;
    public GameObject cameraPrison;

    // NPC Animations
    public GameObject npcHome;
    public GameObject npcCarrier;
    public GameObject npcPrison;
    public GameObject Boss_Carrier_Home;
    public GameObject Sniper_Carrier_Home;

    // Background
    public GameObject backgroundHome;
    public GameObject backgroundCarrier;
    public GameObject backgroundPrison;

    // Menu Button
    public GameObject buttonHome;
    public GameObject buttonCarrier;
    public GameObject buttonPrison;

    // Dead
    public GameObject deathPanel;
    public GameObject teamButton;
    public GameObject robberiesButton;
    public GameObject saveButton;

	// Use this for initialization
	void Start () {
		Time.timeScale = 1;
		if (CurrentGame.cg.end.robberyEnded) {
			switch (CurrentGame.cg.end.endCase) {
			case EndCases.Null:
				break;

			case EndCases.Run:
                    cameraHome.SetActive(false);
                    cameraCarrier.SetActive(true);
                    cameraPrison.SetActive(false);

                    npcHome.SetActive(false);
                    npcCarrier.SetActive(true);
                    npcPrison.SetActive(false);

                    backgroundHome.SetActive(false);
                    backgroundCarrier.SetActive(true);
                    backgroundPrison.SetActive(false);

                    buttonHome.SetActive(false);
                    buttonCarrier.SetActive(true);
                    buttonPrison.SetActive(false);

                    Boss_Carrier_Home.GetComponent<NPC_Menu>().Start();
                    Sniper_Carrier_Home.GetComponent<NPC_Menu_2>().Start();
				break;

			case EndCases.Arrested:
                    cameraHome.SetActive(false);
                    cameraCarrier.SetActive(false);
                    cameraPrison.SetActive(true);

                    npcHome.SetActive(false);
                    npcCarrier.SetActive(true);
                    npcPrison.SetActive(true);

                    backgroundHome.SetActive(false);
                    backgroundCarrier.SetActive(false);
                    backgroundPrison.SetActive(true);

                    buttonHome.SetActive(false);
                    buttonCarrier.SetActive(false);
                    buttonPrison.SetActive(true);

                    Boss_Carrier_Home.GetComponent<NPC_Menu>().Start();
                    Sniper_Carrier_Home.GetComponent<NPC_Menu_2>().Start();
                    break;

			case EndCases.Died:
                    cameraHome.SetActive(false);
                    cameraCarrier.SetActive(true);
                    cameraPrison.SetActive(false);

                    npcHome.SetActive(false);
                    npcCarrier.SetActive(true);
                    npcPrison.SetActive(false);

                    backgroundHome.SetActive(false);
                    backgroundCarrier.SetActive(true);
                    backgroundPrison.SetActive(false);

                    buttonHome.SetActive(false);
                    buttonCarrier.SetActive(true);
                    buttonPrison.SetActive(false);

                    Boss_Carrier_Home.SetActive(false);
                    Sniper_Carrier_Home.SetActive(false);

                    deathPanel.SetActive(true);  // METTERE FADE DELL'IMAGE
                    teamButton.GetComponent<Button>().interactable = false;
                    robberiesButton.GetComponent<Button>().interactable = false;
                    saveButton.GetComponent<Button>().interactable = false;

                    break;
			}
			CurrentGame.cg.end.Reset ();
		}
	}
}

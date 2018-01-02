using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour {

    public void Exit_Game()
    {
        Debug.Log("Exit Game");
        Application.Quit();
    }
}

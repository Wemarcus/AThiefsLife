using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class reinforcements_Camera : MonoBehaviour {

    private PlayableDirector playable_director;
    private bool control;

    public void Start()
    {
        if (GetComponent<PlayableDirector>() != null)
        {
            playable_director = GetComponent<PlayableDirector>();
            control = true;
        }
    }

    public void CameraActivate()
    {
        if (GetComponent<Cinematic>() != null)
        {
            Debug.Log("Quante volte entro?");
            GetComponent<Cinemachine.CinemachineVirtualCamera>().enabled = true;
            GetComponent<Cinematic>().isRunning = true;
            //GetComponent<Cinemachine.CinemachineTrackedDolly>().m_PathPosition = 0;
            if (control)
            {
                playable_director.time = 0;
                playable_director.enabled = true;
            }
            else
            {
                StartCoroutine(FakeCamera());
            }
        }
    }

    private IEnumerator FakeCamera()
    {
        yield return new WaitForSeconds(5.0f);
        GetComponent<Cinemachine.CinemachineVirtualCamera>().enabled = false;
        GetComponent<Cinematic>().isRunning = false;
    }
}

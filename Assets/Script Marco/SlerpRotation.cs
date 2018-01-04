using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlerpRotation : MonoBehaviour {

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
    }

    public void CloseDoor()
    {
        open = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTrailer : MonoBehaviour
{

    public GameObject Door;
    public float speed;
    public bool SlerpDoor;
    public float rotation; //rotazione della porta
    private Vector3 defaultRot;
    private Vector3 openRot;
    private bool openDoor;

    void Start()
    {
        defaultRot = Door.transform.eulerAngles;
        openRot = new Vector3(defaultRot.x, defaultRot.y + rotation, defaultRot.z);
    }

    private void Update()
    {
        if (openDoor)
        {
            Door.transform.eulerAngles = Vector3.Slerp(Door.transform.eulerAngles, openRot, Time.deltaTime * speed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(OpenCaveau());
            Destroy(gameObject, 10.0f);
        }
    }

    private IEnumerator OpenCaveau()
    {
        yield return new WaitForSeconds(3.0f);
        openDoor = true;
    }
}

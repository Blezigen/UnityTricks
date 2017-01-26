using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListnerCollider : MonoBehaviour
{

    public GameObject TransportToGameObject;
    public bool TrigerEnter = false;
    public bool TrigerExit = false;

    void OnTriggerEnter (Collider other) {
        if (TrigerEnter)
        {
            TransportToGameObject.SendMessage("Activate");
        }
    }

    void OnTriggerExit(Collider other) {
        if (TrigerExit)
        {
            TransportToGameObject.SendMessage("DiActivate");
        }
    }

    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

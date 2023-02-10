using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this script handles camera transitions. When players enter a room's trigger collider, the corresponding camera is activated. When they leave, it's deactivated.
// (note that these cameras are cinemachine cameras)
public class roomTransition : MonoBehaviour
{

    public GameObject roomCamera;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            roomCamera.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            roomCamera.SetActive(false);
        }
    }
}

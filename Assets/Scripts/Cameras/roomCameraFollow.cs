using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

// This script is used in the player-following camera. It basically just sets the follow property of the cinemachine virtual camera to follow the player.
// We had to do this via script rather than in the inspector because of the way the spawning system works- the player may not exist at Start() if it hasn't spawned yet.
public class roomCameraFollow : MonoBehaviour
{
    private GameManager gm;
    private CinemachineVirtualCamera cam;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.instance;
        cam = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        // Find the player and set the follow property of the camera to it
        cam.Follow = FindObjectOfType<playermovement>().gameObject.transform;

        // If I found the player, delete this script (its job is done)
        if(cam.Follow != null)
        {
            Destroy(this);
        }
    }
}

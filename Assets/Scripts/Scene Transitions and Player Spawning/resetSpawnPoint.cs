using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class allows us to set the spawn point of the player somewhere within a scene whenever they run into this GameObject's Collider.
// It basically acts as a checkpoint- if the player dies somewhere in this scene, they'll respawn here.
public class resetSpawnPoint : MonoBehaviour
{
    public string spawnPointName = null;     // unique ID of the spawn point (set in inspector)
    private GameManager gm;

    void Start()
    {
        gm = GameManager.instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            gm.lastSceneName = spawnPointName;
            gm.lastWaterLevel = gm.waterLevel;
            gm.lastLavaLevel = gm.lavaLevel;
        }
    }
}

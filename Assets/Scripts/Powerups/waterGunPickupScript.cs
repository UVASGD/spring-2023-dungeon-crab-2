using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is the script for the water gun pickup.
public class waterGunPickupScript : MonoBehaviour
{
    private GameManager gm;

    // Don't appear if the player already has the water gun
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        if (gm.waterGunUnlocked)
        {
            gameObject.SetActive(false);
        }
    }

    // rotating animation
    void Update()
    {
        this.transform.Rotate(new Vector3(0, 0, 1));
    }

    // get the water gun if the player walks into the collider trigger
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            WaterGunControl wgc;
            if ((wgc = other.GetComponent<WaterGunControl>()) != null)
            {
                wgc.getWaterGun();
                if (AudioManager.instance)
                {
                    AudioManager.instance.Play("Pickup");
                }
            }
            gameObject.SetActive(false);
        }
    }
}

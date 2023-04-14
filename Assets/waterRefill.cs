using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterRefill : MonoBehaviour
{
    // this class is for the trigger box surrounding the water gun. When it touches fire or water it fills the respective ammo type for the player.
    public void OnTriggerEnter(Collider other)
    {
        // (layer 11 is for player-made projectiles- we don't want shot out projectiles to refill ammo)
        if (other.tag == "Water" && other.gameObject.layer != 11)
        {
            GameManager.instance.waterGunAmmo = 50;
        }
        else if (other.tag == "Fire" && other.gameObject.layer != 11)
        {
            GameManager.instance.flamethrowerAmmo = 50;
        }
    }
}

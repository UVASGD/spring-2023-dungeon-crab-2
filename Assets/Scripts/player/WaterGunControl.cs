using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGunControl : MonoBehaviour
{
    public GameObject WaterGunProjectile;
    public GameObject FlamethrowerProjectile;
    public GameObject WaterGunModel;
    public float coolDownTime = 1;
    public float spawnDistance = 0.2f;
    public float spawnSpeed = 0f;
    public float coolDown = 0;
    public float spread = 1f;

    private GameManager gm;
    private CharacterController thisPlayerController;
    private bool keyDown = false;
    private bool leftKeyDown = false;

    private bool canShoot = true;

    // Start is called before the first frame update
    void Start()
    {
        thisPlayerController = GetComponent<CharacterController>();
        gm = GameManager.instance;
        if (!gm.waterGunUnlocked && !gm.flamethrowerUnlocked)
        {
            WaterGunModel.SetActive(false);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (canShoot && (gm.waterGunUnlocked || gm.flamethrowerUnlocked))
        {
            if (Input.GetKey(KeyCode.RightShift))
            {
                keyDown = true;
            }
            else
            {
                keyDown = false;
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                leftKeyDown = true;
            }
            else
            {
                leftKeyDown = false;
            }

            if ((leftKeyDown) && coolDown <= 0f && gm.waterGunAmmo > 0 && gm.waterGunUnlocked)
            {
                // shoot out water
                GameObject water = (GameObject)Instantiate(WaterGunProjectile, transform.position + transform.forward * spawnDistance + new Vector3(0, 0.2f, 0f), transform.rotation);
                Rigidbody rb;
                if ((rb = water.GetComponent<Rigidbody>()) != null)
                {
                    rb.velocity = (new Vector3(Random.Range(transform.forward.x + spread, transform.forward.x - spread), Random.Range(transform.forward.y + spread, transform.forward.y - spread), Random.Range(transform.forward.z + spread, transform.forward.z - spread)).normalized + new Vector3(0, 0.3f, 0)) * spawnSpeed * Random.Range(0.9f, 1.1f);
                }
                coolDown = coolDownTime;
                gm.waterGunAmmo--;

            }else if(keyDown && coolDown <= 0f && gm.flamethrowerAmmo > 0 && gm.flamethrowerUnlocked)
            {
                // shoot out fire
                GameObject fire = (GameObject)Instantiate(FlamethrowerProjectile, transform.position + transform.forward * spawnDistance + new Vector3(0, 0.2f, 0f), transform.rotation);
                Rigidbody rb;
                if ((rb = fire.GetComponent<Rigidbody>()) != null)
                {
                    rb.velocity = (new Vector3(Random.Range(transform.forward.x + spread, transform.forward.x - spread), Random.Range(transform.forward.y + spread, transform.forward.y - spread), Random.Range(transform.forward.z + spread, transform.forward.z - spread)).normalized + new Vector3(0, 0.3f, 0)) * spawnSpeed * Random.Range(0.9f, 1.1f);
                }
                coolDown = coolDownTime;
                gm.flamethrowerAmmo--;
            }

            if (coolDown > 0f)
            {
                coolDown -= Time.deltaTime;
            }
        }

    }

    // call this to make the water gun work
    public void getWaterGun()
    {
        gm.waterGunUnlocked = true;
        WaterGunModel.SetActive(true);
    }

    // call this to make the water gun work
    public void getFlamethrower()
    {
        gm.flamethrowerUnlocked = true;
        WaterGunModel.SetActive(true);
    }

    // set whether the player can shoot either projectile at al; this is set to false when grabbing something
    public void setCanShoot(bool shouldBeAbleToShoot)
    {
        canShoot = shouldBeAbleToShoot;
        if(canShoot && (gm.flamethrowerUnlocked || gm.waterGunUnlocked))
        {
            WaterGunModel.SetActive(true);
        }
        else
        {
            WaterGunModel.SetActive(false);
        }
    }

    // When the player touches fire or water it fills the respective ammo type for the player.
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

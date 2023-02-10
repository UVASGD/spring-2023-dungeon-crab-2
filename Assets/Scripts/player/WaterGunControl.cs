using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGunControl : MonoBehaviour
{
    public GameObject WaterGun;
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

    // Start is called before the first frame update
    void Start()
    {
        thisPlayerController = GetComponent<CharacterController>();
        gm = GameManager.instance;
        if (!gm.waterGunUnlocked)
        {
            WaterGunModel.SetActive(false);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (gm.waterGunUnlocked)
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

            if ((keyDown || leftKeyDown) && coolDown <= 0f && gm.waterGunAmmo > 0)
            {
                // shoot out water
                GameObject water = (GameObject)Instantiate(WaterGun, transform.position + transform.forward * spawnDistance + new Vector3(0, 0.05f, 0f), transform.rotation);
                Rigidbody rb;
                if ((rb = water.GetComponent<Rigidbody>()) != null)
                {
                    rb.velocity = (new Vector3(Random.Range(transform.forward.x + spread, transform.forward.x - spread), Random.Range(transform.forward.y + spread, transform.forward.y - spread), Random.Range(transform.forward.z + spread, transform.forward.z - spread)).normalized + new Vector3(0, 0.3f, 0)) * spawnSpeed * Random.Range(0.9f, 1.1f);
                }
                coolDown = coolDownTime;
                gm.waterGunAmmo--;

                /*
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                nextSpark.SendMessage("setupDir", nextDirection);
                nextSpark.SendMessage("setupSpark", spark);
                nextSpark.SendMessage("setupIntense", 150);
                nextSpark.SendMessage("setJoint");*/

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

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Water")
        {
            GameManager.instance.waterGunAmmo = 50;
        }
    }
}

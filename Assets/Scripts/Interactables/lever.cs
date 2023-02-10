using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// script for the levers.
public class lever : MonoBehaviour
{
    public bool leverIsForWater = true; //determines if the lever controls the water level (if it doesn't, it controls the laval level)
    private GameManager gm;
    private bool playerInArea = false;
    public Transform leverFulcrum;
    private int lastLevel;  //saves the last remembered water/lava level

    private float speed = 0.3f;
    private float timeCount = 0.0f;
    private Quaternion targetRotation;
    private float originalZ;

    // Start is called before the first frame update
    void Start()
    {
        //set up everything
        gm = GameManager.instance;
        originalZ = transform.rotation.eulerAngles.z + 45;
        if (leverIsForWater)
        {
            lastLevel = gm.waterLevel;
            if (gm.waterLevel == 0)
            {
                leverFulcrum.localRotation = Quaternion.Euler(0, 0, originalZ - 90f);
            }
            else
            {
                leverFulcrum.localRotation = Quaternion.Euler(0, 0, originalZ);
            }
        }
        else
        {
            lastLevel = gm.lavaLevel;
            if (gm.lavaLevel == 0)
            {
                leverFulcrum.localRotation = Quaternion.Euler(0, 0, originalZ - 90f);
            }
            else
            {
                leverFulcrum.localRotation = Quaternion.Euler(0, 0, originalZ);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //check for player input if they're in the area, change the water/lava level if so
        if (playerInArea && Input.GetKeyDown(KeyCode.E))
        {
            // timeCount = 0f;
            if (leverIsForWater)
            {
                if (gm.waterLevel == 1)
                {
                    gm.setWaterLevel(0);
                }
                else
                {
                    gm.setWaterLevel(1);
                }
            }
            else
            {
                if (gm.lavaLevel == 1)
                {
                    gm.setLavaLevel(0);
                }
                else
                {
                    gm.setLavaLevel(1);
                }
            }
        }

        //reset some values for the lerp if the water/lava level changed
        if (leverIsForWater)
        {
            if (lastLevel != gm.waterLevel)
            {
                timeCount = 0.0f;
                lastLevel = gm.waterLevel;
            }
        }
        else
        {
            if (lastLevel != gm.lavaLevel)
            {
                timeCount = 0.0f;
                lastLevel = gm.lavaLevel;
            }
        }

        //move the fulrum of the lever to match whatever the water/lava level is (using a lerp)
        if ((leverIsForWater && gm.waterLevel == 1) || (!leverIsForWater && gm.lavaLevel == 1))
        {
            targetRotation = Quaternion.Euler(leverFulcrum.rotation.x, leverFulcrum.rotation.y,  originalZ - 90f);
        }
        else
        {
            targetRotation = Quaternion.Euler(leverFulcrum.rotation.x, leverFulcrum.rotation.y, originalZ);
        }
        leverFulcrum.rotation = Quaternion.Lerp(leverFulcrum.rotation, targetRotation, timeCount * speed);
        timeCount = timeCount + Time.deltaTime;
    }

    private void FixedUpdate()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInArea = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInArea = false;
        }
    }
}

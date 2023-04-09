using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ice : MonoBehaviour
{
    public playerGrab pGrab = null;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(transform.localScale.magnitude < 0.1f)
        {
            if (pGrab)
            {
                pGrab.breakJoint();
            }
            Destroy(gameObject);
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Fire"))
        {
            Fire otherFire = other.GetComponent<Fire>();
            playerFire otherPlayerFire = other.GetComponent<playerFire>();
            if ((otherFire != null && otherFire.isFireActive) || (otherPlayerFire != null && otherPlayerFire.isFireActive))
            {
                transform.localScale *= 0.98f;
                if(pGrab != null)
                {
                    pGrab.breakJoint();
                    pGrab = null;
                }
            }
            

        }
        if (other.CompareTag("Lava"))
        {
            transform.localScale *= 0.9f;
            if (pGrab != null)
            {
                pGrab.breakJoint();
                pGrab = null;
            }
        }
    }
}

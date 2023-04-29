using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ice : MonoBehaviour
{
    public float timeToDespawn = 2.0f;
    public playerGrab pGrab = null;
    private ParticleSystem ps;
    private float startDespawnTime = -1f;
    private Rigidbody rb;
    private Collider col;
    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(startDespawnTime > 0)
        {
            if(Time.time > startDespawnTime + timeToDespawn)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if (transform.localScale.magnitude < 0.5f)
            {
                if (pGrab)
                {
                    pGrab.breakJoint();
                }
                GetComponent<Rigidbody>().isKinematic = true;
                GetComponent<MeshRenderer>().enabled = false;
                GetComponent<Collider>().enabled = false;
                ps.Play();
                startDespawnTime = Time.time;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Fire"))
        {
            Fire otherFire = other.GetComponent<Fire>();
            playerFire otherPlayerFire = other.GetComponent<playerFire>();
            if ((otherFire != null && otherFire.isFireActive) || (otherPlayerFire != null && otherPlayerFire.isFireActive) || (otherFire==null && otherPlayerFire==null))
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

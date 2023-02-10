using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is a duplicate of the fire script, with the exception being the type of object it destroys, which is a GameObject instead of a fireParent, and that it also affects the player movement script.
// The reason this script exists is that it was the quickest way to get the fire to kill the player (crunch night is this week, sue me lol- sorry future people)
public class playerFire : MonoBehaviour
{
    public bool isFireActive;
    public ParticleSystem fire;
    public ParticleSystem smoke;
    public ParticleSystem burstSmoke;
    public GameObject fireLight;

    public bool destroyObject = false;  // set to true only if this will destroy an object after a time (ie, box)- destroyed object is usually the parent of the fire
    public GameObject objectToDestroy = null;
    public playermovement playerToDie = null;
    public int loopsBeforeDestroyObject = 250; //max number of loops before destroying object
    private int loopsToDestroy;
    private bool destroyed = false;

    private bool waterlogged;

    private bool isSmokeActive;

    private int loopsBeforeFireStartsOnCoals = 100;
    private int loopsBeforeFire;
    private bool onCoals = false;

    private void Start()
    {
        loopsBeforeFire = loopsBeforeFireStartsOnCoals;
        loopsToDestroy = loopsBeforeDestroyObject;
        if (isFireActive)
        {
            ReLight();
        }
        else
        {
            isFireActive = false;
            fire.Stop();
            smoke.Stop();
            fireLight.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            waterlogged = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            waterlogged = false;
        }
        if (other.CompareTag("Coals"))
        {
            onCoals = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("Water") && isFireActive)
        {
            waterlogged = true;
            Extinguish();
        }
        if (other.CompareTag("Fire") && !isFireActive && !waterlogged)
        {
            Fire otherFire = other.GetComponent<Fire>();
            if (otherFire != null && otherFire.isFireActive)
            {
                ReLight();
            }
        }
        if (other.CompareTag("Lava") && !isFireActive)
        {
            //ReLight();
        }
        if (other.CompareTag("Coals"))
        {
            if (!isFireActive && !waterlogged)
            {
                onCoals = true;
            }
            else
            {
                onCoals = false;
            }

        }
    }

    public void Extinguish()
    {
        playerToDie.fireExtinguished();
        isFireActive = false;
        isSmokeActive = false;
        fire.Stop();
        smoke.Stop();
        burstSmoke.Play();
        fireLight.SetActive(false);
    }

    public void ReLight()
    {
        if (!destroyed)
        {
            playerToDie.setOnFire();
            isFireActive = true;
            isSmokeActive = true;
            smoke.Play();
            fire.Play();
            burstSmoke.Stop();
            fireLight.SetActive(true);
        }
    }
    void Update()
    {
        if (this.transform.parent != null)
        {
            this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, this.transform.parent.rotation.z * -1.0f);
        }
    }
    private void FixedUpdate()
    {
        if (destroyObject)
        {
            if (isFireActive)
            {
                loopsToDestroy--;
            }
            else
            {
                loopsToDestroy = loopsBeforeDestroyObject;
            }

            if (loopsToDestroy < 0)
            {
                if (!destroyed)
                {
                    objectToDestroy.SetActive(false);
                    destroyed = true;
                    Extinguish();
                    playerToDie.Die();
                }
            }
        }


        if (!onCoals)
        {
            loopsBeforeFire = loopsBeforeFireStartsOnCoals;
            if (!isFireActive && isSmokeActive)
            {
                isSmokeActive = false;
                smoke.Stop();
            }
        }


        if (onCoals && loopsBeforeFire <= 0 && !isFireActive)
        {
            ReLight();
            loopsBeforeFire = loopsBeforeFireStartsOnCoals;
        }
        else if (onCoals && !isFireActive)
        {
            if (!isSmokeActive)
            {
                smoke.Play();
                isSmokeActive = true;
            }
            loopsBeforeFire -= 1;
        }
        else
        {
            loopsBeforeFire = loopsBeforeFireStartsOnCoals;
        }
    }
}

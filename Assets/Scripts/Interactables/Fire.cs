using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script for a fire object. The fire is put out in water, lights itself in contact with other fire, and will slowly be lit by touching coals. Can also destroy (burn up) its parent with the correct setting.
public class Fire : MonoBehaviour
{
    public bool isFireActive;
    public ParticleSystem fire;
    public ParticleSystem smoke;
    public ParticleSystem burstSmoke;
    public GameObject fireLight;

    public bool destroyObject = false;  // set to true only if this will destroy an object after a time (ie, box)- destroyed object is usually the parent of the fire
    public flammableParent objectToDestroy = null;
    public int loopsBeforeDestroyObject = 250; //max number of loops before destroying object
    private int loopsToDestroy;
    private bool destroyed = false;

    private bool waterlogged;

    private bool isSmokeActive;

    private int loopsBeforeFireStartsOnCoals = 100;
    private int loopsBeforeFire;
    private bool onCoals = false;

    public int loopsUntilCleanup = 200; //loops after destroying an object before deleting oneself

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

        if(other.CompareTag("Water") && isFireActive)
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
            playerFire otherFire2 = other.GetComponent<playerFire>();
            if (otherFire2 != null && otherFire2.isFireActive)
            {
                ReLight();
            }
        }
        if (other.CompareTag("Lava") && !isFireActive)
        {
            ReLight();
        }
        if (other.CompareTag("Coals"))
        {
            if(!isFireActive && !waterlogged)
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
        if(this.transform.parent != null)
        {
            this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, this.transform.parent.rotation.z * -1.0f);
        }
    }
    private void FixedUpdate()
    {
        // delete self once thing has burned up and the animation is done
        if (destroyed)
        {
            loopsUntilCleanup--;
            if(loopsUntilCleanup < 0)
            {
                Destroy(this.gameObject);
            }
            return;
        }

        // make progress on destroying object
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
                    this.transform.parent = null;
                    objectToDestroy.BurnUp();
                    destroyed = true;
                    Extinguish();
                }
            }
        }
        
        // Coals logic- start fire if on coals long enough, otherwise rest progress towards that
        if (!onCoals)
        {
            loopsBeforeFire = loopsBeforeFireStartsOnCoals;
            if (!isFireActive && isSmokeActive)
            {
                isSmokeActive = false;
                smoke.Stop();
            }
        }

        if(onCoals && loopsBeforeFire <= 0 && !isFireActive)
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

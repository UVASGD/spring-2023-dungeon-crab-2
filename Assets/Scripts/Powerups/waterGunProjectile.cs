using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterGunProjectile : MonoBehaviour
{
    bool destroy = false;
    float timeToDestroy = 0.15f;
    float pushPower = 0.8f;
    Rigidbody rb;
    public ParticleSystem smoke = null;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (destroy)
        {
            
            this.transform.localScale -= new Vector3(transform.localScale.x * Time.deltaTime / timeToDestroy, transform.localScale.x * Time.deltaTime / timeToDestroy, transform.localScale.x * Time.deltaTime / timeToDestroy);
            timeToDestroy -= Time.deltaTime;
        }
    }
    private void FixedUpdate()
    {
        if (timeToDestroy < 0)
        {
            Object.Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag != "Player" && (other.isTrigger == false || other.tag=="Lava") && other.tag != "Grate" && other.tag != "Pot")
        {
            destroy = true;

            // push anything with a rigidbody
            Rigidbody body;
            if ((body = other.GetComponent<Rigidbody>()) != null)
            {
                if (body.isKinematic)
                    return;
                Vector3 pushDir = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                body.velocity = pushDir * pushPower;
            }
            
            if(other.tag=="Lava" && smoke != null)
            {
                smoke.Play();
            }
        }
    }
}

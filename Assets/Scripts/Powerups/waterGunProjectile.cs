using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterGunProjectile : MonoBehaviour
{
    bool destroy = false;
    float timeToDestroy = 0.05f;
    float pushPower = 30f;
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
        if(other.tag != "Player" && (other.isTrigger == false || other.tag=="Lava") && other.tag != "Grate")
        {
            destroy = true;

            // push anything with a rigidbody
            if(other.tag != "Pot")
            {
                Rigidbody body;
                if ((body = other.GetComponent<Rigidbody>()) != null)
                {
                    if (body.isKinematic)
                        return;
                    Vector3 pushDir = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z);
                    body.AddForce(pushDir * pushPower);
                }
            }
            
            
            if(other.tag=="Lava" && smoke != null)
            {
                smoke.Play();
            }
        }
    }
}

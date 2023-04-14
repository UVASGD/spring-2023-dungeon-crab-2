using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamethrowerProjectile : MonoBehaviour
{
    bool destroy = false;
    float timeToDestroy = 0.04f;
    float lifetime = 0.3f;
    Rigidbody rb;
    float startTime;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (destroy)
        {

            this.transform.localScale -= new Vector3(transform.localScale.x * Time.deltaTime / timeToDestroy, transform.localScale.x * Time.deltaTime / timeToDestroy, transform.localScale.x * Time.deltaTime / timeToDestroy);
            timeToDestroy -= Time.deltaTime;
        }
        if(Time.time > startTime + lifetime)
        {
            destroy = true;
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
        if (other.tag != "Player" && (other.isTrigger == false || other.tag == "Water" || other.tag == "Lava") && other.tag != "Grate" && other.tag !="Ice")
        {
            destroy = true;
        }
    }
}

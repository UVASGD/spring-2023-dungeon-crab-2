using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is for objects that float in liquids. It provides an actual force to make them float.
// Note: this code is a bit scuffed, but hey it works
public class floater : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rb;
    private Collider col;
    public float floatingForce = 45.0f;
    public float angularDragInWater = 100f;
    public float dragInWater = 10f;
    public float verticalDragInWater = 9f;
    public float gravityForce = -20f;
    private bool inWater = false;

    // tag of trigger to float inside of- typically Water but can be configured to Lava
    public string floatInTag = "Water";
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (inWater)
        {
            Vector3 temp_velocity = rb.velocity;
            temp_velocity.x *= 1f / dragInWater;
            temp_velocity.z *= 1f / dragInWater;
            rb.velocity = temp_velocity;
        }
        rb.AddForce(new Vector3(0, gravityForce));
    }

    // function to determine if 
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == floatInTag)
        {
            // if more than a quarter of the box is in water, then dramatically increase drag and angular drag
            if (col.bounds.center.y - col.bounds.extents.y / 2f < other.bounds.max.y)
            {
                // increase drag if in water
                inWater = true;
                rb.drag = 5;
                rb.angularDrag = angularDragInWater;
            }
            else
            {
                inWater = false;
                rb.drag = 0;
                rb.angularDrag = 0.05f;
            }
            // only apply force if more than halfway submerged in the water
            if(col.bounds.center.y < other.bounds.max.y)
            {
                float dist = other.bounds.max.y - col.bounds.center.y;
                float actualForce = floatingForce;
                actualForce *= dist;

                // cancel out force if already at water's surface
                if (Mathf.Abs(Mathf.Abs(other.bounds.max.y) - Mathf.Abs(col.bounds.center.y)) < 0f)
                {
                    actualForce = 0f;
                }

                //actually apply the force
                rb.AddForce(new Vector3(0, -1* gravityForce + actualForce, 0));
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        // decrease drag back to normal on leaving water
        rb.drag = 0;
        rb.angularDrag = 0.05f;
        inWater = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > 2)
        {
            //GetComponent<AudioSource>().Play();
        }
    }


}

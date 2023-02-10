using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script for wind boxes- triggers that apply forces to rigidbodies that enter their trigger area. 
// These can be configured to either apply a continuous force or a single burst from the inspector
// You can change the magnitude and direction of the force in the inspector as well
public class WindBox : MonoBehaviour
{
    // the force vector to be applied to any rigidbodies that enter its trigger
    public Vector3 windForce;

    // if set to true, apply the force once as an impulse (useful for a jump pad). If set to false, then continue applying force (more like a current or wind)
    public bool impulse;

    // private list of things to push/things currently in trigger zone
    private Dictionary<GameObject, Rigidbody> thingsToPush;

    // Start is called before the first frame update
    void Start()
    {
        thingsToPush = new Dictionary<GameObject, Rigidbody>();
    }

    private void FixedUpdate()
    {
        foreach(var key in new List<GameObject>(thingsToPush.Keys)){
            Rigidbody rb = thingsToPush[key];
            if (impulse)
            {
                // zero out velocity in direction of the impulse
                rb.velocity = rb.velocity - windForce.normalized * Vector3.Dot(rb.velocity, windForce.normalized);
                // add a force in that direction
                rb.AddForce(windForce, ForceMode.VelocityChange);
                // force full gravity if it's the player (prevents crazy high launches)
                playermovement temp = key.GetComponent<playermovement>();
                if (temp)
                {
                    temp.forceStrongGravity();
                }
                thingsToPush.Remove(key);
            }
            else
            {
                rb.AddForce(windForce);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb)
        {
            thingsToPush.Add(other.gameObject, rb);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (thingsToPush.ContainsKey(other.gameObject))
        {
            thingsToPush.Remove(other.gameObject);
        }
    }
}

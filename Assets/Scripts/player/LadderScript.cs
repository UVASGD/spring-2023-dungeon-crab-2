using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is the script that's added to the player in order to allow them to climb up ladders.
public class LadderScript : MonoBehaviour
{

    bool inside = false;
    bool keyDown = false;
    public float speed = 3.3f;
    private playermovement MoveInput;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        MoveInput = GetComponent<playermovement>();
        rb = GetComponent<Rigidbody>();
        inside = false;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Ladder")
        {
            inside = !inside;
        }

    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Ladder")
        {
            inside = !inside;
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.E))
        {
            keyDown = true;
        }
        else {
            keyDown = false;
        }
        if (inside == true && keyDown == true)
        {
            
            MoveInput.enabled = false;
            rb.velocity = Vector3.up * speed;
        }
        else {
            MoveInput.enabled = true;
        }
    }
}

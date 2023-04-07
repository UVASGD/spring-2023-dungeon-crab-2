using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// code for grates that are movable by buttons
// note: this is a bit buggy and hard to set up, somewhat likely to be fixed later
public class Grate : ActivatableObject
{
    public GameObject grateStartPosition;
    public GameObject grateEndPosition;
    public float speed;
    public bool moveBackAndForthWhileActive = false;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private Rigidbody rBody;

    private bool goToEnd = true;

    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
        startPosition = grateStartPosition.transform.position;
        endPosition = grateEndPosition.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (moveBackAndForthWhileActive)
        {
            // in this mode, travel continuously between the two points; stop traveling when not button deactivates.
            if (isCurrentlyActive)
            {
                if (goToEnd)
                {
                    goToEnd = !MoveTowards(endPosition);
                }
                else
                {
                    goToEnd = MoveTowards(startPosition);
                }
            }
        }
        else
        {
            // in this mode, travel to the end position when active; when the button deactivates, travel back to the start position.
            if (isCurrentlyActive)
            {
                MoveTowards(endPosition);

            }
            if (!isCurrentlyActive)
            {
                MoveTowards(startPosition);
            }
        }
        
    }

    private bool MoveTowards(Vector3 destination)
    {
        if (Vector3.Distance(transform.position, destination) < speed)
        {
            rBody.MovePosition(destination);
            return true;
        }
        else
        {
            rBody.MovePosition(transform.position + (destination - transform.position).normalized * speed);
            return false;
        }
    }

}

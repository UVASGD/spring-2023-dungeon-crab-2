using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is attatched to the grate that opens up and down.
public class GrateOld : MonoBehaviour
{
    
    public bool currentlyOpen = false;
    public bool opening = false;
    public bool closing = false;

    
    //Position Information
    public int endPositionNum = 10;
    public int startPositionNum = 0;

    public int count = 0;

    //Directional Booleans
    public bool grateopensPositiveX = true;
    public bool grateopensNegativeX = false;

    public bool grateopensPositiveZ = false;
    public bool grateopensNegativeZ = false;
    
    public bool grateopensPositiveY = false;
    public bool grateopensNegativeY = false;

    /*public bool carriesObjects = true;
    public List<string> tagDoesntMove= new List<string>();*/

    // Start is called before the first frame update
    void Start()
    {
        if (grateopensPositiveZ && grateopensNegativeZ) { // Quick Fix for if opposite directions are checked
            grateopensNegativeZ = false;
        }
        if (grateopensPositiveX && grateopensNegativeX){
            grateopensNegativeX = false;
        }

        if (grateopensPositiveY && grateopensNegativeY)
        {
            grateopensNegativeY = false;
        }

    }

    public void close()
    {
        //Starts the closing process
        opening = false;
        closing = true;
    }

    public void open()
    {
        opening = true;
        closing = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //General Opening Process
        if (opening && count < endPositionNum) //Opens door if open is called when partially closed
        {
            openingAnimation();
            count++;
        }
        else if (opening) //Finished Opening
        {
            opening = false;
            currentlyOpen = true;
        }

        //General Closing Process
        if (closing && count > startPositionNum) //Closes door if close is called when partially open
        {
            closingAnimation();
            count--;
        }
        else if (closing) //Finished Closing
        {
            closing = false;
            currentlyOpen = false;
        }

        //General Intertia - Grate Closes in Absense of triggers
        if (!opening && !closing && !currentlyOpen && count > startPositionNum) {
            closingAnimation();
            count--;
        }
        else if (!opening && !closing && !currentlyOpen) //Finished Closing
        {
            currentlyOpen = false;
        }

    }

    private void openingAnimation() {
        if (grateopensPositiveZ) {
            transform.position += new Vector3(0, 0, 10f * Time.deltaTime);
        }
        else if (grateopensNegativeZ)
        {
            transform.position += new Vector3(0, 0, -10f * Time.deltaTime);
        }

        if (grateopensPositiveX)
        {
            transform.position += new Vector3(10f * Time.deltaTime, 0, 0);
        }
        else if (grateopensNegativeX)
        {
            transform.position += new Vector3(-10f * Time.deltaTime, 0, 0);
        }

        if (grateopensPositiveY)
        {
            transform.position += new Vector3(0, 10f * Time.deltaTime, 0);
        }
        else if (grateopensNegativeY)
        {
            transform.position += new Vector3(0, -10f * Time.deltaTime, 0);
        }

    }

    private void closingAnimation()
    {
        if (grateopensPositiveZ)
        {
            transform.position += new Vector3(0, 0, -10f * Time.deltaTime);
        }
        else if (grateopensNegativeZ)
        {
            transform.position += new Vector3(0, 0, 10f * Time.deltaTime);
        }

        if (grateopensPositiveX)
        {
            transform.position += new Vector3(-10f * Time.deltaTime, 0, 0);
        }
        else if (grateopensNegativeX)
        {
            transform.position += new Vector3(+10f * Time.deltaTime, 0, 0);
        }

        if (grateopensPositiveY)
        {
            transform.position += new Vector3(0, -10f * Time.deltaTime, 0);
        }
        else if (grateopensNegativeY)
        {
            transform.position += new Vector3(0, +10f * Time.deltaTime, 0);
        }

    }

    /*
    private void OnCollisionEnter(Collision collision)
    {
        if (carriesObjects && !tagDoesntMove.Contains(collision.gameObject.tag)) {
            collision.collider.transform.parent = transform;
        } 
    }

    private void OnCollisionExit(Collision collision)
    {
        if (carriesObjects && !tagDoesntMove.Contains(collision.gameObject.tag))
        {
            collision.collider.transform.parent = null;
        }
    }*/
}

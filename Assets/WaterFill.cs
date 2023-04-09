using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFill : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {   

    }

    public GameObject FillZone;
    public GameObject water;
    public Vector3 fillSpeed = new Vector3(0f,0.003f,0f);
    public Vector3 growSpeed = new Vector3(0.0001f,0.0f,0.0001f);
    public Vector3 shrinkSpeed = new Vector3(0.0000000001f, 0.0f, 0.0000000001f);
    //float empty = 0.0f;
    //public float low = 0.25f;
    public float mid = 0.5f;
    //public float full = 0.75f;

    void OnTriggerStay(Collider other)
    {
        float full = FillZone.transform.position.y + 0.37f;
        float low = FillZone.transform.position.y -0.35f;

        if (other.tag == "Water")
        {
            // water = FillZone.transform.Find("Circular Water");
            // add a delay and bounding box later
            if (water.transform.position.y < full)
            {
                water.transform.position += fillSpeed;
            }

            //size appropriately as filling
            if (water.transform.position.y <= low)
            {
                water.transform.localScale += growSpeed;
            }

        }
    }

}

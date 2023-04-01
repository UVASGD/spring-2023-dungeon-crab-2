using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFill : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {   

    }

    Vector3 fillSpeed = new Vector3(0f,0.003f,0f);
    Vector3 growSpeed = new Vector3(0.0001f,0.0f,0.0001f);
    Vector3 shrinkSpeed = new Vector3(0.0000000001f, 0.0f, 0.0000000001f);
    //float empty = 0.0f;
    float low = 0.25f;
    float mid = 0.5f;
    float full = 0.75f;

    // Update is called once per frame
    void Update()
    {
        // add a delay and bounding box later
        if (Input.GetKey(KeyCode.F)){
            // fill while not full
            if (transform.position.y < full) {
                transform.position += fillSpeed;
            }

            //size appropriately as filling
            if (transform.position.y <= low)
            {
                transform.localScale += growSpeed;
            } 
            else if (transform.position.y > low && transform.position.y < mid)
            {
                //transform.localScale += sizeSpeed;
            }
            else if (transform.position.y >= mid)
            {
                //transform.localScale -= shrinkSpeed;
            }
        }

        

    }
}

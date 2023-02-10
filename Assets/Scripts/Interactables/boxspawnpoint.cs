using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// defines the place in which a box will spawn; spawn a box on scene start, respawn it at this point if it burns up
public class boxspawnpoint : MonoBehaviour
{
    public GameObject thingToSpawn;
    private GameObject currentObject = null;
    // Start is called before the first frame update
    void Start()
    {
        if(currentObject == null)
        {
            currentObject = GameObject.Instantiate(thingToSpawn, this.transform);
        }
        else
        {
        }
    }
    private void FixedUpdate()
    {
        if(currentObject == null)
        {
            currentObject = GameObject.Instantiate(thingToSpawn, this.transform);
        }
    }
    // function to see the spawnpoint in the editor
    void OnDrawGizmosSelected()
    {
        // Draw a semitransparent blue cube at the transforms position
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(transform.position, new Vector3(1, 1, 1));
    }
}

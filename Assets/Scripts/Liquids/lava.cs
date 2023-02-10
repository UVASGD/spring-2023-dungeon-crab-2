using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script handles the movement of the water to the correct height for the water level.
// Note that the way an object interacts with water is usually determined by that object's OnTriggerStay function for colliders with a Water tag (ie in the floater.cs file)
// Also note that while the water level is stored in the GameManager, you can set what actual y level those heights correspond to in the inspector
// of this water object. The water level stored in the GameManager just tells us where in the waterLevelHeights array to index to get the actual height.
// (It's implemented like this to give level designers the ability to tweak the exact values on a per-scene basis)
// Generally, it's best to put lower water levels before higher ones in the array in order to stay more consistent across scenes.
public class lava : MonoBehaviour
{
    public List<float> lavaLevelHeights;
    public float speedOfChange = 1.0f;
    private GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.instance;
        if(lavaLevelHeights.Count == 0)
        {
            lavaLevelHeights.Add(transform.position.y);
        }
        transform.position = new Vector3(transform.position.x, lavaLevelHeights[gm.lavaLevel], transform.position.z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, lavaLevelHeights[gm.lavaLevel], transform.position.z), speedOfChange);
    }
}

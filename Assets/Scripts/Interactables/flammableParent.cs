using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script to be placed on the parent of a fire object (ie, box) that allows the parent to be burned up and get deleted by the fire object
public class flammableParent : MonoBehaviour
{
    private bool started = false;
    public bool spawnThingOnBurnup = false;
    public GameObject thingToSpawn;

    public void BurnUp()
    {
        if (!started)
        {
            if (this.GetComponent<MeshRenderer>() != null)
                this.GetComponent<MeshRenderer>().enabled = false;
            if (this.GetComponent<Rigidbody>() != null)
                Destroy(this.GetComponent<Rigidbody>());
            if (this.GetComponent<BoxCollider>() != null)
                this.GetComponent<BoxCollider>().enabled = false;
            if (spawnThingOnBurnup && thingToSpawn != null)
                thingToSpawn.name = "Object spawned by " + this.name;
                Instantiate(thingToSpawn, transform.position + new Vector3(0,0.4f,0), Quaternion.identity);
        }
        Destroy(this.gameObject);

    }
}

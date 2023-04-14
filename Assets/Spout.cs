using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spout : MonoBehaviour
{
    public GameObject projectile;

    public float spawnDistance = 0f;
    public float spread = 1f;
    public float coolDownTime = 1;
    private float coolDown = 0;
    public float spawnSpeed = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (coolDown <= 0f)
        {
            GameObject proj = (GameObject)Instantiate(projectile, transform.position + transform.forward * spawnDistance, transform.rotation);
            Rigidbody rb;
            if ((rb = proj.GetComponent<Rigidbody>()) != null)
            {
                rb.velocity = (new Vector3(Random.Range(transform.forward.x + spread, transform.forward.x - spread), Random.Range(transform.forward.y + spread, transform.forward.y - spread), Random.Range(transform.forward.z + spread, transform.forward.z - spread)).normalized + new Vector3(0, 0.3f, 0)) * spawnSpeed * Random.Range(0.9f, 1.1f);
            }
            coolDown = coolDownTime;
        }

        if (coolDown > 0f)
        {
            coolDown -= Time.deltaTime;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSpin : MonoBehaviour
{

    public Transform cam;

    void Start()
    {
        Debug.Log("cope");
        cam.LookAt(transform);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0.05f, 0.0f));
    }
}

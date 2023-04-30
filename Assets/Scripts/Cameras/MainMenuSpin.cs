using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSpin : MonoBehaviour
{

    public Transform cam;

    void Start()
    {
        cam.LookAt(transform);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0.5f, 0.0f));
    }
}

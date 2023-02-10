using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is attached to the parent of the locked door object. It handles the opening of the door.
public class ParentDoor : MonoBehaviour
{
    // Start is called before the first frame update
    public bool currentlyOpen = false;
    public bool opening = false;
    public int countdown = 110;
    public bool requiresKey = true;
    public GameObject thelock;
    public GameObject ps;
    private GameManager gm;
    private BoxCollider playerDetect;
    void Start()
    {
        gm = GameManager.instance;
    }

    public void open()
    {
        opening = true;
        thelock.SetActive(false);
        ps.SetActive(true);
    }

    public void startOpen()
    {
        thelock.SetActive(false);
        currentlyOpen = true;
        transform.Rotate(0, countdown, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (opening&&countdown>0)
        {
            transform.Rotate(0,1,0);
            countdown--;
        }else if (opening)
        {
            opening = false;
            currentlyOpen = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// code for grates that are movable by buttons
// note: this is a bit buggy and hard to set up, somewhat likely to be fixed later
public class Grate : MonoBehaviour
{
    public GameObject gratePositionStart;
    public GameObject gratePositionEnd;
    public int speed;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private Rigidbody rBody;

    public bool currentlyOpen = false;

    private bool opening = false;
    private bool closing = false;

    // Start is called before the first frame update
    //Based Off: https://www.monkeykidgc.com/2021/03/unity-moving-platform.html#comments
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
        startPosition = gratePositionStart.transform.position;
        endPosition = gratePositionEnd.transform.position;
        if (currentlyOpen)
        {
            StartCoroutine(Vector3LerpCoroutine(gameObject, startPosition, speed));
        }
        else {
            StartCoroutine(Vector3LerpCoroutine(gameObject, endPosition, speed));
        }

    }

    IEnumerator Vector3LerpCoroutine(GameObject obj, Vector3 target, float speed)
    {
        Vector3 startPosition = obj.transform.position;
        float time = 0f;

        while (rBody.position != target)
        {
            rBody.MovePosition(Vector3.Lerp(startPosition, target, (time / Vector3.Distance(startPosition, target)) * speed));
            time += Time.deltaTime;
            yield return null;
        }
    }


    public void close()
    {
        opening = false;
        closing = true;

        //StartCoroutine(Vector3LerpCoroutine(gameObject, startPosition, speed));
        //currentlyOpen = false;
    }

    public void open()
    {
        opening = true;
        closing = false;
        //StartCoroutine(Vector3LerpCoroutine(gameObject, endPosition, speed));
        //currentlyOpen = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (opening && rBody.position == startPosition)
        {
            StartCoroutine(Vector3LerpCoroutine(gameObject, endPosition, speed));
        }
        else if (opening && rBody.position == endPosition) //Finished Opening
        {
            opening = false;
            currentlyOpen = true;
        }

        if (closing && rBody.position == endPosition)
        {
            StartCoroutine(Vector3LerpCoroutine(gameObject, startPosition, speed));
        }
        else if (closing && rBody.position == startPosition) //Finished Closing
        {
            closing = false;
            currentlyOpen = false;
        }

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Based Off: https://www.monkeykidgc.com/2021/03/unity-moving-platform.html#comments
public class MovePlatform : ActivatableObject
{
    public GameObject platformPathStart;
    public GameObject platformPathEnd;
    public int speed;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private Rigidbody rBody;


    private playermovement character;
    public Vector3 moveDirection;
    public bool characterOnPlatform = false;

    void Start()
    {
        rBody = GetComponent<Rigidbody>();
        startPosition = platformPathStart.transform.position;
        endPosition = platformPathEnd.transform.position;
        rBody.MovePosition(platformPathStart.transform.position);
        if (isCurrentlyActive)
        {
            StartCoroutine(Vector3LerpCoroutine(gameObject, endPosition, speed));
        }
        
    }

    void Update()
    {
        if (!isCurrentlyActive) {
            return;
        }

        if (rBody.position == endPosition)
        {
            StartCoroutine(Vector3LerpCoroutine(gameObject, startPosition, speed));
            //Vector3LerpCoroutine(gameObject, startPosition, speed);
        }
        if (rBody.position == startPosition)
        {
            StartCoroutine(Vector3LerpCoroutine(gameObject, endPosition, speed));
            //Vector3LerpCoroutine(gameObject, endPosition, speed);
        }
    }

    IEnumerator Vector3LerpCoroutine(GameObject obj, Vector3 target, float speed)
    {
        Vector3 startPosition = obj.transform.position;
        float time = 0f;

        while (rBody.position != target)
        {
            moveDirection = rBody.velocity;
            if (characterOnPlatform && character) {
                character.externalMoveSpeed = moveDirection;
            }
            rBody.MovePosition(Vector3.Lerp(startPosition, target, (time / Vector3.Distance(startPosition, target)) * speed));
            time += Time.deltaTime;
            yield return null;
        }
    }

    public void activate()
    {
        isCurrentlyActive = true;
    }

    public void deactivate()
    {
        isCurrentlyActive = false;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            //col.gameObject.transform.SetParent(gameObject.transform, true);
            characterOnPlatform = true;
            if (!character)
            {
                character = col.gameObject.GetComponent<playermovement>();
            }

            character.externalMoveSpeed = moveDirection;
        }


    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            characterOnPlatform = false;
            //col.gameObject.transform.parent = null;
            character.externalMoveSpeed = Vector3.zero;
        }
    }

}
    
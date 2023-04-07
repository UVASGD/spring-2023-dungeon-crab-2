using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTalkScript : MonoBehaviour
{
    public float talkRange = 3f;
    public LayerMask layersToTalkTo;

    // defines how long to wait after you finish talking before you accept new talking inputs (should help with people mashing the button)
    public float cooldownAfterTalking = 0.3f;

    // last recorded timestep when the player was talking
    private float timeLastTalk;

    // Start is called before the first frame update
    void Start()
    {
        timeLastTalk = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && GameManager.instance.currentState == GameManager.GameState.Play && Time.time - timeLastTalk > cooldownAfterTalking)
        {
            if (Physics.Raycast(transform.position, transform.forward, out var hit, talkRange, layersToTalkTo, QueryTriggerInteraction.Ignore))
            {
                DialogueTrigger temp = hit.collider.gameObject.GetComponent<DialogueTrigger>();
                if(temp)
                {
                    temp.Interact();
                }
            }
        }

        if(GameManager.instance.currentState == GameManager.GameState.Talk)
        {
            timeLastTalk = Time.time;
        }
    }

    private void OnDrawGizmosSelected()
    {
        //draws a vector that's the same as the ray cast for grabbing something (shows the range)
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * talkRange);
    }
}

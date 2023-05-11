using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// script that lets the player grab other objects (anything with a rigidbody) in the scene
public class playerGrab : MonoBehaviour
{
    public float grabRange = 3f;
    public LayerMask layersToGrabFrom;
    public float breakGrabForce = 100f;
    private FixedJoint joint = null;
    private Collider itemCollider = null;
    private playermovement movementScript;
    public PhysicMaterial materialToApplyToHeldThings;
    public Vector3 grabPositionChange;

    private ice grabbedIce = null;

    private WaterGunControl watergunLogic;

    // Start is called before the first frame update
    void Start()
    {
        movementScript = GetComponent<playermovement>();
        watergunLogic = GetComponent<WaterGunControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(joint == null)
            {
                if (Physics.Raycast(transform.position, transform.forward, out var hit, grabRange, layersToGrabFrom, QueryTriggerInteraction.Ignore))
                {
                    if (hit.rigidbody)
                    {
                        hit.transform.position += grabPositionChange;
                        joint = gameObject.AddComponent<FixedJoint>() as FixedJoint;
                        joint.connectedBody = hit.rigidbody;
                        joint.breakForce = breakGrabForce;
                        if (hit.collider && materialToApplyToHeldThings && materialToApplyToHeldThings.dynamicFriction < hit.collider.material.dynamicFriction)
                        {
                            hit.collider.material = materialToApplyToHeldThings;
                            itemCollider = hit.collider;
                        }
                        // while carrying something, don't turn in the direction you move (would cause a lot of physics problems/forces, plus is a lot less predicatable)
                        movementScript.turnInDirectionOfMovement = false;
                        grabbedIce = hit.collider.gameObject.GetComponent<ice>();
                        if(grabbedIce != null)
                        {
                            grabbedIce.pGrab = this;
                        }
                        watergunLogic.setCanShoot(false);
                        if(AudioManager.instance != null)
                        {
                            AudioManager.instance.Play("Thunk");
                        }
                    }
                }
            }
            else
            {
                breakJoint();
            }

        }
    }

    // will be called when an excessive outside force breaks the joint between the player and what they're holding
    private void OnJointBreak(float breakForce)
    {
        joint = null;
        if (grabbedIce)
        {
            grabbedIce = null;
        }
        movementScript.turnInDirectionOfMovement = true;
        if (itemCollider)
        {
            itemCollider.material = null;
        }
        watergunLogic.setCanShoot(true);
    }

    // draws gizmos when the player object is highlighted in the editor (useful for debugging! Won't do anything in the final release/build.)
    private void OnDrawGizmosSelected()
    {
        //draws a vector that's the same as the ray cast for grabbing something (shows the range)
        //Gizmos.DrawLine(transform.position, transform.position + transform.forward * grabRange);
    }
    public void breakJoint()
    {
        Destroy(joint);
        joint = null;
        if (grabbedIce)
        {
            grabbedIce.pGrab = null;
        }
        grabbedIce = null;
        movementScript.turnInDirectionOfMovement = true;
        if (itemCollider && itemCollider.material == materialToApplyToHeldThings)
        {
            itemCollider.material = null;
        }
        watergunLogic.setCanShoot(true);
        if (AudioManager.instance != null)
        {
            AudioManager.instance.Play("Thwack");
        }
    }
    
}

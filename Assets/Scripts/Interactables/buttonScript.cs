using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonScript : MonoBehaviour
{

    private List<GameObject> thingsOnButton = new List<GameObject>();
    private int stepsUntilCheckObject = 10;     //determines how often a button checks to make sure all the objects that were on it still exist (in terms of Physics steps)
                                                // (it's implemented like this to improve performance- fewer iterations over the list)

    // list of things to activate when something is on the button
    public List<ActivatableObject> thingsToActivate;
    private bool thingsAreActive = false;

    //List of Tags that cannot trigger the button
    //TODO: Should this instead be a list of items that do trigger the button?
    private List<string> tagDoesntTriggerButton = new List<string> { "Fire" };

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //TODO: Should the isActive turn the trigger off entirely?
    }

    private void FixedUpdate()
    {
        // the following code handles cases where gameObjects have been disabled since they entered the button space
        if(stepsUntilCheckObject < 0)
        {
            if(thingsOnButton.Count > 0)
            {
                thingsOnButton.RemoveAll(x => x == null);
            }
            stepsUntilCheckObject = 10;
        }
        else
        {
            stepsUntilCheckObject--;
        }

        if(thingsAreActive && thingsOnButton.Count == 0)
        {
            setInactive();
        }else if(!thingsAreActive && thingsOnButton.Count > 0)
        {
            setActive();
        }

    }
   

    private void OnTriggerEnter(Collider other)
    {
        if (!isTriggerActive(other))
        {
            return;
        }


        thingsOnButton.Add(other.gameObject);
        if (thingsOnButton.Count > 0){

            
        }
        
    }

    private void OnTriggerExit(Collider other) { 

        //Break method if the button isn't active for safety
        if (!isTriggerActive(other))
        {
            return;
        }

        thingsOnButton.Remove(other.gameObject);
        if (thingsOnButton.Count == 0)
        {
            setInactive();
        }

    }

    private void setActive()
    {
        foreach(ActivatableObject obj in thingsToActivate)
        {
            obj.isCurrentlyActive = true;
        }
        thingsAreActive = true;
    }

    private void setInactive()
    {
        foreach (ActivatableObject obj in thingsToActivate)
        {
            obj.isCurrentlyActive = false;
        }
        thingsAreActive = false;
    }

    private bool isTriggerActive(Collider other) {
        //Break method if the button isn't active for safety
        //don't trigger if tag is in list, for example, fire in crates don't trigger the button
        if (!tagDoesntTriggerButton.Contains(other.gameObject.tag))
        {
            return true;
        }

        return false;
    }

}

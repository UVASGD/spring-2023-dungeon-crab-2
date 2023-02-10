using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonScript : MonoBehaviour
{
    public bool isActive; //Boolean that controls whether the button is active
    public List<GameObject> thingsOnButton = new List<GameObject>();
    private int stepsUntilCheckObject = 10;     //determines how often a button checks to make sure all the objects that were on it still exist (in terms of Physics steps)
                                                // (it's implemented like this to improve performance- fewer iterations over the list)

    public bool buttonOpensGrate = true; //Whether the button opens or closes the grate
    public List<Grate> grateList = new List<Grate>(); //List of Grates controlled by the button

    public bool buttonActivatesPlatform = true;
    public List<MovePlatform> platformList = new List<MovePlatform>(); //List of Platforms controlled by the button

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
                if (thingsOnButton.Count == 0)
                {
                    if (buttonOpensGrate)
                    {
                        closeAllGrates();
                    }
                    else
                    {
                        openAllGrates();
                    }

                    if (buttonActivatesPlatform)
                    {
                        deactivateAllPlatforms();
                    }
                    else
                    {
                        activateAllPlatforms();
                    }

                }
            }
            stepsUntilCheckObject = 10;
        }
        else
        {
            stepsUntilCheckObject--;
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

            if (buttonOpensGrate)
            {
                openAllGrates();
            }
            else
            {
                closeAllGrates();
            }

            if (buttonActivatesPlatform)
            {
                activateAllPlatforms();
            }
            else {
                deactivateAllPlatforms();
            }
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
            if (buttonOpensGrate)
            {
                closeAllGrates();
            }
            else {
                openAllGrates();
            }

            if (buttonActivatesPlatform)
            {
                deactivateAllPlatforms();
            }
            else
            {
                activateAllPlatforms();
            }

        }

    }

    private bool isTriggerActive(Collider other) {
        //Break method if the button isn't active for safety
        //don't trigger if tag is in list, for example, fire in crates don't trigger the button
        if (isActive && !tagDoesntTriggerButton.Contains(other.gameObject.tag))
        {
            return true;
        }

        return false;
    }

    private void openAllGrates() {
        foreach (Grate g in grateList) {
            g.open();
        }
    }

    private void closeAllGrates()
    {
        foreach (Grate g in grateList)
        {
            g.close();
        }
    }

    private void activateAllPlatforms()
    {
        foreach (MovePlatform p in platformList)
        {
            p.activate();
        }
    }

    private void deactivateAllPlatforms()
    {
        foreach (MovePlatform p in platformList)
        {
            p.deactivate();
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script that loads another scene when the player enters a trigger. Right now this just loads the scene,
// in the future we'll need to edit it to record more information so that in cases with multiple entries/exits
// it'll spawn the player in the right place. We'll also probably want to add a wipe in/out to the Game Manager.
// For now though, this'll let us transition between scenes.
public class sceneTransition : MonoBehaviour
{
    public string nameOfSceneToLoad;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // note: make sure the scene is in the build index before you call this! Otherwise it won't work.
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GameManager.instance.loadLevel(nameOfSceneToLoad);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// This is the script for key pickups. When the player walks into it, they get a key.
// It also will make sure not to spawn if the player has already collected this key.
public class keyScript : MonoBehaviour
{
    //this id is used in the game manager to keep track of which keys have been collected already. Automatically set on start.
    private string id;
    // Start is called before the first frame update
    void Start()
    {
        id = SceneManager.GetActiveScene().name + this.name;
        // if this key has already been collected, destroy it
        if (GameManager.instance.keysCollected.Contains(id) == true)
        {
            Destroy(this.gameObject);
        }
    }

    // rotating animation
    void Update()
    {
        this.transform.Rotate(new Vector3(0,0,1));
    }

    // collect the key if the player walks into the collider trigger
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            GameManager.instance.keyObtained();
            if (GameManager.instance.keysCollected.Contains(id) == false)
            {
                GameManager.instance.keysCollected.Add(id);
            }
            gameObject.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// This is the script for money pickups. When the player walks into it, they get a key.
// It also will make sure not to spawn if the player has already collected this key.
public class moneyScript : MonoBehaviour
{
    //this id is used in the game manager to keep track of which money have been collected already. Automatically set on start.
    private string id;
    // Start is called before the first frame update
    void Start()
    {
        id = SceneManager.GetActiveScene().name + this.name;
        // if this money has already been collected, destroy it
        if (GameManager.instance.moneyCollected.Contains(id) == true)
        {
            Destroy(this.gameObject);
        }
    }

    // rotating animation
    void Update()
    {
        this.transform.Rotate(new Vector3(0, 0, (float)0.7));
    }

    // collect the money if the player walks into the collider trigger
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameManager.instance.moneyObtained();
            if (GameManager.instance.moneyCollected.Contains(id) == false)
            {
                GameManager.instance.moneyCollected.Add(id);
            }
            gameObject.SetActive(false);
        }
    }
}

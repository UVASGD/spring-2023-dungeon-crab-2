using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//this is the script for the detection box of the door prefab. It checks if the player is near the door
// and if they press E. If that happens, it tells its parent object to open the door and then deactiates.
public class doorScript : MonoBehaviour
{
    private GameManager gm;
    private bool playerInArea = false;
    public ParentDoor door;
    private string id;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.instance;
        id = SceneManager.GetActiveScene().name + this.transform.position.ToString();
        if (gm.doorsUnlocked.Contains(id))
        {
            door.startOpen();
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInArea && Input.GetKeyDown(KeyCode.E))
        {
            if (gm.useKey())
            {
                gm.doorsUnlocked.Add(id);
                door.open();
                gameObject.SetActive(false);
            }
        }
    }
 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInArea = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInArea = false;
        }
    }
}

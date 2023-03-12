using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class doorBoardsScript : MonoBehaviour
{
    public flammableParent col;
    private string id;
    private GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        id = SceneManager.GetActiveScene().name + this.name;
        if (GameManager.instance.burnedThings.Contains(id) == true)
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (col == null)
        {
            if(GameManager.instance.burnedThings.Contains(id) == false)
            {
                GameManager.instance.burnedThings.Add(id);
            }
        }
    }
}

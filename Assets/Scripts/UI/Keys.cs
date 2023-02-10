using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keys : MonoBehaviour
{
    // Start is called before the first frame update
    public Text keys;
    public Image im;
    private int waitToCheck = -1;

    // Update is called once per frame
    void Update()
    {
        if (waitToCheck < 0)
        {
            if (GameManager.instance.getKeys() == 0)
            {
                keys.text = "";
                im.enabled = false;
            }
            else
            {
                keys.text = "x " + GameManager.instance.getKeys().ToString();
                im.enabled = true;
            }
            waitToCheck = 10;
        }
        else
        {
            waitToCheck--;
        }
        
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyUI : MonoBehaviour
{
    // Start is called before the first frame update
    public GameManager script; 
    public Text keys;

    // Update is called once per frame
    void Update()
    {
        keys.text = "Keys: "+script.getKeys().ToString();
    }
}

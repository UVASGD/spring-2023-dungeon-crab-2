using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyUI : MonoBehaviour
{
    // Start is called before the first frame update
    public Text moneyText;
    public Image im;
    private int waitToCheck = -1;

    // Update is called once per frame
    void Update()
    {
        if (waitToCheck < 0)
        {
            if (GameManager.instance.getMoney() == 0)
            {
                moneyText.text = "";
                im.enabled = false;
            }
            else
            {
                moneyText.text = "x " + GameManager.instance.getMoney().ToString();
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

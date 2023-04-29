using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatScript : MonoBehaviour
{
    public int hatUnlockNumber;
    public int cost;
    public TextMesh moneyIndicator;
    private bool inside;

    public void Start()
    {
        if (!GameManager.instance.unlockedHats[hatUnlockNumber])
        {
            moneyIndicator.text = "" + cost;
        }
        else if(GameManager.instance.hatNum == hatUnlockNumber)
        {
            moneyIndicator.text = ":)";
        }
        else
        {
            moneyIndicator.text = ":(";
        }
    }

    public void Update()
    {
        if(inside && Input.GetKeyDown(KeyCode.E))
        {
            if (GameManager.instance.hatNum != hatUnlockNumber)
            {
                if (GameManager.instance.unlockedHats[hatUnlockNumber])
                {
                    GameManager.instance.hatNum = hatUnlockNumber;
                    moneyIndicator.text = ":)";
                }
                else if (GameManager.instance.numberofMoney >= cost)
                {
                    GameManager.instance.numberofMoney -= cost;
                    GameManager.instance.unlockedHats[hatUnlockNumber] = true;
                    GameManager.instance.hatNum = hatUnlockNumber;
                    moneyIndicator.text = ":)";
                }
            }
            else
            {
                GameManager.instance.hatNum = 0;
                moneyIndicator.text = ":(";
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            moneyIndicator.gameObject.SetActive(true);
            inside = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            moneyIndicator.gameObject.SetActive(false);
            inside = false;
        }
    }
}

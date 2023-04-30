using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatManager : MonoBehaviour
{
    public GameObject[] hats;
    public int curHat = 0;

    // Update is called once per frame
    void Update()
    {
        if(curHat != GameManager.instance.hatNum)
        {
            if(curHat != 0)
            {
                hats[curHat - 1].SetActive(false);
            }
            curHat = GameManager.instance.hatNum;
            if (curHat != 0)
            {
                hats[curHat - 1].SetActive(true);
            }
        }
    }
}

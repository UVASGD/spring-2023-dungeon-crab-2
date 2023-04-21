using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private int waitToCheck = -1;
    private Slider slider;
    private CanvasGroup group;
    public float timeBeforeFade = 10f;    // time at full health before the health bar should fade away
    public bool fadeAtFullHalth = true;
    private float startTimeAtFullHealth;
    private bool fadeActive = true;
    private bool shouldFade = true;
    public float timeOfFade = 1f;

    private void Start()
    {
        slider = GetComponent<Slider>();
        group = GetComponent<CanvasGroup>();
        startTimeAtFullHealth = Time.time - timeBeforeFade - timeOfFade - 20f;
    }

    // Update is called once per frame
    void Update()
    {
        if (waitToCheck < 0)
        {
            slider.value = GameManager.instance.playerHealth / 5f;
            if(GameManager.instance.playerHealth > 4.9f)
            {
                if (!fadeActive)
                {
                    fadeActive = true;
                    startTimeAtFullHealth = Time.time;
                }
                else
                {
                    if(Time.time > startTimeAtFullHealth + timeBeforeFade)
                    {
                        shouldFade = true;
                    }
                    else
                    {
                        shouldFade = false;
                    }
                }

            }
            else
            {
                fadeActive = false;
                shouldFade = false;
            }

            if (shouldFade)
            {
                group.alpha = Mathf.Max(0,group.alpha - Time.deltaTime / (timeOfFade));
            }
            else
            {
                group.alpha = Mathf.Min(1, group.alpha + Time.deltaTime / (timeOfFade));
            }
            waitToCheck = 1;
        }
        else
        {
            waitToCheck--;
        }


    }
}

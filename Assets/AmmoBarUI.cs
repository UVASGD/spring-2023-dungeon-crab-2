using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoBarUI : MonoBehaviour
{
    
    public Slider waterSlider;
    public Slider fireSlider;
    private CanvasGroup group;

    // Start is called before the first frame update
    void Start()
    {
        group = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.waterGunUnlocked || GameManager.instance.flamethrowerUnlocked)
        {
            group.alpha = 1;
            if (GameManager.instance.waterGunUnlocked)
            {
                waterSlider.value = (float)GameManager.instance.waterGunAmmo / 50f;
            }
            else
            {
                waterSlider.value = 0;
            }
            if (GameManager.instance.flamethrowerUnlocked)
            {
                fireSlider.value = (float)(GameManager.instance.flamethrowerAmmo) / 50f;
            }
            else
            {
                fireSlider.value = 0;
            }
        }
        else
        {
            group.alpha = 0;
        }
    }
}

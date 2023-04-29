using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private float maxHealth = 5f;
    public float graceTime = 0.1f;  // determines how much invincibility time you get after taking an attack
    public float timeBeforeRegen = 2f; // defines how long you have to be not damaged before you start regening
    public float regenTime = 0.01f; // determines how frequently you get regen'd
    public GameObject playerGraphics;

    public float flashRate = 10f;

    private float timeOfLastDamage;

    private float timeOfLastRegen;

    // Start is called before the first frame update
    void Start()
    {
        timeOfLastDamage = Time.time - graceTime;
        GameManager.instance.playerHealth = maxHealth;
    }

    // Slowly regen after not taking damage for a while
    void Update()
    {
        if(Time.time > timeOfLastRegen + regenTime && Time.time > timeOfLastDamage + timeBeforeRegen)
        {
            Heal(0.01f);
            timeOfLastRegen = Time.time;
        }
        if(Time.time < timeOfLastDamage + graceTime)
        {
            if(Mathf.Sin(Time.time * flashRate * Mathf.PI) > 0)
            {
                playerGraphics.SetActive(false);
            }
            else
            {
                playerGraphics.SetActive(true);
            }
            
        }
        else
        {
            playerGraphics.SetActive(true);
        }
        
    }
    public void TakeDamage(float damage)
    {
        
        if(Time.time > timeOfLastDamage + graceTime)
        {
            GameManager.instance.playerHealth -= damage;
            timeOfLastDamage = Time.time;
            timeOfLastRegen = Time.time;
            if (GameManager.instance.playerHealth <= 0)
            {
                GameManager.instance.RestartScene();
                gameObject.SetActive(false);
            }
        }
        
    }

    public void Heal(float damage)
    {
        if(GameManager.instance.playerHealth + damage > maxHealth)
        {
            GameManager.instance.playerHealth = maxHealth;
        }
        else
        {
            GameManager.instance.playerHealth += damage;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Fire") && other.gameObject.layer != 11)
        {
            Fire otherFire = other.GetComponent<Fire>();
            playerFire otherFire2 = other.GetComponent<playerFire>();
            if (otherFire != null)
            {
                if (otherFire.isFireActive)
                    TakeDamage(0.04f);
            }
            else if (otherFire2 != null)
            {
                if (otherFire2.isFireActive)
                    TakeDamage(0.04f);
            }
            else
            {
                // this should only trigger for fire projectiles
                TakeDamage(0.04f);
            }

        }
    }
}

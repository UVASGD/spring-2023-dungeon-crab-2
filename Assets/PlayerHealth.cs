using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int maxHealth = 5;
    public float graceTime = 1.0f;
    public float regenTime = 2.0f;

    private float flashTime = 0.2f;

    private float timeOfLastDamage;

    private float timeOfLastRegen;

    // Start is called before the first frame update
    void Start()
    {
        timeOfLastDamage = Time.time;
        timeOfLastDamage = Time.time;
        GameManager.instance.playerHealth = maxHealth;
    }

    // Slowly regen after not taking damage for a while
    void Update()
    {
        if(Time.time > timeOfLastRegen + regenTime)
        {
            Heal(1);
            timeOfLastRegen = Time.time;
        }
        
    }
    public void TakeDamage(int damage)
    {
        
        if(Time.time > timeOfLastDamage + graceTime)
        {
            GameManager.instance.playerHealth -= damage;
            timeOfLastDamage = Time.time;
            timeOfLastRegen = Time.time;
            if (GameManager.instance.playerHealth <= 0)
            {
                GameManager.instance.RestartScene();
            }
        }
        
    }

    public void Heal(int damage)
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
                    TakeDamage(1);
            }
            else if (otherFire2 != null)
            {
                if (otherFire2.isFireActive)
                    TakeDamage(1);
            }
            else
            {
                // this should only trigger for fire projectiles
                TakeDamage(1);
            }

        }
    }
}

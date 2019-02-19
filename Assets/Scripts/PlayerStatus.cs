using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatus : MonoBehaviour
{

    public float maxHealth = 100;
    public float health = 100;
    public float energy = 0;
    public ValueBar healthBar;
    public ValueBar energyBar;

    private Animator animator;
    private bool isFiring = false;
    private float waitTime = 0f;
    private Invincible invincible;
    private bool isInvincible = false;
    private float invincibleTimer = 0f;



    public void ApplyDamage(float totalDamage)
    {
        health -= totalDamage;
        healthBar.UpdateValue(health, maxHealth);
        if (health <= 0) Kill();
    }

    public void HealthRegen(float amount)
    {
        if (health < maxHealth)
        {
            health += amount;
            healthBar.UpdateValue(health, maxHealth);
        }
    }

    public void GainEnergy(float amount)
    {
        if (energy < 100)
        {
            energy += amount;
            energyBar.UpdateValue(energy, 100);
        }
    }

    void PassiveEnergyGain()
    {
        if (energy < 100)
        {
            GainEnergy(.01f);
        }
    }

    void Kill()
    {
        print("YOU ARE DEAD");
        SceneManager.LoadScene("Dead");
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void Win()
    {
        print("YOU WON");
        //SceneManager.LoadScene("WinScene");
    }

    // Use this for initialization
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        PassiveEnergyGain();
    }

}


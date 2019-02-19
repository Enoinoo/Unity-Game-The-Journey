using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MageStatus : MonoBehaviour {

    public float maxHealth = 100;
    public float health = 100;
    public float maxMana = 100;
    public float mana = 100;
    public float points = 0;
    public float energy = 0;
    public ValueBar healthBar;
    public ValueBar manaBar;
    public ValueBar energyBar;
    public Image shiftKey;

    private Animator animator;
    private bool isFiring = false;
    private float waitTime = 0f;
    private Invincible invincible;
    private bool isInvincible = false;
    private float invincibleTimer = 0f;
    private bool isInvincCD = false;
    private float invincibleCD = 0f;


    public void ResetWaitTime()
    {
        waitTime = 0f;
    }

    public void ApplyDamage(float totalDamage)
    {
        health -= totalDamage;
        healthBar.UpdateValue(health, maxHealth);
        if (health <= 0) Kill();
    }

    public void UsingMana(float manaUsed)
    {
        ResetWaitTime();
        if (mana > 0)
        {
            mana -= manaUsed;
            manaBar.UpdateValue(mana, maxMana);
        }
    }

    public void ManaRegen(float amount)
    {
        if(mana < maxMana)
        {
            mana += amount;
            manaBar.UpdateValue(mana, maxMana);
        }
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


    public void AddPoints(float morePoints)
    {
        points += morePoints;
        //ScoreUpdate.progressBar.fillAmount = points / 100f;
        if (points >= 100) Win();
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
    void Start () {
        invincible = gameObject.GetComponentInParent<Invincible>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update () {
        waitTime += Time.deltaTime;
        isFiring = animator.GetBool("isFiring");
        if (!isInvincible)
        {
            if (isFiring == true)
            {
                UsingMana(.4f);
            }
            if (isFiring == false && mana < maxMana && waitTime >= 2)
            {
                ManaRegen(.1f);
            }
            if (!isInvincible && !isInvincCD && Input.GetKeyDown(KeyCode.LeftShift))
            {
                animator.SetBool("isInvinc", true);
                BeingInvinc();
            }
            if (isInvincCD && invincibleCD <= 5)
            {
                shiftKey.fillAmount = 1 - invincibleCD / 5;
                invincibleCD += Time.deltaTime;
            }
            else if (isInvincCD && invincibleCD > 5)
            {
                isInvincCD = false;
                invincibleCD = 0;
            }
        }
        else
        {
            if (invincibleTimer <= 1 && isInvincible)
            {
                invincibleTimer += Time.deltaTime;
            }

            else
            {
                StopBeingInvincible();
                animator.SetBool("isInvinc", false);
            }
        }
        PassiveEnergyGain();
	}

    void BeingInvinc()
    {
        shiftKey.fillAmount = 1;
        isInvincible = true;
        invincible.GoInvincible();
    }
    void StopBeingInvincible()
    {
        if (!isInvincible || invincibleCD != 0f) return;
        isInvincible = false;
        invincible.ExitInvincible();
        invincibleTimer = 0f;
        InvincCD();
    }
    void InvincCD()
    {
        isInvincCD = true;
    }
    public bool GetIsInvinc()
    {
        return isInvincible;
    }
}


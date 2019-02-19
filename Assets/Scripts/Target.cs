using UnityEngine;

public class Target : MonoBehaviour {

    public float health = 10;
    public GameObject canvas;
    public ValueBar healthBar;

    private Animator animator;
    private bool dying = false;
    private float disappearTimer = 0f;
    private FollowingAI ai;
    private float maxHealth;
    private bool isOnFire = false;
    private float onFireTimer = 0f;

    void Start()
    {
        canvas.SetActive(false);
        animator = GetComponent<Animator>();
        ai = GetComponent<FollowingAI>();
        maxHealth = health;
    }

    void Update()
    {
        if (isOnFire)
        {
            TakeDamage(.1f);
            onFireTimer += Time.deltaTime;
            if (onFireTimer >= 2f) isOnFire = false;
        }
        if (health < maxHealth)
        {
            canvas.SetActive(true);
        }
        if (health <= 0f && !dying) Die();
        if (dying)
        {
            disappearTimer += Time.deltaTime;
            if (disappearTimer >= 2f) Destroy(gameObject);
        }
    }
    public void TakeDamage(float amount)
    {
        if (health <= 0f && !dying) Die();
        else
        {
            health -= amount;
            healthBar.UpdateValue(health, maxHealth);
        }
    }

    public void GainEnergy(float amount)
    {
        MageStatus mageStatus = FindObjectOfType<MageStatus>();
        PlayerStatus playerStatus = FindObjectOfType<PlayerStatus>();
        if (mageStatus != null)
        {
            mageStatus.GainEnergy(amount);
        }
        else playerStatus.GainEnergy(amount);
    }

    public void GainMana(float amount)
    {
        MageStatus mage = FindObjectOfType<MageStatus>();
        mage.ResetWaitTime();
        mage.ManaRegen(amount);
    }

    public void Die()
    {
        dying = true;
        animator.SetTrigger("Dead");
        gameObject.layer = 13;
        ai.SetDeathPos(ai.transform.position);
        ai.SetDeathRot(ai.transform.rotation);
        //PlayerStatus playerStatus = FindObjectOfType<PlayerStatus>();
        //Destroy(gameObject);
        //playerStatus.AddPoints(score);
    }

    /*
    void OnCollisionEnter(Collision col)
    {
        if (col.collider.GetComponentInChildren<MageStatus>())
        {
            MageStatus mageStatus = FindObjectOfType<MageStatus>();
            mageStatus.ApplyDamage(damage);
        }
    }*/

    public bool GetDying()
    {
        return dying;
    }

    public void SetOnFire()
    {
        isOnFire = true;
        onFireTimer = 0f;
    }
}

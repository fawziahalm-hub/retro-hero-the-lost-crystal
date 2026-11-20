using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5;
    private int currentHealth;
    private Animator anim;

    void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("صحة اللاعب المتبقية: " + currentHealth);

        // تشغيل أنيميشن الضرر لللاعب إن وجد
        if (anim != null) anim.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("مات اللاعب!");
        if (anim != null) anim.SetBool("isDead", true);
        
        // تعطيل حركة اللاعب عند الموت
        GetComponent<PlayerController>().enabled = false;
    }
}
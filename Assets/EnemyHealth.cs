using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3;
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

        // تشغيل أنيميشن الضرر
        if (anim != null) anim.SetTrigger("Hurt");

        // التحقق من الموت
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // 1. تشغيل أنيميشن الموت
        if (anim != null) anim.SetBool("isDead", true);

        // 2. إلغاء الكولايدر والفيزياء
        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null) rb.simulated = false;

        this.enabled = false;

        // 3. حذف العدو بعد انتهاء أنيميشن الموت
        Destroy(gameObject, 1.5f);
    }
}
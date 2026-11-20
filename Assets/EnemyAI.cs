using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("إعدادات الصحة")]
    public int maxHealth = 3;
    private int currentHealth;

    [Header("إعدادات الحركة والتعقب")]
    public float moveSpeed = 2f;
    public float detectionRange = 5f; // مسافة كشف اللاعب
    public float attackRange = 1f;    // مسافة بدء الهجوم

    [Header("إعدادات الهجوم")]
    public int attackDamage = 1;
    public float attackRate = 1.5f;   // الوقت بين كل ضربة والثانية
    private float nextAttackTime = 0f;

    [Header("المكونات والطبقات")]
    public LayerMask playerLayer;     // تحديد طبقة اللاعب (Player)
    private Transform player;
    private Animator anim;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // البحث عن اللاعب في المشهد عبر الـ Tag الخاص به
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Update()
    {
        if (isDead || player == null) return;

        // حساب المسافة بين العدو واللاعب
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // 1. إذا كان اللاعب في نطاق الكشف وخارج نطاق الهجوم -> امشي واحق اللاعب
        if (distanceToPlayer <= detectionRange && distanceToPlayer > attackRange)
        {
            ChasePlayer();
        }
        // 2. إذا كان اللاعب قريب جداً -> توقف وهاجم
        else if (distanceToPlayer <= attackRange)
        {
            StopMovement();

            if (Time.time >= nextAttackTime)
            {
                AttackPlayer();
                nextAttackTime = Time.time + attackRate;
            }
        }
        // 3. إذا كان اللاعب بعيداً -> توقف مكانك (Idle)
        else
        {
            StopMovement();
        }
    }

    void ChasePlayer()
    {
        // تحديد اتجاه حركة العدو نحو اللاعب
        float direction = player.position.x - transform.position.x;

        // تحريك العدو
        rb.linearVelocity = new Vector2(Mathf.Sign(direction) * moveSpeed, rb.linearVelocity.y);

        // التدوير (Flip) لاتجاه الحركة
        if (direction > 0) spriteRenderer.flipX = false;
        else if (direction < 0) spriteRenderer.flipX = true;

        // تشغيل أنيميشن المشي (Walk) عبر رفع قيمة Speed
        if (anim != null) anim.SetFloat("Speed", 1f);
    }

    void StopMovement()
    {
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        
        // العودة لأنيميشن الوقوف (Idle) عند إرجاع Speed لـ 0
        if (anim != null) anim.SetFloat("Speed", 0f);
    }

    void AttackPlayer()
    {
        // تشغيل أنيميشن الهجوم
        if (anim != null) anim.SetTrigger("Attack");

        // كشف إصابة اللاعب وإلحاق الضرر به
        Collider2D hitPlayer = Physics2D.OverlapCircle(transform.position, attackRange, playerLayer);
        if (hitPlayer != null)
        {
            PlayerHealth playerHealth = hitPlayer.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
            }
        }
    }

    // دالة تلقي الضربات من اللاعب
    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        if (anim != null) anim.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;

        // 1. تشغيل أنيميشن الموت
        if (anim != null) anim.SetBool("isDead", true);

        // 2. تعطيل الفيزياء والكولايدر فوراً
        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        rb.simulated = false;
        this.enabled = false;

        // 3. مسح العدو من اللعبة بعد ثانية ونصف
        Destroy(gameObject, 1.5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float checkRadius = 0.2f;س
    public LayerMask groundLayer;
    private bool isGrounded;

    [Header("Components")]
    private Rigidbody2D rb;
    private Animator anim;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip jumpSound; // اسحبي صوت القفز هنا في الـ Inspector

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        // إحضار الـ AudioSource تلقائياً إن لم يُربط يدوياً
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        // التحقق من ملامسة الأرض
        if (groundCheck != null)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
        }

        // الحركة الأفقیة
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // تغيير اتجاه الشخصية (Flip)
        if (moveInput > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (moveInput < 0)
            transform.localScale = new Vector3(-1, 1, 1);

        // تحديث بارامترات الأنيميشن
        if (anim != null)
        {
            anim.SetFloat("Speed", Mathf.Abs(moveInput));
            anim.SetBool("isGrounded", isGrounded);
        }

        // القفز وتشيغيل الصوت
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            // تشغيل صوت القفز
            if (audioSource != null && jumpSound != null)
            {
                audioSource.PlayOneShot(jumpSound);
            }
        }
    }
}
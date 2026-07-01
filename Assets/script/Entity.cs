using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Animator anim;
    protected Collider2D col;
    protected SpriteRenderer sr;

    [Header("Entity Health")]
    [SerializeField] protected int maxHealth = 1; 
    [SerializeField] protected int currentHealth;
    [SerializeField] private Material damageMaterial;
    [SerializeField] private float damageFeedBackDuration = .2f;
    private Coroutine damageFeedbackCoroutine;


    [Header("Attack Details")]
    [SerializeField] protected float attackRadius;
    [SerializeField] protected Transform attackPoint;
    [SerializeField] protected LayerMask WhatIsTarget;


    [Header("Collision Details")]
    [SerializeField] private float groundCheckDistance = 0.5f;
    [SerializeField] private LayerMask whatisGround;
    protected bool isGrounded;


    //Facing Direction Details
    protected int facingDir = 1;
    protected bool FacingRight = true;
    protected bool canMove = true;


    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        anim = GetComponentInChildren<Animator>();
        sr = GetComponentInChildren<SpriteRenderer>();

        currentHealth = maxHealth;
    }

    protected virtual void Update()
    {

        HandleCollision();
        HandleMovement();
        HandleAnimation();
        Handleflip();

    }

    public void DamageTargets()
    {
        Collider2D[] enemyColliders = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, WhatIsTarget);

        foreach (Collider2D enemy in enemyColliders)
        {
            Entity entityTarget = enemy.GetComponent<Entity>();
            entityTarget.TakeDamage();
        }
    }

    private void TakeDamage()
    {
        currentHealth -= 1;
        PlayerDamageFeedBack();
        if(currentHealth <= 0)
            Die();
    }

    protected virtual void Die()
    {
        anim.enabled = false;
        col.enabled = false;

        rb.gravityScale = 12;
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 15);

        Destroy(gameObject, 3);
    }

    private void PlayerDamageFeedBack()
    {
        if (damageFeedbackCoroutine != null)
            StopCoroutine(damageFeedbackCoroutine);
        StartCoroutine(DamageFeedbackCo());
    }

    private IEnumerator DamageFeedbackCo()
    {
        Material originalMat = sr.material;
        sr.material = damageMaterial;
        yield return new WaitForSeconds(damageFeedBackDuration);
        sr.material = originalMat;
    }


    public virtual void EnableMovement(bool enable)
    {
        canMove = enable;
    }


    protected void HandleAnimation()
    {
        anim.SetFloat("xVelocity", rb.linearVelocity.x);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", rb.linearVelocity.y);
    }

    protected virtual void HandleAttack()
    {
        if (isGrounded)
            anim.SetTrigger("attack");
    }

    protected virtual void HandleMovement()
    {
    }

    protected virtual void HandleCollision()
    {
           isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatisGround);
    }

    protected virtual void Handleflip()
    {
        if (rb.linearVelocity.x > 0 && FacingRight == false)
            flip();
        else if (rb.linearVelocity.x < 0 && FacingRight == true)
            flip();
    }

    public virtual void  flip()
    {
        transform.Rotate(0, 180, 0);
        FacingRight = !FacingRight;
        facingDir *= -1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -groundCheckDistance));

        if(attackPoint != null)
            Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}
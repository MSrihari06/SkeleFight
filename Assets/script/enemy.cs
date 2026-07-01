using UnityEngine;

public class enemy : Entity
{
    private bool playerDetected;

    [Header("Movement Details")]
    [SerializeField] protected float speed = 5f;


    protected override void Update()
    {
        base.Update();
        HandleAttack();
    }

    protected override void HandleMovement()
    {
        base.HandleMovement();
        if (canMove)
            rb.linearVelocity = new Vector2(facingDir * speed, rb.linearVelocity.y);
        else
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
    }

    protected override void HandleAttack()
    {
        if (playerDetected)
            anim.SetTrigger("attack");
    }

    protected override void HandleCollision()
    {
        base.HandleCollision();
        playerDetected = Physics2D.OverlapCircle(attackPoint.position, attackRadius, WhatIsTarget);
    }

    protected override void Die()
    {
        base.Die();
        UI.Instance.AddKillCount();
    }

}

using UnityEngine;

public class Player : Entity
{

    [Header("Movement Details")]
    [SerializeField] protected float speed = 5f;
    protected float xInput;
    [SerializeField] protected float jumpForce = 5f;

    protected bool canJump = true;
    private float moveInput;

    protected override void Update()
    {
        base.Update();
        HandleInput();
    }

    private void HandleInput()
    {
        moveInput = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space))
            TrytoJump();
        if (Input.GetKeyDown(KeyCode.Mouse0))
            HandleAttack();
    }

    protected override void HandleMovement()
    {
        if (canMove)
            rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);
        else
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
    }

    private void TrytoJump()
    {
        if (isGrounded && canJump)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    public override void EnableMovement(bool enable)
    {
        base.EnableMovement(enable);
        canJump = enable;
    }

    protected override void Die()
    {
        base.Die();
        UI.Instance.EnableGameOver();
    }

}

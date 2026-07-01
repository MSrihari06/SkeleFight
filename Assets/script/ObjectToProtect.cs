using UnityEngine;

public class ObjectToProtect : Entity
{
    private Transform Player;

    protected override void Awake()
    {
        Player = FindFirstObjectByType<Player>().transform;
    }

    protected override void Update()
    {
        Handleflip();
    }

    protected override void Handleflip()
    {
        if (Player != null)
            return;

        if (Player.transform.position.x > transform.position.x && FacingRight == false)
            flip();
        else if (Player.transform.position.x < transform.position.x && FacingRight == true)
            flip();
    }

    protected override void Die()
    {
        base.Die();
        UI.Instance.EnableGameOver();
    }
}

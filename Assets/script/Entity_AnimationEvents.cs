using UnityEngine;

public class Entity_AnimationEvents : MonoBehaviour
{
    private Entity entity;
   

    private void Awake()
    {
        entity = GetComponentInParent<Entity>();
    }

    public void DamageTargets() => entity.DamageTargets();

    public void DisableMovement() => entity.EnableMovement(false);
    

    public void EnableMovement() => entity.EnableMovement(true);

}

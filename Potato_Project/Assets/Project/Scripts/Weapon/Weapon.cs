using UnityEngine;

public class Weapon : MonoBehaviour
{
    public bool isAttacking;
    public WeaponPivot pivot;
    public float attackCooldown = 0.3f;
    protected float currentCooldown;
    public Player player;
    
    void Update()
    {
        if (currentCooldown > 0f){
            currentCooldown -= Time.deltaTime;
        }

        if (!isAttacking)
        {
            float angle = pivot.GetMouseAngle();
            pivot.SetRotation(angle);
        }
    }

    public virtual bool CanAttack()
    {
        return currentCooldown <= 0f;
    }

    public bool IsAttackFinished()
    {
        return !isAttacking;
    }
    public virtual void Attack() {
        if (!CanAttack()){
            return;
        }
        currentCooldown = attackCooldown;
    }
}

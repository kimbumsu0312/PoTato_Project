using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sword : Weapon
{
    public TrailRenderer trailEffect;
    public BoxCollider2D attackArea;

    public float minAngle = -120f;
    public float maxAngle = 120f;
    public float duration = 0.25f;
    public float pushPower = 3f;
    public override void Attack()
    {
        if (!CanAttack()) return;

        currentCooldown = attackCooldown;

        StartCoroutine(Slash());
    }
    IEnumerator Slash()
    {
        isAttacking = true;

        float baseAngle = pivot.transform.eulerAngles.z;

        Vector2 forward = new Vector2(
            Mathf.Cos(baseAngle * Mathf.Deg2Rad),
            Mathf.Sin(baseAngle * Mathf.Deg2Rad)
        );
        player.lastMoveDir = forward;
        float time = 0f;

        while (time < duration)
        {
            float t = time / duration;
            float angle = Mathf.Lerp(minAngle, maxAngle, t);
            pivot.SetRotation(baseAngle + angle);
            if (time < 0.05f)
            {
                player.rigid.MovePosition(
                    player.rigid.position + forward * pushPower * Time.deltaTime
                );
            }
            time += Time.deltaTime;
            yield return null;
        }

        isAttacking = false;
    }
}

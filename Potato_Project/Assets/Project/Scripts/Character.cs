using UnityEngine;

public class Character : MonoBehaviour
{
    public float    speed;
    public bool     isDead = false;
    protected Vector2 Move(Vector2 dir)
    {
        Vector2 nextVec = dir * speed * Time.fixedDeltaTime;
        return nextVec;
    }
}

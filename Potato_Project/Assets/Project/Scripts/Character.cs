using UnityEngine;
using UnityEngine.Timeline;

public class Character : MonoBehaviour
{
    public float    speed;
    public bool     isDead = false;
    public Vector2 Move(Vector2 dir)
    {
        Vector2 nextVec = dir * speed * Time.fixedDeltaTime;
        return nextVec;
    }
}

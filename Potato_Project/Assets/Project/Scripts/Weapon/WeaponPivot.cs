using UnityEngine;
using UnityEngine.InputSystem;
public class WeaponPivot : MonoBehaviour
{
    Camera cam;

    void Awake()
    {
        cam = Camera.main;
    }
    public void SetRotation(float angle)
    {
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    public float GetMouseAngle()
    {
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPos = cam.ScreenToWorldPoint(mouseScreenPos);
        mouseWorldPos.z = 0f;

        Vector3 dir = mouseWorldPos - transform.position;
        return Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    }
}

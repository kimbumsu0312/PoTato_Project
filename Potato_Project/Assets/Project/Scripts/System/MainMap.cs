using UnityEngine;

public class MainMap : MonoBehaviour
{
    [SerializeField] private RectTransform map;
    [SerializeField] private float moveDistance = 500f;
    [SerializeField] private float speed = 5f;

    private Vector2 targetPos;

    public void MoveLeft()
    {
        targetPos += new Vector2(moveDistance, 0);
    }

    public void MoveRight()
    {
        targetPos -= new Vector2(moveDistance, 0);
    }

    public void Update()
    {
        map.anchoredPosition = Vector2.Lerp(
            map.anchoredPosition,
            targetPos,
            Time.deltaTime * speed
        );
    }
}

using System.Collections;
using UnityEngine;

public class VillagePortal : MonoBehaviour
{
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private int villageSceneIndex;
    [SerializeField] private float delay;

    private SpriteRenderer spriter;
    private Animator animator;
    private Collider2D col2D;

    private bool isTriggered = false;

    void Awake()
    {
        spriter = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        col2D = GetComponent<Collider2D>();
        animator.speed = 0.5f;
    }

    public void OnstageCleared()
    {
        animator.SetTrigger("open");
        col2D.enabled = true;
        spriter.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isTriggered) return;

        if (collision.CompareTag("Player"))
        {
            isTriggered = true;
            StartCoroutine(CloseAndLoadLevel());
        }
    }

    private IEnumerator CloseAndLoadLevel()
    {
        animator.SetTrigger("close");
        col2D.enabled = false;
        yield return new WaitForSeconds(delay);
        sceneLoader.LoadScene(villageSceneIndex);
    }
}
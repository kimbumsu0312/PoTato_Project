using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private Monster monsterPrefab;
    [SerializeField] private Transform[] spawnPoints;

    public Rigidbody2D target;

    private const string MONSTER_KEY = "Monster";

    private void Awake()
    {
        PoolManager.Instance.CreatePool<Monster>(
            MONSTER_KEY, factory: () => Instantiate(monsterPrefab), 
            onGet: m => m.Init(), onReturn: m => m.ResetState(),initialize: 2);
    }

    private void Start()
    {
        Spawn(0);
        Spawn(1);
    }

    public void Spawn(int index)
    {
        var monster = PoolManager.Instance.Get<Monster>(MONSTER_KEY);
        monster.SetTarget(target);
        monster.transform.position = spawnPoints[index].position;
    }

    private void OnDestroy()
    {
        PoolManager.Instance.ClearPool(MONSTER_KEY);
    }

}

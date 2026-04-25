using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class MonsterPoolEntry
{
    public string key;
    public Monster prefab;
    public int initialSize = 2;
}

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private MonsterPoolEntry[] monsterEntries;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Rigidbody2D Player;

    private int spawnIndex;
    public event Action<Monster> OnMonsterSpawned;

    private void Awake()
    {
        foreach (var entry in monsterEntries)
        {
            var prefab = entry.prefab;
            PoolManager.Instance.CreatePool<Monster>(
                entry.key,
                factory: () => Instantiate(prefab),
                onGet: m => m.Init(),
                onReturn: m => m.ResetState(),
                initialize: entry.initialSize);
        }
    }

    // WaveManager가 호출 — monsterKey 종류의 몬스터를 count만큼 interval 간격으로 스폰
    public IEnumerator SpawnWave(string monsterKey, int count, float interval)
    {
        for (int i = 0; i < count; i++)
        {
            Monster monster = Spawn(monsterKey);
            OnMonsterSpawned?.Invoke(monster);
            yield return new WaitForSeconds(interval);
        }
    }

    private Monster Spawn(string monsterKey)
    {
        var monster = PoolManager.Instance.Get<Monster>(monsterKey);
        monster.poolKey = monsterKey;
        monster.SetTarget(GameManager.Instance.player.GetComponent<Rigidbody2D>());
        
        monster.transform.position = spawnPoints[spawnIndex % spawnPoints.Length].position;
        spawnIndex++;
        return monster;
    }

    private void OnDestroy()
    {
        foreach (var entry in monsterEntries)
            PoolManager.Instance.ClearPool(entry.key);
    }
}

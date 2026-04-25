using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class SpawnEntry
{
     public string monsterKey;  // 풀 키로 어떤 몬스터인지 구분
    public int     count;
}
[CreateAssetMenu(fileName = "WaveData", menuName = "Game/Wave Data")]
public class WaveData : ScriptableObject
{
    public List<SpawnEntry> spawnEntries;
    public float spawnInterval = 0.5f;
    public float waveStartDelay = 2f;
}

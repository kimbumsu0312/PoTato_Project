  using System;
  using System.Collections;
  using UnityEngine;

  public class WaveManager : MonoBehaviour
  {
      [SerializeField] private StageData stageData;
      [SerializeField] private MonsterSpawner spawner; 

      public static event Action<int, int> OnWaveChanged;  // (현재, 전체)
      public static event Action OnStageComplete;

      private int currentWaveIndex;
      private int aliveCount;

      private void OnEnable()
      {
        spawner.OnMonsterSpawned += TrackMonster;
      }

      private void OnDisable()
      {
        spawner.OnMonsterSpawned -= TrackMonster;
      }

      private void Start()
      {
        StartCoroutine(RunWave(0));
      }

      private IEnumerator RunWave(int index)
      {
          currentWaveIndex = index;
          WaveData wave = stageData.waves[index];

          aliveCount = 0;
          OnWaveChanged?.Invoke(index + 1, stageData.waves.Count);

          yield return new WaitForSeconds(wave.waveStartDelay);

          foreach (var entry in wave.spawnEntries)
              yield return spawner.SpawnWave(entry.monsterKey, entry.count, wave.spawnInterval);
      }

      private void TrackMonster(Monster monster)
      {
          aliveCount++;
          monster.deadEvent += HandleMonsterDied;
      }

      private void HandleMonsterDied(Monster monster)
      {
          monster.deadEvent -= HandleMonsterDied;
          aliveCount--;
          GameManager.Instance.kill++;

          if (aliveCount > 0) return;

          bool isLastWave = currentWaveIndex + 1 >= stageData.waves.Count;
          if (isLastWave)
              OnStageComplete?.Invoke();
          else
              StartCoroutine(RunWave(currentWaveIndex + 1));
      }
  }
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "StageData", menuName = "Game/Stage Data")]
public class StageData : ScriptableObject
{
    public string stageName;
    public GameManager.DugeonType dungeonType;
    public List<WaveData> waves;
}


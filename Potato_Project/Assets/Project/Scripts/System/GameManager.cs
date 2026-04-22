using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public enum DugeonType { Village, TimeAttack, Kill}
    public Player player;
    
    public float gameTime;
    public int kill;

    [Header("# Player Info")]
    public int      maxHealth = 100;
    public int      curHealth = 100;
    public int      maxMp = 100;
    public int      curMp = 100;

    public int      level;
    public int      exp;
    public int[]    nextExp = { 10, 30, 60, 100, 150, 210, 280, 360, 450, 600};


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SeleteDugeon(DugeonType DugeonType)
    {
    }
}

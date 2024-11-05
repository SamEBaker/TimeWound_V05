using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    //https://www.youtube.com/watch?v=oh33gd7zs_o&t=292s
    [SerializeField]
    private Transform[] spawnPts;
    //[SerializeField]
    //private int maxPoolSize;

    [SerializeField]
    private float spawnTime;
    private float timeSinceSpawn;

    [SerializeField]
    private EnemyBehavior EnemyPrefab;
    private IObjectPool<EnemyBehavior> enemyPool;

    private void Awake()
    {
        enemyPool = new ObjectPool<EnemyBehavior>(CreateEnemy, OnGet, OnRelease) ;

    }

    private void OnGet(EnemyBehavior enemy)
    {
        enemy.gameObject.SetActive(true);
        Transform randomSpawn = spawnPts[Random.Range(0, spawnPts.Length)];
        enemy.transform.position = randomSpawn.position;
    }

    private void OnRelease(EnemyBehavior enemy)
    {
        enemy.gameObject.SetActive(false);
    }
    private EnemyBehavior CreateEnemy()
    {
        EnemyBehavior enemy = Instantiate(EnemyPrefab) ;
        enemy.SetPool(enemyPool);
        return enemy ;
    }
    // Update is called once per frame
    void Update()
    {
        if(Time.time > timeSinceSpawn)
        {
            enemyPool.Get();
            timeSinceSpawn = Time.time + spawnTime;
        }
    }
}

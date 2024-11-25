using UnityEngine;
using UnityEngine.Pool;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    //https://www.youtube.com/watch?v=oh33gd7zs_o&t=292s
    [SerializeField]
    private List<GameObject> spawnPts;

    [SerializeField]
    private GameObject EnemyPrefab;


    [SerializeField]
    private float spawnTime;
    private float timeSinceSpawn;

    public int maxPoolSize = 22;
    public int stackDefaultCapacity = 10;
    public IObjectPool<EnemyBehavior> Pool
    {
        get
        {
            if (_pool == null)
                _pool =
                new ObjectPool<EnemyBehavior>(
                    CreatedPooledItem,
                    OnTakeFromPool,
                    OnReturnedToPool,
                    OnDestroyPoolObject,
                    true,
                    stackDefaultCapacity,
                    maxPoolSize);
            return _pool;
        }
    }
    private IObjectPool<EnemyBehavior> _pool;

    public void Update()
    {
        if (Time.time > timeSinceSpawn)
        {
            Spawn();
            timeSinceSpawn = Time.time + spawnTime;
        }
    }

    private EnemyBehavior CreatedPooledItem()
    {
        var go =
        Instantiate(EnemyPrefab);
        EnemyBehavior enemy = go.GetComponent<EnemyBehavior>();
        go.name = "Enemy";
        enemy.Pool = Pool;
        //enemy.transform.position = spawnPts[Random.Range(1, 22)].transform.position;
        return enemy;
    }
    private void OnReturnedToPool(EnemyBehavior enemy)
    {
        enemy.gameObject.SetActive(false);
    }
    private void OnTakeFromPool(EnemyBehavior enemy)
    {
        enemy.gameObject.SetActive(true);
    }
    private void OnDestroyPoolObject(EnemyBehavior enemy)
    {
        Destroy(enemy.gameObject);
    }
    public void Spawn()
    {
            var enemy = Pool.Get();
            enemy.transform.position = spawnPts[Random.Range(1, 22)].transform.position;
    }
}
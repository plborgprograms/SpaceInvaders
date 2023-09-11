using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Pool;

public class BulletPool : MonoBehaviour
{
    public Bullet enemyBulletPrefab;
    public Bullet playerBulletPrefab;

    // stack-based ObjectPool available with Unity 2021 and above
    public static IObjectPool<Bullet> enemyObjectPool;
    public static IObjectPool<Bullet> playerObjectPool;


    // throw an exception if we try to return an existing item, already in the pool
    [SerializeField] private bool collectionCheck = true;

    // extra options to control the pool capacity and maximum size
    [SerializeField] private int defaultCapacity = 50;
    [SerializeField] private int maxSize = 100;

    private void Awake()
    {
        enemyObjectPool = new ObjectPool<Bullet>(CreateEnemyProjectile,
            OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject,
            collectionCheck, defaultCapacity, maxSize);
        playerObjectPool = new ObjectPool<Bullet>(CreatePlayerProjectile,
            OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject,
            collectionCheck, defaultCapacity, maxSize);
    }

    // invoked when creating an item to populate the object pool
    private Bullet CreateEnemyProjectile()
    {
        Bullet projectileInstance = Instantiate(enemyBulletPrefab);
        projectileInstance.objectPool = enemyObjectPool;
        return projectileInstance;
    }

    // invoked when creating an item to populate the object pool
    private Bullet CreatePlayerProjectile()
    {
        Bullet projectileInstance = Instantiate(playerBulletPrefab);
        projectileInstance.objectPool = playerObjectPool;
        return projectileInstance;
    }

    // invoked when returning an item to the object pool
    private void OnReleaseToPool(Bullet pooledObject)
    {
        pooledObject.gameObject.SetActive(false);
    }

    // invoked when retrieving the next item from the object pool
    private void OnGetFromPool(Bullet pooledObject)
    {
        pooledObject.gameObject.SetActive(true);
    }

    // invoked when we exceed the maximum number of pooled items (i.e. destroy the pooled object)
    private void OnDestroyPooledObject(Bullet pooledObject)
    {
        Destroy(pooledObject.gameObject);
    }

    private void FixedUpdate()
    {

    }
}

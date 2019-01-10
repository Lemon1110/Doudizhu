using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PoolManager {
    private static PoolManager instance;
    
    public static PoolManager Instance
    {
        get
        {
            if (instance == null)
            { instance = new PoolManager();
            }
            return instance;
        }
    }

    public const string PoolConfigPath = "Assets/Game/Scripts/Framework/Pool/Resources/pool.asset";
    //一个字典 key是池子名字 value是单个池子
    private Dictionary<string,ObjectPool>  poolDic = new Dictionary<string, ObjectPool>();
    public PoolManager()
    {
        ObjectPoolList objectPoolList = Resources.Load<ObjectPoolList>("pool");
        foreach (var pool in objectPoolList.PoolList)
        {
            this.poolDic.Add(pool.Name, pool);
        }
    }


    /// <summary>
    /// 根据名字取物体
    /// </summary>
    /// <param name="poolName"></param>
    /// <returns></returns>
    public GameObject GetObject(string poolName)
    {
        if (!poolDic.ContainsKey(poolName))
        {
            Debug.LogError("没有这个" + poolName + "池子!");
            return null;
        }
        ObjectPool pool = poolDic[poolName];
        return pool.GetObject();
    }
    /// <summary>
    /// 隐藏指定物体
    /// </summary>
    /// <param name="go"></param>
    public void HideObject(GameObject go)
    {
        foreach (ObjectPool p in poolDic.Values)
        {
            if (p.Contains(go))
            {
                p.HideObject(go);
                return;
            }
        }
    }
    /// <summary>
    /// 根据一个池子隐藏该池子的所有物体
    /// </summary>
    public void HideAllObject(string poolName)
    {
        if (!poolDic.ContainsKey(poolName))
        {
            Debug.LogError("没有这个" + poolName + "池子!");
            return ;
        }
        ObjectPool pool = poolDic[poolName];
        pool.HideAllObject();
    }
    /// <summary>
    /// 还原对象池
    /// </summary>
    public void InitAllPool()
    {
        foreach (var pool in poolDic.Values)
        {
            pool.InitPool();
        }
    }
}

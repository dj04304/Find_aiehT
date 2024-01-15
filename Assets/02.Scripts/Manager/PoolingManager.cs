using OpenCover.Framework.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PoolingManager : MonoBehaviour
{
    [Serializable]
    public struct Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }
    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;
    Queue<GameObject> _poolObject = new Queue<GameObject>();

    private void Awake()
    {
        Initialize();
    }

    private void Initialize() //�̸� ���� �ص� ������Ʈ
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (var pool in pools)
        {
            for (int i = 0; i < pool.size; i++)
            {
                var newObj = Instantiate(pool.prefab);
                newObj.gameObject.SetActive(false);
                _poolObject.Enqueue(newObj);
            }
            poolDictionary.Add(pool.tag, _poolObject);
        }
    }

    public void ReturnObject(GameObject obj) //������Ʈ ��Ȱ��ȭ ��ų �޼���
    {
        obj.SetActive(false);
        _poolObject.Enqueue(obj);
    }

    public GameObject SpawnFromPool(string tag) 
    {
        if (!poolDictionary.ContainsKey(tag))
            return null;

        GameObject obj = poolDictionary[tag].Dequeue();
        poolDictionary[tag].Enqueue(obj);

        return obj;
    }

    private void RespawnObject()
    {
        var newObj = Instantiate(gameObject);
        newObj.gameObject.SetActive(false);
    }
}

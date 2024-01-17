using OpenCover.Framework.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PoolingManager : MonoBehaviour
{
    public static PoolingManager Instance;
    [Serializable]
    public struct Pool
    {
        public string ObjectName;
        public GameObject Prefab;
        public int Size;
        public Transform SpawnPoint;
    }
    public List<Pool> Pools;
    Queue<GameObject> _poolObject = new Queue<GameObject>();
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Awake()
    {
        Instance = this;
        Initialize();
    }

    private void Start()
    {
        GetObject();
    }

    private void Initialize() //�̸� ���� �ص� ������Ʈ
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (var pool in Pools)
        {
            for (int i = 0; i < pool.Size; i++)
            {
                var newObj = Instantiate(pool.Prefab, pool.SpawnPoint);
                newObj.gameObject.SetActive(false);
                _poolObject.Enqueue(newObj);
            }
            poolDictionary.Add(pool.ObjectName, _poolObject);
        }
    }

    public void ReturnObject(GameObject obj) //������Ʈ ��Ȱ��ȭ ��ų �޼���
    {
        _poolObject.Enqueue(obj);
        obj.SetActive(false);
    }

    public GameObject GetObject() // ȣ��
    {
        GameObject obj = _poolObject.Dequeue();
        obj.SetActive(true);

        return obj;
    }

    //�߰� ����
}

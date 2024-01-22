using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NatureItemObject : MonoBehaviour
{
    public ItemSO ItemData;
    private float _respawnTimer;
    public float RespawnTime;

    public void GetItem() // 상호작용 됬을때
    {
        //인벤토리 Add
        gameObject.SetActive(false);
    }

    private void Awake()
    {
        _respawnTimer = RespawnTime;
    }

    private void FixedUpdate()
    {
        if (gameObject.activeSelf == false)
        {
            _respawnTimer -= Time.deltaTime;
            if (_respawnTimer <= 0)
            {
                ItemRespawn();
            }
        }
    }

    private void ItemRespawn()
    {
        _respawnTimer = RespawnTime;
        gameObject.SetActive(true);
    }
}

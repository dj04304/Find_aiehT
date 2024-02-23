using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TomatoWeapon : MonoBehaviour
{
    PlayerSO _playerSO;
    Player _player;

    private Transform _playerTrans;

    private Collider _skillCollider;
    private Rigidbody _skillRigidbody;

    [SerializeField] private float _offsetAngle = 0.2f;
    [SerializeField] private float _speed = 10f;

    private const int _skillIndex = 0;

    private float _distance;
    private float _skillDistance;
    private Vector3 _initiatePos;

    private const float _waitTime = 0.1f;
    private float _damage;

    private readonly List<Collider> _alreadyCollidedObjects = new List<Collider>();
    private EquipmentDatas _equipmentDatas;

    private void Awake()
    {
        if (_equipmentDatas == null)
        {
            _equipmentDatas = GameObject.FindWithTag(TagName.Player).GetComponent<EquipmentDatas>();
        }
    }
    private void Start()
    {
        _skillCollider = GetComponentInChildren<Collider>();
        _skillRigidbody = GetComponentInChildren<Rigidbody>();

        GameObject playerObject = GameObject.FindGameObjectWithTag(TagName.Player);
        if (playerObject != null)
        {
            _player = playerObject.GetComponent<Player>();
            _playerSO = _player.Data;
            _playerTrans = playerObject.transform;
        }

        _damage = SkillTotalDamage();
        _distance = 0f;
        _skillDistance = _playerSO.SkillData.GetSkillData(_skillIndex).SkillDistance;
        _initiatePos = transform.position;

        StartCoroutine(ShootSkill());
    }
    private void OnEnable()
    {
        InitializeCollider();
    }

    private void InitializeCollider()
    {
        _alreadyCollidedObjects.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == _skillCollider) return;
        if (_alreadyCollidedObjects.Contains(other)) return;

        _alreadyCollidedObjects.Add(other);

        if (other.TryGetComponent(out EnemyHealthSystem health))
        {
            health.TakeDamage(_damage);
            Destroy(_skillCollider.gameObject);
        }
        if (other.TryGetComponent(out BossHealthSystem bosshealth))
        {
            bosshealth.TakeDamage(_damage);
        }

    }

    IEnumerator ShootSkill()
    {
        Vector3 dir = GetPlayerDir();

        yield return new WaitForSeconds(_waitTime);
        Rotate(dir);

        Vector3 offset = transform.forward * _offsetAngle;

        dir += offset;

        _skillRigidbody.AddForce(dir * _speed, ForceMode.Impulse);
        GameManager.Instance.SoundManager.SFXPlay(SFXSoundPathName.Skill1);
        StartCoroutine(SkillDistance());
    }

    private Vector3 GetPlayerDir()
    {
        Vector3 forwardDir = _playerTrans.forward;
        forwardDir.Normalize();

        return forwardDir;
    }

    private void Rotate(Vector3 dir)
    {
        if (dir == Vector3.zero) return;

        Quaternion rotation = Quaternion.LookRotation(dir);
        transform.rotation = rotation;
    }


    private float SkillTotalDamage()
    {
        float playerDamage = _playerSO.PlayerData.PlayerAttack;
        float skillDamage = _playerSO.SkillData.GetSkillData(_skillIndex).SkillDamage;
        float weaponDamage = _equipmentDatas.SumDmg;

        return playerDamage + (skillDamage + weaponDamage)* _playerSO.PlayerData.PlayerLevel;
    }

    IEnumerator SkillDistance()
    {
        while(_distance < _skillDistance)
        {
            _distance = Vector3.Distance(_initiatePos, gameObject.transform.position);

            yield return null;
        }

        Destroy(_skillCollider.gameObject);
    }
}

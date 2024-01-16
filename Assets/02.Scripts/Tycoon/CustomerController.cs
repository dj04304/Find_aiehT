using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerController : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Animator _animator;
    private GameObject _targetFood;

    public GameObject TargetFood
    {
        get { return _targetFood; }
        set
        {
            _targetFood = value;
            Debug.Log(_targetFood.name);
        }
    }

    public Transform AgentDestination
    {
        //TODO : ���������� or ���� -> �ڸ��� �ִٸ� �ٽ� ����?
        set
        {
            _agent.SetDestination(value.position);
        }
    }

    [NonSerialized] public int _seatNum = 0;

    private void OnEnable()
    {
        _agent = GetComponent<NavMeshAgent>();

        _animator = GetComponentInChildren<Animator>();
        _animator.SetBool("IsWalk", true);
    }

    private void Update()
    {
        if(!_agent.hasPath)
        {
            _animator.SetBool("IsWalk", false);
            transform.rotation = Quaternion.identity;
        }
    }
}

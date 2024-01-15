using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcController : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Animator _animator;

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

    public void DecideDestination(Transform destinationTransform)
    {
        //TODO : ���������� or ���� -> �ڸ��� �ִٸ� �ٽ� ����?
        _agent.SetDestination(destinationTransform.position);
    }
}

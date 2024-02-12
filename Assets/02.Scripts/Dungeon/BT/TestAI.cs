using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestAI : Tree
{
    [SerializeField]
    private Transform _playerTransform;
    [SerializeField]
    private Transform _pigTransform;
    private NavMeshAgent _navMeshAgent;
    private SkinnedMeshRenderer[] meshRenderers;

    private void Awake()
    {
        meshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
        _playerTransform = GameManager.Instance.Player.transform;
        _navMeshAgent =GetComponent<NavMeshAgent>();
        _pigTransform = gameObject.transform;
        MaterialPropertyBlock propBlock = new MaterialPropertyBlock();
  
        for (int x = 0; x < meshRenderers.Length; x++)
        {
            meshRenderers[x].GetPropertyBlock(propBlock);
            propBlock.SetColor("_Color", new Color(1.0f, 0.6f, 0.6f));
            meshRenderers[x].SetPropertyBlock(propBlock);
        }

    }

    protected override Node SetupBehaviorTree()
    {
        _navMeshAgent.speed = 3.5f;
        Node root = new SelectorNode(new List<Node>
        {
            new SequenceNode
            (
                new List<Node>()
                {
                    new CheckPlayerDistanceNode(_pigTransform,2.0f),
                }
            ),
            new SequenceNode
            (
                new List<Node>()
                {
                    new CheckPlayerDistanceNode(_pigTransform,5.0f),
                    new RunAwayNode(_pigTransform,_navMeshAgent),
                }
            ),
            new GoToPlayerNode(_playerTransform, _pigTransform, _navMeshAgent), 
        });
        return root;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out HealthSystem health))
        {
            health.TakeDamage(10);
        }
    }
    
}
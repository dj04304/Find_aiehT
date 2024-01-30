using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerController : MonoBehaviour
{
    #region Field
    private TycoonManager _tycoonManager;
    private FoodCreater _foodCreater;
    private string _targetFoodName;

    private NavMeshAgent _agent;
    private Animator _animator;
    private Collider _collider;

    private float _waitTime;
    private const float _agentBaseOffset = 0.45f;
    private bool _isExit = false;
    private bool _isOrderFood = false;

    private List<GameObject> _collidingAIs = new();

    private FoodPlace _targetFoodPlace;
    public FoodPlace TargetFoodPlace
    {
        get { return _targetFoodPlace; }
        set
        {
            _targetFoodPlace = value;
            _targetFoodPlace.OnCustomerGetFood += GetFood;
        }
    }

    public string TargetFoodName
    {
        get { return _targetFoodName; }
        set { _targetFoodName = value; }
    }

    public Transform AgentDestination
    {
        set { _agent.SetDestination(value.position); }
    }

    public int AgentPriority
    {
        set { _agent.avoidancePriority = value; }
    }

    private Coroutine _co;

    #endregion

    #region Event

    public event Action<GameObject> OnCreateFood;
    public event Action OnCustomerExit;

    #endregion

    #region MonoBehaviour

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _collider = GetComponent<Collider>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        _tycoonManager = TycoonManager.Instance;
        _foodCreater = _tycoonManager._FoodCreater;
        _waitTime = _tycoonManager.CustomerWaitTime;
    }

    private void OnEnable()
    {
        Init();
    }

    private void Update()
    {
        if (_agent.remainingDistance <= _agent.stoppingDistance)
        {
            if(_agent.destination != _tycoonManager.CustomerCreatePos.position)
            {
            }

            if (!_isOrderFood)
            {
                SelectFood();
                _isOrderFood = true;

                //TODO: 한번만 실행되도록 다른곳으로 이동..
                _animator.SetBool("IsWalk", false);
                _agent.baseOffset = _agentBaseOffset;
                transform.rotation = _targetFoodPlace.gameObject.transform.rotation;

                _collider.enabled = false;
                _agent.isStopped = true;
            }

            _waitTime -= Time.deltaTime;
            if (_waitTime <= 0)
            {
                NoReceivedFood();
            }

            if (_isExit)
            {
                _isExit = false;

                StopAllCoroutines();
                OnCustomerExit?.Invoke();
                _targetFoodPlace.CurrentCustomer = null;
                GameManager.instance.PoolingManager.ReturnObject(gameObject);
            }
        }
        else
        {
            //TODO
            _collidingAIs.RemoveAll(ai => !ai.activeInHierarchy);
            for (int i = 0; i < _collidingAIs.Count; ++i)
            {
                if (_collidingAIs[i].GetComponent<NavMeshAgent>().isStopped)
                {
                    RemoveList(_collidingAIs[i]);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("AI"))
        {
            NavMeshAgent otherAgent = other.gameObject.GetComponent<NavMeshAgent>();

            if (Mathf.Approximately(_agent.destination.x, _tycoonManager.CustomerCreatePos.position.x)
                && Mathf.Approximately(_agent.destination.z, _tycoonManager.CustomerCreatePos.position.z)
                && otherAgent.isStopped == false
                && !_collidingAIs.Contains(other.gameObject))
            {
                _collidingAIs.Add(other.gameObject);
                _agent.isStopped = true;
                _animator.SetBool("IsIdle", true);
            }
        }
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.CompareTag("AI"))
    //    {
    //        NavMeshAgent otherAgent = other.gameObject.GetComponent<NavMeshAgent>();
    //        if (otherAgent.isStopped == true)
    //        {
    //            RemoveList(other.gameObject);
    //        }
    //    }
    //}

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("AI"))
        {
            RemoveList(other.gameObject);
        }
    }

    #endregion

    #region 
    private void Init()
    {
        _animator.SetBool("IsWalk", true);
        //_agent.baseOffset = 0.0f;
        
        _isOrderFood = false;

        transform.rotation = Quaternion.identity;
        _animator.gameObject.transform.localPosition = Vector3.zero;
        _animator.gameObject.transform.localRotation = Quaternion.identity;

        _collidingAIs.Clear();
    }

    private void SelectFood()
    {
        List<OrderFood> menu = _tycoonManager.TodayFoods;

        int targetFoodNum = UnityEngine.Random.Range(0, menu.Count);
        _targetFoodName = menu[targetFoodNum].foodSO.CookedFoodObject.name;
        OnCreateFood?.Invoke(menu[targetFoodNum].foodSO.CookedFoodObject);

        --_tycoonManager.TodayFoods[targetFoodNum].foodCount;
        if(_tycoonManager.TodayFoods[targetFoodNum].foodCount == 0)
            _tycoonManager.TodayFoods.Remove(menu[targetFoodNum]);
    }

    private void RemoveList(GameObject obj)
    {
        if (_collidingAIs.Contains(obj))
        {
            _collidingAIs.Remove(obj);
        }

        if (_collidingAIs.Count == 0)
        {
            _agent.isStopped = false;
            _animator.SetBool("IsIdle", false);
        }
    }

    private void GetFood()
    {
        if (_co == null)
        {
            _animator.SetTrigger("GetFood");

            _co = StartCoroutine(EatFood());
        }
    }

    private void NoReceivedFood()
    {
        if (_co == null)
        {
            _animator.SetTrigger("Angry");
            _co = StartCoroutine(ExitRestaurant());
        }
    }

    #endregion

    #region Coroutine

    IEnumerator EatFood()
    {
        _animator.SetBool("IsEat", true);
        yield return new WaitForSeconds(10f);
        _animator.SetBool("IsEat", false);

        StartCoroutine(ExitRestaurant());
    }

    IEnumerator ExitRestaurant()
    {
        _targetFoodName = null;
        _targetFoodPlace.OnCustomerGetFood -= GetFood;

        yield return new WaitForSeconds(8f);
        
        _agent.SetDestination(_tycoonManager.CustomerCreatePos.position);
        _animator.SetBool("IsWalk", true);

        _agent.baseOffset = 0.0f;
        _isExit = true;

        // another AI
        _collider.enabled = true;
        _agent.isStopped = false;


        _waitTime = _tycoonManager.CustomerWaitTime;

        _foodCreater.UnsubscribeCreateFoodEvent(this);

        _co = null;
    }
    #endregion
}

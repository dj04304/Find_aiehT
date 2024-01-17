using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashForceReceiver : MonoBehaviour
{
    private Player _player;
    private StaminaSystem _staminaSystem;
   
    [SerializeField] private float _dashDuration = 0.5f;
    [SerializeField] private float _dashCoolTime = 5f;

    private float _dashTime = 0f; // �뽬�� �ϱ����� �ð�
    private float _coolTime= 0f; //  ��Ÿ���� ����ϱ����� �ð�

    private bool _isDash;
    public bool IsCoolTime { get; private set; } // true => ��Ÿ�� ��

    //private int _maxStamina;
    //private int _stamina;

    //private Vector3 _dashStartPosition;
    //private Vector3 _dashTargetPosition;

    private void Start()
    {
        _player = GetComponent<Player>();
        _staminaSystem = _player.GetComponent<StaminaSystem>();


        IsCoolTime = false;
        _isDash = false;

        //_maxStamina = _player.Data.PlayerData.GetPlayerMaxStamina();
        //_stamina = _maxStamina;

        //Debug.Log(_maxStamina);
        //Debug.Log(_stamina);
    }

    void FixedUpdate()
    {
        if(_isDash)
        {
            _dashTime += Time.fixedDeltaTime;

            if(_dashTime >= _dashDuration )
            {
                _isDash = false;

                if (!IsCoolTime)
                    StartCoroutine(CoolDown());

            }
            // ��Ÿ���� ���� ���
            else if (IsCoolTime)
            {
                _coolTime += Time.fixedDeltaTime;

                if(_coolTime >= _dashCoolTime)
                {
                    IsCoolTime = false;
                    _coolTime = 0f;
                }

            }


        }    
    }

    //public bool CanUseDash(int dashStamina)
    //{
    //    return _stamina >= dashStamina;
    //}

    ///// �뽬�� - 10;
    //public void UseDash(int dashStamina)
    //{
    //    if (_stamina == 0) return;
    //    _stamina = Mathf.Max(_stamina - dashStamina, 0);

    //    //Debug.Log("���¹̳�" + _stamina);
    //}

    public void Dash(float dashForce)
    {
        if (_player.GroundCheck.IsGrounded() && !_isDash && !IsCoolTime)
        {
            _isDash = true;
            _dashTime = 0f;

            StartCoroutine(DashCoroutine(dashForce));

        }

    }

    IEnumerator DashCoroutine(float dashForce)
    {
        Vector3 dashDirection = transform.forward;
        Vector3 dashPower = dashDirection;

       while (_dashTime <= _dashDuration)
        {
            
            dashPower += dashDirection * dashForce;

            //* -Mathf.Log(1 / Player.Rigidbody.drag)

            //_player.Rigidbody.velocity += dashPower;

            _player.Rigidbody.AddForce(dashPower, ForceMode.VelocityChange);

            yield return new WaitForSeconds(0.01f);
        }

        yield return null;
    }

    IEnumerator CoolDown()
    {
        IsCoolTime = true;
        yield return new WaitForSeconds(_dashCoolTime);
        IsCoolTime = false;
    }

}

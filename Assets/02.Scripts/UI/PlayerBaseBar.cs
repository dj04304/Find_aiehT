using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBaseBar : MonoBehaviour
{
    //public Playerscript Player;
    protected Slider _slider;

    [SerializeField] protected TMP_Text _hpText;

    //�÷��̾� ������ ��������
    // �÷��̾� �� ��ġ ����� += ChangeHpBar();

    //FIX
    // �׽�Ʈ�� , Player Data �޾ƿ���
    [SerializeField] protected int _currentValue = 10;
    [SerializeField] protected int _maxValue = 20;


    protected void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    protected void Start()
    {
        // ü�� ���� �� �̺�Ʈ �߰�  +=ChangeHpBar();
    }
    public void Update()
    {
        //�׽�Ʈ�� ������Ʈ��
        ChangeHpBar();
    }


    // Player Hp ���� ��
    public virtual void ChangeHpBar()
    {
        _hpText.text = (_currentValue + "/" + _maxValue);
        _slider.value = (float)_currentValue / (float)_maxValue;
    }
}

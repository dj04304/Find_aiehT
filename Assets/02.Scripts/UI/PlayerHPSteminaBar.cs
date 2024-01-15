using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPSteminaBar : PlayerBaseBar
{

    [SerializeField] private Image _leftImage;
    [SerializeField] private Image _rightImage;

    //�÷��̾� ������ ��������
    // �÷��̾� �� ��ġ ����� += ChangeHpBar();


    // Player Hp ���� ��
    public override void ChangeHpBar() 
    {
        
        if (_slider.value == 1)
        {
            _rightImage.color = new Color32(255, 255, 255, 255);
        }
        else if (_slider.value != 1)
        {
            _rightImage.color = new Color32(155, 140, 140, 60);
        }

        if (_slider.value == 0)
        {
            _leftImage.color = new Color32(155, 140, 140, 60);
        }
        else if(_slider.value != 0)
        {
            _leftImage.color = new Color32(255, 255, 255, 255);
        }
        base.ChangeHpBar();
    }

}

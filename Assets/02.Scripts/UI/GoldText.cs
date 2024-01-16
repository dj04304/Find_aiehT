using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldText : MonoBehaviour
{
    private TMP_Text _glodText;
    //FIX
    //�׽�Ʈ
    public int _goldAmount;

    private void Awake()
    {
        _glodText = GetComponent<TMP_Text>();
        //FIX
        //��尪 �ٲ� �� += gold
        _goldAmount = 400;
    }
    private void Update()
    {
        _glodText.text = GetComma(_goldAmount).ToString();
    }
    public string GetComma(int data)
    {
        return string.Format("{0:#,###}", data); 
    }
    
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private Stack<GameObject> openPopups = new Stack<GameObject>();
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        // �ڷΰ��� Ű�� ������ ���� �ֱٿ� ���� �˾��� �ݽ��ϴ�.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseLastPopup();
        }
    }

    private void OpenPopup(string uiname) 
    {

        GameObject newUI = Instantiate(Resources.Load<GameObject>("Sound/SFX/" + uiname));
        newUI.SetActive(true); 
        openPopups.Push(newUI);
    }
    private void CloseLastPopup()
    {
        throw new NotImplementedException();
    }
}

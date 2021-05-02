using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private GameObject closedGate;
    [SerializeField] private GameObject openGate;

    public void SetOpened(bool opened)
    {
        if (opened)
        {
            closedGate.SetActive(false);
            openGate.SetActive(true);
        }
        else
        {
            openGate.SetActive(false);
            closedGate.SetActive(true);
        }
            
    }
}

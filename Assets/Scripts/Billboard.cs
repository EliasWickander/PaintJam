using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform target;

    private void Awake()
    {
        target = FindObjectOfType<PlayerController>().transform;
    }

    private void Update()
    {
        Vector3 targetPos = new Vector3(target.position.x, transform.position.y, target.position.z);
        
        Vector3 toTarget = (targetPos - transform.position).normalized;

        transform.rotation = Quaternion.LookRotation(toTarget);
    }
}

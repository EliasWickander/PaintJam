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
        Vector3 toTarget = (target.position - transform.position).normalized;

        toTarget.y = 0;
        transform.rotation = Quaternion.LookRotation(toTarget);
    }
}

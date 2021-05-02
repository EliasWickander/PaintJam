using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    private GrenadeLauncher launcher;
    [SerializeField] private float explosionRadius;

    private void Awake()
    {
        launcher = FindObjectOfType<GrenadeLauncher>();
    }

    private void Update()
    {

    }

    private void OnCollisionEnter(Collision other)
    {
        Explode();
    }

    private void Explode()
    {
        Collider[] explosionHits = Physics.OverlapSphere(transform.position, explosionRadius, LayerMask.GetMask("Enemy"));
        if (explosionHits.Length > 0)
        {
            for (int i = 0; i < explosionHits.Length; i++)
            {
                explosionHits[i].GetComponentInParent<Enemy>().ModifyHealth(-launcher.Damage);   
            }
        }

        Destroy(gameObject);
    }
}

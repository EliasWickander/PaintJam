using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected float attackRate;
    [SerializeField] protected float damage;
    [SerializeField] protected float range;

    public abstract void Shoot(Transform origin);
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public event Action<Room> OnRoomEnter;

    public int spawnCredits = 10;
    public float spawnDuration = 5;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            OnRoomEnter?.Invoke(this);
        }
    }
}

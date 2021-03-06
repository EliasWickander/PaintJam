using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class Wave
{
    public int spawnCredits = 10;
    public float spawnDuration = 5;
    
    public List<Enemy> enemies = new List<Enemy>();
    public int UnitsAlive { get; set; }
}

[Serializable]
public class PickupSpawnData
{
    public Pickup pickup;
    public int amount;
}

public class Room : MonoBehaviour
{
    public event Action<Room> OnRoomEnter;

    public Gate gate;
    
    public List<Wave> waves = new List<Wave>();
    
    public Queue<Wave> waveQueue = new Queue<Wave>();

    public Transform enemySpawnPointContainer;
    
    [HideInInspector]
    public List<Transform> enemySpawnPoints = new List<Transform>();
    
    public Transform pickupSpawnPointContainer;
    
    [HideInInspector]
    public List<Transform> pickupsSpawnPoints = new List<Transform>();
    
    public List<PickupSpawnData> pickupsToSpawn = new List<PickupSpawnData>();
    
    public List<Pickup> activePickups = new List<Pickup>();

    private void Awake()
    {
        foreach (Wave wave in waves)
        {
            waveQueue.Enqueue(wave);
        }

        for (int i = 0; i < enemySpawnPointContainer.childCount; i++)
            enemySpawnPoints.Add(enemySpawnPointContainer.GetChild(i));
        
        for (int i = 0; i < pickupSpawnPointContainer.childCount; i++)
            pickupsSpawnPoints.Add(pickupSpawnPointContainer.GetChild(i));
    }

    public void SpawnPickups()
    {
        foreach (Pickup pickup in activePickups)
        {
            activePickups.Remove(pickup);
            Destroy(pickup.gameObject);
        }

        List<Transform> availableSpawnPoints = pickupsSpawnPoints.ToList();
        
        foreach (PickupSpawnData pickup in pickupsToSpawn)
        {
            for (int j = 0; j < pickup.amount; j++)
            {
                if (availableSpawnPoints.Count <= 0)
                {
                    Debug.LogError("Not enough spawnpoints for all the selected pickups. Ignoring.");
                    return;
                }
                
                Transform randSpawnPoint = availableSpawnPoints[Random.Range(0, availableSpawnPoints.Count)];
                Instantiate(pickup.pickup, randSpawnPoint.position, quaternion.identity);
                availableSpawnPoints.Remove(randSpawnPoint);
                
            }
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            OnRoomEnter?.Invoke(this);
        }
    }

    public void RoomFinished()
    {
        gate.SetOpened(true);
    }
}

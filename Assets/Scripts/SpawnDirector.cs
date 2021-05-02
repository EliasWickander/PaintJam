using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class EnemyData
{
    public Enemy enemyPrefab;
    public int credits;
}

public class SpawnDirector : MonoBehaviour
{
    public static SpawnDirector Instance { get; set; }
    
    [SerializeField] private bool active;
    
    [SerializeField] private List<Room> rooms = new List<Room>();
    [SerializeField] private List<EnemyData> enemiesToSpawn = new List<EnemyData>();
    
    private Dictionary<Enemy, EnemyData> enemyDataPair = new Dictionary<Enemy, EnemyData>();

    public Wave activeWave;

    private Coroutine roomFinishedCoroutine;
    

    private void Awake()
    {
        Instance = this;
        
        foreach (Room room in rooms)
        {
            room.OnRoomEnter += PrepareRoom;
        }

        foreach (EnemyData enemy in enemiesToSpawn)
        {
            enemyDataPair.Add(enemy.enemyPrefab, enemy);
        }
    }

    private void PrepareRoom(Room room)
    {
        if (!active)
            return;

        if (room.waveQueue.Count <= 0)
            return;
        
        activeWave = room.waveQueue.Dequeue();

        if (activeWave.enemies.Count <= 0)
            throw new Exception("Wave has no enemies. Aborting.");

        int creditsLeft = activeWave.spawnCredits;

        Queue<Enemy> enemyWave = new Queue<Enemy>();
        
        while (creditsLeft > 0)
        {
            Enemy randomEnemy = activeWave.enemies[Random.Range(0, activeWave.enemies.Count)];
            EnemyData data = enemyDataPair[randomEnemy];
            
            enemyWave.Enqueue(data.enemyPrefab);
            creditsLeft -= data.credits;
        }

        StartCoroutine(SpawnEnemies(room, enemyWave));
    }

    private IEnumerator CheckForRoomFinished(Room room)
    {
        while (activeWave.UnitsAlive > 0)
        {
            yield return new WaitForEndOfFrame();
        }
        
        room.RoomFinished();
    }
    
    private IEnumerator SpawnEnemies(Room room, Queue<Enemy> wave)
    {
        float spawnRate = activeWave.spawnDuration / wave.Count;
        
        while (wave.Count > 0)
        {
            yield return new WaitForSeconds(spawnRate);

            Transform randSpawnPoint = room.enemySpawnPoints[Random.Range(0, room.enemySpawnPoints.Count)];
            
            Instantiate(wave.Dequeue(), randSpawnPoint.position, Quaternion.identity);
            activeWave.UnitsAlive++;
        }

        if (room.waveQueue.Count <= 0)
        {
            room.OnRoomEnter -= PrepareRoom;
        }
        else
        {
            yield return new WaitForSeconds(1);
            PrepareRoom(room);   
        }
        
        roomFinishedCoroutine = StartCoroutine(CheckForRoomFinished(room));
    }
}

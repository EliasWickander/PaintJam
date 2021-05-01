using System;
using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private bool active;
    
    [SerializeField] private List<Room> rooms = new List<Room>();
    [SerializeField] private List<EnemyData> enemiesToSpawn = new List<EnemyData>();

    private void Awake()
    {
        foreach (Room room in rooms)
        {
            room.OnRoomEnter += PrepareRoom;
        }
    }

    private void PrepareRoom(Room room)
    {
        if (!active)
            return;
        
        int creditsLeft = room.spawnCredits;

        Queue<Enemy> enemyWave = new Queue<Enemy>();
        
        while (creditsLeft > 0)
        {
            EnemyData randomEnemy = enemiesToSpawn[Random.Range(0, enemiesToSpawn.Count)];
            enemyWave.Enqueue(randomEnemy.enemyPrefab);
            creditsLeft -= randomEnemy.credits;
        }

        StartCoroutine(SpawnEnemies(room, enemyWave));
    }

    private IEnumerator SpawnEnemies(Room room, Queue<Enemy> wave)
    {
        float spawnRate = room.spawnDuration / wave.Count;
        
        while (wave.Count > 0)
        {
            yield return new WaitForSeconds(spawnRate);
            Instantiate(wave.Dequeue());   
        }

        room.OnRoomEnter -= PrepareRoom;
    }
}

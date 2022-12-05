using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject[] enemyPrefab;
    [SerializeField]
    int count = 30;
    public Transform[] spawnPoint;
    public float spawnTimeMin;
    public float spawnTimeMax;

    float spawnTime;
    float timeAfterSpawn;

    // Start is called before the first frame update
    void Start()
    {
        timeAfterSpawn = 0f;
        spawnTime = Random.Range(spawnTimeMin, spawnTimeMax);
    }

    // Update is called once per frame
    void Update()
    {
        if(count != 0) { Spawn(); }
    }

    void Spawn()
    {
        timeAfterSpawn += Time.deltaTime;

        if(timeAfterSpawn >= spawnTime)
        {
            int spawnPos = Random.Range(0, spawnPoint.Length);  
            GameObject enemy = Instantiate(enemyPrefab[Random.Range(0,3)], spawnPoint[spawnPos].position, spawnPoint[spawnPos].rotation);

            timeAfterSpawn = 0f;
            count--;
        }

        spawnTime = Random.Range(spawnTimeMin, spawnTimeMax);
    }
}

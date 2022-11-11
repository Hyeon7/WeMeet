using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject targetPos;
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
        Spawn();
    }

    void Spawn()
    {
        timeAfterSpawn += Time.deltaTime;

        if(timeAfterSpawn >= spawnTime)
        {
            int spawnPos = Random.Range(0, spawnPoint.Length);
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint[spawnPos].position, spawnPoint[spawnPos].rotation);
            enemy.GetComponent<EnemyNav>().target = targetPos.transform;
            timeAfterSpawn = 0f;
        }

        spawnTime = Random.Range(spawnTimeMin, spawnTimeMax);
    }
}

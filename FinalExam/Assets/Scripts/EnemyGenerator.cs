using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{

    public int count = 30; // 생성할 적 몬스터의 수

    public GameObject enemyPrefab; // 출현할 적 프리펩 
    public Transform[] spawnPoint; // 적 출현시킬 위치
    public float spawnTimeMin; //최소 출현시간
    public float spawnTimeMax; //최대 출현시간

    float spawnTime; //출현시간
    float timeAfterSpawn; //쿨타임

    // Start is called before the first frame update
    void Start()
    {
        timeAfterSpawn = 0f; //쿨타임을 0초로
        spawnTime = Random.Range(spawnTimeMin, spawnTimeMax); //출현시간을 최대시간 최소시간 범위 내로 지정
    }

    // Update is called once per frame
    void Update()
    {
        if(count != 0) { Spawn(); } //생성 몬스터의 수가 0이되면 생성을 멈춘다

/*        if(count == 0)
        {
            Debug.Log("Game Clear");
           // GameDirector.instance.NextSTAGE();
        }*/
    }

    void Spawn()
    {
        timeAfterSpawn += Time.deltaTime; //쿨타임에 시간을 계속 더한다.

        if(timeAfterSpawn >= spawnTime) //쿨타임에 더해진 시간이 생성 시간보다 높아질때
        {
            int spawnPos = Random.Range(0, spawnPoint.Length); //적 출현시킬 위치 중 하나의 좌표값에
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint[spawnPos].position, spawnPoint[spawnPos].rotation); //생성 좌표와 방향에 맞춰 랜덤한 적을 생성시킨다.

            timeAfterSpawn = 0f; //생성한 후에는 쿨타임을 0으로 맞춤
            count--; //적 한마리 생성 시 1개 감소
        }

        spawnTime = Random.Range(spawnTimeMin, spawnTimeMax); //스폰 시간은 최소~최대 출현시간 범위 내로 지정한다.
    }
}

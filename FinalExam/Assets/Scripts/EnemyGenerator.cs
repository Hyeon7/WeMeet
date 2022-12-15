using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{

    public int count = 30; // ������ �� ������ ��

    public GameObject enemyPrefab; // ������ �� ������ 
    public Transform[] spawnPoint; // �� ������ų ��ġ
    public float spawnTimeMin; //�ּ� �����ð�
    public float spawnTimeMax; //�ִ� �����ð�

    float spawnTime; //�����ð�
    float timeAfterSpawn; //��Ÿ��

    // Start is called before the first frame update
    void Start()
    {
        timeAfterSpawn = 0f; //��Ÿ���� 0�ʷ�
        spawnTime = Random.Range(spawnTimeMin, spawnTimeMax); //�����ð��� �ִ�ð� �ּҽð� ���� ���� ����
    }

    // Update is called once per frame
    void Update()
    {
        if(count != 0) { Spawn(); } //���� ������ ���� 0�̵Ǹ� ������ �����

/*        if(count == 0)
        {
            Debug.Log("Game Clear");
           // GameDirector.instance.NextSTAGE();
        }*/
    }

    void Spawn()
    {
        timeAfterSpawn += Time.deltaTime; //��Ÿ�ӿ� �ð��� ��� ���Ѵ�.

        if(timeAfterSpawn >= spawnTime) //��Ÿ�ӿ� ������ �ð��� ���� �ð����� ��������
        {
            int spawnPos = Random.Range(0, spawnPoint.Length); //�� ������ų ��ġ �� �ϳ��� ��ǥ����
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint[spawnPos].position, spawnPoint[spawnPos].rotation); //���� ��ǥ�� ���⿡ ���� ������ ���� ������Ų��.

            timeAfterSpawn = 0f; //������ �Ŀ��� ��Ÿ���� 0���� ����
            count--; //�� �Ѹ��� ���� �� 1�� ����
        }

        spawnTime = Random.Range(spawnTimeMin, spawnTimeMax); //���� �ð��� �ּ�~�ִ� �����ð� ���� ���� �����Ѵ�.
    }
}

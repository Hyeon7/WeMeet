using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private int hp = 10; // 적 HP

    //[SerializeField]
    //private int enemyDamage = 1; // 적 데미지

    NavMeshAgent agent; // Nav agent

    public Transform target; // 쫓아갈 대상

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Magic") // 마법에 맞았을 때
        {
            hp -= collision.gameObject.GetComponent<MagicCasting>().magicDamage; // 매직스크립트에 적힌 데미지만큼 감소
        }
/*        if (collision.gameObject.tag == "Player") // 플레이어한테 도착했을 때
        {
            
        }*/
    }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>(); // 네비게이션메쉬
/*        agent.enabled = false;*/
    }
    // Start is called before the first frame update
    void Start()
    {
/*        StartCoroutine(FindPlayer());*/
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);

        if (hp <= 0) // hp가 0 이하가 될 경우
        {
            //agent.enabled = false;
            Destroy(gameObject); // 오브젝트 삭제
        }
    }

/*    IEnumerator FindPlayer()
    {
        agent.enabled = true;
        while (agent.Warp(target.position))
        {
            yield return null;
        }
    }*/
}

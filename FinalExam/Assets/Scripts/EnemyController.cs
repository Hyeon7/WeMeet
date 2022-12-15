using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public int hp; // 적 HP

    NavMeshAgent agent; // Nav agent
    Rigidbody rigid; // 리지드바디
    Animator anim; // 애니메이터
    public float Damage;
    public Transform target; // 쫓아갈 대상  
    public bool isChase; // 뒤쫓는 상태
    GameObject Player;
    GameObject Director;
    bool isDamage = false;
    public GameObject director;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Magic") // 마법에 맞았을 때
        {
            hp -= collision.gameObject.GetComponent<MagicCasting>().magicDamage; // 매직스크립트에 적힌 데미지만큼 감소
        }
        if (collision.gameObject.tag == "Player") // 플레이어를 만났을 때
        {
            anim.SetBool("Run Forward", false); // 달리는 애니메이션 비활성화
            anim.SetTrigger("Punch"); // 펀치 애니메이션 트리거 활성화
            Player.GetComponent<PlayerController>().PlayerHp -= Damage;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Column")
        {
            if (!isDamage)
            {
                hp -= 5;
                StartCoroutine(DamageMagic1(hp));
                isDamage = true;
            }
        }

        if (other.gameObject.tag == "Ball")
        {
            if (!isDamage)
            {
                hp -= 10;
                StartCoroutine(DamageMagic3(hp));
                isDamage = true;
            }
        }
    }

    IEnumerator DamageMagic1(int hp)
    {

        yield return new WaitForSeconds(1f);
        hp -= 5;
        isDamage = false;
    }

    IEnumerator DamageMagic3(int hp)
    {

        yield return new WaitForSeconds(1f);
        hp -= 100;
        isDamage = false;
    }

    void Awake()
    {
        isChase = true;
        agent = GetComponent<NavMeshAgent>(); // 네비게이션메쉬
        anim = GetComponent<Animator>(); // 애니메이터
        rigid = GetComponent<Rigidbody>(); // 리지드바디
        Invoke("ChaseStart", 2); // 2초 뒤에 함수 실행
        Player = GameObject.Find("OVRPlayerController");
        Director = GameObject.Find("GameDirector");
    }

    void ChaseStart()
    {
        isChase = true; // 뒤쫓는 상태 활성화
        anim.SetBool("Run Forward", true); // 뛰는 애니메이션 활성화
    }

    void FixedUpdate()
    {
        FreezeVelocity(); // 물체에 부딪혔을 때 감속 제외
    }

    void FreezeVelocity() // 물체에 부딪혔을 때 감속 제외
    {
        if (isChase)
        {
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.enabled) // agent가 활성화 되어있는 경우
        {
            agent.SetDestination(target.position); // 타겟을 쫓아감
            agent.isStopped = !isChase; // 뒤쫓는 상태가 아닐 때 agent 비활성화
        }
        if (hp <= 0) // hp가 0 이하가 될 경우
        {
            isChase = false; // 뒤쫓는 상태 비활성화
            agent.enabled = false; // agent 비활성화
            anim.SetTrigger("Die"); // 다이 애니메이션 트리거 작동
            Destroy(gameObject, 3); // 오브젝트 삭제
            Director.GetComponent<GameDirector>().count -= 1;
            Debug.Log(Director.GetComponent<GameDirector>().count);
            hp = 50000;
        }
    }
}
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
    Rigidbody rigid;

    public Transform target; // 쫓아갈 대상  

    Animator anim;
    public bool isChase;
    public bool isAttack;
    public BoxCollider meleeArea;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Magic") // 마법에 맞았을 때
        {
            hp -= collision.gameObject.GetComponent<MagicCasting>().magicDamage; // 매직스크립트에 적힌 데미지만큼 감소
        }
        if (collision.gameObject.tag == "Player")
        {
            anim.SetBool("Run Forward", false);
            anim.SetTrigger("Punch");
        }
    }

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>(); // 네비게이션메쉬
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        Invoke("ChaseStart", 2);
    }

    // Start is called before the first frame update
    void Start()
    {
        isChase = true;
    }

    void ChaseStart()
    {
        if (isChase)
        {
            isChase = true;
            anim.SetBool("Run Forward", true);
        }
    }

    //void Targeting()
    //{
    //    float targetRadius = 1.5f;
    //    float targetRange = 3f;

    //    RaycastHit[] rayHits = Physics.SphereCastAll(transform.position, targetRadius, transform.forward, targetRange, LayerMask.GetMask("Player"));

    //    if(rayHits.Length > 0 && !isAttack)
    //    {
    //        StartCoroutine(Attack());
    //    }
    //}

    //IEnumerator Attack()
    //{
    //    isChase = false;
    //    isAttack = true;
    //    anim.SetBool("Strafe Right", true);

    //    yield return new WaitForSeconds(0.2f);
    //    meleeArea.enabled = true;

    //    yield return new WaitForSeconds(1f);
    //    meleeArea.enabled = false;

    //    isChase = true;
    //    isAttack = false;
    //    anim.SetBool("Strafe Right", false);
    //}

    void FixedUpdate()
    {
        FreezeVelocity();
        //Targeting();
    }

    void FreezeVelocity()
    {
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.enabled)
        {
            agent.SetDestination(target.position);
            agent.isStopped = !isChase;
        }
        if (hp <= 0) // hp가 0 이하가 될 경우
        {
            isChase = false;
            agent.enabled = false;
            anim.SetTrigger("Die");
            Destroy(gameObject, 3); // 오브젝트 삭제
        }
    }
}
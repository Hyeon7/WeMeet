using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private int hp = 10; // �� HP

    //[SerializeField]
    //private int enemyDamage = 1; // �� ������

    NavMeshAgent agent; // Nav agent
    Rigidbody rigid;

    public Transform target; // �Ѿư� ���  

    Animator anim;
    public bool isChase;
    public bool isAttack;
    public BoxCollider meleeArea;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Magic") // ������ �¾��� ��
        {
            hp -= collision.gameObject.GetComponent<MagicCasting>().magicDamage; // ������ũ��Ʈ�� ���� ��������ŭ ����
        }
        if (collision.gameObject.tag == "Player")
        {
            anim.SetBool("Run Forward", false);
            anim.SetTrigger("Punch");
        }
    }

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>(); // �׺���̼Ǹ޽�
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
        if (hp <= 0) // hp�� 0 ���ϰ� �� ���
        {
            isChase = false;
            agent.enabled = false;
            anim.SetTrigger("Die");
            Destroy(gameObject, 3); // ������Ʈ ����
        }
    }
}
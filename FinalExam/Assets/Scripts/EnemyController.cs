using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private int hp = 10; // �� HP

    NavMeshAgent agent; // Nav agent
    Rigidbody rigid; // ������ٵ�
    Animator anim; // �ִϸ�����

    public Transform target; // �Ѿư� ���  
    public bool isChase; // ���Ѵ� ����


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Magic") // ������ �¾��� ��
        {
            hp -= collision.gameObject.GetComponent<MagicCasting>().magicDamage; // ������ũ��Ʈ�� ���� ��������ŭ ����
        }
        if (collision.gameObject.tag == "Player") // �÷��̾ ������ ��
        {
            anim.SetBool("Run Forward", false); // �޸��� �ִϸ��̼� ��Ȱ��ȭ
            anim.SetTrigger("Punch"); // ��ġ �ִϸ��̼� Ʈ���� Ȱ��ȭ
        }
    }

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>(); // �׺���̼Ǹ޽�
        anim = GetComponent<Animator>(); // �ִϸ�����
        rigid = GetComponent<Rigidbody>(); // ������ٵ�
        Invoke("ChaseStart", 2); // 2�� �ڿ� �Լ� ����
    }

    void ChaseStart()
    {
        isChase = true; // ���Ѵ� ���� Ȱ��ȭ
        anim.SetBool("Run Forward", true); // �ٴ� �ִϸ��̼� Ȱ��ȭ
    }

    void FixedUpdate()
    {
        FreezeVelocity(); // ��ü�� �ε����� �� ���� ����
    }

    void FreezeVelocity() // ��ü�� �ε����� �� ���� ����
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
        if (agent.enabled) // agent�� Ȱ��ȭ �Ǿ��ִ� ���
        {
            agent.SetDestination(target.position); // Ÿ���� �Ѿư�
            agent.isStopped = !isChase; // ���Ѵ� ���°� �ƴ� �� agent ��Ȱ��ȭ
        }
        if (hp <= 0) // hp�� 0 ���ϰ� �� ���
        {
            isChase = false; // ���Ѵ� ���� ��Ȱ��ȭ
            agent.enabled = false; // agent ��Ȱ��ȭ
            anim.SetTrigger("Die"); // ���� �ִϸ��̼� Ʈ���� �۵�
            Destroy(gameObject, 3); // ������Ʈ ����
        }
    }
}
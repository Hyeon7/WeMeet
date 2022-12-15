using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public int hp; // �� HP

    NavMeshAgent agent; // Nav agent
    Rigidbody rigid; // ������ٵ�
    Animator anim; // �ִϸ�����
    public float Damage;
    public Transform target; // �Ѿư� ���  
    public bool isChase; // ���Ѵ� ����
    GameObject Player;
    GameObject Director;
    bool isDamage = false;
    public GameObject director;

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
        agent = GetComponent<NavMeshAgent>(); // �׺���̼Ǹ޽�
        anim = GetComponent<Animator>(); // �ִϸ�����
        rigid = GetComponent<Rigidbody>(); // ������ٵ�
        Invoke("ChaseStart", 2); // 2�� �ڿ� �Լ� ����
        Player = GameObject.Find("OVRPlayerController");
        Director = GameObject.Find("GameDirector");
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
            Director.GetComponent<GameDirector>().count -= 1;
            Debug.Log(Director.GetComponent<GameDirector>().count);
            hp = 50000;
        }
    }
}
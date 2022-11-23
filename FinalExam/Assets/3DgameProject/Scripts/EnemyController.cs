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

    public Transform target; // �Ѿư� ���

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Magic") // ������ �¾��� ��
        {
            hp -= collision.gameObject.GetComponent<MagicCasting>().magicDamage; // ������ũ��Ʈ�� ���� ��������ŭ ����
        }
/*        if (collision.gameObject.tag == "Player") // �÷��̾����� �������� ��
        {
            
        }*/
    }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>(); // �׺���̼Ǹ޽�
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

        if (hp <= 0) // hp�� 0 ���ϰ� �� ���
        {
            //agent.enabled = false;
            Destroy(gameObject); // ������Ʈ ����
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

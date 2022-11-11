using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private int hp = 10; // �� HP

    [SerializeField]
    private int enemyDamage = 1; // �� ������

    NavMeshAgent agent; // Nav agent

    public GameObject target; // �Ѿư� ���

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Magic") // ������ �¾��� ��
        {
            hp -= collision.gameObject.GetComponent<MagicCasting>().magicDamage; // ������ũ��Ʈ�� ���� ��������ŭ ����
        }
        if (collision.gameObject.tag == "Player") // �÷��̾����� �������� ��
        {
            
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); // �׺���̼Ǹ޽�
        StartCoroutine(FindPlayer());
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0) // hp�� 0 ���ϰ� �� ���
        {
            Destroy(gameObject); // ������Ʈ ����
        }
    }

    IEnumerator FindPlayer()
    {
        agent.enabled = true;
        while (agent.SetDestination(target.transform.position))
        {
            yield return null;
        }
    }
}

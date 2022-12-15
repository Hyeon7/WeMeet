using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCasting : MonoBehaviour
{
    [SerializeField]
    private float magicSpeed = 6f; // ���� �ӵ�
    public int magicDamage; // ���� ������

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy") // ���� �浹
        {
            Destroy(gameObject); // ������Ʈ ����
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * magicSpeed); // ���� �������� �����ӵ��� ���ư���
        Destroy(gameObject, 3.5f); // 6�ʵ� ���� ����
    }
}

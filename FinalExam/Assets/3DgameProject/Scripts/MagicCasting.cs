using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCasting : MonoBehaviour
{
    [SerializeField]
    private float magicSpeed = 0.5f; // ���� �ӵ�

    public int magicDamage = 3; // ���� ������

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy") // ���� �浹
        {
            Destroy(gameObject); // ������Ʈ ����
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * magicSpeed); // ���� �������� �����ӵ��� ���ư���
        Destroy(gameObject, 6); // 3�ʵ� ���� ����
    }
}

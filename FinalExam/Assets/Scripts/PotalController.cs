using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotalController : MonoBehaviour
{
    [SerializeField]
    private Transform ptTr; // ��Ż��ġ

    [SerializeField]
    private GameObject pt; // ��Ż

    [SerializeField]
    private GameObject effect; // ����Ʈ

    private float ptTime; // ���� �ð�

    bool isGen = false; // ���� ����

    // Start is called before the first frame update
    void Start()
    {
        ptEffectGen(); // ����Ʈ ����
    }

    // Update is called once per frame
    void Update()
    {
        ptGen(); // ��Ż ����  
    }

    void ptGen() // ��Ż ����
    {
        ptTime += Time.deltaTime; // ���� �ð�
        if (ptTime > 2 && !isGen) // 2�ʰ� �Ѿ�� �� ���� �ʾ��� ��
        {
            Instantiate(pt, ptTr.transform.position, ptTr.rotation); // ��Ż����
            isGen = true; // ��Ż �ѹ��� ����
        }
    }

    void ptEffectGen() // ����Ʈ ����
    {
        Instantiate(effect, ptTr.transform.position, pt.transform.rotation); // ����Ʈ ����
    }
}
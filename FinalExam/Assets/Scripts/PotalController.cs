using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotalController : MonoBehaviour
{
    [SerializeField]
    private Transform ptTr; // 포탈위치

    [SerializeField]
    private GameObject pt; // 포탈

    [SerializeField]
    private GameObject effect; // 이펙트

    private float ptTime; // 생성 시간

    bool isGen = false; // 생성 여부

    // Start is called before the first frame update
    void Start()
    {
        ptEffectGen(); // 이펙트 생성
    }

    // Update is called once per frame
    void Update()
    {
        ptGen(); // 포탈 생성  
    }

    void ptGen() // 포탈 생성
    {
        ptTime += Time.deltaTime; // 생성 시간
        if (ptTime > 2 && !isGen) // 2초가 넘어가며 젠 되지 않았을 때
        {
            Instantiate(pt, ptTr.transform.position, ptTr.rotation); // 포탈생성
            isGen = true; // 포탈 한번만 생성
        }
    }

    void ptEffectGen() // 이펙트 생성
    {
        Instantiate(effect, ptTr.transform.position, pt.transform.rotation); // 이펙트 생성
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCasting : MonoBehaviour
{
    [SerializeField]
    private float magicSpeed = 6f; // 마법 속도
    public int magicDamage; // 마법 데미지

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy") // 적과 충돌
        {
            Destroy(gameObject); // 오브젝트 삭제
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * magicSpeed); // 앞의 방향으로 마법속도로 날아가기
        Destroy(gameObject, 3.5f); // 6초뒤 마법 삭제
    }
}

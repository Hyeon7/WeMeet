using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private int hp = 10;

    [SerializeField]
    private float speed = 5.0f;

    [SerializeField]
    private int enemyDamage = 1;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Magic")
        {
            hp -= collision.gameObject.GetComponent<MagicCasting>().magicDamage;
        }
        if (collision.gameObject.tag == "Player")
        {
            
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartController : MonoBehaviour
{
    [SerializeField]
    private int hp = 1;

    public bool isStart = false;

    public int num;

    public GameObject director;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Magic")      
        {
           hp -= collision.gameObject.GetComponent<MagicCasting>().magicDamage;
        }
    }

    // Update is called once per frame
    void Update()
    {
       if (hp <= 0)
       {
          switch (num)
          {
               case 1:
                   director.GetComponent<GameDirector>().Stage1();
                   break;
               case 2:
                   director.GetComponent<GameDirector>().Stage2();
                   break;
               case 3:
                   director.GetComponent<GameDirector>().Stage3();
                   break;
               default:
                   Debug.Log("Error"); break;
           }
       }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartController : MonoBehaviour
    {
        [SerializeField]
        private int hp = 1;

        public bool isStart = false;

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
                isStart = true;
            }
        }

    }

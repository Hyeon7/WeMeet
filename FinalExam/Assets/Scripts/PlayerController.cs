using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public float PlayerHp = 30;
    public GameObject director;

    private void Start()
    {
        if (PlayerHp <= 0)
        {
            director.GetComponent<GameDirector>().StartPage();
        }
    }
}

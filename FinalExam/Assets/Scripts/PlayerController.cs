using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float PlayerHp;

    void GameOver()
    {
        if(PlayerHp <= 0)
        {
            Debug.Log("Game Over");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIController : MonoBehaviour
{
    //public Text countText;
    public GameObject GD;

    private void Start()
    {
     
    }
    // Update is called once per frame
    void Update()
    {
       // countText.text = "몬스터 수 : " + GetComponent<GameDirector>().count;
       if (Input.GetKeyDown(KeyCode.R))
        {
            GD.GetComponent<GameDirector>().StartPage();  
        }
    }
}

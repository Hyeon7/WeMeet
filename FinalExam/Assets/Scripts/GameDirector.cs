using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{

    
    public int count = 15;

    private void Start()
    {
        
    }

    private void Update()
    {
        if(count == 0)
        {
            StartPage();
        }
    }

    public void StartPage()
    {
        SceneManager.LoadScene("Start");
    }

    public void Stage1()
    {
        SceneManager.LoadScene("Stage1");
    }

    public void Stage2()
    {
        SceneManager.LoadScene("Stage2");
    }

    public void Stage3()
    {
        SceneManager.LoadScene("Stage3");
    }

}

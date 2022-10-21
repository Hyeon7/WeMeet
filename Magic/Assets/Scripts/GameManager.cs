using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public bool isConnect = false;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // 실행화면을 960 x 640의 창모드로 설정
        Screen.SetResolution(960, 640, FullScreenMode.Windowed);

        // 데이터 송수신 빈도를 매 초당 30회로 설정
        PhotonNetwork.SendRate = 30;
        PhotonNetwork.SerializationRate = 30;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

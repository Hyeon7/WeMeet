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
        // ����ȭ���� 960 x 640�� â���� ����
        Screen.SetResolution(960, 640, FullScreenMode.Windowed);

        // ������ �ۼ��� �󵵸� �� �ʴ� 30ȸ�� ����
        PhotonNetwork.SendRate = 30;
        PhotonNetwork.SerializationRate = 30;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

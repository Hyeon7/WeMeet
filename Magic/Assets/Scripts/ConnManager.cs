using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ConnManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.GameVersion = "0.1";

        int num = Random.Range(0, 1000); // 게임에서 사용할 사용자의 이름을 랜덤으로 정한다.
        PhotonNetwork.NickName = "Player " + num;

        PhotonNetwork.AutomaticallySyncScene = true; // 게임에 참여하면 마스터 클라이언트가 구성한 씬에 자동으로 동기화하도록 한다.

        PhotonNetwork.ConnectUsingSettings(); // 서버 접속
    }

    public override void OnConnectedToMaster() 
    {
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    public override void OnJoinedLobby() 
    {
        Debug.Log("로비 접속 완료!");
        RoomOptions ro = new RoomOptions()
        {
            IsVisible = true,
            IsOpen = true,
            MaxPlayers = 8
        };
        PhotonNetwork.JoinOrCreateRoom("NetTest", ro, TypedLobby.Default);
    }

    public override void OnJoinedRoom() 
    {
        Debug.Log("룸 입장");

        Vector2 originPos = Random.insideUnitCircle * 2.0f;
        PhotonNetwork.Instantiate("Player", new Vector3(originPos.x, 0, originPos.y), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

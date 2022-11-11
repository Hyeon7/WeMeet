using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicController : MonoBehaviour
{
    public Transform curser;
    public GameObject magic;
    public LineRenderer lr;

    Vector3 startPos, endPos;
    public GameObject hitInfo;

    // Start is called before the first frame update
    void Start()
    {
        lr.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawRay(curser.position, curser.rotation.eulerAngles, Color.red, 0.1f);
        if (OVRInput.GetDown(OVRInput.Button.Three)) // 왼손 x버튼 눌렀을 때
        {
            lr.enabled = true; // 라인렌더러 활성화
        }

        if (OVRInput.Get(OVRInput.Button.Three)) // 왼손 x버튼 누르고 있을 때
        {
            lr.SetPosition(0, curser.position); // 시작지점
            RaycastHit hit; // 레이캐스트 선언
            if (Physics.Raycast(curser.position, curser.forward, out hit)) // 레이캐스트를 커서 위치에서 앞방향으로 날린다
            {
                if (hit.collider) // 레이캐스트로 충돌됐을 시
                {
                    lr.SetPosition(1, hit.point); // 몬스터 -> 레이저 안 뚫고 나가게하는거
                    hitInfo.transform.position = hit.point; // 충돌 위치를 구로 표현
                }
            }
        }

        if (OVRInput.GetUp(OVRInput.Button.Three)) // 왼손 x버튼 뗐을 때
        {
            lr.enabled = false; // 라인렌더러 비활성화
        }

        if (OVRInput.GetDown(OVRInput.Button.Four)) // 왼손 y버튼 눌렀을 때
        {
            lr.enabled = true; // 라인렌더러 활성화
        }

        if (OVRInput.Get(OVRInput.Button.Four)) // 왼손 y버튼 누르고 있을 때
        {
            RaycastHit hit; // 레이캐스트 선언
            if (Physics.Raycast(curser.position, curser.forward, out hit)) // 레이캐스트를 커서 위치에서 앞방향으로 날린다
            {
                if (hit.collider) // 레이캐스트로 충돌됐을 시
                {
                    startPos = curser.position; // 라인렌더러 시작지점 -> 커서위치
                    endPos = hit.point; // 라인렌더러 마지막 위치 -> 충돌 지점
                    hitInfo.transform.position = hit.point; // 충돌 위치를 구로 표현
                }
            }

            Vector3 center = (startPos + endPos) * 0.5f; // 포물선의 중간 지점

            center.y -= 3.0f; // 중간지점의 높이를 줄임

            startPos = startPos - center; // 시작지점 높이를 줄임
            endPos = endPos - center; // 끝지점 높이를 줄임

            for (int i = 0; i < lr.positionCount; i++) // 라인렌더러가 갖는 포지션 개수만큼 반복
            {
                Vector3 point = Vector3.Slerp(startPos, endPos, i / (float)(lr.positionCount - 1)); // 시작지점부터 끝지점까지 구형보간을 사용해서 포지션 번호만큼 나눔
                point += center; // 포물선을 위로 그리기 위해 빼준 센터값을 다시 더해줌
                lr.SetPosition(i, point); // 각 위치의 라인렌더러값 설정
            }
        }

        if (OVRInput.GetUp(OVRInput.Button.Four)) // 왼손 Y버튼을 뗐을 때
        {
            lr.enabled = false; // 라인렌더러 비활섷와
        }

        if (OVRInput.GetDown(OVRInput.Button.One)) // 오른손 A버튼
        {
            Instantiate(magic, curser.transform.position, curser.transform.rotation); // 인스턴스생성
        }
    }
}
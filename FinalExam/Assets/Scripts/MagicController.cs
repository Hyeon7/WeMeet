using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicController : MonoBehaviour
{
    public Transform curser; // 커서
    public GameObject magic; // 마법
    public GameObject[] magics = new GameObject[10]; // 마법 종류
    public LineRenderer lr; // 라인렌더러
    public LayerMask magicly; // 마법 레이어마스크
    public GameObject hitInfo; // 충돌된 위치 표현
    public GameObject SelectNow; // 커서로 선택된 스킬 버튼
    bool is_NormalCooltime; // 기본 스킬 쿨타임

    public GameObject Btn; // 버튼
    public GameObject canvas; // 캔버스
    public Sprite[] Skillimgs = new Sprite[10]; // 버튼 스킬 이미지
    GameObject NewBtn; // 재생성 버튼


    // Start is called before the first frame update
    void Start()
    {
        lr.enabled = false; // 라인렌더러 비활성화
    }

    private void Awake()
    {
        SkillUIGeneratior(); // 스킬버튼 생성
    }

    void SkillUIGeneratior() // 스킬버튼 생성
    {
        int SkillNumber; // 스킬 번호
        for (int i = 0; i < 4; i++) 
        {
            SkillNumber = Random.Range(0, 9); // 랜덤 숫자 설정
            GameObject b = Instantiate(Btn); //버튼 생성
            b.name = "BTN" + i; // 버튼 이름 지정
            b.transform.parent = canvas.transform; // 하이어라키 위치 지정
            b.GetComponent<RectTransform>().anchoredPosition = new Vector3(37.5f + 75 * i, 50, 0); // 버튼 위치 지정
            b.GetComponent<BtnController>().SkillType = SkillNumber; // 스킬 종류를 정해진 번호의 스킬로 설정
            b.GetComponent<Image>().sprite = Skillimgs[SkillNumber]; // 스킬 종류를 정해진 번호의 이미지로 설정
        }
    }

    // Update is called once per frame
    void Update()
    {
        lr.SetPosition(0, curser.position); // 시작지점
        RaycastHit hit; // 레이캐스트 선언
        if (Physics.Raycast(curser.position, curser.forward, out hit, Mathf.Infinity, ~magicly) && VRUiKits.Utils.LaserPointer.is_Out && SelectNow != null) // 레이캐스트를 커서 위치에서 앞방향으로 날린다
        {
            if (hit.collider) // 레이캐스트로 충돌됐을 때
            {
                lr.enabled = true; // 라인렌더러 활성화
                lr.SetPosition(1, hit.point); // 몬스터 -> 레이저 안 뚫고 나가게하는거
                hitInfo.transform.position = hit.point; // 충돌 위치를 구로 표현

                if (hit.collider.gameObject.tag == "Sky" && hitInfo.activeInHierarchy) // 레이저가 하늘에 닿고 충돌여부가 표현되고 있으면
                {
                    hitInfo.SetActive(false); // 충돌여부 표현 비활성화
                }
                else if (hit.collider.gameObject.tag != "Sky" && !hitInfo.activeInHierarchy) // 레이저가 하늘에 안닿고 충돌여부가 표현되고 있지 않으면 
                {
                    hitInfo.SetActive(true); // 충돌여부 표현 활성화
                }
            }
        } // 레이캐스트로 충돌하지 않았을 때
        else
        {
            hitInfo.SetActive(false); // 충돌여부 표현 비활성화
            lr.enabled = false; // 라인렌더러 비활성화
        }

        if (OVRInput.Get(OVRInput.Button.One) && !is_NormalCooltime) // 오른손 A버튼을 누르고 있으며 쿨타임이 돌아가고 있지 않을 때
        {
            is_NormalCooltime = true; // 쿨타임을 활성화
            Instantiate(magic, curser.transform.position, curser.transform.rotation); // 인스턴스생성
            Invoke("CoolTimeDone", 0.1f); // 쿨타임함수 0.5초뒤 실행
        }

        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger) && SelectNow != null && VRUiKits.Utils.LaserPointer.is_Out) // 오른손 A버튼을 뗐으며 현재 선택된 버튼이 있고 UI칸을 벗어났을 경우(스킬을 선택하여 발사한 경우) 
        {
            GameObject Skill = Instantiate(magics[SelectNow.GetComponent<BtnController>().SkillType], hit.point, curser.transform.rotation); // 선택된 스킬 생성
            NewBtn = Instantiate(Btn); // 새 버튼 생성
            NewBtn.transform.parent = canvas.transform; // 새 버튼 하이어라키 부모 설정
            NewBtn.transform.position = SelectNow.transform.position; // 새 버튼 위치 초기화
            NewBtn.name = SelectNow.name; // 새 버튼 이름 설정
            int SkillNumber = Random.Range(0, 8); // 숫자 랜덤 배정
            NewBtn.GetComponent<BtnController>().SkillType = SkillNumber; // 스킬 종류를 정해진 번호의 스킬로 설정
            NewBtn.GetComponent<Image>().sprite = Skillimgs[SkillNumber]; // 스킬 종류를 정해진 번호의 이미지로 설정
            NewBtn.SetActive(false); // 새 버튼 비활성화
            StartCoroutine(CreateNewBtn(NewBtn)); // 코루틴 시작
            Destroy(SelectNow);
            Destroy(Skill, 3f);
        }
    }

    IEnumerator CreateNewBtn (GameObject newBtn) // 스킬 쿨타임 (UI스킬 버튼 재생성)
    {
        yield return new WaitForSeconds(20f); // 20초 후에 생성
        newBtn.SetActive(true); // 새 버튼 활성화
        
    }
    
    void CoolTimeDone()  // 쿨타임 함수
    {
        is_NormalCooltime = false; // 쿨타임 초기화
    }
}
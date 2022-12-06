using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class BtnController : MonoBehaviour
{
    RectTransform BtnRT; // 버튼 위치
    public int SkillType; // 스킬 종류
    public Vector2 OriginPos; // 버튼의 고유 위치
    bool Hover; // 커서를 가져다댔을 떼
    GameObject Btn; // 버튼
    public static Vector3 HitPos; // 캔버스의 레이저를 쐈을 때 맞은 위치

    // Start is called before the first frame update
    void Start()
    {
        Btn = gameObject; // 버튼 오브젝트
        BtnRT = GetComponent<RectTransform>(); // 버튼 위치
        BtnRT.localScale = new Vector3(1, 1, 1); // 버튼의 크기 초기화
        BtnRT.localRotation = Quaternion.identity; // 버튼의 회전 초기화
        OriginPos = gameObject.GetComponent<RectTransform>().anchoredPosition; // 버튼의 고유 위치 초기값
    }

    // Update is called once per frame
    void Update()
    {
        if (Hover && VRUiKits.Utils.LaserPointer.On_Clicked) // 커서를 가져다 대면서 클릭 되었을 때
        {
            Btn.transform.position = HitPos; // 버튼의 위치가 커서를 따라갈 수 있도록

            GameObject.Find("MagicManager").GetComponent<MagicController>().SelectNow = gameObject; // 커서를 가져다 댄 오브젝트를 선택된 오브젝트로 저장
        }
        else
        {
            Btn.GetComponent<RectTransform>().anchoredPosition = OriginPos; // 커서를 땠을 때 버튼의 위치를 고유 위치의 초기값으로 복귀
        }
    }

    public void OnCursorEnter() // 커서를 가져다 댔을 때
    {
        Hover = true; 
    }

    public void OnCursorExit() // 커서를 땠을 때
    {
        Hover = false;
    }
}

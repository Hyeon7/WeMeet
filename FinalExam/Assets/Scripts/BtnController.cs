using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class BtnController : MonoBehaviour
{
    RectTransform BtnRT; // ��ư ��ġ
    public int SkillType; // ��ų ����
    public Vector2 OriginPos; // ��ư�� ���� ��ġ
    bool Hover; // Ŀ���� �����ٴ��� ��
    GameObject Btn; // ��ư
    public static Vector3 HitPos; // ĵ������ �������� ���� �� ���� ��ġ

    // Start is called before the first frame update
    void Start()
    {
        Btn = gameObject; // ��ư ������Ʈ
        BtnRT = GetComponent<RectTransform>(); // ��ư ��ġ
        BtnRT.localScale = new Vector3(1, 1, 1); // ��ư�� ũ�� �ʱ�ȭ
        BtnRT.localRotation = Quaternion.identity; // ��ư�� ȸ�� �ʱ�ȭ
        OriginPos = gameObject.GetComponent<RectTransform>().anchoredPosition; // ��ư�� ���� ��ġ �ʱⰪ
    }

    // Update is called once per frame
    void Update()
    {
        if (Hover && VRUiKits.Utils.LaserPointer.On_Clicked) // Ŀ���� ������ ��鼭 Ŭ�� �Ǿ��� ��
        {
            Btn.transform.position = HitPos; // ��ư�� ��ġ�� Ŀ���� ���� �� �ֵ���

            GameObject.Find("MagicManager").GetComponent<MagicController>().SelectNow = gameObject; // Ŀ���� ������ �� ������Ʈ�� ���õ� ������Ʈ�� ����
        }
        else
        {
            Btn.GetComponent<RectTransform>().anchoredPosition = OriginPos; // Ŀ���� ���� �� ��ư�� ��ġ�� ���� ��ġ�� �ʱⰪ���� ����
        }
    }

    public void OnCursorEnter() // Ŀ���� ������ ���� ��
    {
        Hover = true; 
    }

    public void OnCursorExit() // Ŀ���� ���� ��
    {
        Hover = false;
    }
}

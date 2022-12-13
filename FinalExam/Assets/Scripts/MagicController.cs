using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicController : MonoBehaviour
{
    public Transform curser; // Ŀ��
    public GameObject magic; // ����
    public GameObject[] magics = new GameObject[10]; // ���� ����
    public LineRenderer lr; // ���η�����
    public LayerMask magicly; // ���� ���̾��ũ
    public GameObject hitInfo; // �浹�� ��ġ ǥ��
    public GameObject SelectNow; // Ŀ���� ���õ� ��ų ��ư
    bool is_NormalCooltime; // �⺻ ��ų ��Ÿ��

    public GameObject Btn; // ��ư
    public GameObject canvas; // ĵ����
    public Sprite[] Skillimgs = new Sprite[10]; // ��ư ��ų �̹���
    GameObject NewBtn; // ����� ��ư


    // Start is called before the first frame update
    void Start()
    {
        lr.enabled = false; // ���η����� ��Ȱ��ȭ
    }

    private void Awake()
    {
        SkillUIGeneratior(); // ��ų��ư ����
    }

    void SkillUIGeneratior() // ��ų��ư ����
    {
        int SkillNumber; // ��ų ��ȣ
        for (int i = 0; i < 4; i++) 
        {
            SkillNumber = Random.Range(0, 9); // ���� ���� ����
            GameObject b = Instantiate(Btn); //��ư ����
            b.name = "BTN" + i; // ��ư �̸� ����
            b.transform.parent = canvas.transform; // ���̾��Ű ��ġ ����
            b.GetComponent<RectTransform>().anchoredPosition = new Vector3(37.5f + 75 * i, 50, 0); // ��ư ��ġ ����
            b.GetComponent<BtnController>().SkillType = SkillNumber; // ��ų ������ ������ ��ȣ�� ��ų�� ����
            b.GetComponent<Image>().sprite = Skillimgs[SkillNumber]; // ��ų ������ ������ ��ȣ�� �̹����� ����
        }
    }

    // Update is called once per frame
    void Update()
    {
        lr.SetPosition(0, curser.position); // ��������
        RaycastHit hit; // ����ĳ��Ʈ ����
        if (Physics.Raycast(curser.position, curser.forward, out hit, Mathf.Infinity, ~magicly) && VRUiKits.Utils.LaserPointer.is_Out && SelectNow != null) // ����ĳ��Ʈ�� Ŀ�� ��ġ���� �չ������� ������
        {
            if (hit.collider) // ����ĳ��Ʈ�� �浹���� ��
            {
                lr.enabled = true; // ���η����� Ȱ��ȭ
                lr.SetPosition(1, hit.point); // ���� -> ������ �� �հ� �������ϴ°�
                hitInfo.transform.position = hit.point; // �浹 ��ġ�� ���� ǥ��

                if (hit.collider.gameObject.tag == "Sky" && hitInfo.activeInHierarchy) // �������� �ϴÿ� ��� �浹���ΰ� ǥ���ǰ� ������
                {
                    hitInfo.SetActive(false); // �浹���� ǥ�� ��Ȱ��ȭ
                }
                else if (hit.collider.gameObject.tag != "Sky" && !hitInfo.activeInHierarchy) // �������� �ϴÿ� �ȴ�� �浹���ΰ� ǥ���ǰ� ���� ������ 
                {
                    hitInfo.SetActive(true); // �浹���� ǥ�� Ȱ��ȭ
                }
            }
        } // ����ĳ��Ʈ�� �浹���� �ʾ��� ��
        else
        {
            hitInfo.SetActive(false); // �浹���� ǥ�� ��Ȱ��ȭ
            lr.enabled = false; // ���η����� ��Ȱ��ȭ
        }

        if (OVRInput.Get(OVRInput.Button.One) && !is_NormalCooltime) // ������ A��ư�� ������ ������ ��Ÿ���� ���ư��� ���� ���� ��
        {
            is_NormalCooltime = true; // ��Ÿ���� Ȱ��ȭ
            Instantiate(magic, curser.transform.position, curser.transform.rotation); // �ν��Ͻ�����
            Invoke("CoolTimeDone", 0.1f); // ��Ÿ���Լ� 0.5�ʵ� ����
        }

        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger) && SelectNow != null && VRUiKits.Utils.LaserPointer.is_Out) // ������ A��ư�� ������ ���� ���õ� ��ư�� �ְ� UIĭ�� ����� ���(��ų�� �����Ͽ� �߻��� ���) 
        {
            GameObject Skill = Instantiate(magics[SelectNow.GetComponent<BtnController>().SkillType], hit.point, curser.transform.rotation); // ���õ� ��ų ����
            NewBtn = Instantiate(Btn); // �� ��ư ����
            NewBtn.transform.parent = canvas.transform; // �� ��ư ���̾��Ű �θ� ����
            NewBtn.transform.position = SelectNow.transform.position; // �� ��ư ��ġ �ʱ�ȭ
            NewBtn.name = SelectNow.name; // �� ��ư �̸� ����
            int SkillNumber = Random.Range(0, 8); // ���� ���� ����
            NewBtn.GetComponent<BtnController>().SkillType = SkillNumber; // ��ų ������ ������ ��ȣ�� ��ų�� ����
            NewBtn.GetComponent<Image>().sprite = Skillimgs[SkillNumber]; // ��ų ������ ������ ��ȣ�� �̹����� ����
            NewBtn.SetActive(false); // �� ��ư ��Ȱ��ȭ
            StartCoroutine(CreateNewBtn(NewBtn)); // �ڷ�ƾ ����
            Destroy(SelectNow);
            Destroy(Skill, 3f);
        }
    }

    IEnumerator CreateNewBtn (GameObject newBtn) // ��ų ��Ÿ�� (UI��ų ��ư �����)
    {
        yield return new WaitForSeconds(20f); // 20�� �Ŀ� ����
        newBtn.SetActive(true); // �� ��ư Ȱ��ȭ
        
    }
    
    void CoolTimeDone()  // ��Ÿ�� �Լ�
    {
        is_NormalCooltime = false; // ��Ÿ�� �ʱ�ȭ
    }
}
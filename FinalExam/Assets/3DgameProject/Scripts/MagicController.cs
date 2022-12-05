using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicController : MonoBehaviour
{
    public Transform curser;
    public GameObject magic;
    public GameObject[] magics = new GameObject[10];
    public LineRenderer lr;
    public LayerMask magicly;
    bool is_NormalCooltime;
    public GameObject hitInfo;
    public GameObject SelectNow;

    public GameObject Btn;
    public GameObject canvas;
    public Sprite[] Skillimgs = new Sprite[10];
    GameObject NewBtn;


    // Start is called before the first frame update
    void Start()
    {
        lr.enabled = false;
    }

    private void Awake()
    {
        int SkillNumber;
        for (int i = 0; i < 4; i++)
        {
            SkillNumber = Random.Range(0, 9);
            GameObject b = Instantiate(Btn);
            b.name = "BTN" + i;
            b.transform.parent = canvas.transform;
            b.GetComponent<RectTransform>().anchoredPosition = new Vector3(37.5f + 75 * i, 50, 0);
            b.GetComponent<BtnController>().SkillType = SkillNumber;
            b.GetComponent<Image>().sprite = Skillimgs[SkillNumber];
        }
    }

    // Update is called once per frame
    void Update()
    {
        lr.SetPosition(0, curser.position); // ��������
        RaycastHit hit; // ����ĳ��Ʈ ����
        if (Physics.Raycast(curser.position, curser.forward, out hit, Mathf.Infinity, ~magicly) 
            && VRUiKits.Utils.LaserPointer.is_Out && SelectNow != null) // ����ĳ��Ʈ�� Ŀ�� ��ġ���� �չ������� ������
        {
            if (hit.collider) // ����ĳ��Ʈ�� �浹���� ��
            {
                lr.enabled = true;
                lr.SetPosition(1, hit.point); // ���� -> ������ �� �հ� �������ϴ°�
                hitInfo.transform.position = hit.point; // �浹 ��ġ�� ���� ǥ��
                if (hit.collider.gameObject.tag == "Sky" && hitInfo.activeInHierarchy) hitInfo.SetActive(false);        // �������� �ϴÿ� ������ �� ���� �Ⱥ��̰�
                else if (hit.collider.gameObject.tag != "Sky" && !hitInfo.activeInHierarchy) hitInfo.SetActive(true);
            }
        }
        else
        {
            hitInfo.SetActive(false);
            lr.enabled = false;
        }

        if (OVRInput.Get(OVRInput.Button.One) && !is_NormalCooltime) // ������ A��ư
        {
            is_NormalCooltime = true;
            Instantiate(magic, curser.transform.position, curser.transform.rotation); // �ν��Ͻ�����
            Invoke("CoolTimeDone", 0.5f);
        }

        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger) && SelectNow != null && VRUiKits.Utils.LaserPointer.is_Out)
        {
            GameObject p = Instantiate(magics[SelectNow.GetComponent<BtnController>().SkillType], hit.point, curser.transform.rotation);
            NewBtn = Instantiate(Btn);
            NewBtn.transform.parent = canvas.transform;
            NewBtn.transform.position = SelectNow.transform.position;
            NewBtn.name = SelectNow.name;
            int SkillNumber = Random.Range(0, 9);
            NewBtn.GetComponent<BtnController>().SkillType = SkillNumber;
            NewBtn.GetComponent<Image>().sprite = Skillimgs[SkillNumber];
            NewBtn.SetActive(false);
            StartCoroutine(CreateNewBtn(NewBtn));
            Destroy(SelectNow);
            Destroy(p, 3f);
        }
    }

    IEnumerator CreateNewBtn (GameObject newBtn)
    {
        yield return new WaitForSeconds(20f);
        newBtn.SetActive(true);
        
    }
    

    void CoolTimeDone()
    {
        is_NormalCooltime = false;
    }
}
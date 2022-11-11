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
        if (OVRInput.GetDown(OVRInput.Button.Three)) // �޼� x��ư ������ ��
        {
            lr.enabled = true; // ���η����� Ȱ��ȭ
        }

        if (OVRInput.Get(OVRInput.Button.Three)) // �޼� x��ư ������ ���� ��
        {
            lr.SetPosition(0, curser.position); // ��������
            RaycastHit hit; // ����ĳ��Ʈ ����
            if (Physics.Raycast(curser.position, curser.forward, out hit)) // ����ĳ��Ʈ�� Ŀ�� ��ġ���� �չ������� ������
            {
                if (hit.collider) // ����ĳ��Ʈ�� �浹���� ��
                {
                    lr.SetPosition(1, hit.point); // ���� -> ������ �� �հ� �������ϴ°�
                    hitInfo.transform.position = hit.point; // �浹 ��ġ�� ���� ǥ��
                }
            }
        }

        if (OVRInput.GetUp(OVRInput.Button.Three)) // �޼� x��ư ���� ��
        {
            lr.enabled = false; // ���η����� ��Ȱ��ȭ
        }

        if (OVRInput.GetDown(OVRInput.Button.Four)) // �޼� y��ư ������ ��
        {
            lr.enabled = true; // ���η����� Ȱ��ȭ
        }

        if (OVRInput.Get(OVRInput.Button.Four)) // �޼� y��ư ������ ���� ��
        {
            RaycastHit hit; // ����ĳ��Ʈ ����
            if (Physics.Raycast(curser.position, curser.forward, out hit)) // ����ĳ��Ʈ�� Ŀ�� ��ġ���� �չ������� ������
            {
                if (hit.collider) // ����ĳ��Ʈ�� �浹���� ��
                {
                    startPos = curser.position; // ���η����� �������� -> Ŀ����ġ
                    endPos = hit.point; // ���η����� ������ ��ġ -> �浹 ����
                    hitInfo.transform.position = hit.point; // �浹 ��ġ�� ���� ǥ��
                }
            }

            Vector3 center = (startPos + endPos) * 0.5f; // �������� �߰� ����

            center.y -= 3.0f; // �߰������� ���̸� ����

            startPos = startPos - center; // �������� ���̸� ����
            endPos = endPos - center; // ������ ���̸� ����

            for (int i = 0; i < lr.positionCount; i++) // ���η������� ���� ������ ������ŭ �ݺ�
            {
                Vector3 point = Vector3.Slerp(startPos, endPos, i / (float)(lr.positionCount - 1)); // ������������ ���������� ���������� ����ؼ� ������ ��ȣ��ŭ ����
                point += center; // �������� ���� �׸��� ���� ���� ���Ͱ��� �ٽ� ������
                lr.SetPosition(i, point); // �� ��ġ�� ���η������� ����
            }
        }

        if (OVRInput.GetUp(OVRInput.Button.Four)) // �޼� Y��ư�� ���� ��
        {
            lr.enabled = false; // ���η����� ��Ȱ�E��
        }

        if (OVRInput.GetDown(OVRInput.Button.One)) // ������ A��ư
        {
            Instantiate(magic, curser.transform.position, curser.transform.rotation); // �ν��Ͻ�����
        }
    }
}
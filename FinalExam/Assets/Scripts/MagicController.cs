using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (OVRInput.GetDown(OVRInput.Button.Three)) // �޼� x��ư
        {
            lr.enabled = true;
        }

        if (OVRInput.Get(OVRInput.Button.Three))
        {
            lr.SetPosition(0, curser.position); // ��������
            RaycastHit hit;
            if (Physics.Raycast(curser.position, curser.forward, out hit))
            {
                if (hit.collider)
                {
                    lr.SetPosition(1, hit.point); // ���� -> ������ �ȶհ������ϴ°�
                    hitInfo.transform.position = hit.point;
                }
            }
        }

        if (OVRInput.GetUp(OVRInput.Button.Three))
        {
            lr.enabled = false;
        }

        if (OVRInput.GetDown(OVRInput.Button.Four)) // �޼� y��ư
        {
            lr.enabled = true;
            RaycastHit hit;
            if (Physics.Raycast(curser.position, curser.forward, out hit))
            {
                if (hit.collider)
                {
                    lr.SetPosition(1, hit.point);
                    startPos = curser.position;
                    endPos = hit.point;
                    hitInfo.transform.position = hit.point;
                }
            }

            Vector3 center = (startPos + endPos) * 0.5f;

            center.y -= 3.0f;

            startPos = startPos - center;
            endPos = endPos - center;

            for (int i = 0; i < lr.positionCount; i++)
            {
                Vector3 point = Vector3.Slerp(startPos, endPos, i / (float)(lr.positionCount - 1));
                point += center;
                lr.SetPosition(i, point);
            }
        }
        if (OVRInput.GetUp(OVRInput.Button.Four))
        {
            lr.enabled = false;
        }

        if (OVRInput.GetDown(OVRInput.Button.One)) // ������ A��ư
        {
            Instantiate(magic, curser.transform.position, curser.transform.rotation);
        }
    }
}

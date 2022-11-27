using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorDirector : MonoBehaviour
{
    //Cursor ������Ʈ Ǯ�� �ý���
    GameObject[] Cursors = new GameObject[20];
    public GameObject Cursor;
    int no = 0;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 20; i++)     // ���۽� Ŀ�� �ʱ�ȭ
        {
            Cursors[i] = Instantiate(Cursor);
            Cursors[i].name = "Cursor" + i;
        }
    }

    public void DisplayCursor(Vector3 pos)      
    {
        Cursors[no].SetActive(true);    // ���������� ������Ʈ Ȱ��ȭ
        Cursors[no].transform.position = pos;   // ������ ���ÿ� Ŀ���� ��ġ�� ���� ������ ��ġ�� �ű�
        StartCoroutine(InactiveCursor(Cursors[no]));    // 1�ʵڿ� ������ ��
        no++;
        if (no >= 20) no = 0;   // 19��° ���� �ٽ� 0��°�� ��ġ
    }

    IEnumerator InactiveCursor(GameObject go)
    {
        yield return new WaitForSeconds(1.0f);
        go.SetActive(false);
    }
}

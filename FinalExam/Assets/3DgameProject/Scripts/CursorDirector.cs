using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorDirector : MonoBehaviour
{
    //Cursor 오브젝트 풀링 시스템
    GameObject[] Cursors = new GameObject[20];
    public GameObject Cursor;
    int no = 0;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 20; i++)     // 시작시 커서 초기화
        {
            Cursors[i] = Instantiate(Cursor);
            Cursors[i].name = "Cursor" + i;
        }
    }

    public void DisplayCursor(Vector3 pos)      
    {
        Cursors[no].SetActive(true);    // 순차적으로 오브젝트 활성화
        Cursors[no].transform.position = pos;   // 켜짐과 동시에 커서의 위치를 내가 선택한 위치로 옮김
        StartCoroutine(InactiveCursor(Cursors[no]));    // 1초뒤에 꺼지게 함
        no++;
        if (no >= 20) no = 0;   // 19번째 이후 다시 0번째로 위치
    }

    IEnumerator InactiveCursor(GameObject go)
    {
        yield return new WaitForSeconds(1.0f);
        go.SetActive(false);
    }
}

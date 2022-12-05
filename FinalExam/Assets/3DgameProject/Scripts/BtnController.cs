using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class BtnController : MonoBehaviour
{
    RectTransform BtnRT;
    public int SkillType;
    public Vector2 OriginPos;
    bool Hover;
    GameObject Btn;
    public static Vector3 HitPos;
    // Start is called before the first frame update
    void Start()
    {
        Btn = gameObject;
        BtnRT = GetComponent<RectTransform>();
        BtnRT.localScale = new Vector3(1, 1, 1);
        BtnRT.localRotation = Quaternion.identity;
        OriginPos = gameObject.GetComponent<RectTransform>().anchoredPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Hover && VRUiKits.Utils.LaserPointer.On_Clicked)
        {
            Btn.transform.position = HitPos;

            GameObject.Find("MagicManager").GetComponent<MagicController>().SelectNow = gameObject;
        }
        else
        {
            Btn.GetComponent<RectTransform>().anchoredPosition = OriginPos;
        }
    }

    public void OnCursorEnter()
    {
        Hover = true;
    }

    public void OnCursorExit()
    {
        Hover = false;
    }
}

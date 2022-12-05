using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGenerator : MonoBehaviour
{
    public GameObject Btn;
    public GameObject canvas;
    public Sprite[] Skillimgs = new Sprite[10];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}

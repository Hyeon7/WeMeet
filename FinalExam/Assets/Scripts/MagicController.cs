using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicController : MonoBehaviour
{
    public Transform curser;
    public GameObject magic;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            Debug.DrawLine(curser.position, transform.forward * 50f, Color.red, 1f);
        }

        if (OVRInput.GetDown(OVRInput.Button.One)) // ¿ÞÂÊ A¹öÆ°
        {
            Instantiate(magic, curser.transform.position, curser.transform.rotation);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotalController : MonoBehaviour
{
    [SerializeField]
    private Transform ptTr;

    [SerializeField]
    private GameObject pt;

    [SerializeField]
    private GameObject effect;

    private float pttime;

    bool isGen = true;

    // Start is called before the first frame update
    void Start()
    {
        ptGen();
    }

    // Update is called once per frame
    void Update()
    {
        pttime += Time.deltaTime;
        if (pttime > 2 && isGen)
        {
            Instantiate(pt, ptTr.transform.position, ptTr.rotation);
            isGen = false;
        }
    }

    void ptGen()
    {
        Instantiate(effect, ptTr.transform.position, pt.transform.rotation);
    }
}
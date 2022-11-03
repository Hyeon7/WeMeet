using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviourPun
{
    private Rigidbody rigidbody;
    private PhotonView pv;

    private float v;
    private float h;
    private float r;

    public float moveSpeed = 8.0f;
    public float turnSpeed = 0.0f;

    private float turnSpeedValue = 200.0f;

    RaycastHit hit;

    [SerializeField]
    private Transform shotTr;

    [SerializeField]
    private GameObject fireBall;

    [SerializeField]
    private GameObject waterBall;

    [SerializeField]
    private GameObject iceBall;

    [SerializeField]
    private GameObject stoneBall;

    IEnumerator Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        pv = GetComponent<PhotonView>();

        turnSpeed = 0.0f;
        yield return new WaitForSeconds(0.5f);

        if (pv.IsMine)
        {
            Camera.main.GetComponent<CameraController>().target = transform.Find("CamPivot").transform;
        }
        else
        {
            GetComponent<Rigidbody>().isKinematic = true;
        }
        turnSpeed = turnSpeedValue;
    }

    // Update is called once per frame
    void Update()
    {
        v = Input.GetAxis("Vertical");
        h = Input.GetAxis("Horizontal");
        r = Input.GetAxis("Mouse X");

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Instantiate(fireBall, shotTr.transform.position, shotTr.transform.rotation);
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            Instantiate(waterBall, shotTr.transform.position, shotTr.transform.rotation);
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            Instantiate(iceBall, shotTr.transform.position, shotTr.transform.rotation);
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            Instantiate(stoneBall, shotTr.transform.position, shotTr.transform.rotation);
        }
    }

    void FixedUpdate()
    {
        Vector3 dir = (Vector3.forward * v) + (Vector3.right * h);
        transform.Translate(dir.normalized * Time.deltaTime * moveSpeed, Space.Self);
        transform.Rotate(Vector3.up * Time.smoothDeltaTime * turnSpeed * r);
    }

}
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
    }

    void FixedUpdate()
    {
        Vector3 dir = (Vector3.forward * v) + (Vector3.right * h);
        transform.Translate(dir.normalized * Time.deltaTime * moveSpeed, Space.Self);
        transform.Rotate(Vector3.up * Time.smoothDeltaTime * turnSpeed * r);
    }

}
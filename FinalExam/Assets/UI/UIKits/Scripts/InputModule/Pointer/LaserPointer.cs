/***
 * Author: Yunhan Li
 * Any issue please contact yunhn.lee@gmail.com
 ***/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using OVR;

namespace VRUiKits.Utils
{
    public class LaserPointer : MonoBehaviour
    {
        LineRenderer lr;
        public GameObject CursorDirector;
        public static bool On_Clicked = false;
        public LayerMask Hand;
        public GameObject Cursor;
        public static bool is_Out;

        #region MonoBehaviour Callbacks
        void Awake()
        {
            lr = GetComponent<LineRenderer>();
            Cursor = Instantiate(Cursor);
        }

        void Update()
        {
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
            {
                On_Clicked = true;
            }
            if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger))
            {
                On_Clicked = false;
            }
            
            lr.SetPosition(0, transform.position);
            RaycastHit hit;
            Vector3 fwd = transform.TransformDirection(Vector3.forward);

            if (Physics.Raycast(transform.position, fwd, out hit, Mathf.Infinity, ~Hand))
            {
                if (hit.collider)
                {
                    Vector3 hitView = hit.point - fwd * 0.5f;
                    lr.SetPosition(1, hit.point);
                    if (hit.collider.tag == "UI")
                    {
                        Cursor.SetActive(true);
                        BtnController.HitPos = hit.point;
                        Cursor.transform.position = hitView;
                        is_Out = false;
                        
                    }
                    else
                    {
                        Cursor.SetActive(false);
                        is_Out = true;
                    }
                }
            }
            else
            {
                lr.SetPosition(1, transform.forward * 5000 + transform.position);
            }
        }

        void OnDisable()
        {
            if (null != lr)
            {
                // Reset position for smooth transtation when enbale laser pointer
                lr.SetPosition(0, Vector3.zero);
                lr.SetPosition(1, Vector3.zero);
            }
        }
        #endregion
    }
}

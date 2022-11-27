﻿/***
 * Author: Yunhan Li
 * Any issue please contact yunhn.lee@gmail.com
 ***/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace VRUiKits.Utils
{
    public class LaserPointer : MonoBehaviour
    {
        LineRenderer lr;
        public GameObject CursorDirector;

        #region MonoBehaviour Callbacks
        void Awake()
        {
            lr = GetComponent<LineRenderer>();
            
        }

        void LateUpdate()
        {
            lr.SetPosition(0, transform.position);
            RaycastHit hit;
            Vector3 fwd = transform.TransformDirection(Vector3.forward);

            if (Physics.Raycast(transform.position, fwd, out hit))
            {
                if (hit.collider)
                {
                    lr.SetPosition(1, hit.point);
                    if (hit.collider.tag == "UI")
                    {
                        CursorDirector.GetComponent<CursorDirector>().DisplayCursor(hit.point); // 콜라이더 맞은게 UI 캔버스면 DisplayCursor 메서드 실행
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

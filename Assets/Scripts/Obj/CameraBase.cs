using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBase : MonoBehaviour
{
    void LateUpdate()
    {
        if (!GManager.Instance.IsSettingFlag) return;
        transform.position = GManager.Instance.IsUserTrans.position;
    }
}

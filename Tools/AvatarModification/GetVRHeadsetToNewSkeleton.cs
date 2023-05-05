using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetVRHeadsetToNewSkeleton : MonoBehaviour
{
    Transform cameraOffset;

    public Transform targetPos;

    Transform goToAttach;
    float v = 0;

    private void OnEnable()
    {
        goToAttach = GameObject.Find("MainXRAttach").transform.Find("ZEL").transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform;
        /*cameraOffset = this.transform.Find("Camera Offset").gameObject.transform;
        cameraOffset.eulerAngles = new Vector3(90, 0, -180);*/
    }

    private void FixedUpdate()
    {

        targetPos.transform.position = goToAttach.transform.position;
        targetPos.transform.forward = goToAttach.transform.up;

        targetPos.transform.localScale = new Vector3(-1, 1, 1);

         Camera.main.transform.position = targetPos.transform.position;

        //Camera.main.transform.eulerAngles = goToAttach.transform.eulerAngles;
        Camera.main.transform.rotation = targetPos.transform.rotation;

        



    }
}

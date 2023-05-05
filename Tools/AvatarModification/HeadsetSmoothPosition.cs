using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadsetSmoothPosition : MonoBehaviour
{
    [SerializeField]
    Transform target;

    [Range(0.0f, 100.0f)]
    [SerializeField]
    float smoothPosition;

    [Range(0.0f, 100.0f)]
    [SerializeField]
    float smoothRotation;

    Vector3 oldPos;
    Vector3 oldRot;

    Vector3 smoothedPos;
    Vector3 smoothedRot;

    // Update is called once per frame

    private void Start()
    {
        oldPos = target.transform.position;
        oldRot = target.transform.eulerAngles;

    }
    void Update()
    {
        //smoothedPos = (target.transform.position - target.transform.position / smoothPosition) + (oldPos * (smoothPosition / 100));

        //smoothedRot = (target.transform.eulerAngles - target.transform.eulerAngles / smoothRotation) + (oldRot * (smoothRotation / 100));

        smoothedRot = target.transform.eulerAngles;
        smoothedPos = target.transform.eulerAngles;
        this.transform.position = smoothedPos;
        this.transform.eulerAngles = smoothedRot;
    }
}

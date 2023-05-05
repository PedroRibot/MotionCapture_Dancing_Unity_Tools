using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetValuesFromBone : MonoBehaviour
{
    private Vector3 currentPosition = Vector3.zero;
    private Vector3 lastPosition;

    private float velocity;
    private float currVelocity;
    private float lastVelocity;

    private float acceleration;
    private float accelerationCal;
    private float currAcc;
    private float lastAcc;

    private float jerk;
    private float jerkCal;

    private float lastJerk;

    public float smoothPercentage = 10;

    private void OnEnable()
    {
        lastPosition = transform.position;
        lastVelocity = 0;
        lastAcc = 0;
        lastJerk = 0;
    }

    public Vector3 GetValues()
    {
        GetVelocity();
        GetAcceleration();
        GetJerk();
        Calibrations();
        Vector3 v = new Vector3( velocity, accelerationCal, jerkCal);

        return v;
    }


    void GetVelocity()
    {
        
        currentPosition = new Vector3(SmoothValue(transform.position.x, lastPosition.x), SmoothValue(transform.position.y, lastPosition.y), SmoothValue(transform.position.z, lastPosition.z));
        
        Vector3 var = new Vector3((currentPosition.x - lastPosition.x) / Time.deltaTime, (currentPosition.y - lastPosition.y) / Time.deltaTime, (currentPosition.z - lastPosition.z) / Time.deltaTime);

        velocity = SmoothValue(var.magnitude, lastVelocity);
        
        lastPosition = transform.position;
    }

    void GetAcceleration()
    {
        currVelocity = velocity;

        acceleration = SmoothValue((currVelocity - lastVelocity) / Time.deltaTime, lastAcc);

        lastVelocity = velocity;
    }

    void GetJerk()
    {
        currAcc = acceleration;

        jerk = SmoothValue((currAcc - lastAcc) / Time.deltaTime, lastJerk);

        lastAcc = acceleration;
        lastJerk = jerk;
    }

    void Calibrations()
    {
        if (velocity <= 0)
        {
            //velocity = 0;
        }

        jerkCal = jerk / 1000000;
        accelerationCal = acceleration / 1000;
    }

    float SmoothValue(float newVal, float oldVal)
    {
        float valOfSmooth = smoothPercentage;

        float smoothedValue = (newVal - newVal / valOfSmooth) + (oldVal * (valOfSmooth / 100));

        return smoothedValue;
    }
}

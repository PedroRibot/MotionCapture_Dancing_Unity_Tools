using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetVelocity : MonoBehaviour
{
    private Vector3 currentPosition = Vector3.zero;
    private Vector3 lastPosition;

    public bool stopGettingVelocity;

    private float velocity;

    public float smoothPercentage = 10;

    List<float> magnitudes = new List<float>();

    float v;
    float sum;
    float avg;

    private void OnEnable()
    {
        lastPosition = transform.position;
    }

    public float GetSpeed()
    {

        currentPosition = new Vector3(transform.position.x , transform.position.y, transform.position.z);

        Vector3 var = new Vector3((currentPosition.x - lastPosition.x) / Time.deltaTime, (currentPosition.y - lastPosition.y) / Time.deltaTime, (currentPosition.z - lastPosition.z) / Time.deltaTime);

        velocity = var.magnitude;


        lastPosition = transform.position;

        return velocity;
    }

    private void Update()
    {
        if (!stopGettingVelocity)
        {
            v = GetSpeed();

            magnitudes.Add(v);
            sum += v;
        }
    }

    public float GetAverage()
    {

        avg = sum / magnitudes.Count;

        Clear();
        return avg;
    }

    public void Clear()
    {
        magnitudes.Clear();
        sum = 0;
    }
    
}

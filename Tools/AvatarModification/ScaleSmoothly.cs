using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleSmoothly : MonoBehaviour
{

    public Vector3 endScale;
    public Vector3 startScale;
    public float duration;
    public float elapsedTime;




    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (startScale != endScale)
        {
            float percentageComplete = elapsedTime / duration;

            transform.localScale = Vector3.Lerp(startScale, endScale, percentageComplete);
        }


    }
}
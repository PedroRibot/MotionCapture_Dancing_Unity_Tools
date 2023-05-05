using System.Collections;
using System.Collections.Generic;
using Unity;
using UnityEngine;

public class FloorMoving : MonoBehaviour
{
    public Transform leftFoot;
    public Transform RightFoot;

    Vector3 transformToTrack;
    public float offset = 0.2f;


    private void FixedUpdate()
    {
        if (leftFoot.position.y < RightFoot.position.y)
        {
            transformToTrack.y = leftFoot.position.y;
        }
        else
        {
            transformToTrack.y = RightFoot.position.y;
        }

        this.transform.position = new Vector3(this.transform.position.x, transformToTrack.y - offset, this.transform.position.z);
    }

  

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixPositionTo : MonoBehaviour
{
    public Transform boneToroot;
    public bool getRotation;

    

    // Update is called once per frame
    void LateUpdate()
    {

        this.transform.position = boneToroot.position;
        if (getRotation){
            this.transform.rotation = boneToroot.rotation;
        }

        
    }
}

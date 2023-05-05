using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow : MonoBehaviour
{
    public GameObject gOFollow;

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(gOFollow.transform.position.x, this.transform.position.y, gOFollow.transform.position.z);
    }
}

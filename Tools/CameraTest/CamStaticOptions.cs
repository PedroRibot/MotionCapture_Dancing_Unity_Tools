using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamStaticOptions : MonoBehaviour
{
    [SerializeField] public int lookAtValue;

    public int LookAtValue { get => lookAtValue; set => lookAtValue = value; }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleSourceCharacter : MonoBehaviour
{
    
    

    [Header("Root bone onto other")]
    [SerializeField] private toRoot[] bonesToFix;

    [System.Serializable]
    public struct toRoot
    {
        public Transform originBone;
        public Transform boneToRoot;
        public bool getRotation;

    }


    

    // Update is called once per frame
    void LateUpdate()
    {

        foreach (toRoot item in bonesToFix)
        {
            item.originBone.position = item.boneToRoot.position;

            if (item.getRotation)
            {
                item.originBone.rotation = item.boneToRoot.rotation;
            }
        }

       


    }
}

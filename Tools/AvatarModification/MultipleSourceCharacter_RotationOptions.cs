using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleSourceCharacter_RotationOptions : MonoBehaviour
{
    
    

    [Header("Root bone onto other")]
    [SerializeField] private toRoot[] bonesToFix;

    [System.Serializable]
    public struct toRoot
    {
        public Transform originBone;
        public Transform boneToRoot;
        public bool getRotationy;
        public bool getRotationx;
        public bool getRotationz;
    }

    Vector3 anglesToRoot;

    private void Awake()
    {
        anglesToRoot = new Vector3();
    }
    // Update is called once per frame
    void LateUpdate()
    {

        foreach (toRoot item in bonesToFix)
        {
            item.originBone.position = item.boneToRoot.position;

            if (item.getRotationx)
            {
                anglesToRoot.x = item.boneToRoot.eulerAngles.x;
            }
            else
            {
                anglesToRoot.x = item.originBone.eulerAngles.x;
            }

            if (item.getRotationy)
            {
                anglesToRoot.y = item.boneToRoot.eulerAngles.y;
            }
            else
            {
                anglesToRoot.y = item.originBone.eulerAngles.y;
            }

            if (item.getRotationz)
            {
                anglesToRoot.z = item.boneToRoot.eulerAngles.z;
            }
            else
            {
                anglesToRoot.z = item.originBone.eulerAngles.z;
            }


            item.originBone.eulerAngles = anglesToRoot;
        }

       


    }
}

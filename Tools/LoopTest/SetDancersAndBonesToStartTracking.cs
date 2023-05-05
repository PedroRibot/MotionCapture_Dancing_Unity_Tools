using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SetDancersAndBonesToStartTracking : MonoBehaviour {

    bool awaken;

    [SerializeField] public Dancers[] DancersToTrack;
    [System.Serializable]
    public struct Dancers
    {
        public Transform[] T_bonesToTrack;
    }

    private void Awake()
    {
        foreach (Dancers item in DancersToTrack)
        {
            foreach (Transform subitem in item.T_bonesToTrack)
            {
                subitem.gameObject.AddComponent<GetVelocity>();
            }
        }

        awaken = true;
    }

    /*private void OnDisable()
    {
    if (DancersToTrack != null){

    foreach (Dancers item in DancersToTrack)
        {
            foreach (Transform subitem in item.T_bonesToTrack)
            {
                subitem.gameObject.GetComponent<GetVelocity>().stopGettingVelocity = true;
            }
        }
    }

        
    }

    private void OnEnable()
    {
        if (awaken)
        {
            foreach (Dancers item in DancersToTrack)
            {
                foreach (Transform subitem in item.T_bonesToTrack)
                {
                    subitem.gameObject.GetComponent<GetVelocity>().stopGettingVelocity = false;
                }
            }
        }
    }*/
}

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using TMPro;

public class DropDownHandler : MonoBehaviour
{
    private TMP_Dropdown dropdown;
    CameraControls cmpController;
    CinemachineTargetGroup cmpTargetGroup;

    private void Awake()
    {
       
        cmpController = FindObjectOfType<CameraControls>();
        cmpTargetGroup = FindObjectOfType<CinemachineTargetGroup>();
    }

    // Start is called before the first frame update
    void Start()
    {
        dropdown = transform.GetComponent<TMP_Dropdown>();
        dropdown.options.Clear();

        fillTargetGroup();
        fillDropDown();
    }

    void fillTargetGroup()
    {
        foreach (GameObject hip in cmpController.dancerHips)
        {
            cmpTargetGroup.AddMember(hip.transform, 1, 2);
        }
    }

    public void fillDropDown()
    {
        int j = 0;

        dropdown.options.Add(new TMP_Dropdown.OptionData { text = "All" });

        dropdown.value = 0;

        foreach (GameObject item in cmpController.dancerHips)
        {
            dropdown.options.Add(new TMP_Dropdown.OptionData { text = j.ToString() });
            j++;
        }

        

        dropdown.onValueChanged.AddListener(delegate { ChangeDropdown(); });

        
    }

    void ChangeDropdown()
    {
        if (dropdown.value == 0)
        {
            foreach (CinemachineTargetGroup.Target item in cmpTargetGroup.m_Targets)
            {
                cmpTargetGroup.RemoveMember(item.target);
            }

            foreach (GameObject hip in cmpController.dancerHips)
            {
                cmpTargetGroup.AddMember(hip.transform, 1, 2);
            }
        }
        else
        {
            foreach (CinemachineTargetGroup.Target item in cmpTargetGroup.m_Targets)
            {
                cmpTargetGroup.RemoveMember(item.target);
            }
            int h = 0;
            foreach (GameObject hip in cmpController.dancerHips)
            {
                //Accounting that dropdown value for all is 0
                if (h == (dropdown.value - 1))
                {
                    cmpTargetGroup.AddMember(hip.transform, 1, 2);
                }
                h++;
            }
        }

        if (cmpController.activeCam.tag == "Static")
        {
                      cmpController.activeCam.GetComponent<CamStaticOptions>().LookAtValue = dropdown.value;

        }

    }


    public void ValueStatic(int val)
    {

        dropdown.value = val;
    }


}

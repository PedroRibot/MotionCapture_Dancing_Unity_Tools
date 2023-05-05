using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSmoothly_Skybox : MonoBehaviour
{

    public Color endColor;
    public Color startColor;
    public float duration;
    public float elapsedTime;

    public Material mymat;

    private void Awake()
    {


    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (startColor != endColor)
        {
            float percentageComplete = elapsedTime / duration;

          

           
            mymat.SetColor("_Tint", Color.Lerp(startColor, endColor, percentageComplete));
        }
       
        
    }


    public Color GetCurrentColor()
    {
        return mymat.GetColor("_Tint");
    }

}

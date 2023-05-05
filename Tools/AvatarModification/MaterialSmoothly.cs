using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSmoothly : MonoBehaviour
{

    public Color endColor;
    public Color startColor;
    public float duration;
    public float elapsedTime;

    Material mymat;

    private void Awake()
    {
        mymat = GetComponent<Renderer>().material;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (startColor != endColor)
        {
            float percentageComplete = elapsedTime / duration;



            
            mymat.SetColor("_EmissiveColor", Color.Lerp(startColor, endColor, percentageComplete) * 100f );
        }
       
        
    }

    public Color GetCurrentColor()
    {
        return mymat.GetColor("_EmissiveColor");
    }
}

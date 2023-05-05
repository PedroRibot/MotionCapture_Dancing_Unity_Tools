using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Expansion : MonoBehaviour
{
    // WITH OBJECTS CANGING THEIR COLORS


    /*[SerializeField] Vector3 veloAccJerk;
    [SerializeField] float divided = 100;S
    [SerializeField] float smoothPerc;*/

    [SerializeField] float timeBetweenCalls = 1;
    float t;

    [Header("Limbs that get Expanded")]
    [SerializeField] private Limbs[] LimbsToTrack;

    [SerializeField] private GameObject objectsParent;

    private GameObject[] interactebleObjects;

    private float divisionObjectsPerBone;

    public Gradient colorThroughSpeed;
    public Gradient colorThroughSpeed2;

    public float valueOfScaleOfBody  = 1;

    [SerializeField] float minScale = 1;
    [SerializeField] float maxScale = 2;


    //Debug
    float sum;
    float avg;
    float v;
    float scale;
    int valueofInteractable;
    Vector3 currentLS;

    public int value = 4;
    public bool all;

   [System.Serializable]
    public struct Limbs
    {
        public GameObject GOToExtend;

        public Transform T_thatFixesIn;
        public Transform T_toFix;
        public Transform T_positionToTrackValues;

        public float sensitivity;

        
    }


    private void Awake()
    {
        t = timeBetweenCalls;

        // ASSIGN OBJECTS

        if (objectsParent.transform.childCount != 0)
        {
            interactebleObjects = new GameObject[objectsParent.transform.childCount];

            for (int i = 0; i < objectsParent.transform.childCount; i++)
            {
                interactebleObjects[i] = objectsParent.transform.GetChild(i).gameObject;

                interactebleObjects[i].AddComponent<ScaleSmoothly>();

                /// MATERIAL! ///////////////////////////////////////////////////////
                interactebleObjects[i].AddComponent<MaterialSmoothly>();
            }

            divisionObjectsPerBone = interactebleObjects.Length / LimbsToTrack.Length;

            print(interactebleObjects.Length + " " + LimbsToTrack.Length + " Division = " + divisionObjectsPerBone);


        }

       
        // ASSIGN BONES OR JOINTS TO TRACK EXTEND
        int z = 0;
        foreach (Limbs item in LimbsToTrack)
        {

            if (z == value || all)
            {

                if (item.GOToExtend)
                {
                    if (item.T_thatFixesIn != null && item.T_toFix != null)
                    {
                        FixPositionTo x = item.T_thatFixesIn.gameObject.AddComponent<FixPositionTo>();
                        x.boneToroot = item.T_toFix;
                    }

                    GetVelocity cmp_GVFB = item.T_positionToTrackValues.gameObject.AddComponent<GetVelocity>();
                    ScaleSmoothly cmp_scaleSmoothly = item.GOToExtend.gameObject.AddComponent<ScaleSmoothly>();

                    

                }
            }

            z++;
        }

       

       
    }

    private void Update()
    {
        t -= Time.deltaTime;

        if (t <= 0)
        {

            ScaleWithVelocity();

            t = timeBetweenCalls;
        }
    }




    void ScaleWithVelocity()
    {
        int n = 0;
        valueofInteractable = 0;
        foreach (Limbs item2 in LimbsToTrack)
        {
            if (n == value || all)
            {
                GetVelocity cmpV = item2.T_positionToTrackValues.GetComponent<GetVelocity>();

                v = cmpV.GetAverage();
                cmpV.Clear();

                scale = (Mathf.Clamp( (v * item2.sensitivity) / valueOfScaleOfBody, minScale, maxScale));


                Vector3 newScale = new Vector3(scale, scale, scale);

                currentLS = item2.GOToExtend.transform.localScale;

                ScaleSmoothly cmp_scaleSmoothly = item2.GOToExtend.GetComponent<ScaleSmoothly>();

                cmp_scaleSmoothly.startScale = currentLS;
                cmp_scaleSmoothly.endScale = newScale;
                cmp_scaleSmoothly.elapsedTime = 0;
                cmp_scaleSmoothly.duration = timeBetweenCalls;


                //OBJECTS (twice because is two per limb)
                ScaleSmoothly cmp_ObjectSmoothly = interactebleObjects[valueofInteractable].GetComponent<ScaleSmoothly>();

                float sc = (Mathf.Clamp(((v * item2.sensitivity) / valueOfScaleOfBody) * 2 + 1, 1, 5));

                Vector3 objScale = new Vector3(sc * 1.8f, sc * 7.8f, sc * 3.2f);

                cmp_ObjectSmoothly.startScale = interactebleObjects[valueofInteractable].transform.localScale;
                cmp_ObjectSmoothly.endScale = objScale;
                cmp_ObjectSmoothly.elapsedTime = 0;
                cmp_ObjectSmoothly.duration = timeBetweenCalls;

                ///////////////////////////////////////////////////////////////// MATERIAL ///////////////////////////////////////////////////////////////////////////////////////////
                ///
                MaterialSmoothly cmp_ObjectMatSmoothly = interactebleObjects[valueofInteractable].GetComponent<MaterialSmoothly>();

                float timeKey = (Mathf.Clamp((v * item2.sensitivity) / valueOfScaleOfBody, 0, 2) / 2);

                Color currentColor = cmp_ObjectMatSmoothly.GetCurrentColor();
                Color finalColor = colorThroughSpeed.Evaluate(timeKey);


                cmp_ObjectMatSmoothly.endColor = finalColor;
                cmp_ObjectMatSmoothly.startColor = currentColor;
                cmp_ObjectMatSmoothly.elapsedTime = 0;
                cmp_ObjectMatSmoothly.duration = timeBetweenCalls;

                valueofInteractable++;

                    ///////// 4 TIMES TIME (because I could not manage fitting it in a FOR loop :/)
                ScaleSmoothly cmp_ObjectSmoothly2 = interactebleObjects[valueofInteractable].GetComponent<ScaleSmoothly>();
                Vector3 objScale2 = new Vector3(sc * 4.3f, sc *3.2f, sc * 3.5f);

                cmp_ObjectSmoothly2.startScale = interactebleObjects[valueofInteractable].transform.localScale;
                cmp_ObjectSmoothly2.endScale = objScale2;
                cmp_ObjectSmoothly2.elapsedTime = 0;
                cmp_ObjectSmoothly2.duration = timeBetweenCalls;

                ///////////////////////////////////////////////////////////////// MATERIAL ////////////////////////////////////////////////////////////////////////////////////////////

                MaterialSmoothly cmp_ObjectMatSmoothly2 = interactebleObjects[valueofInteractable].GetComponent<MaterialSmoothly>();

                Color currentColor2 = cmp_ObjectMatSmoothly2.GetCurrentColor();
                Color finalColor2 = colorThroughSpeed2.Evaluate(timeKey);


                cmp_ObjectMatSmoothly2.endColor = finalColor2;
                cmp_ObjectMatSmoothly2.startColor = currentColor2;
                cmp_ObjectMatSmoothly2.elapsedTime = 0;
                cmp_ObjectMatSmoothly2.duration = timeBetweenCalls;

                valueofInteractable++;

                ScaleSmoothly cmp_ObjectSmoothly3 = interactebleObjects[valueofInteractable].GetComponent<ScaleSmoothly>();
                Vector3 objScale3 = new Vector3(sc * 1.2f, sc * 9.2f, sc * 2.2f);

                cmp_ObjectSmoothly3.startScale = interactebleObjects[valueofInteractable].transform.localScale;
                cmp_ObjectSmoothly3.endScale = objScale2;
                cmp_ObjectSmoothly3.elapsedTime = 0;
                cmp_ObjectSmoothly3.duration = timeBetweenCalls;

                ///////////////////////////////////////////////////////////////// MATERIAL ////////////////////////////////////////////////////////////////////////////////////////////

                MaterialSmoothly cmp_ObjectMatSmoothly3 = interactebleObjects[valueofInteractable].GetComponent<MaterialSmoothly>();

                Color currentColor3 = cmp_ObjectMatSmoothly3.GetCurrentColor();
                Color finalColor3 = colorThroughSpeed2.Evaluate(timeKey);


                cmp_ObjectMatSmoothly3.endColor = finalColor3;
                cmp_ObjectMatSmoothly3.startColor = currentColor3;
                cmp_ObjectMatSmoothly3.elapsedTime = 0;
                cmp_ObjectMatSmoothly3.duration = timeBetweenCalls;

                valueofInteractable++;

                ScaleSmoothly cmp_ObjectSmoothly4 = interactebleObjects[valueofInteractable].GetComponent<ScaleSmoothly>();
                Vector3 objScale4 = new Vector3(sc * 5.3f, sc * 5.6f, sc * 4.2f);

                cmp_ObjectSmoothly4.startScale = interactebleObjects[valueofInteractable].transform.localScale;
                cmp_ObjectSmoothly4.endScale = objScale4;
                cmp_ObjectSmoothly4.elapsedTime = 0;
                cmp_ObjectSmoothly4.duration = timeBetweenCalls;

                ///////////////////////////////////////////////////////////////// MATERIAL ////////////////////////////////////////////////////////////////////////////////////////////

                MaterialSmoothly cmp_ObjectMatSmoothly4 = interactebleObjects[valueofInteractable].GetComponent<MaterialSmoothly>();

                Color currentColor4 = cmp_ObjectMatSmoothly4.GetCurrentColor();
                Color finalColor4 = colorThroughSpeed.Evaluate(timeKey);


                cmp_ObjectMatSmoothly4.endColor = finalColor4;
                cmp_ObjectMatSmoothly4.startColor = currentColor4;
                cmp_ObjectMatSmoothly4.elapsedTime = 0;
                cmp_ObjectMatSmoothly4.duration = timeBetweenCalls;

                valueofInteractable++;

                ScaleSmoothly cmp_ObjectSmoothly5 = interactebleObjects[valueofInteractable].GetComponent<ScaleSmoothly>();
                Vector3 objScale5 = new Vector3(sc * 0.5f, sc * 11.7f, sc * 1.5f);

                cmp_ObjectSmoothly5.startScale = interactebleObjects[valueofInteractable].transform.localScale;
                cmp_ObjectSmoothly5.endScale = objScale5;
                cmp_ObjectSmoothly5.elapsedTime = 0;
                cmp_ObjectSmoothly5.duration = timeBetweenCalls;

                ///////////////////////////////////////////////////////////////// MATERIAL ////////////////////////////////////////////////////////////////////////////////////////////

                MaterialSmoothly cmp_ObjectMatSmoothly5 = interactebleObjects[valueofInteractable].GetComponent<MaterialSmoothly>();

                Color currentColor5 = cmp_ObjectMatSmoothly5.GetCurrentColor();
                Color finalColor5 = colorThroughSpeed2.Evaluate(timeKey);


                cmp_ObjectMatSmoothly5.endColor = finalColor5;
                cmp_ObjectMatSmoothly5.startColor = currentColor5;
                cmp_ObjectMatSmoothly5.elapsedTime = 0;
                cmp_ObjectMatSmoothly5.duration = timeBetweenCalls;

                valueofInteractable++;

                ScaleSmoothly cmp_ObjectSmoothly6 = interactebleObjects[valueofInteractable].GetComponent<ScaleSmoothly>();
                Vector3 objScale6 = new Vector3(sc * 6.2f, sc * 5.4f, sc * 3.5f);

                cmp_ObjectSmoothly6.startScale = interactebleObjects[valueofInteractable].transform.localScale;
                cmp_ObjectSmoothly6.endScale = objScale6;
                cmp_ObjectSmoothly6.elapsedTime = 0;
                cmp_ObjectSmoothly6.duration = timeBetweenCalls;

                ///////////////////////////////////////////////////////////////// MATERIAL ////////////////////////////////////////////////////////////////////////////////////////////

                MaterialSmoothly cmp_ObjectMatSmoothly6 = interactebleObjects[valueofInteractable].GetComponent<MaterialSmoothly>();

                Color currentColor6 = cmp_ObjectMatSmoothly6.GetCurrentColor();
                Color finalColor6 = colorThroughSpeed.Evaluate(timeKey);


                cmp_ObjectMatSmoothly6.endColor = finalColor6;
                cmp_ObjectMatSmoothly6.startColor = currentColor6;
                cmp_ObjectMatSmoothly6.elapsedTime = 0;
                cmp_ObjectMatSmoothly6.duration = timeBetweenCalls;

                valueofInteractable++;

                ScaleSmoothly cmp_ObjectSmoothly7 = interactebleObjects[valueofInteractable].GetComponent<ScaleSmoothly>();
                Vector3 objScale7 = new Vector3(sc * 2f, sc * 13f, sc * 4.6f);

                cmp_ObjectSmoothly7.startScale = interactebleObjects[valueofInteractable].transform.localScale;
                cmp_ObjectSmoothly7.endScale = objScale7;
                cmp_ObjectSmoothly7.elapsedTime = 0;
                cmp_ObjectSmoothly7.duration = timeBetweenCalls;

                ///////////////////////////////////////////////////////////////// MATERIAL ////////////////////////////////////////////////////////////////////////////////////////////

                MaterialSmoothly cmp_ObjectMatSmoothly7 = interactebleObjects[valueofInteractable].GetComponent<MaterialSmoothly>();

                Color currentColor7 = cmp_ObjectMatSmoothly7.GetCurrentColor();
                Color finalColor7 = colorThroughSpeed2.Evaluate(timeKey);


                cmp_ObjectMatSmoothly7.endColor = finalColor7;
                cmp_ObjectMatSmoothly7.startColor = currentColor7;
                cmp_ObjectMatSmoothly7.elapsedTime = 0;
                cmp_ObjectMatSmoothly7.duration = timeBetweenCalls;

                valueofInteractable++;

                ScaleSmoothly cmp_ObjectSmoothly8 = interactebleObjects[valueofInteractable].GetComponent<ScaleSmoothly>();
                Vector3 objScale8 = new Vector3(sc * 8f, sc * 7.4f, sc * 4f);

                cmp_ObjectSmoothly8.startScale = interactebleObjects[valueofInteractable].transform.localScale;
                cmp_ObjectSmoothly8.endScale = objScale4;
                cmp_ObjectSmoothly8.elapsedTime = 0;
                cmp_ObjectSmoothly8.duration = timeBetweenCalls;

                ///////////////////////////////////////////////////////////////// MATERIAL ////////////////////////////////////////////////////////////////////////////////////////////

                MaterialSmoothly cmp_ObjectMatSmoothly8 = interactebleObjects[valueofInteractable].GetComponent<MaterialSmoothly>();

                Color currentColor8 = cmp_ObjectMatSmoothly8.GetCurrentColor();
                Color finalColor8 = colorThroughSpeed.Evaluate(timeKey);


                cmp_ObjectMatSmoothly8.endColor = finalColor8;
                cmp_ObjectMatSmoothly8.startColor = currentColor8;
                cmp_ObjectMatSmoothly8.elapsedTime = 0;
                cmp_ObjectMatSmoothly8.duration = timeBetweenCalls;

                valueofInteractable++;


                //print(v);
            }

            n++;
        }
    }


}

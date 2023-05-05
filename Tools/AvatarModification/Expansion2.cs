using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Expansion2 : MonoBehaviour
{

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

    [SerializeField] private GameObject skyboxController;

    //Debug
    float sum;
    float avg;
    float v;
    float scale;
    int valueofInteractable;
    Vector3 currentLS;
    float velocitiesAll;

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
        velocitiesAll = 0;
        foreach (Limbs item2 in LimbsToTrack)
        {
            if (n == value || all)
            {
                GetVelocity cmpV = item2.T_positionToTrackValues.GetComponent<GetVelocity>();

                v = cmpV.GetAverage();
                cmpV.Clear();

                scale = (Mathf.Clamp( (v * item2.sensitivity) / valueOfScaleOfBody, 0.2f, 2));


                Vector3 newScale = new Vector3(scale, scale, scale);

                currentLS = item2.GOToExtend.transform.localScale;

                ScaleSmoothly cmp_scaleSmoothly = item2.GOToExtend.GetComponent<ScaleSmoothly>();

                cmp_scaleSmoothly.startScale = currentLS;
                cmp_scaleSmoothly.endScale = newScale;
                cmp_scaleSmoothly.elapsedTime = 0;
                cmp_scaleSmoothly.duration = timeBetweenCalls;


                //OBJECTS (twice because is two per limb)
                ScaleSmoothly cmp_ObjectSmoothly = interactebleObjects[valueofInteractable].GetComponent<ScaleSmoothly>();

                float sc = (Mathf.Clamp(((v * item2.sensitivity) / valueOfScaleOfBody) * 4 + 2, 3, 8));

                Vector3 objScale = new Vector3(sc * 1.8f , sc * 4.8f , sc * 3.2f);

                cmp_ObjectSmoothly.startScale = interactebleObjects[valueofInteractable].transform.localScale;
                cmp_ObjectSmoothly.endScale = objScale;
                cmp_ObjectSmoothly.elapsedTime = 0;
                cmp_ObjectSmoothly.duration = timeBetweenCalls;

                valueofInteractable++;

                    ///////// ANOTHER TIME (because I could not manage fitting it in a FOR loop :/)
                ScaleSmoothly cmp_ObjectSmoothly2 = interactebleObjects[valueofInteractable].GetComponent<ScaleSmoothly>();
                Vector3 objScale2 = new Vector3(sc * 4.3f, sc * 1.2f, sc * 3.5f);

                cmp_ObjectSmoothly2.startScale = interactebleObjects[valueofInteractable].transform.localScale;
                cmp_ObjectSmoothly2.endScale = objScale2;
                cmp_ObjectSmoothly2.elapsedTime = 0;
                cmp_ObjectSmoothly2.duration = timeBetweenCalls;

                valueofInteractable++;

                velocitiesAll += v;

                //print(v);
            }

            n++;
        }

        avg = velocitiesAll / LimbsToTrack.Length;

        float timeKey = (Mathf.Clamp(  avg, 0, 2) / 2);

        MaterialSmoothly_Skybox cmp_SkyboxMatController = skyboxController.GetComponent<MaterialSmoothly_Skybox>();

        Color currentColor2 = cmp_SkyboxMatController.GetCurrentColor();
        Color finalColor2 = colorThroughSpeed2.Evaluate(timeKey);


        cmp_SkyboxMatController.endColor = finalColor2;
        cmp_SkyboxMatController.startColor = currentColor2;
        cmp_SkyboxMatController.elapsedTime = 0;
        cmp_SkyboxMatController.duration = timeBetweenCalls ;




    }


}

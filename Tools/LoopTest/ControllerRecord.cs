using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;


public class ControllerRecord : MonoBehaviour
{
    private GameObjectRecorder m_Recorder;
   
    [Header("GameObjects")]
    [SerializeField] private GameObject goToRecord;
    [SerializeField] private GameObject goToInstantiate;

    [Header("Places To Instantiate")]
    [SerializeField] private Transform[] trToInstantiate;

    [Header("Control")]
    [SerializeField] bool fullReset = false;
    int spacesToMove = 1;

    bool record = false;

    [SerializeField] bool cancelRecording;

    int clipNumber;
    float timeOfAnimation;

    float timeBetweenRecords;

    int maxRecordings = 100;  

    bool recordingAutomatic = false;

    int numberToInstantiate = 0;

    public GameObject[] instances;

    public float timeToDissolveBeforeReset = 1;
    public bool dissolve;

    [Header("Reproduction")]
    [SerializeField] float timeBetweenCalls = 0.2f;
   
    bool controlReproductionSpeed;

    [SerializeField] bool applyToAll;

    [SerializeField] int dancerNum;

    [SerializeField] float[] reproductionSpeed;

    private float sum;
    private float avg;
    private SetDancersAndBonesToStartTracking cmp_DancersToTrack;

    private bool once;

    Vector3 debug;

    float t;
    

    [Header("UI")]
    //.........UI.........
    [SerializeField] private Text textMax;
    [SerializeField] private Button butStart;
    [SerializeField] private Button butStop;
    [SerializeField] private Button butReset;
    [SerializeField] private Button butCancel;
    [SerializeField] private Toggle togFixedDuration;
    [SerializeField] private Slider sliFixedDuration;
    [SerializeField] private Toggle togAutomaticRecord;
    [SerializeField] private Slider sliAutomaticRecord;
    [SerializeField] private Button butAuto;
    [SerializeField] private Toggle tog_controlReproductionSpeed;

    [SerializeField] private Toggle tog_recMAE;
    [SerializeField] private Toggle tog_recIVA;
    [SerializeField] private Toggle tog_recSUS;

    [Header("Switch Controller Input")]
    LoopInput JoyconControls;

    private void Awake()
    {
        JoyconControls = new LoopInput();

        JoyconControls.JoyConInput.StartRecording.performed += ctx => StartRecording();
        JoyconControls.JoyConInput.StopRecording.performed += ctx => StopRecording();
        JoyconControls.JoyConInput.Reset.performed += ctx => ResetMoveRecording();
        JoyconControls.JoyConInput.ControlReproductionSpeed.performed += ctx => UpdateControlRSpeedUI();

        cmp_DancersToTrack = this.gameObject.GetComponent<SetDancersAndBonesToStartTracking>();
        reproductionSpeed = new float[cmp_DancersToTrack.DancersToTrack.Length];

        t = timeBetweenCalls;
        // UI Update + Add listeners to change values of toggles and sliders
        UpdateUI();
        
        togFixedDuration.onValueChanged.AddListener(delegate { UpdateUI(); });
        sliFixedDuration.onValueChanged.AddListener(delegate { UpdateUI(); });

        togAutomaticRecord.onValueChanged.AddListener(delegate { UpdateUI(); });
        sliAutomaticRecord.onValueChanged.AddListener(delegate { UpdateUI(); });

        tog_controlReproductionSpeed.onValueChanged.AddListener(delegate { UpdateControlRSpeedUI(); });



        tog_controlReproductionSpeed.isOn = false;
        controlReproductionSpeed = false;
        togAutomaticRecord.isOn = false;
        togFixedDuration.isOn = false;
        sliFixedDuration.value = 5f;
        sliAutomaticRecord.value = 2f;

        instances = new GameObject[maxRecordings];
    }

    public void StartRecording() 
    {

        if (!record)
        {
            record = true;

            print("Record " + clipNumber);

            // Create recorder and record the script GameObject.
            m_Recorder = new GameObjectRecorder(goToRecord);

            // Bind all the Transforms on the GameObject and all its children.
            m_Recorder.BindComponentsOfType<Transform>(goToRecord, true);




            if (togFixedDuration.isOn)
            {
                StartCoroutine(RecordFixedDuration());
            }

            if (togAutomaticRecord.isOn)
            {
                recordingAutomatic = true;
            }
            UpdateUI();
        }
        else
        {
            StopRecording();
        }

        
    }

    public void StopRecording()
    {
        record = false;
        print("Stop Record");

        if (togAutomaticRecord.isOn)
        {
            
        }
        else
        {
            UpdateUI();
        }
        


    }

    public void CancelRecording()
    {

        cancelRecording = true;
        
        StopRecording();
        StartCoroutine(WaitFor2Milisecond());


    }

    public void StopAuto()
    {

        print("Stop Automatic Recording");


        record = false;
        recordingAutomatic = false;


        StopAllCoroutines();
        UpdateUI();


    }

    public void ResetMoveRecording() //TO WORK ON IT
    {

        if (fullReset)
        {
            if (!dissolve)
            {
                foreach (GameObject item in instances)
                {
                    Destroy(item);
                }

                clipNumber = 0;
                textMax.text = "Max Anim " + clipNumber + " / " + maxRecordings;

                print("Full reset no dissolve");
            }
            else
            {
                int z = 0;
                foreach (GameObject item in instances)
                {
                    DisolveMeshBeforeDelete(item);
                    StartCoroutine(WaitForXSeconds(item));
                    z++;
                    if (z >= clipNumber)
                    {
                        break;
                    }
                }
            }
            
        }
        else
        {
            int rest = clipNumber - trToInstantiate[0].childCount;

            print("reset move: " + rest + " child count " + trToInstantiate[0].childCount + " clip number " + clipNumber);


            int childCount = trToInstantiate[0].childCount;

            for (int i = 0; i < childCount; i++)
            {
                print("inside of For " + i);

                instances[rest].transform.parent = trToInstantiate[spacesToMove];
                instances[rest].transform.position = trToInstantiate[spacesToMove].position;
                instances[rest].transform.rotation = trToInstantiate[spacesToMove].rotation;
                rest++;
            }


            print("out of For");
            spacesToMove++;

            if (spacesToMove >= trToInstantiate.Length)
            {
                spacesToMove = 1;
            }
        }
    }

    private void LateUpdate()
    {
        if (controlReproductionSpeed)
        {
            t -= Time.deltaTime;

            if (t <= 0)
            {
                DancerControlsReproductionSpeed();
                t = timeBetweenCalls;
            }

            once = true;
        }
        else
        {
            if (once)
            {
                int z = 0;
                for (int i = 0; i < clipNumber; i++)
                {
                    instances[i].GetComponent<Animator>().speed = 1;
                    if (z < reproductionSpeed.Length)
                    {
                        reproductionSpeed[i] = 1;
                    }
                    z++;
                }
                once = false;
            }

        }

        Record();
    }


    //It's going to record every frame all the transforms
    private void Record()
    {
        if (record == true)
        {
            // Take a snapshot and record all the bindings values for this frame.
            m_Recorder.TakeSnapshot(Time.deltaTime);
        }
        else
        {
            if (m_Recorder != null &&  m_Recorder.isRecording)
            {
                FinnishRecording();
            }
        }
    }

    private void FinnishRecording()
    {
        
        if (!cancelRecording)
        {
            // Save the recorded session to the clip.


            AnimationClip clipToStore = new AnimationClip();

            m_Recorder.SaveToClip(clipToStore);
            m_Recorder = null;


            instances[clipNumber] = Instantiate(goToInstantiate, trToInstantiate[0].transform); //// CHANGE THIS FOR SPAWNING IN DIFFERENT PLACES With this -> numberToInstantiate



            print("Instaciated GO");

            AddClipToAnimatorController(clipToStore, instances[clipNumber]);

            //CheckIfSomeDancersShouldNotBeSpawned(instances[clipNumber]);

            StartCoroutine(WaitFor1Milisecond(instances[clipNumber]));



            //Array controllers
            clipNumber++;

            if ((trToInstantiate.Length - 1) == numberToInstantiate)
            {
                numberToInstantiate = 0;
            }
            else
            {
                numberToInstantiate++;
            }

            //UI text feedback
            textMax.text = "Max Anim " + clipNumber + " / " + maxRecordings;

            //Controller of Max Recordings
            if (clipNumber >= maxRecordings)
            {
                MaxRecordings();
                recordingAutomatic = false;
            }
        }
        else
        {
            m_Recorder = null;
        }
        



    }

    private void MaxRecordings()
    {
        butStart.interactable = false;
        butStop.interactable = false;
        StopAuto();
    }


    private void AddClipToAnimatorController(AnimationClip animationClip, GameObject GO_Ins)
    {

        // GO_Ins.GetComponent<Animator>().

        Animator animator = GO_Ins.GetComponent<Animator>();

        AnimatorOverrideController aoc = new AnimatorOverrideController(animator.runtimeAnimatorController);
        var anims = new List<KeyValuePair<AnimationClip, AnimationClip>>();
        foreach (var a in aoc.animationClips)
            anims.Add(new KeyValuePair<AnimationClip, AnimationClip>(a, animationClip));
        aoc.ApplyOverrides(anims);
        animator.runtimeAnimatorController = aoc;

    }


    // NEEDS SetDANCERANDBoneComponent + GET VELOCITY
    private void DancerControlsReproductionSpeed()
    {
        if (cmp_DancersToTrack != null)
        {
            int n = 0;
            foreach (SetDancersAndBonesToStartTracking.Dancers dancer in cmp_DancersToTrack.DancersToTrack)
            {
                Transform[] b = dancer.T_bonesToTrack;

                int nOfBones = b.Length;
                sum = 0;

                // Get Reproduction SPEED VALUE
                foreach (Transform bone in b)
                {
                    sum += bone.GetComponent<GetVelocity>().GetAverage();
                }
                avg = sum / nOfBones;

                reproductionSpeed[n] = Mathf.Clamp(avg * 1.6f, 0.05f, 5);

                

                n++;
            }

            debug = new Vector3(reproductionSpeed[0], reproductionSpeed[1], reproductionSpeed[2]);

            print(debug);

            // APPLY TO EVERY GROUP OF DANCERS
            if (applyToAll)
            {
                for (int i = 0; i < clipNumber; i++)
                {
                    instances[i].GetComponent<Animator>().speed = reproductionSpeed[dancerNum]; // WHAT DANCER IS APPLYING IT
                }
            }
            else
            {
                //APPLY DIFERENT DANCER TO EACH GROUP CREATED 
                int j = 0;
                for (int i = 0; i < clipNumber; i++)
                {
                    instances[i].GetComponent<Animator>().speed = reproductionSpeed[j];
                    j++;

                    if (j > reproductionSpeed.Length)
                    {
                        j = 0;
                    }
                }
            }





        }
    }

    IEnumerator RecordFixedDuration()
    {

        UpdateUI();
        print("Start recording of animation of " + timeOfAnimation);
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(timeOfAnimation);

        print("Finish recording of animation");
        StopRecording();

        if (togAutomaticRecord.isOn)
        {
            StartCoroutine(AutomaticRecord());
        }
    }

    IEnumerator AutomaticRecord()
    {
        print("Start wait time between animations of " + timeBetweenRecords);
        yield return new WaitForSeconds(timeBetweenRecords);
        print("Finish waiting for start recording");

        if (clipNumber < maxRecordings)
        {
            StartRecording();
        }
        
        
    }



    private void UpdateUI()
    {
        if (recordingAutomatic)
        {
            butAuto.interactable = true;
            butStop.interactable = false;
            butCancel.interactable = false;
            butReset.interactable = false;
            butStart.interactable = false;

            togFixedDuration.interactable = false;
            togAutomaticRecord.interactable = false;

            sliAutomaticRecord.interactable = false;
            sliFixedDuration.interactable = false;
        }
        else
        {
            butAuto.interactable = false;
            togFixedDuration.interactable = true;
            togAutomaticRecord.interactable = true;

            butStop.interactable = true;
            butCancel.interactable = true;
            butReset.interactable = true;
            butStart.interactable = true;

            


            if (togFixedDuration.isOn)
            {

                sliFixedDuration.interactable = true;
                timeOfAnimation = sliFixedDuration.value;
            }
            else
            {

                sliFixedDuration.interactable = false;
            }

            if (togAutomaticRecord.isOn)
            {
                sliAutomaticRecord.interactable = true;
                timeBetweenRecords = sliAutomaticRecord.value ;

                togFixedDuration.isOn = true;
                togFixedDuration.interactable = false;
                sliFixedDuration.interactable = true;
                timeOfAnimation = sliFixedDuration.value ;

                butStop.interactable = false;
                butStart.interactable = true;

                butAuto.gameObject.SetActive(true);

                
            }
            else
            {
                togFixedDuration.interactable = true;
                sliAutomaticRecord.interactable = false;

                butAuto.interactable = false;
                butAuto.gameObject.SetActive(false);

            }



            if (!record)
            {
                butStart.interactable = true;
                butStop.interactable = false;
                butCancel.interactable = false;
                butReset.interactable = true;
            }
            else
            {
                if (togAutomaticRecord.isOn)
                {
                    butStop.interactable = false;
                    butCancel.interactable = false;
                }
                else
                {
                    butStop.interactable = true;
                    butCancel.interactable = true;
                }

                butStart.interactable = false;
                butReset.interactable = false;
            }
        }







    }

    private void UpdateControlRSpeedUI()
    {
        if (controlReproductionSpeed)
        {
            controlReproductionSpeed = false;
        }
        else
        {
            controlReproductionSpeed = true;
        }
    }

    void DisolveMeshBeforeDelete(GameObject go)
    {
        for (int j = 0; j < go.transform.childCount; j++)
        {
            for (int i = 0; i < go.transform.GetChild(j).childCount; i++)
            {
                if (go.transform.GetChild(j).GetChild(i).GetComponent<SkinnedMeshRenderer>() != null && go.transform.GetChild(j).GetChild(i).GetComponent<SkinnedMeshRenderer>().enabled == true)
                {
                    AvatarExplosion aE = go.transform.GetChild(j).GetChild(i).gameObject.AddComponent<AvatarExplosion>();
                    aE.TimeToExplode = timeToDissolveBeforeReset;
                }
            }
        }
        
    }

    void CheckIfSomeDancersShouldNotBeSpawned(GameObject go)
    {
        if (tog_recSUS.isOn)
        {
            for (int i = 0; i < go.transform.GetChild(2).childCount; i++)
            {
                if (go.transform.GetChild(0).GetChild(i).GetComponent<SkinnedMeshRenderer>() != null)
                {
                    go.transform.GetChild(0).GetChild(i).GetComponent<SkinnedMeshRenderer>().enabled = true;
                }
            }
        }
        
        if (tog_recIVA.isOn)
        {
            for (int i = 0; i < go.transform.GetChild(2).childCount; i++)
            {
                if (go.transform.GetChild(1).GetChild(i).GetComponent<SkinnedMeshRenderer>() != null)
                {
                    go.transform.GetChild(1).GetChild(i).GetComponent<SkinnedMeshRenderer>().enabled = true;
                }
            }
        }

        if (tog_recMAE.isOn)
        {
            for (int i = 0; i < go.transform.GetChild(2).childCount; i++)
            {
                if (go.transform.GetChild(2).GetChild(i).GetComponent<SkinnedMeshRenderer>() != null)
                {
                    go.transform.GetChild(2).GetChild(i).GetComponent<SkinnedMeshRenderer>().enabled = true;
                }
            } 
        }

        
    }


    public void dancersToShow(string name)
    {
        switch (name)
        {
            case "SUS":
                tog_recSUS.isOn = !tog_recSUS.isOn;
                break;
            case "MAE":
                tog_recMAE.isOn = !tog_recMAE.isOn;
                break;
            case "IVA":
                tog_recIVA.isOn = !tog_recIVA.isOn;
                break;
            default:
                break;
        }
    }

    IEnumerator WaitFor1Milisecond(GameObject go)
    {
       
        yield return new WaitForSeconds(0.1f);
        CheckIfSomeDancersShouldNotBeSpawned(go);
    }


    IEnumerator WaitForXSeconds(GameObject go)
    {
        yield return new WaitForSeconds(timeToDissolveBeforeReset);
        Destroy(go);
        clipNumber = 0;
        textMax.text = "Max Anim " + clipNumber + " / " + maxRecordings;

        print("Full reset with dissolve");
    }
    IEnumerator WaitFor2Milisecond()
    {

        yield return new WaitForSeconds(0.2f);
        cancelRecording = false;
    }

    private void OnEnable()
    {
        JoyconControls.JoyConInput.Enable();
        AvatarRecorderActivator.AvatarRecorderActivatorEvent += dancersToShow;

    }

    private void OnDisable()
    {
        JoyconControls.JoyConInput.Disable();
        AvatarRecorderActivator.AvatarRecorderActivatorEvent -= dancersToShow;

    }
}

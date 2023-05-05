using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;


public class BodyDelay: MonoBehaviour
{
    private GameObjectRecorder m_Recorder;
   
    [Header("GameObjects")]
    [SerializeField] private GameObject goToRecord;
    [SerializeField] private GameObject[] goToUpdate;

    [Header("Control")]

    bool record = false;

    int numberToUpdate = 0;


    [SerializeField]float timeOfAnimation = 0.6f;

    float delayTimeBetweenExtremities;

    float t;

    bool DoOnce;

   AnimationClip clipToStore;


    private void Awake() 
    { 
        delayTimeBetweenExtremities = timeOfAnimation / goToUpdate.Length;

        t = timeOfAnimation;

        clipToStore = new AnimationClip();
    }


    private void LateUpdate()
    {

        t -= Time.deltaTime;

        if (t <= 0)
        {
            // Save the recorded session to the clip.
            if (m_Recorder != null && m_Recorder.isRecording)
            {
                FinnishRecording();
                record = false;
            }
        }
        else
        {
            Record();
        }

        
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
            record = true;

            // Create recorder and record the script GameObject.
            m_Recorder = new GameObjectRecorder(goToRecord);

            // Bind all the Transforms on the GameObject and all its children.
            m_Recorder.BindComponentsOfType<Transform>(goToRecord, true);
            
        }
    }

    private void FinnishRecording()
    {

        m_Recorder.SaveToClip(clipToStore);

        if (numberToUpdate >= goToUpdate.Length)
        {
            m_Recorder = null;
        }
        
        if (!DoOnce)
        {
            AddClipToAnimatorController(clipToStore, goToUpdate[numberToUpdate]);
            DoOnce = true;
        }
        

        //StartCoroutine(WaitFor1Milisecond(goToUpdate[numberToUpdate]));



        //Array controllers
        numberToUpdate++;
        t = timeOfAnimation;
    }



    private void AddClipToAnimatorController(AnimationClip animationClip, GameObject GO_Ins)
    {
        
        Animator animator = GO_Ins.GetComponent<Animator>();

        AnimatorOverrideController aoc = new AnimatorOverrideController(animator.runtimeAnimatorController);
        var anims = new List<KeyValuePair<AnimationClip, AnimationClip>>();
        foreach (var a in aoc.animationClips)
            anims.Add(new KeyValuePair<AnimationClip, AnimationClip>(a, animationClip));
        aoc.ApplyOverrides(anims);
        animator.runtimeAnimatorController = aoc;

    }


    void CheckIfSomeDancersShouldNotBeSpawned(GameObject go)
    {
            for (int i = 0; i < go.transform.GetChild(2).childCount; i++)
            {
                if (go.transform.GetChild(0).GetChild(i).GetComponent<SkinnedMeshRenderer>() != null)
                {
                    go.transform.GetChild(0).GetChild(i).GetComponent<SkinnedMeshRenderer>().enabled = true;
                }
            }

    }

   


    IEnumerator WaitFor1Milisecond(GameObject go)
    {
       
        yield return new WaitForSeconds(0.1f);
        CheckIfSomeDancersShouldNotBeSpawned(go);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Cinemachine;

public class CameraControls : MonoBehaviour
{
    CameraInputActions controls;

    [Header("Cameras")]
    [SerializeField] private GameObject camerasParent;
    [SerializeField] private GameObject cameraStaticToInstantiate;
    [SerializeField] private GameObject cameraOrbitToInstantiate;
    [SerializeField] private GameObject cameraFollowToInstantiate;
    private CinemachineTargetGroup cinemachineTargetGroup;
    private GameObject[] camsTypes;
    [HideInInspector] public CinemachineVirtualCamera activeCam;
    private CinemachineTrackedDolly cmpTrackDolly;
    private CinemachineOrbitalTransposer cmpFollow;
    private CinemachineOrbitalTransposer cmpOrbit;

    [Tooltip("Dancers to follow, look at and orbit around")]
    [Header("Dancers")]
    [SerializeField] public GameObject[] dancerHips;
    List<Transform> trackedPos = new List<Transform>();

    [Header("Controls")]

    [SerializeField] private float movementSpeed;
    [SerializeField] private float verticalMovementSpeed;
    [SerializeField] private float sensitivity;

    private float base_movementSpeed;
    private float base_sensitivity;
    private float base_verticalMovementSpeed;

    private Vector2 move;
    private Vector2 rotate;

    private float yMove;

    float followCloser = 10;
    float followYOffset = 0;
    float followInputValue = 0;

    float orbitCloser = -3;
    float orbitYOffset = 0;

    float railY = 1.5f;

    [Tooltip("Value that multiplies or divides when pressing the triggers: default  = 2")]
    [SerializeField] private float valueToMultiplyOrDivide;
    [SerializeField] private float timeToBlendBetweenCameras;

    //CAMERA BASIC CONTROLLERS
    private int camCount;
    bool unlockMovement = false;

    //CAMERA RAIL CONTROLLERS
    bool goRail;
    public float timeToReachEndRail;
    float t = 0;
    bool inverse = false;
    float percentComplete;

    [Header("UI")]
    [SerializeField] private Text t_cameraName;
    [SerializeField] private Text t_lookingAt;
    [SerializeField] private Slider sli_timeBetweenBlends;
    [SerializeField] private Toggle tog_showControls;
    [SerializeField] private GameObject uiControls;
    [SerializeField] private GameObject uiControlFree;
    [SerializeField] private GameObject uiControlRail;
    [SerializeField] private GameObject uiControlStatic;
    [SerializeField] private DropDownHandler cmpDropdown;
    [SerializeField] private GameObject ui_modifying;




    private void Awake()
    {
        cinemachineTargetGroup = FindObjectOfType<CinemachineTargetGroup>();

        SettingUpOrbitAndFollowCameraOnDancers();
        DefineDefaultCameras();
        DefaultVariables();       
        SettingUpControllerInput();
        AddListeners();
        UpdateUI();
    }

     void SettingUpOrbitAndFollowCameraOnDancers()
    {
        if (dancerHips.Length != 0)
        {
            float g = 0;
            foreach (GameObject dancerHips in dancerHips)
            {
                //Get Pelvis of dancer
                Transform x = dancerHips.transform;

                //Create a gameObject to make cameras Look At
                GameObject go = new GameObject();
                go.name = "goToTrackOfDancer" + g;
                go.transform.parent = x;
                go.transform.position = x.position;
                go.transform.rotation = x.rotation;

                //This code for the dancers SKM RUD JOZ SUS/////////////////////////////////////////////////////////////////////////////////////////////////////
                //go.transform.Rotate(90, 0, 90, Space.Self);

                //Instantiate the cameras
                GameObject followIns = Instantiate(cameraFollowToInstantiate, camerasParent.transform.GetChild(2));
                GameObject OrbitIns = Instantiate(cameraOrbitToInstantiate, camerasParent.transform.GetChild(3));

                followIns.name = "CamFollow " + g.ToString();
                OrbitIns.name = "CamOrbit " + g.ToString();

                //Look and follow each target
                followIns.GetComponent<CinemachineVirtualCamera>().m_LookAt = go.transform;
                followIns.GetComponent<CinemachineVirtualCamera>().m_Follow = go.transform;

                OrbitIns.GetComponent<CinemachineVirtualCamera>().m_LookAt = go.transform;
                OrbitIns.GetComponent<CinemachineVirtualCamera>().m_Follow = go.transform;

                //Add to the list each trackd object
                trackedPos.Add(go.transform);
                g++;
            }

            //Add a follow that gets all the group of dancer
            if (cinemachineTargetGroup != null)
            {
                GameObject followIns = Instantiate(cameraFollowToInstantiate, camerasParent.transform.GetChild(2));

                followIns.name = "CamFollow " + "group of dancers";

                followIns.GetComponent<CinemachineVirtualCamera>().m_LookAt = cinemachineTargetGroup.transform;
                followIns.GetComponent<CinemachineVirtualCamera>().m_Follow = cinemachineTargetGroup.transform;

                followIns.GetComponent<CinemachineVirtualCamera>().AddCinemachineComponent<CinemachineGroupComposer>();
            }
        }
        

        
    }

     void DefineDefaultCameras()
    {

        //Setting Parent of camera gameobjects
        camsTypes = new GameObject[camerasParent.transform.childCount];
        int a = 0;
        int b = 0;
        int c = 0;
        int d = 0;


        for (int i = 0; i < camerasParent.transform.childCount; i++)
        {
            camsTypes[i] = camerasParent.transform.GetChild(i).gameObject;

            //Call this function to inactive and activate the different groups
            int n = camsTypes[i].transform.childCount;
            for (int j = 0; j < n; j++)
            {
                CinemachineVirtualCamera x = camsTypes[i].transform.GetChild(j).GetComponent<CinemachineVirtualCamera>();

                if (x != null)
                {

                    switch (x.tag)
                    {
                        case "Free":
                            x.Priority = 500;
                            activeCam = x;
                            x.gameObject.SetActive(true);

                            break;

                        case "Orbit":
                            a++;
                            x.Priority = 400;
                            if (a > 1)
                                x.gameObject.SetActive(false);
                            else
                                x.gameObject.SetActive(true);


                            break;

                        case "Rail":
                            

                            b++;
                            x.Priority = 300;
                            if (b > 1)
                                x.gameObject.SetActive(false);
                            else
                                x.gameObject.SetActive(true);

                            break;

                        case "Follow":
                            
                            c++;
                            x.Priority = 200;
                            if (c > 1)
                                x.gameObject.SetActive(false);
                            else
                                x.gameObject.SetActive(true);

                            break;

                        case "Static":
                            d++;
                            x.Priority = 100;

                            
                            if (d > 1)
                            {
                                x.gameObject.SetActive(false);
                            }
                            else
                                x.gameObject.SetActive(true);

                            break;

                        default:
                            break;
                    }
                }
                
            }   
        }
        
        //Setting up camra actives parents
        SwitchCameraGroup(0);
    }

    private void DefaultVariables()
    {
        if (movementSpeed == 0)
        {
            movementSpeed = 7f;
        }

        if (verticalMovementSpeed == 0)
        {
            verticalMovementSpeed = 5f;
        }

        if (sensitivity == 0)
        {
            sensitivity = 100;
        }

        if (valueToMultiplyOrDivide == 0)
        {
            valueToMultiplyOrDivide = 2;
        }

        base_movementSpeed = movementSpeed;
        base_sensitivity = sensitivity;
        base_verticalMovementSpeed = verticalMovementSpeed;
        SetTimeBetweenBlends();
        ui_modifying.SetActive(false);
    }

    private void SetTimeBetweenBlends()
    {
        var brain = Camera.main.GetComponent<CinemachineBrain>();
        brain.m_DefaultBlend.m_Time = timeToBlendBetweenCameras;
    }

    private void SettingUpControllerInput()
    {
        controls = new CameraInputActions();

        controls.Controller.GoUp.performed += ctx => yMove = ctx.ReadValue<float>();
        controls.Controller.GoUp.canceled += ctx => yMove = 0;

        controls.Controller.GoDown.performed += ctx => yMove = -ctx.ReadValue<float>();
        controls.Controller.GoDown.canceled += ctx => yMove = 0;

        controls.Controller.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Controller.Move.canceled += ctx => move = Vector2.zero;

        controls.Controller.Rotation.performed += ctx => rotate = ctx.ReadValue<Vector2>();
        controls.Controller.Rotation.canceled += ctx => rotate = Vector2.zero;

        controls.Controller.Faster.performed += ctx => Multiply();
        controls.Controller.Faster.canceled += ctx => ResetSensSpeed();

        controls.Controller.Slower.performed += ctx => Divide();
        controls.Controller.Slower.canceled += ctx => ResetSensSpeed();

        controls.Controller.ChangeCameraUp.performed += ctx => SwitchCameraGroup(+1);
        controls.Controller.ChangeCameraDown.performed += ctx => SwitchCameraGroup(-1);

        controls.Controller.ChangeCameraRight.performed += ctx => SwitchCameraInsideGroup(+1);
        controls.Controller.ChangeCameraLeft.performed += ctx => SwitchCameraInsideGroup(-1);

        controls.Controller.PlaySelect.performed += ctx => PlaySelect();

        controls.Controller.DestroyBack.performed += ctx => DestroyBack();

        controls.Controller.LookAt.performed += ctx => MakeLookAt();

        controls.Controller.Inverse.performed += ctx => InvertRail();

    }

    private void Update()
    {
        ControllerMovementAndRotation();
        MoveThroughRail();
    }

    

    private void ControllerMovementAndRotation()
    {

        if (activeCam.tag == "Free")
        {
            Vector3 mh = new Vector3(move.x, 0, move.y) * movementSpeed * Time.deltaTime;
            Vector3 mv = new Vector3(0, yMove, 0) * verticalMovementSpeed * Time.deltaTime;
            Vector3 m = new Vector3(mh.x, mv.y, mh.z);

            activeCam.transform.Translate(m);

            
        }

        if (activeCam.tag == "Static" && unlockMovement)
        {
            Vector3 mh = new Vector3(move.x, 0, move.y) * movementSpeed * Time.deltaTime;
            Vector3 mv = new Vector3(0, yMove, 0) * verticalMovementSpeed * Time.deltaTime;
            Vector3 m = new Vector3(mh.x, mv.y, mh.z);

            activeCam.transform.Translate(m);

        }

        if (activeCam.tag == "Free" || activeCam.tag == "Static" || activeCam.tag == "Rail")
        {
            Vector3 r = new Vector3(-rotate.y, rotate.x, 0) * sensitivity * Time.deltaTime;
            activeCam.transform.Rotate(r.x, r.y, 0);

            activeCam.transform.eulerAngles = new Vector3(activeCam.transform.eulerAngles.x, activeCam.transform.eulerAngles.y, 0);
        }
           

        if (activeCam.tag == "Follow")
        {
            cmpFollow = activeCam.GetCinemachineComponent<CinemachineOrbitalTransposer>();


            followCloser += move.y * movementSpeed * Time.deltaTime * -1;
            followYOffset += rotate.y * movementSpeed * Time.deltaTime;
            followInputValue = rotate.x * sensitivity * Time.deltaTime * -1;

            cmpFollow.m_FollowOffset.z = followCloser;
            cmpFollow.m_FollowOffset.y = followYOffset;
            cmpFollow.m_XAxis.m_InputAxisValue = followInputValue;

            //We move the game object to trak that is in the hip
            Vector3 mv = new Vector3(0, yMove, 0) * verticalMovementSpeed * Time.deltaTime;
            trackedPos[int.Parse(activeCam.gameObject.name.Substring(activeCam.gameObject.name.Length - 1))].Translate(mv);
        }

        if (activeCam.tag == "Orbit")
        {
            cmpOrbit = activeCam.GetCinemachineComponent<CinemachineOrbitalTransposer>();

            orbitCloser += move.y * movementSpeed * Time.deltaTime * -1;
            orbitYOffset += rotate.y * movementSpeed * Time.deltaTime;

            cmpOrbit.m_FollowOffset.z = orbitCloser;
            cmpOrbit.m_FollowOffset.y = orbitYOffset;

            Vector3 mv = new Vector3(0, yMove, 0) * verticalMovementSpeed * Time.deltaTime;

            /*print("El hueso a mover es " + trackedPos[int.Parse(activeCam.gameObject.name.Substring(activeCam.gameObject.name.Length - 1))] + " con un vector " + mv);*/
            Transform goTracked = trackedPos[int.Parse(activeCam.gameObject.name.Substring(activeCam.gameObject.name.Length - 1))];

            goTracked.Translate(mv);


            /// NEWW
            Vector3 r = new Vector3(rotate.y, rotate.x, 0) * sensitivity * Time.deltaTime;

            goTracked.Rotate(0, r.y, 0);

            goTracked.eulerAngles = new Vector3(0, goTracked.eulerAngles.y, 0);

        }

        if (activeCam.tag == "Rail")
        {
            cmpTrackDolly = activeCam.GetCinemachineComponent<CinemachineTrackedDolly>();

            railY += yMove * verticalMovementSpeed * Time.deltaTime ;
            cmpTrackDolly.m_PathOffset.y = railY;
        }
    }

    void Multiply()
    {
        movementSpeed = movementSpeed * valueToMultiplyOrDivide;
        verticalMovementSpeed = verticalMovementSpeed * valueToMultiplyOrDivide;
    }

    void Divide()
    {
        movementSpeed = movementSpeed / valueToMultiplyOrDivide;
        verticalMovementSpeed = verticalMovementSpeed / valueToMultiplyOrDivide;
        sensitivity = sensitivity / valueToMultiplyOrDivide;
    }

    void ResetSensSpeed()
    {
        movementSpeed = base_movementSpeed;
        sensitivity = base_sensitivity;
        verticalMovementSpeed = base_verticalMovementSpeed;
    }


    /** CAMERA GROUP**/
    void SwitchCameraGroup(int value)
    {
        //Add value of Up or down to var
        camCount += value;

        //Check if it is out of bounds
        if (camCount < 0)
        {
            camCount = camsTypes.Length - 1;
        }
        if (camCount >= camsTypes.Length)
        {
            camCount = 0;
        }

        //Active and unactive groups
        for (int i = 0; i < camsTypes.Length; i++)
        {
            
            if (i != camCount)
            {                
                camsTypes[i].gameObject.SetActive(false);
            }
            else
            {
                camsTypes[i].gameObject.SetActive(true);

                for (int l = 0; l < camsTypes[i].transform.childCount; l++)
                {
                    if (camsTypes[i].transform.GetChild(l).gameObject.activeSelf)
                    {
                        activeCam = camsTypes[i].transform.GetChild(l).gameObject.GetComponent<CinemachineVirtualCamera>();
                        //print("Setting up as active Cam" + activeCam.gameObject.name);
                        UpdateUI();
                    }
                }
            }
        }


    }

    /**CAMERA INSIDE GROUP**/

    void SwitchCameraInsideGroup(int value)
    {
        
        //search for what group is active
        for (int i = 0; i < camsTypes.Length; i++)
        {
            
            if (camsTypes[i].activeSelf)
            {
                //go throught the childs of the group that is active
                for (int k = 0; k < camsTypes[i].transform.childCount; k++)
                {
                    //get the camera that is active
                    if (camsTypes[i].transform.GetChild(k).gameObject == activeCam.gameObject)
                    {
                       
                        //Check if it is out of bounds
                        int h = k + value;
                        if (h < 0)
                        {
                            h = camsTypes[i].transform.childCount - 1;
                        }
                        if (h >= camsTypes[i].transform.childCount)
                        {   
                            h = 0;
                        }

                        if (camsTypes[i].transform.childCount != 1)
                        {
                            //set cameras active and unactive camera
                            camsTypes[i].transform.GetChild(h).gameObject.SetActive(true);
                            camsTypes[i].transform.GetChild(k).gameObject.SetActive(false);
                            activeCam = camsTypes[i].transform.GetChild(h).GetComponent<CinemachineVirtualCamera>();
                        }
                        break;
                    }
                }
                break;
            }
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        t_cameraName.text = activeCam.name.ToString();

        sli_timeBetweenBlends.interactable = true;
        timeToBlendBetweenCameras = sli_timeBetweenBlends.value;
        SetTimeBetweenBlends();

        if (activeCam.m_LookAt != null)
        {
            t_lookingAt.text = "Is Looking at Target";
            t_lookingAt.color = Color.yellow;
        }
        else
        {
            t_lookingAt.text = "Not Looking at Target";
            t_lookingAt.color = Color.grey;
        }

        if (activeCam.tag == "Free")
        {
            uiControlFree.SetActive(true);
        }
        else
        {
            uiControlFree.SetActive(false);
        }

        if (activeCam.tag == "Rail")
        {
            uiControlRail.SetActive(true);
        }
        else
        {
            uiControlRail.SetActive(false);
        }

        if (activeCam.tag == "Static")
        {
            uiControlStatic.SetActive(true);

            //// Get if the parameter if it looks at something
            StaticIsLookingAt();
        }
        else
        {
            uiControlStatic.SetActive(false);
        }
    }

    void HideControls()
    {
        if (tog_showControls.isOn)
        {
            uiControls.SetActive(true);
        }
        else
        {
            uiControls.SetActive(false);
        }
    }

    void AddListeners()
    {
        sli_timeBetweenBlends.onValueChanged.AddListener(delegate { UpdateUI(); });
        tog_showControls.onValueChanged.AddListener(delegate { HideControls(); });
        tog_showControls.isOn = false;
    }

    void PlaySelect()
    {

        if (activeCam.tag == "Rail")
        {
            ExecuteRail();
        }

        if (activeCam.tag == "Free")
        {
            SpawnStaticCam();
        }

        if (activeCam.tag == "Static")
        {
            if (unlockMovement)
            {
                ui_modifying.SetActive(false);
                unlockMovement = false;
            }
            else
            {
                ui_modifying.SetActive(true);
                unlockMovement = true;
            }
            
        }
        else
        {
            ui_modifying.SetActive(false);
            unlockMovement = false;
        }

    }

    void DestroyBack()
    {
        if (activeCam.tag == "Static")
        {
            Destroy(activeCam.gameObject);
            SwitchCameraInsideGroup(+1);
        }
    }

    void ExecuteRail() 
    {   
        if (goRail)
        {
            goRail = false;
        }
        else
        {
            cmpTrackDolly = activeCam.GetCinemachineComponent<CinemachineTrackedDolly>();
            percentComplete = cmpTrackDolly.m_PathPosition;
            t = percentComplete * timeToReachEndRail;
            goRail = true;
        }
        
    }

    private void MoveThroughRail()
    {
        if (goRail)
        {
            if (inverse)
            {
                t -= Time.deltaTime;
            }
            else
            {
                t += Time.deltaTime;
            }
            
            percentComplete = t / timeToReachEndRail;
            percentComplete = Mathf.Clamp01(percentComplete);

            cmpTrackDolly.m_PathPosition = percentComplete;

            if (percentComplete == 1)
            {
                goRail = false;
                inverse = true;
                percentComplete = 0.99f;
            }

            if (percentComplete == 0)
            {
                goRail = false;
                inverse = false;
                percentComplete = 0.01f;
            }
        }
    }

    void InvertRail()
    {
        if (inverse)
        {
            inverse = false;
        }
        else
        {
            inverse = true;
        }
    }

    void SpawnStaticCam()
    {
        GameObject go = Instantiate(cameraStaticToInstantiate, activeCam.transform.position, activeCam.transform.rotation, camsTypes[camsTypes.Length-1].transform);
        go.SetActive(false);
    }


    void MakeLookAt()
    {
        if (activeCam.tag == "Static" || activeCam.tag == "Rail" || activeCam.tag == "Free")
        {
            if (activeCam.m_LookAt == null)
            {
                //Average of positions of cameras
                activeCam.m_LookAt = cinemachineTargetGroup.transform;
                activeCam.AddCinemachineComponent<CinemachineGroupComposer>();
            }
            else
            {
                activeCam.m_LookAt = null;
                activeCam.DestroyCinemachineComponent<CinemachineGroupComposer>();
            }
            
        }

        UpdateUI();
    }

    void OldStaticIsLookingAt()
    {
        cmpDropdown.ValueStatic(activeCam.GetComponent<CamStaticOptions>().LookAtValue);
    }

    void StaticIsLookingAt()
    {
        cmpDropdown.ValueStatic(activeCam.GetComponent<CamStaticOptions>().LookAtValue);
    }

    private void OnEnable()
    {
        controls.Controller.Enable();
    }

    private void OnDisable()
    {
        
        controls.Controller.Disable();
    }
}

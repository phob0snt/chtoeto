using System;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using mitaywalle.UICircleSegmentedNamespace;
using UnityEngine.XR;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.XR.Interaction.Toolkit.AffordanceSystem.Theme.Primitives;
using Unity.XR.CoreUtils;

public class carScript : MonoBehaviour
{
    public float currentSpeed;

    [SerializeField] private TMP_Text text;
    [SerializeField] private WheelCollider[] wheelColliders = new WheelCollider[4]; //fr, fl, br, bl
    [SerializeField] private Transform frontLeftWheel;
    [SerializeField] private Transform frontRightWheel;
    [SerializeField] private Transform backLeftWheel;
    [SerializeField] private Transform backRightWheel;

    [SerializeField] private float maxTorque = 1000f;
    [SerializeField] private float maxSteeringAngle = 45f;
    [SerializeField] public float maxRpm = 1600f;
    [SerializeField] private float downforceValue = 50f;


    [SerializeField] private Rigidbody carBody;

    [SerializeField] private float maxSpeedCoeff = 1.5f;

    [SerializeField] private GameObject speedo;


    public int gearNum = 1;

    private bool canStart;

    private float currGearCoef = 0.55f;

    private float movement;

    public float enginePitchCoef;

    private float kmhCoef = 5f;

    private float currTorque;

    [SerializeField] private float enginePitch;

    private int mask = 1 << 3;

    private Vector2 axises;

    [SerializeField] private bool[] wheelsOnGround = new bool[4];

    private bool isGrounded = false;

    private InputAction moveAction;

    private Vector2 currInputVector;

    private Vector2 smoothVel;

    private bool canSteer;

    Vector2 currSteerVal;

    private Vector2 steerVal;
    private int triggerVal;

    private bool gasVal;
    private bool breakVal;

    private XRNode lnode = XRNode.LeftHand;
    private XRNode rnode = XRNode.RightHand;

    private bool go;

    UnityEngine.XR.InputDevice ldevice;
    UnityEngine.XR.InputDevice rdevice;

    List<UnityEngine.XR.InputDevice> ldevices = new List<UnityEngine.XR.InputDevice>();
    List<UnityEngine.XR.InputDevice> rdevices = new List<UnityEngine.XR.InputDevice>();



    private void Awake()
    {
        PlayerInput playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["carControl"];
    }
    public void GetAxis(InputAction.CallbackContext val)
    {
        axises = val.ReadValue<Vector2>();
    }

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        carBody.GetComponent<Rigidbody>().centerOfMass = carBody.transform.GetChild(0).localPosition;
        enginePitchCoef = 5f;
    }

    private void UpdateWheels(WheelCollider wheel, Transform transf)
    {
        Vector3 _pos;
        Quaternion _quat;

        wheel.GetWorldPose(out _pos, out _quat);

        transf.SetPositionAndRotation(_pos, _quat);

    }

    private void DownForce()
    {
        if (isGrounded)
            this.GetComponent<Rigidbody>().AddForce(-transform.up * downforceValue * this.GetComponent<Rigidbody>().velocity.magnitude);
    }

    private void CarControl()
    {
        go = GameObject.Find("Loading").GetComponent<gameMng>().go;
        canSteer = GameObject.Find("Left Hand Model").GetComponent<PlrLInput>().canSteer;
        //canSteer = true;
        enginePitch = carBody.GetComponent<engineSound>().enginePitch;
        if (rdevice.name != null)
        {
            triggerVal = Convert.ToInt32(gasVal) + (-Convert.ToInt32(breakVal));
            if (enginePitch <= 1.6f && go && canStart)
            {
                movement = -maxTorque * 0.7f * Convert.ToInt32(triggerVal) * currGearCoef;
            }
            if (canSteer)
            {
                wheelColliders[1].steerAngle = currSteerVal.x * maxSteeringAngle;
                wheelColliders[0].steerAngle = currSteerVal.x * maxSteeringAngle;
            }
        }
        else
        {
            if (enginePitch <= 1.6f && go && canStart)
            {
                movement = -maxTorque * 0.7f * currInputVector.y * currGearCoef;
            }
            if (canSteer)
            {
                wheelColliders[1].steerAngle = currInputVector.x * maxSteeringAngle;
                wheelColliders[0].steerAngle = currInputVector.x * maxSteeringAngle;
            }
        }
        
        if (MathF.Abs(-wheelColliders[3].rpm) < maxRpm)
        {
            wheelColliders[1].motorTorque = movement * 0.7f;
            wheelColliders[0].motorTorque = movement * 0.7f;
            wheelColliders[3].motorTorque = movement;
            wheelColliders[2].motorTorque = movement;
        }
        else
        {
            wheelColliders[1].motorTorque = 0f;
            wheelColliders[0].motorTorque = 0f;
            wheelColliders[3].motorTorque = 0f;
            wheelColliders[2].motorTorque = 0f;
        }
        GameObject.Find("Main Camera").GetComponent<Camera>().fieldOfView = 70 + (currentSpeed / 300) * 10;
    }

    private void AutoGearsChange()
    {
        GameObject.Find("speedo").transform.GetChild(1).GetComponent<TMP_Text>().text = gearNum.ToString();
        enginePitch = carBody.GetComponent<engineSound>().enginePitch;
        

        if (wheelColliders[2].rpm < 0)
        {
            if ((enginePitch >= 1.5f && gearNum == 1) && isGrounded)
            {
                gearNum++;
                currGearCoef = 0.65f;
                enginePitchCoef = 3.8f;
            }
            if (enginePitch < 0.8 && gearNum == 2 && isGrounded)
            {
                gearNum--;
                currGearCoef = 0.55f;
                enginePitchCoef = 5;
            }
            if ((enginePitch >= 1.5f && gearNum == 2) && isGrounded)
            {
                gearNum++;
                currGearCoef = 0.8f;
                enginePitchCoef = 2.9f;
            }
            if (enginePitch < 0.9 && gearNum == 3 && isGrounded)
            {
                gearNum--;
                currGearCoef = 0.65f;
                enginePitchCoef = 3.8f;
            }
            if (enginePitch >= 1.5f && gearNum == 3 && isGrounded)
            {
                gearNum++;
                currGearCoef = 0.9f;
                enginePitchCoef = 2.4f;
            }
            if (enginePitch < 1 && gearNum == 4 && isGrounded)
            {
                gearNum--;
                currGearCoef = 0.8f;
                enginePitchCoef = 2.6f;
            }
            if (enginePitch >= 1.5f && gearNum == 4 && isGrounded)
            {
                gearNum++;
                currGearCoef = 1;
                enginePitchCoef = 1.8f;
            }
            if (enginePitch < 1.1 && gearNum == 5 && isGrounded)
            {
                gearNum--;
                currGearCoef = 0.95f;
                enginePitchCoef = 2f;
            }
        }
        else
        {
            GameObject.Find("speedo").transform.GetChild(1).GetComponent<TMP_Text>().text = "R";
        }
    }

    private void MaxSpeed()
    {
        //print(wheelColliders[3].rpm);
        if (Math.Abs(wheelColliders[3].rpm) > 100f)
        {
            if (axises.y != 0)
            {
                carBody.drag = 0.1f + currentSpeed * 0.001f * maxSpeedCoeff;
                carBody.angularDrag = 0.05f + currentSpeed * 0.001f * maxSpeedCoeff * 0.5f;
            }
            else
            {
                carBody.drag = 0.1f + currentSpeed * 0.0001f;
                carBody.angularDrag = 0.05f + currentSpeed * 0.00005f;
            }
            
        }
        currentSpeed = Mathf.Ceil((float)carBody.gameObject.GetComponent<Rigidbody>().velocity.magnitude * kmhCoef);
    }


    private void SteeringWheel()
    {
        if (canSteer)
        {
            GameObject steer = GameObject.Find("steeringWheel");
            float rotation;
            if (rdevice.name != null)
                rotation = currSteerVal.x * 100;
            else
                rotation = currInputVector.x * 100;
            steer.transform.localEulerAngles = new Vector3(0, rotation - 90, 0);
        }
    }

    public void Sounds()
    {
        AudioSource music = GameObject.Find("xzcho").GetComponent<AudioSource>();
        AudioSource beep = GameObject.Find("gudok").GetComponent<AudioSource>();
        if (GameObject.Find("bCl").GetComponent<handCollides>().rCollides || GameObject.Find("bCr").GetComponent<handCollides>().rCollides)
        {
            if (music.enabled)
                music.enabled = false;
            else
                music.enabled = true;
        }
        if (GameObject.Find("bCl").GetComponent<handCollides>().bibiCollides || GameObject.Find("bCr").GetComponent<handCollides>().bibiCollides)
        {
            beep.Play();
        }
    }

    private void StartCar()
    {
        canStart = GameObject.Find("startEngine").GetComponent<KeyInside>().canStart;
        if (!canStart)
        {
            carBody.gameObject.GetComponent<AudioSource>().enabled = false;
        }
        else
        {
            carBody.gameObject.GetComponent<AudioSource>().enabled = true;
        }
        //canStart= true;
    }

    private void GroundCheck()
    {
        for (int i = 0; i < 4; i++)
        {
            RaycastHit hit;
            
            if (Physics.Raycast(wheelColliders[i].transform.position, -transform.up, out hit, 1.8f, mask))
            {
                Debug.DrawRay(wheelColliders[i].transform.position, -transform.up * hit.distance, Color.red);
                wheelsOnGround[i] = true;
            }
            else
            {
                wheelsOnGround[i] = false;
            }
        }
        int count = 0;
        foreach (bool wheel in wheelsOnGround)
        {
            if (wheel)
                count++;
        }
        if (count == 4)
            isGrounded = true;
        else
            isGrounded = false;
        print(isGrounded);


    }

    private void OnEnable()
    {
        if (ldevice.isValid)
        {
            GetLDevice();
        }
        if (rdevice.isValid)
        {
            GetRDevice();
        }
    }

    private void Speedo()
    {
        text.text = currentSpeed.ToString() + " km/h";
        float coef = carBody.GetComponent<engineSound>().enginePitch * (carBody.GetComponent<engineSound>().enginePitch * 0.2f);
        speedo.GetComponent<UICircleSegmented>().fillAmount = coef;
    }

    
    void GetLDevice()
    {
        UnityEngine.XR.InputDevices.GetDevicesAtXRNode(lnode, ldevices);
        ldevice = ldevices.FirstOrDefault();
    }

    void GetRDevice()
    {
        UnityEngine.XR.InputDevices.GetDevicesAtXRNode(rnode, rdevices);
        rdevice = rdevices.FirstOrDefault();
    }

    private void Update()
    {
        if (!ldevice.isValid)
        {
            GetLDevice();
        }

        if (!rdevice.isValid)
        {
            GetRDevice();
        }
        Debug.Log(triggerVal);

        rdevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxis, out steerVal);
        rdevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out gasVal);
        ldevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out breakVal);
        if (rdevice.name == null)
        {
            Vector2 input = moveAction.ReadValue<Vector2>();
            currInputVector = Vector2.SmoothDamp(currInputVector, input, ref smoothVel, 0.15f);
        }
        else
        {
            currSteerVal = Vector2.SmoothDamp(currSteerVal, steerVal, ref smoothVel, 0.15f);
        }

        //xrOrigin.position = new Vector3(this.transform.position.x + 0.92f, this.transform.position.y, this.transform.position.z);
        //xrOrigin.localRotation = Quaternion.Euler(this.transform.rotation.x, this.transform.rotation.y + 180, this.transform.rotation.z);

        StartCar();


        CarControl();

        MaxSpeed();

        UpdateWheels(wheelColliders[1], frontLeftWheel);
        UpdateWheels(wheelColliders[0], frontRightWheel);
        UpdateWheels(wheelColliders[3], backLeftWheel);
        UpdateWheels(wheelColliders[2], backRightWheel);

        SteeringWheel();

        Speedo();

        GroundCheck();

        DownForce();

        AutoGearsChange();
        
    }
}

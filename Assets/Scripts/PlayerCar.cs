using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCar : MonoBehaviour {

    private BoxCollider playerBoxCollider;
    private void Awake()
    {
        playerBoxCollider = GetComponent<BoxCollider>();
    }

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
 
    }
        

    public List<AxleInfo> axleInfos; // the information about each individual axle
    public float maxMotorTorque; // maximum torque the motor can apply to wheel
    public float maxSteeringAngle; // maximum steer angle the wheel can have

    // Control wheel transform
    public void ApplyLocalPositionToVisuals(WheelCollider collider, Transform wheel_T)
    {
        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        wheel_T.position = position;
        wheel_T.rotation = rotation;
    }

    public void FixedUpdate()
    {

        // Control wheel collider
        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");

        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            if (axleInfo.motor)
            {
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
            }
            ApplyLocalPositionToVisuals(axleInfo.leftWheel, axleInfo.leftWheel_T);
            ApplyLocalPositionToVisuals(axleInfo.rightWheel, axleInfo.rightWheel_T);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "CollisionFail")
            Debug.Log("Game over !");
    }


    [SerializeField]
    private ParkingSlots parkingSlots;

    public void OnTriggerStay(Collider collider)
    {
        if(playerBoxCollider.bounds.min.x > collider.bounds.min.x &&
           playerBoxCollider.bounds.max.x < collider.bounds.max.x &&

           playerBoxCollider.bounds.min.y > collider.bounds.min.y &&
           playerBoxCollider.bounds.max.y < collider.bounds.max.y &&

           playerBoxCollider.bounds.min.z > collider.bounds.min.z &&
           playerBoxCollider.bounds.max.z < collider.bounds.max.z)
        {
            parkingSlots.SetVictoryTexture();
        }
    }
}

[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public Transform leftWheel_T;
    public Transform rightWheel_T;
    public bool motor; // is this wheel attached to motor?
    public bool steering; // does this wheel apply steer angle?
}


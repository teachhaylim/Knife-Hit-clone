using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetRotationController : MonoBehaviour
{
    [SerializeField] 
    private MyRotation[] patterns;
    private WheelJoint2D wheelJoint;  //this will be set to the Wheel Joint 2D from the LogMotor object
    private JointMotor2D motor; //something has to actually apply a force to the log through the Wheel Joint 2D
    public float torques = 10000;

    private void Awake()
    {
        //setting fields
        wheelJoint = GetComponent<WheelJoint2D>();
        motor = new JointMotor2D();
        //starting an infinitely looping coroutine defined below right when this script loads (awakes)
        StartCoroutine("PlayPattern");
    }

    private IEnumerator PlayPattern()
    {
        int index = 0;
        //infinite coroutine loop
        while (true)
        {
            //working with physics, executing as if this was running in a FixedUpdate method
            yield return new WaitForFixedUpdate();

            motor.motorSpeed = patterns[index].Speed;

            //hard coded 10000, feel free to experiment with other torques if you wish
            motor.maxMotorTorque = torques;

            //set the updated motor to be the motor "sitting" on the Wheel Joint 2D
            wheelJoint.motor = motor;

            //let the motor do its thing for the specified duration
            yield return new WaitForSecondsRealtime(patterns[index].Duration);
            index++;

            //infinite loop through the patterns
            index = index < patterns.Length ? index : 0;
        }
    }
}

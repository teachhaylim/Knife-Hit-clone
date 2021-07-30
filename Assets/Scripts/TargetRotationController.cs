using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetRotationController : MonoBehaviour
{
    public MyRotation[] patterns;
    private WheelJoint2D wheelJoint;
    private JointMotor2D motor;
    public float torques = 10000;

    private void Awake()
    {
        wheelJoint = GetComponent<WheelJoint2D>();
        motor = new JointMotor2D();
        StartCoroutine("PlayPattern");
    }

    private IEnumerator PlayPattern()
    {
        int index = 0;

        while (true)
        {
            yield return new WaitForFixedUpdate();

            motor.motorSpeed = patterns[index].Speed;
            motor.maxMotorTorque = torques;
            wheelJoint.motor = motor;

            yield return new WaitForSecondsRealtime(patterns[index].Duration);
            index++;

            index = index < patterns.Length ? index : 0;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollForeArms : MonoBehaviour
{
    [Header("Hand Bones")]
    [SerializeField] private CharacterJoint[] foreArms;
    [SerializeField] private float lowV;
    [SerializeField] private float highV;
    [SerializeField] private float swing1V;
    [SerializeField] private float swing2V;
    private Rigidbody rb;
    public void HoldingHand()
    {
        foreach (var foreArm in foreArms)
        {
            SoftJointLimit low = foreArm.lowTwistLimit;
            low.limit = -70;
            foreArm.lowTwistLimit = low;

            SoftJointLimit high = foreArm.highTwistLimit;
            high.limit = -70;
            foreArm.highTwistLimit = high;

            SoftJointLimit swing1 = foreArm.swing1Limit;
            swing1.limit = 0f;
            foreArm.swing1Limit = swing1;

            SoftJointLimit swing2 = foreArm.swing2Limit;
            swing2.limit = 0f;
            foreArm.swing2Limit = swing2;
        }
    }
    public void ReleaseHand()
    {
        foreach (var foreArm in foreArms)
        {
            SoftJointLimit low = foreArm.lowTwistLimit;
            low.limit = lowV;
            foreArm.lowTwistLimit = low;

            SoftJointLimit high = foreArm.highTwistLimit;
            high.limit = highV;
            foreArm.highTwistLimit = high;

            SoftJointLimit swing1 = foreArm.swing1Limit;
            swing1.limit = swing1V;
            foreArm.swing1Limit = swing1;

            SoftJointLimit swing2 = foreArm.swing2Limit;
            swing2.limit = swing2V;
            foreArm.swing2Limit = swing2;
        }
    }
}

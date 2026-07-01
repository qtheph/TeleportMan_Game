using UnityEngine;

public class RagdollUpperArms : MonoBehaviour
{
    [Header("Hand Bones")]
    [SerializeField] private CharacterJoint[] upperArms;
    [SerializeField] private float lowV;
    [SerializeField] private float highV;
    [SerializeField] private float swing1V;
    [SerializeField] private float swing2V;
    private Rigidbody rb;
    public void HoldingHand()
    {
        foreach (var upperArms in upperArms)
        {
            SoftJointLimit low = upperArms.lowTwistLimit;
            low.limit = -90f;
            upperArms.lowTwistLimit = low;

            SoftJointLimit high = upperArms.highTwistLimit;
            high.limit = -90f;
            upperArms.highTwistLimit = high;

            SoftJointLimit swing1 = upperArms.swing1Limit;
            swing1.limit = 0f;
            upperArms.swing1Limit = swing1;

            SoftJointLimit swing2 = upperArms.swing2Limit;
            swing2.limit = 0f;
            upperArms.swing2Limit = swing2;
        }
    }
    public void ReleaseHand()
    {
        foreach (var upperArms in upperArms)
        {
            SoftJointLimit low = upperArms.lowTwistLimit;
            low.limit = lowV;
            upperArms.lowTwistLimit = low;

            SoftJointLimit high = upperArms.highTwistLimit;
            high.limit = highV;
            upperArms.highTwistLimit = high;

            SoftJointLimit swing1 = upperArms.swing1Limit;
            swing1.limit = swing1V;
            upperArms.swing1Limit = swing1;

            SoftJointLimit swing2 = upperArms.swing2Limit;
            swing2.limit = swing2V;
            upperArms.swing2Limit = swing2;
        }
    }
}

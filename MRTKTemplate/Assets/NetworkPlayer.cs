using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit;

public class NetworkPlayer : MonoBehaviour
{
    public Transform head;
    public Transform leftHand;
    public Transform rightHand;

    private PhotonView photonView;

    private GameObject headRig;
    private MixedRealityPose leftRig;
    private MixedRealityPose rightRig;

    void Start()
    {
        photonView = GetComponent<PhotonView>();
        headRig = GameObject.FindWithTag("MainCamera");
        if (photonView.IsMine)
        {
            foreach (var item in GetComponentsInChildren<Renderer>())
            {
                item.enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(photonView.IsMine)
        {
            head.position = headRig.transform.position;
            head.rotation = headRig.transform.rotation;
            if (HandJointUtils.TryGetJointPose(TrackedHandJoint.Palm, Handedness.Right, out rightRig))
            {
                rightHand.transform.position = rightRig.Position;
                rightHand.transform.rotation = rightRig.Rotation;
            }
            if (HandJointUtils.TryGetJointPose(TrackedHandJoint.Palm, Handedness.Left, out leftRig))
            {
                leftHand.transform.position = leftRig.Position;
                leftHand.transform.rotation = leftRig.Rotation;
            }
        }
    }
}

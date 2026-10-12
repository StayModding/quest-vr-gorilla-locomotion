using UnityEngine;
using UnityEngine.XR;

public class VRControllerGrabber : MonoBehaviour
{
    [SerializeField] private XRNode controllerNode = XRNode.RightHand;
    [SerializeField] private float grabRadius = 0.18f;
    [SerializeField] private LayerMask grabLayerMask;

    private bool isGrabbing;
    private Rigidbody grabbedBody;
    private ConfigurableJoint joint;

    private void Update()
    {
        if (ReadTriggerState(controllerNode) && !isGrabbing)
        {
            TryGrabNearestObject();
        }

        if (!ReadTriggerState(controllerNode) && isGrabbing)
        {
            ReleaseObject();
        }
    }

    private void TryGrabNearestObject()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, grabRadius, grabLayerMask);
        if (colliders.Length == 0)
            return;

        Collider nearest = GetNearestCollider(colliders);
        if (nearest == null)
            return;

        if (nearest.attachedRigidbody != null)
        {
            grabbedBody = nearest.attachedRigidbody;
            isGrabbing = true;

            if (joint == null)
            {
                joint = gameObject.AddComponent<ConfigurableJoint>();
            }

            joint.connectedBody = grabbedBody;
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = Vector3.zero;
            joint.xMotion = ConfigurableJointMotion.Locked;
            joint.yMotion = ConfigurableJointMotion.Locked;
            joint.zMotion = ConfigurableJointMotion.Locked;
            joint.angularXMotion = ConfigurableJointMotion.Locked;
            joint.angularYMotion = ConfigurableJointMotion.Locked;
            joint.angularZMotion = ConfigurableJointMotion.Locked;
        }
    }

    private void ReleaseObject()
    {
        isGrabbing = false;
        if (joint != null)
        {
            joint.connectedBody = null;
            Destroy(joint);
            joint = null;
        }

        grabbedBody = null;
    }

    private Collider GetNearestCollider(Collider[] candidates)
    {
        Collider nearest = null;
        float nearestDistanceSquared = Mathf.Infinity;

        foreach (Collider candidate in candidates)
        {
            float distanceSquared = (candidate.transform.position - transform.position).sqrMagnitude;
            if (distanceSquared < nearestDistanceSquared)
            {
                nearest = candidate;
                nearestDistanceSquared = distanceSquared;
            }
        }

        return nearest;
    }

    private bool ReadTriggerState(XRNode node)
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(node);
        if (device.isValid)
        {
            if (device.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerPressed))
                return triggerPressed;
        }

        return Input.GetMouseButton(0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour
{
    [SerializeField] private PortalCamera otherPortal;
    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private Transform virtualPortal;
    [SerializeField] private Transform portalCamera;

    [SerializeField] private float nearClipOffset;

    private void Update()
    {
        Vector3 l_position = virtualPortal.InverseTransformPoint(playerCameraTransform.position);
        otherPortal.portalCamera.transform.position = otherPortal.transform.TransformPoint(l_position);

        float playerDist = (playerCameraTransform.position - transform.position).magnitude;

        otherPortal.portalCamera.GetComponent<Camera>().nearClipPlane = playerDist + nearClipOffset;

        otherPortal.portalCamera.GetComponent<Camera>().fieldOfView = 120 / playerDist;

        Vector3 local_direction = virtualPortal.InverseTransformDirection(playerCameraTransform.forward);
        otherPortal.portalCamera.forward = otherPortal.transform.TransformDirection(local_direction);
        otherPortal.portalCamera.GetComponent<Camera>().nearClipPlane =
            (transform.position - playerCameraTransform.position).magnitude + nearClipOffset;
    }
}

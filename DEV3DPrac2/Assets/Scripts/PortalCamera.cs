using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour
{
    [SerializeField] private PortalCamera otherPortal;
    [SerializeField] Camera playerCamera;
    [SerializeField] private Transform virtualPortal;
    [SerializeField] private Transform portalCamera;

    [SerializeField]
    private void Update()
    {
        Vector3 l_position = virtualPortal.InverseTransformPoint(playerCamera.transform.position);
        otherPortal.portalCamera.transform.position = otherPortal.transform.TransformPoint(l_position);

        Vector3 l_direction = virtualPortal.InverseTransformPoint(playerCamera.transform.forward);
        otherPortal.portalCamera.transform.forward = otherPortal.transform.TransformDirection(l_direction);

    }
}
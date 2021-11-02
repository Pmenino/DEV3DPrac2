using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalGun : MonoBehaviour
{
    [SerializeField] private string portalEnabledTag;
    [SerializeField] private GameObject previewPortal;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float maxDistance;
    [SerializeField] private LayerMask portalLayerMask;
    private bool isActive;
    [SerializeField] private GameObject bluePortal;
    [SerializeField] private GameObject orangePortal;


    void Update()
    {
        if(Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            isActive = movePreviewPortal();
        }

        previewPortal.SetActive(isActive);

        if (Input.GetMouseButtonUp(0) && isActive)
        {
            bluePortal.SetActive(true);
            bluePortal.transform.position = previewPortal.transform.position;
            bluePortal.transform.forward = previewPortal.transform.forward;
            previewPortal.SetActive(false);
        }
        if (Input.GetMouseButtonUp(1) && isActive)
        {
            orangePortal.SetActive(true);
            orangePortal.transform.position = previewPortal.transform.position;
            orangePortal.transform.forward = previewPortal.transform.forward;
            previewPortal.SetActive(false);
        }
    }

    bool movePreviewPortal()
    {
        Ray r = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
        if(Physics.Raycast(r, out RaycastHit hitInfo, maxDistance, portalLayerMask))
        {
            if (hitInfo.transform.gameObject.CompareTag(portalEnabledTag))
            {
                previewPortal.transform.position = hitInfo.point;
                previewPortal.transform.forward = hitInfo.normal;
                return previewPortal.GetComponent<PreviewPortal>().isValidPosition(mainCamera);
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }

    }
}

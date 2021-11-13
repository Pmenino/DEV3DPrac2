using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    [SerializeField] private float resizeSpeed;
    [SerializeField] private Animation anim;
    [SerializeField] private UnityEvent<string> updateUI;
    private float portalReference;

    [Header("Audio")]
    public AudioClip shootPortal;

    public AudioSource audioSource;

    private void Start()
    {
        portalReference = previewPortal.transform.localScale.x;
    }

    void Update()
    {
        if(Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            isActive = movePreviewPortal();
        }

        previewPortal.SetActive(isActive);


        if(Input.GetAxis("Mouse ScrollWheel") > 0f && isActive && portalReference * 2 > previewPortal.transform.localScale.x)
        {
            previewPortal.transform.localScale = Vector3.Lerp(previewPortal.transform.localScale, 
                new Vector3(previewPortal.transform.localScale.x * 2, previewPortal.transform.localScale.y * 2, previewPortal.transform.localScale.z), Time.deltaTime * resizeSpeed);
            
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f && isActive && portalReference * 0.5 < previewPortal.transform.localScale.x)
        {
            previewPortal.transform.localScale = Vector3.Lerp(previewPortal.transform.localScale,
                new Vector3(previewPortal.transform.localScale.x / 2, previewPortal.transform.localScale.y / 2, previewPortal.transform.localScale.z), Time.deltaTime * resizeSpeed);
        }

        if (Input.GetMouseButtonUp(0) && isActive)
        {
            bluePortal.SetActive(true);
            bluePortal.transform.position = previewPortal.transform.position;
            bluePortal.transform.forward = previewPortal.transform.forward;
            bluePortal.transform.localScale = previewPortal.transform.localScale;
            updateUI.Invoke("blue");
            previewPortal.SetActive(false);
            previewPortal.transform.localScale = new Vector3(0.3512299f, 0.6572009f, 0.59f);
            previewPortal.transform.localPosition = new Vector3(1000f,0f,0f);

            audioSource.GetComponent<AudioSource>().clip = shootPortal;
            audioSource.Play();
        }
        if (Input.GetMouseButtonUp(1) && isActive)
        {
            orangePortal.SetActive(true);
            orangePortal.transform.position = previewPortal.transform.position;
            orangePortal.transform.forward = previewPortal.transform.forward;
            orangePortal.transform.localScale = previewPortal.transform.localScale;
            updateUI.Invoke("orange");
            previewPortal.SetActive(false);
            previewPortal.transform.localScale = new Vector3(0.3512299f, 0.6572009f, 0.59f);
            previewPortal.transform.localPosition = new Vector3(1000f, 0f, 0f);

            audioSource.GetComponent<AudioSource>().clip = shootPortal;
            audioSource.Play();
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

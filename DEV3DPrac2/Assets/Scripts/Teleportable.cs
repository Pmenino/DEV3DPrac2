using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportable : MonoBehaviour
{
    [SerializeField] private float teleportOffset;

    private bool isTeleporting = false;
    private Vector3 teleportPosition;
    private Vector3 teleportForward;
    [SerializeField] Rigidbody cubeRb;
    [SerializeField] float throwForce;

    [Header("Audio")]
    public AudioClip teleport;

    public AudioSource audioSource;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out PortalCamera portal) && !isTeleporting)
        {
            isTeleporting = true;
            Vector3 l_position = portal.virtualPortal.InverseTransformPoint(transform.position);
            Vector3 l_direction = portal.virtualPortal.InverseTransformDirection(transform.forward);

            teleportForward = portal.otherPortal.transform.TransformDirection(l_direction);
            teleportPosition = portal.otherPortal.transform.TransformPoint(l_position) + portal.otherPortal.transform.forward * teleportOffset;

            audioSource.GetComponent<AudioSource>().clip = teleport;
            audioSource.Play();
        }

        if(TryGetComponent(out Rigidbody rb) && other.gameObject.tag == "portal")
        {
            Vector3 l_Velocity =
            portal.virtualPortal.transform.InverseTransformDirection(rb.velocity);
            rb.velocity =
            portal.otherPortal.transform.TransformDirection(l_Velocity);
            transform.localScale *=
            (portal.otherPortal.transform.localScale.x / portal.transform.localScale.x);
        }
    }

    private void LateUpdate()
    {
        if(isTeleporting)
        {
            isTeleporting = false;
            if(TryGetComponent(out CharacterController charController))
            {
                charController.enabled = false;
            }
            transform.position = teleportPosition;
            transform.forward = teleportForward;
            if(TryGetComponent(out FPSController fpsController))
            {
                fpsController.recalculateYawAndPitch();
            }
            if (TryGetComponent(out CharacterController characterController))
            {
                characterController.enabled = true;
            }
        }
    }
}

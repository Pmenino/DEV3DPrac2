using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [SerializeField] private GameObject lockedDoor;

    [Header("Audio")]
    public AudioClip doorOpen;
    public AudioClip doorClose;

    public AudioSource audioSource;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Cube")
        {
            lockedDoor.GetComponent<Animation>().PlayQueued("Door_open");
            audioSource.GetComponent<AudioSource>().clip = doorOpen;
            audioSource.Play();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Cube")
        {
            lockedDoor.GetComponent<Animation>().PlayQueued("Door_close");
            audioSource.GetComponent<AudioSource>().clip = doorClose;
            audioSource.Play();
        }
    }
}

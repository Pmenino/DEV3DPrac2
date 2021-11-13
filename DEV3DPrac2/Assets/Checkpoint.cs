using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private GameMaster gameMaster;
    [Header("Audio")]
    public AudioClip checkpointAudio;

    public AudioSource audioSource;
    bool firstTime = true;

    private void Start()
    {
        gameMaster = GameObject.Find("GameMaster").GetComponent<GameMaster>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<FPSController>())
        {
            gameMaster.UpdateWaypoint(gameObject.transform);
            if (firstTime)
            {
                audioSource.clip = checkpointAudio;
                audioSource.Play();
                firstTime = false;
            }
        }
    }
}
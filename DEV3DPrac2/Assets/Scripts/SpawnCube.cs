using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCube : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject cube;

    [Header("Audio")]
    public AudioClip spawnCube;

    public AudioSource audioSource;
    private void OnTriggerEnter(Collider other)
    {
        Instantiate(cube, spawnPoint);
        audioSource.GetComponent<AudioSource>().clip = spawnCube;
        audioSource.Play();

    }
}


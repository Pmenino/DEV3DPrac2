using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCube : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject cube;
    private void OnTriggerEnter(Collider other)
    {
        Instantiate(cube, spawnPoint);
    }
}


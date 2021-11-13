using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    [SerializeField] private Transform waypoint;
    public void UpdateWaypoint(Transform newWaypoint)
    {
        waypoint = newWaypoint;
    }

    public Transform ReturnWaypoint()
    {
        return waypoint;
    }
}

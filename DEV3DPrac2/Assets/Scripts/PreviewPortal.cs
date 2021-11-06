using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewPortal : MonoBehaviour
{
    [SerializeField] private List<Transform> controlPoints;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private string scenarioTag;
    [SerializeField] private float maxNormalAngle;
    [SerializeField] private float maxDistance;
   public bool isValidPosition(Camera mainCamera)
    {
        foreach(var point in controlPoints)
        {
            if(Physics.Raycast(mainCamera.transform.position, point.position - mainCamera.transform.position, out RaycastHit hitInfo, float.MaxValue, layerMask)){

                if (!hitInfo.transform.gameObject.CompareTag(scenarioTag))
                {
                    return false;
                }
                if(Vector3.Angle(hitInfo.normal, point.forward) > maxNormalAngle)
                {
                    return false;
                }
                if((hitInfo.point - point.position).magnitude > maxDistance)
                {
                    return false;
                }
            }
            else{
                return false;
            }

            //if !portal_enabled -> return false
            //if angle(normal, lastNormal) > angleThreshold -> return false;
            //if fist(controlPoint, rayCastPoint) > distThreshold -> return false;
        }
        return true;
        
    }
}

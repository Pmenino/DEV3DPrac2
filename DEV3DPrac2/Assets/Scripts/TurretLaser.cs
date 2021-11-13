using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretLaser : MonoBehaviour
{
    [SerializeField] LineRenderer laserRenderer;
    [SerializeField] private bool isActive = true;
    [SerializeField] private float maxLaserDistance;
    [SerializeField] private LayerMask layermask;
    public void updateState(bool isActive)
    {
        laserRenderer.enabled = isActive;
        this.isActive = isActive;
    }
    private void Update()
    {
        if (isActive)
        {
            Ray r = new Ray(laserRenderer.transform.position, laserRenderer.transform.forward);
            //raycast
            if(Physics.Raycast(r, out RaycastHit hitInfo, maxLaserDistance, layermask))
            {
                laserRenderer.SetPosition(1, Vector3.forward * hitInfo.distance);
                hitInfo.transform.gameObject.TryGetComponent(out TurretLaser tl);

                if (hitInfo.transform.gameObject.TryGetComponent(out HealthSystem hs))
                {
                    hs.kill();
                }
                if(tl)
                {
                    tl.updateState(true);
                }
                
            }
            else
            {
                laserRenderer.SetPosition(1, Vector3.forward * maxLaserDistance);
            }
            //if hit
            //laserrenderer 2nd point to hit disttance
            //else
            //laser renderer 2nd point to infinite distance
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretLaser : MonoBehaviour
{
    [SerializeField] LineRenderer laserRenderer;
    [SerializeField] private bool isActive;
    [SerializeField] private float maxLaserDistance;
    [SerializeField] private LayerMask layermask;
    private TurretLaser lastestTurret;

    private void Start()
    {
        laserRenderer.enabled = isActive;
    }
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

                if(lastestTurret != null && tl == null)
                {
                    lastestTurret.updateState(false);
                }
                if (hitInfo.transform.gameObject.TryGetComponent(out HealthSystem hs))
                {
                    hs.kill();
                }
                if(tl)
                {
                    lastestTurret = tl;
                    tl.updateState(true);
                }
                
                
            }
            else
            {
                laserRenderer.SetPosition(1, Vector3.forward * maxLaserDistance);
            }

        }
    }
}

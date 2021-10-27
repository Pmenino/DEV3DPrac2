using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Portal linkedPortal;
    [SerializeField] private MeshRenderer screen;
    Camera playerCam;
    Camera portalCam;
    RenderTexture viewTexture;

    // Start is called before the first frame update
    void Start()
    {
        playerCam = Camera.main;
        portalCam = GetComponentInChildren<Camera>();
        portalCam.enabled = false;
    }

    void CreateViewTexture()
    {
        if (viewTexture == null || viewTexture.width != Screen.width || viewTexture.height != Screen.height)
        {
            if (viewTexture != null)
            {
                viewTexture.Release();
            }
            viewTexture = new RenderTexture(Screen.width, Screen.height, 0);
            // Render the view from the portal camera to the view texture
            portalCam.targetTexture = viewTexture;
            // Display the view texture on the screen of the linked portal
            linkedPortal.screen.material.SetTexture("_MainTex", viewTexture);
        }
    }

    public void Render()
    {
        screen.enabled = false;
        CreateViewTexture();

        var m = transform.localToWorldMatrix * linkedPortal.transform.worldToLocalMatrix * playerCam.transform.localToWorldMatrix;
        portalCam.transform.SetPositionAndRotation(m.GetColumn(3), m.rotation);

        portalCam.Render();

        screen.enabled = true;
    }

    
}

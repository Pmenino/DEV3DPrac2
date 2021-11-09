using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private GameObject blue;
    [SerializeField] private GameObject orange;

    public void enablePortal(string mode)
    {
        switch (mode)
        {
            case "blue": blue.SetActive(true);
                break;
            case "orange": orange.SetActive(true);
                break;
            case "none": orange.SetActive(false);
                blue.SetActive(false);
                break;
        }
    }

}

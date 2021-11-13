using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Cube" || other.gameObject.tag == "Turret")
        {
            Destroy(other.gameObject);
        }
        try { other.GetComponent<HealthSystem>().kill(); }
        catch
        {

        }
    }
}

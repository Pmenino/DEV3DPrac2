using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField]
    private float health = 100;
    // Start is called before the first frame update
    public void takeDamage(float value)
    {
        health -= value;
        if (health <= 0.0f)
        {
            kill();
        }
    }

    public void kill()
    {
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float health = 100;
    [SerializeField] private GameOverManager gameOverManager;

    [Header("Audio")]
    public AudioClip death;

    public AudioSource audioSource;
    public bool dead = false;
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
        if (!dead)
        {
            dead = true;
            Respawn();
            audioSource.clip = death;
            audioSource.Play();
        }
    }

    private void Respawn()
    {
        gameOverManager.ShowGameOverScreen();
    }

    public void addHealth(int healthAdd)
    {
        health += healthAdd;
        if (health > 100)
        {
            health = 100;
        }
    }
}

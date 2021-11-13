using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] Canvas gameOverScreen;
    [SerializeField] GameObject player;

    private GameMaster gameMaster;
    private void Start()
    {
        gameMaster = GameObject.Find("GameMaster").GetComponent<GameMaster>();
    }
    public void ShowGameOverScreen()
    {
        gameOverScreen.enabled = true;
        player.GetComponent<PortalGun>().enabled = false;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
    public void Respawn()
    {
        player.SetActive(false);
        Transform waypoint = gameMaster.ReturnWaypoint();
        player.transform.position = new Vector3(waypoint.transform.position.x, waypoint.transform.position.y, waypoint.transform.position.z);
        gameOverScreen.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        player.SetActive(true);
        player.GetComponent<HealthSystem>().addHealth(1000);
        player.GetComponent<PortalGun>().enabled = true;

    }
    public void CloseGame()
    {
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private float deathLength = 3f;
    [SerializeField] [Range(0,1)] private float deathSlowAmount = 0.2f;
    private static Vector3 currentRespawnPoint = new Vector3(0, 0, 0);

    private void Start()
    {
        //Set Player Spawn
        if (currentRespawnPoint == Vector3.zero)
            currentRespawnPoint = transform.position;
        else
            transform.position = currentRespawnPoint;
    }

    public void Dead()
    {
        StartCoroutine("DeadRoutine");
    }

    private IEnumerator DeadRoutine()
    {
        //Slow while dieing time
        Time.timeScale = deathSlowAmount;

        yield return new WaitForSeconds(deathLength * deathSlowAmount);

        Time.timeScale = 1.0f;
        //Temp Restart Scene (replace this with the proper scene manager and with a HUD element)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void setRespawinPoint(Vector3 pPoint)
    {
        //Change Where the player spawns after dieing
        currentRespawnPoint = pPoint;
    }
}

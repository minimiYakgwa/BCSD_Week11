using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject obstaclePrefab;
    private PlayerController playerControllerscript;
    public Vector3 spawnPos = new Vector3(25, 0, 0);

    private float startDelay = 2;
    private float repeatRate = 2;
    private void Start()
    {
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
        playerControllerscript =
            GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void SpawnObstacle()
    {   
        if (playerControllerscript.gameOver == false)
            Instantiate(obstaclePrefab, spawnPos, obstaclePrefab.transform.rotation);
    }

}

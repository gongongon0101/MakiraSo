using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameController : MonoBehaviour {

    private GameObject player;
    private GameObject start;
    private GameObject gameOver;
    private GameObject spawnPoint;
    private GameObject floor;
    public static bool isPlaying;

    void Awake()
    {
        player = GameObject.Find("Player");
        start = GameObject.Find("Start");
        gameOver = GameObject.Find("GameOver");
        spawnPoint = GameObject.Find("SpawnPoint");
        floor = GameObject.Find("Floor");
    }

    void Start()
    {
        gameOver.SetActive(false);
        floor.GetComponent<Floor>().enabled = false;
        isPlaying = false;
    }

    void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            SceneManager.LoadScene("StageSample");
        }
    }

    public void GameStart()
    {
        start.SetActive(false);
        isPlaying = true;
        floor.GetComponent<Floor>().enabled = true;
        spawnPoint.SendMessage("StartSpawn");
    }

    public void GameOver()
    {
        if (isPlaying == true)
        {
            gameOver.SetActive(true);
            isPlaying = false;
            player.GetComponent<Player>().enabled = false;
            spawnPoint.SendMessage("StopSpawn");
            floor.SendMessage("Stop");
        }
    }
}

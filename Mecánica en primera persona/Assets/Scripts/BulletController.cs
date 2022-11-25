using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletController : MonoBehaviour
{
    PlayerController playerController;
    public GameObject player;

    public GameOverController gameOverController;
    public GameObject gameOver;

    public void Awake()
    {
        player = GameObject.FindWithTag("Player");
        playerController = player.GetComponent<PlayerController>();

        gameOver = GameObject.FindWithTag("Game Over");
        gameOverController = gameOver.GetComponent<GameOverController>();
    }

    public void Start()
    {
        Destroy(gameObject, 5);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            playerController.remainingObstacles--;
            playerController.scoreText.text = "Pilares restantes: " + playerController.remainingObstacles;
        }
        if (playerController.remainingObstacles == 0)
        {
            gameOverController.ShowGameOver("¡Lo lograste!");
        }
    }
}
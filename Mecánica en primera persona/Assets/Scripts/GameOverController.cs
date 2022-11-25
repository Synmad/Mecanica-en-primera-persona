using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
    public Text gameoverText;
    PlayerController playerController;
    public GameObject player;
    [SerializeField] private GameObject gameoverpanel;

    public void Awake()
    {
        gameoverText = gameoverpanel.GetComponent<Text>();
        player = GameObject.FindWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
    }
    public void ShowGameOver(string victoryOrDefeat)
    {
        {
            gameoverText.gameObject.SetActive(true);
            gameoverText.text = victoryOrDefeat;
        }
    }
    
}

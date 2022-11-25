using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float mouseSensitivity = 3f;
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float mass = 1f;
    [SerializeField] Transform camaraTransform;

    CharacterController characterController;
    Vector3 velocity;
    Vector2 look;

    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    [SerializeField] float bulletSpeed = 10;

    public int remainingObstacles = 8;
    public Text scoreText;

    public GameOverController gameOverController;
    public GameObject gameOver;

    bool isAlive = true;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();

        gameOver = GameObject.FindWithTag("Game Over");
        gameOverController = gameOver.GetComponent<GameOverController>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        UpdateLook();
        UpdateMovement();
        UpdateGravity();
        UpdateShoot();
    }

    void UpdateGravity()
    {
        var gravity = Physics.gravity * mass * Time.deltaTime;
        velocity.y = characterController.isGrounded ? -1f : velocity.y + gravity.y;
    }

    void UpdateLook()
    {
        look.x += Input.GetAxis("Mouse X") * mouseSensitivity;
        look.y += Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Prevents vertical camera movement from breaking the player's neck
        look.y = Mathf.Clamp(look.y, -89f, 89f);

        camaraTransform.localRotation = Quaternion.Euler(-look.y, 0, 0);
        transform.localRotation = Quaternion.Euler(0, look.x, 0);
    }

    void UpdateMovement()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");

        var input = new Vector3();
        input += transform.forward * y;
        input += transform.right * x;

        // Prevents diagonal movement from being faster
        input = Vector3.ClampMagnitude(input, 1f);

        characterController.Move((input * movementSpeed + velocity) * Time.deltaTime);
    }

    void UpdateShoot()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isAlive)
        {
            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOverController.ShowGameOver("¡Perdiste!");
            Time.timeScale = 0f;
        }
    }
}
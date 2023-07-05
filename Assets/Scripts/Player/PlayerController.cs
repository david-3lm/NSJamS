using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] GameObject aimPrefab;
    [SerializeField] GameObject bulletPrefab;
    
    private Rigidbody2D playerRb;

    private PlayerInput playerInput;
    InputAction movementAction;
    InputAction aimAction;
    InputAction shootAction;

    private Vector2 aimPos;

    private AudioSource playerAudioSource;
    
    //Camera
    private Camera cam;

    private void OnEnable()
    {
        movementAction.performed += MovePlayer;
        movementAction.canceled += StopPLayer;
        shootAction.started += Shoot;
        
    }



    private void OnDisable()
    {
        movementAction.performed -= MovePlayer;
        movementAction.canceled -= StopPLayer;
        shootAction.started -= Shoot;
    }



    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();

        movementAction = playerInput.actions["Movement"];
        aimAction = playerInput.actions["Aim"];
        shootAction = playerInput.actions["Shoot"];

        cam = Camera.main;

        playerAudioSource = GetComponent<AudioSource>();

    }

    private void Update()
    {
        aimPos = cam.ScreenToWorldPoint(aimAction.ReadValue<Vector2>());
        ClampAim();


    }

    private void MovePlayer(InputAction.CallbackContext obj)
    {
        playerRb.velocity = movementAction.ReadValue<Vector2>() * PlayerStats.Instance.moveSpeed;
    }

    private void StopPLayer(InputAction.CallbackContext obj)
    {
        playerRb.velocity = new Vector2(0,0);
    }

    private void Shoot(InputAction.CallbackContext obj)
    {

        GameObject newBullet = Instantiate(bulletPrefab, transform.position, new Quaternion());
        //newBullet.GetComponent<Rigidbody2D>().velocity = (aimPos- playerRb.position)* PlayerStats.Instance.bulletSpeed;
        playerAudioSource.Play();

        newBullet.GetComponent<BulletScript>().Shoot((aimPos - playerRb.position), PlayerStats.Instance.bulletSpeed);
    }


    private void ClampAim()
    {
        Ray r = new Ray(playerRb.position, aimPos-playerRb.position);
        aimPos = r.GetPoint(1f);
        aimPrefab.transform.position = aimPos;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 12)//Enemy Layer
        {
            playerAudioSource.enabled = false;
            GameManager.Instance.EndGame();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 14)
        {
            
            GameManager.Instance.BuffCollected();
            collision.gameObject.SetActive(false);
            Destroy(collision.gameObject);
        }
    }

}

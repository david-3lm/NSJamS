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
        newBullet.GetComponent<Rigidbody2D>().velocity = (aimPos- playerRb.position)* PlayerStats.Instance.bulletSpeed;
    }


    private void ClampAim()
    {
        Ray r = new Ray(playerRb.position, aimPos-playerRb.position);
        aimPos = r.GetPoint(1f);
        aimPrefab.transform.position = aimPos;

    }

}

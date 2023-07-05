using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    //This variables will determinate the speed of the game


    public float gameSpeed = 1f;
    public float moveSpeed = 15f;//5-30;
    public float bulletDamage = 1f;
    public float bulletSpeed = 1f;
    public float spawnFrec = 1f;
    public int arriveTimeEnemy = 5;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        Mathf.Clamp(gameSpeed, 0.5f, 3);
        Mathf.Clamp(moveSpeed, 5f, 30);
        Mathf.Clamp(bulletSpeed, 0.5f, 3);
        Mathf.Clamp(spawnFrec, 0.1f, 3);
        Mathf.Clamp(arriveTimeEnemy, 2f, 5);
    }


}

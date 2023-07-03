using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    //This variables will determinate the speed of the game


    public float gameSpeed = 1f;
    public float moveSpeed = 10f;//5-30;
    public float bulletDamage = 1f;
    public float bulletSpeed = 1f;


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


}

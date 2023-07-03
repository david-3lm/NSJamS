using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    [SerializeField]GameObject player;
    float arriveTime;
    public bool isAlive;
    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        arriveTime = 5;
        isAlive = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            transform.up = player.transform.position - transform.position;
            transform.DOMove(player.transform.position, arriveTime -= Time.deltaTime);
        }


    }
}

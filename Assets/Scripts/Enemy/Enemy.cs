using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static Unity.Burst.Intrinsics.X86;

public class Enemy : MonoBehaviour
{
    [SerializeField]GameObject player;
    float arriveTime;
    public bool isAlive;
    Transform initialPos;
    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        arriveTime = PlayerStats.Instance.arriveTimeEnemy;
        isAlive = true;


    }
    private void Start()
    {

        int aux = Random.Range(0, GameManager.Instance.spawnPoints.Count);
        initialPos = GameManager.Instance.spawnPoints[aux].transform;
        transform.position = initialPos.position;
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

    


    private void OnCollisionEnter2D(Collision2D collision)
    {

        isAlive = false;
        arriveTime = PlayerStats.Instance.arriveTimeEnemy;
        DOTween.Clear();
        transform.position = initialPos.position;
        gameObject.SetActive(false);
    }
}

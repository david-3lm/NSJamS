using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Mathematics;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] GameObject enemyPrefab;
    [SerializeField] int poolSize = 32;
    List<GameObject> enemyPool;

    float points;
    [SerializeField] TextMeshProUGUI pointsTxt;


    public bool isPlaying;
    [Header("Spawns")]
    [SerializeField] public List<Transform> spawnPoints;

    private void Awake()
    {
        #region Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        #endregion

        enemyPool= new List<GameObject>();
    }

    private void Start()
    {
        InitializeEnemyPool(poolSize);
        GameObject go = SpawnEnemy();
        if (go != null) go.SetActive(true);
        isPlaying = true;
        StartCoroutine(Spawner());
        points = 0;
    }
    private void Update()
    {
        PlayerStats.Instance.spawnFrec -= Time.deltaTime/100;
        points += Time.deltaTime;
        pointsTxt.text = Mathf.FloorToInt(points).ToString();
    }

    private void InitializeEnemyPool(int poolSize)
    {
        for (int i = 0; i < poolSize; i++)
        {
            enemyPool.Add(Instantiate(enemyPrefab));
            enemyPool[i].SetActive(false);
        }
    }

    public GameObject SpawnEnemy()
    {

        for (int i = 0; i < poolSize; i++)
        {

            if (enemyPool[i].gameObject == null)
            {
                enemyPool[i] = Instantiate(enemyPrefab);

            }
            if (!enemyPool[i].activeInHierarchy)
            {

                enemyPool[i].GetComponent<Enemy>().isAlive = true;
                return enemyPool[i];
            }
        }
        return null;
    }

    IEnumerator Spawner()
    {
        while (isPlaying)
        {
            yield return new WaitForSeconds(PlayerStats.Instance.spawnFrec);
            GameObject go = SpawnEnemy();
            if(go!=null) go.SetActive(true);
        }
        yield return null;
    }
}

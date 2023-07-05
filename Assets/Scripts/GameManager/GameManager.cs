using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Mathematics;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] GameObject enemyPrefab;
    [SerializeField] int poolSize = 32;
    List<GameObject> enemyPool;
    [SerializeField] GameObject buffPrefab;

    float points;
    [Header("Canvas")]
    [SerializeField] TextMeshProUGUI pointsTxt;
    [SerializeField] TextMeshProUGUI pointsTxtEndGame;
    [SerializeField] TextMeshProUGUI buffsTxt;
    [SerializeField] GameObject gameOverUI;


    public bool isPlaying;
    [Header("Spawns")]
    [SerializeField] public List<Transform> spawnPoints;


    private Buff[] buffs;

    private void Awake()
    {
        Time.timeScale = 1f;
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

        buffs = new Buff[] {
            new EnemySpeed(),
            new PlayerSpeed(),
            new BulletSpeed()
        };

        gameOverUI.SetActive(false);
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
        if (isPlaying)
        {
            PlayerStats.Instance.spawnFrec -= Time.deltaTime / 100;
            points += Time.deltaTime;
            pointsTxt.text = Mathf.FloorToInt(points).ToString();
        }
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

    public void SpawnBuff(Transform t)
    {
        Instantiate(buffPrefab,t.position,Quaternion.identity);
    }
    public void BuffCollected()
    {
        int r = UnityEngine.Random.Range(0, buffs.Length);
        buffs[r].Effect();
        StartCoroutine(ShowDescription(buffs[r].Description()));
    }

    IEnumerator ShowDescription(string description)
    {
        buffsTxt.text = description;
        yield return new WaitForSeconds(2f);
        buffsTxt.text = " ";


    }
    public void EndGame()
    {
        isPlaying = false;
        pointsTxt.gameObject.SetActive(false);
        pointsTxtEndGame.text = "You got: "+ Mathf.FloorToInt(points).ToString() + " points";
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ChangeToScene(int i)
    {
        SceneManager.LoadSceneAsync(i);
    } 
}

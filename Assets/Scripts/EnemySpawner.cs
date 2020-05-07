using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private int maxEnemiesToSpawn = 3;

    [SerializeField]
    private float timeBetweenSpawns = 3.5f;

    [SerializeField]
    GameObject enemyPrefab;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        for (int i = 0; i < maxEnemiesToSpawn; i++)
        {
            yield return new WaitForSeconds(timeBetweenSpawns);
            Instantiate(enemyPrefab, transform.position, transform.rotation);
            EnemiesManager.Instance.AddEnemy();
        }
        GetComponent<SpriteRenderer>().color = Color.red;
    }
}

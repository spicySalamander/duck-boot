using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDucks : MonoBehaviour
{
    public GameObject duckPrefab;
    private Vector2 screenBounds;

    // Spawns once per second
    public float spawnTime = 1.0f;

    private void Start()
    {
        StartCoroutine(duckWave());
    }

    private void spawnEnemy()
    {
        GameObject a = Instantiate(duckPrefab) as GameObject;
        a.transform.position = new Vector2(screenBounds.x, Random.Range(-screenBounds.y, screenBounds.y));
    }

    IEnumerator duckWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);
            spawnEnemy();
        }
       
    }
}


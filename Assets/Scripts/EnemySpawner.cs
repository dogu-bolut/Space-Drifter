using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject Pill;
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startingWave = 0;
    Level level;

    void Start()
    {
        StartCoroutine(SpawnAllWaves());
        level = FindObjectOfType<Level>();
    }

    private IEnumerator SpawnAllWaves()
    {
        for (int waveIndex = startingWave; waveIndex <= waveConfigs.Count; waveIndex++)
        {
            if (waveIndex == waveConfigs.Count)
            {
                level.LoadSuccess();
            }
            var currentWave = waveConfigs[waveIndex];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
            if (waveIndex % 5 == 0)
            {
                if (waveIndex != 0)
                {
                    FindObjectOfType<Player>().health += FindObjectOfType<Player>().toAdd;
                    Debug.Log("processed");
                    Pill.gameObject.SetActive(true);
                    Invoke("HidePill", 3);
                }
            }
        }
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        for (int enemyCount = 0; enemyCount < waveConfig.GetNumberOfEnemies(); enemyCount++)
        {
            var newEnemy = Instantiate(
            waveConfig.GetEnemyPrefab(),
            waveConfig.GetWaypoints()[0].transform.position,
            Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }

    }
    private void HidePill()
    {
        Pill.gameObject.SetActive(false);
    }
}

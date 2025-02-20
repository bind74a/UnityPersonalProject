using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private Coroutine waveRoutine;//몬스터 웨이브

    [SerializeField] private List<GameObject> enemyPrefabs;

    [SerializeField] List<Rect> spawnAreas;
    [SerializeField] private Color gizmoColor = new Color(1, 0, 0, 0.3f);
    private List<EnemyControl> activeEnemies = new List<EnemyControl>();

    private bool enemySpawnComplite;

    [SerializeField] private float timeBetweenSpawns = 0.2f;
    [SerializeField] private float timeBetweenWaves = 1f;

    GameManager gameManager;

    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public void StartWave(int waveCount)
    {
        if (waveCount < 0)
        {
            gameManager.EndOfWave();
            return;
        }
        if (waveRoutine != null)
        {
            StopCoroutine(waveRoutine);
        }
        waveRoutine = StartCoroutine(SpawnWave(waveCount));
    }

    public void StopWave()
    {
        StopAllCoroutines();
    }

    private IEnumerator SpawnWave(int waveCount)
    {
        enemySpawnComplite = false;
        yield return new WaitForSeconds(timeBetweenWaves);

        for (int i = 0; i < waveCount; i++)
        {
            yield return new WaitForSeconds(timeBetweenSpawns);
            SpawnRandomEnemy();
        }

        enemySpawnComplite = true;
    }

    private void SpawnRandomEnemy()
    {
        if (enemyPrefabs.Count == 0 || spawnAreas.Count == 0)
        {
            Debug.LogWarning("아직 설정 되지않음");
            return;
        }

        GameObject randoPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
        
        //스폰지역 랜덤 설정값
        Rect randomArea = spawnAreas[Random.Range(0, spawnAreas.Count)];
        //랜덤 소환 위치값
        Vector2 randomPosition = new Vector2(
            Random.Range(randomArea.xMin, randomArea.xMax),
            Random.Range(randomArea.yMin, randomArea.yMax));

        //몬스터 생성
        GameObject spawnEnemy = Instantiate(randoPrefab, new Vector3(randomPosition.x, randomPosition.y), Quaternion.identity);
        EnemyControl enemyControl = spawnEnemy.GetComponent<EnemyControl>();
        enemyControl.Init(this, gameManager.topDownPlayer.transform);


        activeEnemies.Add(enemyControl);
    }

    private void OnDrawGizmosSelected()
    {
        if (spawnAreas == null) return;

        Gizmos.color = gizmoColor;
        foreach(var area in spawnAreas)
        {
            Vector3 center = new Vector3(area.x + area.width / 2, area.y + area.height / 2);
            Vector3 size = new Vector3(area.width, area.height);

            Gizmos.DrawCube(center, size);

        }
    }

    public void ReMoveEnemyOnDeath(EnemyControl enemy)
    {
        activeEnemies.Remove(enemy);
        if(enemySpawnComplite && activeEnemies.Count == 0)
        {
            gameManager.EndOfWave();
        }
    }
}

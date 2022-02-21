using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;

    [SerializeField]
    private GameObject _tripleShotPowerPrefab;
    [SerializeField]
    private GameObject _speedBoostPowerPrefab;
    [SerializeField]
    private GameObject _shieldPowerPrefab;
    [SerializeField]
    private GameObject _powerUpContainer;
    private GameObject[] powerArr = new GameObject[3];


    private bool _stopSpawning = false;
    private IEnumerator coroutine;


    // Start is called before the first frame update
    void Start()
    {
        powerArr[0] = _tripleShotPowerPrefab;
        powerArr[1] = _speedBoostPowerPrefab;
        powerArr[2] = _shieldPowerPrefab;

        coroutine = spawnEnemyRoutine(2.0f);
        StartCoroutine(coroutine);
        StartCoroutine(spawnPowerRoutine(10));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private  IEnumerator spawnEnemyRoutine(float waitTime)
    {
        while (!_stopSpawning)            
        {
            GameObject newEnemy = Instantiate(_enemyPrefab, new Vector3(Random.Range(-9.8f, 9.8f), 6.3f, 0f), Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(waitTime);
        }
            
    }

    private IEnumerator spawnPowerRoutine(float waitTIme)
    {
        while (!_stopSpawning)
        {
            GameObject newPower = Instantiate(powerArr[Random.Range(0,3)], new Vector3(Random.Range(-9.8f, 9.8f), 6.3f, 0f), Quaternion.identity);
            newPower.transform.parent = _powerUpContainer.transform;
            yield return new WaitForSeconds(waitTIme);
        }
    }

    public void onPlayerDeath()
    {
        _stopSpawning = true;
    }
}

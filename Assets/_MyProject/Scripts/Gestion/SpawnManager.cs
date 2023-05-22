using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField] private GameObject[] _platformPrefabs = default;
    [SerializeField] private GameObject _wokesPrefabs = default;
    [SerializeField] private GameObject _TrashPrefabs = default;

    private GestionMusiqueFond _musiqueFond;


    private bool _stopSpawn = false;
    void Start()
    {
        StartSpawning();
        _musiqueFond = FindObjectOfType<GestionMusiqueFond>();
    }

    private void StartSpawning()
    {
        StartCoroutine(SpawnPlatformCoroutine());
        StartCoroutine(SpawnWokesCoroutine());
        StartCoroutine(SpawnTrashCoroutine());
    }
    void Update()
    {
     
    }


    IEnumerator SpawnPlatformCoroutine()
    {   
        yield return new WaitForSeconds(5f);
        while (!_stopSpawn)
        {
            Vector3 positionSpawn = new Vector3(12f, Random.Range(-2f, 1f), 0f);
            int randomPlatform = Random.Range(0, _platformPrefabs.Length);
            Instantiate(_platformPrefabs[randomPlatform], positionSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(5.0f, 12.0f));
        }
    }

    IEnumerator SpawnWokesCoroutine()
    {
        yield return new WaitForSeconds(2f);
        while (!_stopSpawn)
        {
            Vector3 positionSpawn = new Vector3(12f, -4f);
            Instantiate(_wokesPrefabs, positionSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3.0f, 8.0f));
        }
    }

    IEnumerator SpawnTrashCoroutine()
    {
        yield return new WaitForSeconds(10f);
        while (!_stopSpawn)
        {
            Vector3 positionSpawn = new Vector3(12f, -4f);
            Instantiate(_TrashPrefabs, positionSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(9.0f, 15.0f));
        }
    }


    public void playerDead()
    {
        _musiqueFond.MusiqueOff();
        _stopSpawn = true;
    }
}

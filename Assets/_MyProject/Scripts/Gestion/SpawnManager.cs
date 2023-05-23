using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField] private GameObject[] _platformPrefabs = default;
    [SerializeField] private GameObject _wokesPrefabs = default;
    [SerializeField] private GameObject _TrashPrefabs = default;


    private GestionMusiqueFond _musiqueFond;
    private UIManager _uiManager;
    private GestionScene _gestionScene;

    private bool _stopSpawn = false;
    void Start()
    {
        StartSpawning();
        _uiManager = FindObjectOfType<UIManager>();
        _gestionScene = FindObjectOfType<GestionScene>();
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
        yield return new WaitForSeconds(0.1f);

        while (!_stopSpawn)
        {
            Vector3 positionSpawn = new Vector3(12f, -4f);
            Instantiate(_wokesPrefabs, positionSpawn, Quaternion.identity);
            if(_uiManager.getScore() > 1000)
            {
                yield return new WaitForSeconds(Random.Range(2.0f, 6.0f));
            }
            else if(_uiManager.getScore() > 5000)
            {
                yield return new WaitForSeconds(Random.Range(1.0f, 5.0f));
            }
            else if(_uiManager.getScore() > 10000)
            {
                yield return new WaitForSeconds(Random.Range(1.0f, 3.0f));
            }
            else if (_uiManager.getScore() > 20000)
            {
                yield return new WaitForSeconds(Random.Range(0.1f, 0.2f));
            }
            else
            {
                yield return new WaitForSeconds(Random.Range(3.0f, 8.0f));
            }
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
        _gestionScene.ChangerScene();
        _stopSpawn = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusiqueFond : MonoBehaviour
{
    [SerializeField] private GameObject _BtnMuteON = default;
    [SerializeField] private GameObject _BtnMuteOFF = default;

    private bool isMuted = false; 


    private void Update()
    {
       
    }

    public void MutedButton()
    {
        if (!isMuted)
        {
            _BtnMuteOFF.SetActive(true);
            _BtnMuteON.SetActive(false);
            isMuted = true;
        }
        else
        {
            _BtnMuteOFF.SetActive(false);
            _BtnMuteON.SetActive(true);
            isMuted = false;
        }
    }

    private void Awake()
    {
        int nbMusiquedeFond = FindObjectsOfType<MusiqueFond>().Length;
        if (nbMusiquedeFond > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}

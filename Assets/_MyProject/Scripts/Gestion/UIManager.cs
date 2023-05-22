using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{


    [SerializeField] private TextMeshProUGUI _txtScore = default;
    [SerializeField] private Image _livesDisplayImage = default;
    [SerializeField] private Sprite[] _liveSprites = default;
    [SerializeField] private GameObject _pausePanel = default;

    //[SerializeField] private int _pointageAugmentation = 500;

    private int _score = 0;
    private bool _pauseOn = false;

    private void Start()
    {
        _score = 0;
        _pauseOn = false;
        Time.timeScale = 1;
        ChangeLivesDisplayImage(4);
        UpdateScore();
    }

    private void Update()
    {

        // Permet la gestion du panneau de pause (marche/arr�t)
        if ((Input.GetKeyDown(KeyCode.Escape) && !_pauseOn))
        {
            _pausePanel.SetActive(true);
            Time.timeScale = 0;
            _pauseOn = true;
        }
        else if ((Input.GetKeyDown(KeyCode.Escape) && _pauseOn))
        {
            _pausePanel.SetActive(false);
            Time.timeScale = 1;
            _pauseOn = false;
        }

    }

    // M�thode qui change le pointage sur le UI
    private void UpdateScore()
    {
        _txtScore.text = "Points : " + _score.ToString();
    }

    // M�thodes publiques ==================================================

    public int getScore()
    {
        return _score;
    }
    // M�thode qui permet l'augmentation du score
    public void AjouterScore(int points)
    {
        _score += points;
        UpdateScore();
    }

    // M�thode qui permet de changer l'image des vies restantes en fonction de la vie du joueur
    public void ChangeLivesDisplayImage(int noImage)
    {
        if (noImage < 0)
        {
            noImage = 0;
        }
        _livesDisplayImage.sprite = _liveSprites[noImage];

        // Si le joueur n'a plus de vie on lance la s�quence de fin de partie
        if (noImage == 0)
        {
            PlayerPrefs.SetInt("Score", _score);
            PlayerPrefs.Save();
            StartCoroutine("FinPartie");
        }
    }

    public void ChangeLivesDisplayImage2()
    {
        _livesDisplayImage.sprite = _liveSprites[0];
    }


    IEnumerator FinPartie()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(2);
    }

    // M�thode qui relance la partie apr�s une pause
    public void ResumeGame()
    {
        _pausePanel.SetActive(false);
        Time.timeScale = 1;
        _pauseOn = false;
    }

    public void ChargerDepart()
    {
        SceneManager.LoadScene(1);
    }
}

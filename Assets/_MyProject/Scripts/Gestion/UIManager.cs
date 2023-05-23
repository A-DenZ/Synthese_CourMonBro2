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
    [SerializeField] private TextMeshProUGUI _txtEscapeCine = default;
    [SerializeField] private GameObject _instructionsMenu = default;

    private bool isInstructionsOpen = false;

    private GestionScene _gestionScene;

    //[SerializeField] private int _pointageAugmentation = 500;

    private int _score = 0;
    private bool _pauseOn = false;

    private void Start()
    {
        int indexScene = SceneManager.GetActiveScene().buildIndex;
        _gestionScene = FindObjectOfType<GestionScene>().GetComponent<GestionScene>();
        if (indexScene == 0)
        {
            TxtEscapeCineSequence();
            StartCoroutine(SkipAfterCineCoroutine());
        }
        _score = 0;
        _pauseOn = false;
        Time.timeScale = 1;
        ChangeLivesDisplayImage(4);
        UpdateScore();
    }

    private void Update()
    {
        int indexScene = SceneManager.GetActiveScene().buildIndex;
        if (indexScene == 2)
        {
            // Permet la gestion du panneau de pause (marche/arrêt)
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

        if(indexScene == 0)
        {
            if ((Input.GetKeyDown(KeyCode.Escape)))
            {
                _gestionScene.ChangerScene();
            }
        }



    }

    // Méthode qui change le pointage sur le UI
    private void UpdateScore()
    {
        _txtScore.text = "Points:" + _score.ToString();
    }

    // Méthodes publiques ==================================================

    public int getScore()
    {
        return _score;
    }
    // Méthode qui permet l'augmentation du score
    public void AjouterScore(int points)
    {
        _score += points;
        UpdateScore();
    }

    // Méthode qui permet de changer l'image des vies restantes en fonction de la vie du joueur
    public void ChangeLivesDisplayImage(int noImage)
    {
        if (noImage < 0)
        {
            noImage = 0;
        }
        _livesDisplayImage.sprite = _liveSprites[noImage];

        // Si le joueur n'a plus de vie on lance la séquence de fin de partie
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

    // Méthode qui relance la partie après une pause
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


    private void TxtEscapeCineSequence()
    {
        _txtEscapeCine.gameObject.SetActive(true);
        StartCoroutine(TxtEscapeCineBlinkRoutine());
    }


    IEnumerator TxtEscapeCineBlinkRoutine()
    {
        while (true)
        {
            _txtEscapeCine.gameObject.SetActive(true);
            yield return new WaitForSeconds(1.2f);
            _txtEscapeCine.gameObject.SetActive(false);
            yield return new WaitForSeconds(1.2f);
        }
    }

    IEnumerator SkipAfterCineCoroutine()
    {
        yield return new WaitForSeconds(97f);
        _gestionScene.ChangerScene();
    }

    public void OnOffInstructionsMenu()
    {
        if (!isInstructionsOpen)
        {
            _instructionsMenu.SetActive(true);
            isInstructionsOpen = true;
        }
        else
        {
            _instructionsMenu.SetActive(false);
            isInstructionsOpen = false;
        }
    }

}

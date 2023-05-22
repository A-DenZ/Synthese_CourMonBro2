using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _txtEscapeCine = default;
    [SerializeField] private GameObject _instructionsMenu = default;

    private bool isInstructionsOpen = false ;

    private GestionScene _gestionScene ; 
    // Start is called before the first frame update
    void Start()
    {
        int indexScene = SceneManager.GetActiveScene().buildIndex;
        _gestionScene = FindObjectOfType<GestionScene>().GetComponent<GestionScene>();
        if (indexScene == 0) {
        TxtEscapeCineSequence();
        StartCoroutine(SkipAfterCineCoroutine());
        }

    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Escape)))
        {
            _gestionScene.ChangerScene();
        }

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
            isInstructionsOpen = true ;
        }
        else
        {
            _instructionsMenu.SetActive(false);
            isInstructionsOpen = false;
        }
    }




}

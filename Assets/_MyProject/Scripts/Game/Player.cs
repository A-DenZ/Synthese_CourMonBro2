using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float MovementSpeed = 1;
    public float jumpForce = 800;
    [SerializeField] private AudioClip _jumSound = default;

    [SerializeField] private Collider2D playerCollider;
    [SerializeField] private Collider2D legCollider;

    public float GroundedTolerance = 0.001f;

    [SerializeField] private Transform ceilingCheck;
    private bool isJumping = false;

    public Projectile LaunchableProjectilePrefab;
    public Transform LaunchOffset;
    private float _canFire = -1;
    private float _canFire2 = -1;

    [SerializeField] public UIManager uiManager;


    private Rigidbody2D _rigidbody;

    [SerializeField] private float _delai = 0.5f;
    [SerializeField] private int _viesJoueur = 4;
    //[SerializeField] private GameObject _playerHurt1 = default;
    //[SerializeField] private GameObject _playerHurt2 = default;

    private UIManager _uiManager;
    private Animator _anim;
    private SpawnManager _spawnManager;


    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _uiManager = FindObjectOfType<UIManager>();
        _spawnManager = FindObjectOfType<SpawnManager>();
    }

    private void Update()
    {
        if (_viesJoueur < 1)
        {
            _spawnManager.playerDead();
            Destroy(this.gameObject);
        }
        else
        {
            // Move
            Movement();

            // Jump
            Jump();

            // Bieres
            ThrowBere();

            // Kick
            Kick();

            // Vérification de la position
            if (transform.position.y < -4f)
            {
                _spawnManager.playerDead();
                Destroy(this.gameObject);
                _viesJoueur = 0;
                _uiManager.ChangeLivesDisplayImage2();
            }
        }
    }


    private void Jump()
    {
        if (isJumping)
        {
            _rigidbody.AddForce(new Vector2(0f, jumpForce));
        }
        isJumping = false;

        if (Input.GetButtonDown("Jump") && Mathf.Abs(_rigidbody.velocity.y) < GroundedTolerance)
        {
            AudioSource.PlayClipAtPoint(_jumSound, Camera.main.transform.position, 1f);
            isJumping = true;
        }
    }

    private void Movement()
    {
        var movement = Input.GetAxis("Horizontal");
        transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * MovementSpeed;

        if (!Mathf.Approximately(0, movement))
        {
            transform.rotation = movement < 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
        }
    }

    private void ThrowBere()
    {
        if (Input.GetKey(KeyCode.M) && Time.time > _canFire)
        {
            StartCoroutine(ResetBeerThrowingAnimation());

            _anim.SetBool("BeerThrowing", true);

            _canFire = Time.time + _delai;

            Instantiate(LaunchableProjectilePrefab, LaunchOffset.position, transform.rotation);

            StartCoroutine(ResetBeerThrowingAnimation());

        }

    }

    private void Kick()
    {
        if (Input.GetKeyDown(KeyCode.K) && Time.time > _canFire2)
        {
            _anim.SetBool("Kick", true);
            // Active le collider de la jambe 
            legCollider.enabled = true; 

            _canFire2 = Time.time + _delai;
            StartCoroutine(ResetKickAnimation());
        }
    }

    public void Degats()
    {
        _viesJoueur--;

        //Particule de degat?
        //if (_viesJoueur == 2)
        //{
        //    _playerHurt1.SetActive(true);
        //}
        //else if (_viesJoueur == 1)
        //{
        //    _playerHurt2.SetActive(true);
        //}

        _uiManager.ChangeLivesDisplayImage(_viesJoueur);

        if (_viesJoueur < 1)
        {
            _spawnManager.playerDead();
            Destroy(this.gameObject);
        }
    }

    private IEnumerator ResetBeerThrowingAnimation()
    {
        yield return new WaitForSeconds(.5f); // Attendre .2 secondes
        _anim.SetBool("BeerThrowing", false);
    }

    private IEnumerator ResetKickAnimation()
    {
        yield return new WaitForSeconds(.5f); // Attendre .2 secondes
        _anim.SetBool("Kick", false);
        // Désactive le collider de la jambe 
        legCollider.enabled = false;
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float MovementSpeed = 1;
    public float jumpForce = 800;

    public float GroundedTolerance = 0.001f;


    [SerializeField] private Transform ceilingCheck;
    private bool isJumping = false;

    public Projectile LaunchableProjectilePrefab;
    public Transform LaunchOffset;



    private Rigidbody2D _rigidbody;



    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }



    private void Update()
    {



        //move
        var movement = Input.GetAxis("Horizontal");
        transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * MovementSpeed;



        if (!Mathf.Approximately(0, movement))
        {
            transform.rotation = movement < 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
        }



        //jump clean
        if (isJumping)
        {
            _rigidbody.AddForce(new Vector2(0f, jumpForce));



        }
        isJumping = false;



        if (Input.GetButtonDown("Jump") && Mathf.Abs(_rigidbody.velocity.y) < GroundedTolerance)
        {
            isJumping = true;
        }



        //bieres
        if (Input.GetKeyDown(KeyCode.M))
        {
            Instantiate(LaunchableProjectilePrefab, LaunchOffset.position, transform.rotation);
        }
    }
}

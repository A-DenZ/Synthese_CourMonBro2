using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float Speed = 4;
    [SerializeField] private float AdditionalForce = 2;
    [SerializeField] private float RotationSpeed = 200;
    [SerializeField] private GameObject explosionBiere = default;
    [SerializeField] private GameObject explosionBiereSol = default;


    [SerializeField] private Vector3 LaunchOffset;
    [SerializeField] private bool Thrown;
    private float Damage = 1;
    private float SplashRange = 1;

    private Vector3 launchDirection; // Nouvelle variable pour stocker la direction du lancer

    // Start est appelée avant la première mise à jour de frame
    void Start()
    {
        if (Thrown)
        {
            // Utilisez la direction du lancer au lieu de la rotation du joueur
            launchDirection = transform.right;
            GetComponent<Rigidbody2D>().velocity = launchDirection * Speed;

            GetComponent<Rigidbody2D>().AddForce(launchDirection * AdditionalForce, ForceMode2D.Impulse);

        }

        transform.Translate(LaunchOffset);

        Destroy(gameObject, 8);
    }

    // Update is called once per frame
    void Update()
    {
        if(!Thrown)
        {
            transform.position += -transform.right * Speed * Time.deltaTime;
        }
        if (Thrown)
        {
            transform.Rotate(Vector3.forward * -RotationSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!collision.collider.CompareTag("Player"))
        {
            if(SplashRange > 0) 
            {
                var hitColliders = Physics2D.OverlapCircleAll(transform.position, SplashRange);
                foreach (var hitCollider in hitColliders) 
                {
                    var enemy = hitCollider.GetComponent<Enemy>();
                    if (enemy)
                    {
                        var closesPoint = hitCollider.ClosestPoint(transform.position);
                        var distance = Vector3.Distance(closesPoint, transform.position);

                        var damagePercent = Mathf.InverseLerp(SplashRange, 0, distance);
                        enemy.TakeHit(damagePercent * Damage);
                    }
                }
            }

            else 
            { 
                var enemy = collision.collider.GetComponent<Enemy>();

                if(enemy) 
                {
                    enemy.TakeHit(Damage);
                }
            }
        Destroy(gameObject);
            Vector2 positionExplosionSol = new Vector2(transform.position.x, transform.position.y + 0.5f);
            if (collision.collider.CompareTag("Ground"))
            {
                Instantiate(explosionBiereSol, positionExplosionSol, Quaternion.identity);
            }
            else
            {
                Instantiate(explosionBiere, transform.position, Quaternion.identity);
                
            }
        
        }
    }
}

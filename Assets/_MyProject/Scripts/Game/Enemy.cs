using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float HitPoints;
    [SerializeField] private float MaxHitPoints = 5;
    [SerializeField] private int _points = 100;

    [SerializeField] private float wokeSpeed = 1.0f;

    [SerializeField] private HealthBar HealthBar;

    private UIManager _uiManager;


    // Start is called before the first frame update
    void Start()
    {
        HitPoints = MaxHitPoints;
        HealthBar.SetHealth(HitPoints, MaxHitPoints);
        _uiManager = FindObjectOfType<UIManager>();

    }

    public void TakeHit(float damage)
    {
        HitPoints -= damage;
        HealthBar.SetHealth(HitPoints, MaxHitPoints);

        if (HitPoints <= 0)
        {   
            Destroy(gameObject);
            _uiManager.AjouterScore(_points);
        }
    }

    // Update is called once per frame
    void Update()
    {
        wokesMovement();
    }

    private void wokesMovement()
    {
        transform.Translate(Vector3.left * Time.deltaTime * wokeSpeed);
        if (transform.position.x <= -12f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Si la collision survient avec le joueur
        if (collision.gameObject.CompareTag("Player"))
        {
            //Récupérer la classe Player afin d'accéder aux méthodes publiques
            Player player = collision.transform.GetComponent<Player>();
            player.Degats();  // Appeler la méthode dégats du joueur

            //instancier la moet du player
            //Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

            Destroy(this.gameObject); // Détruire l'objet ennemi
        }

    }
}

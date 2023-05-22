using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float HitPoints;
    [SerializeField] private float MaxHitPoints = 5;

    [SerializeField] private float wokeSpeed = 7.0f;

    [SerializeField] private HealthBar HealthBar;
    

    // Start is called before the first frame update
    void Start()
    {
        HitPoints = MaxHitPoints;
        HealthBar.SetHealth(HitPoints, MaxHitPoints);
    }

    public void TakeHit(float damage)
    {
        HitPoints -= damage;
        HealthBar.SetHealth(HitPoints, MaxHitPoints);

        if (HitPoints <= 0)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        wokesMovement();
    }

    private void wokesMovement ()
    {
        transform.Translate(Vector3.left * Time.deltaTime * wokeSpeed);
        if (transform.position.x <= -12f)
        {
            Destroy(gameObject);
        }
    }
}
